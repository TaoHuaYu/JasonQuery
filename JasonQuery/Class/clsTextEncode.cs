using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JasonQuery
{
    class Report
    {
        //統計解讀為ASCII、符號、常用字、次常用字及亂碼(非有效字元)字數
        public int Ascii, Symbol, Common, Rare, Unknow;
        /// <summary>
        /// 亂碼指標(數值愈大，不是該編碼的機率愈高)
        /// </summary>
        public float BadSmell
        {
            get
            {
                var total = Ascii + Symbol + Common + Rare + Unknow;

                if (total == 0) return 0;

                return (float)(Rare + Unknow * 3) / total;
            }
        }
    }

    class MyTextEncode
    {
        private static readonly byte[] _utf16BeBom =
        {
            0xFE,
            0xFF
        };

        private static readonly byte[] _utf16LeBom =
        {
            0xFF,
            0xFE
        };

        private static readonly byte[] _utf8Bom =
        {
            0xEF,
            0xBB,
            0xBF
        };

        private const int BUFFER_SIZE = 8192;

        private static bool _nullSuggestsBinary = true;
        private static double _utf16ExpectedNullPercent = 70;
        private static double _utf16UnexpectedNullPercent = 10;

        private enum MyEncoding
        {
            None, // Unknown or binary
            Ansi, // 0-255
            Ascii, // 0-127
            Utf8Bom, // UTF8 with BOM
            Utf8NoBom, // UTF8 without BOM
            Utf16LeBom, // UTF16 LE with BOM
            Utf16LeNoBom, // UTF16 LE without BOM
            Utf16BeBom, // UTF16-BE with BOM
            Utf16BeNoBom, // UTF16-BE without BOM
            Big5,
            Gb2312
        }


        private bool NullSuggestsBinary
        {
            set
            {
                _nullSuggestsBinary = value;
            }
        }

        public double Utf16ExpectedNullPercent
        {
            set
            {
                if (value > 0 && value < 100)
                {
                    _utf16ExpectedNullPercent = value;
                }
            }
        }

        private double Utf16UnexpectedNullPercent
        {
            set
            {
                if (value > 0 && value < 100)
                {
                    _utf16UnexpectedNullPercent = value;
                }
            }
        }

        private static string DetermineEndOfLineStyle(string pathToFile) //檢查檔尾的換行符號
        {
            int iBuf;
            var buf = new char[BUFFER_SIZE];
            string sStyle;

            try
            {
                using (var stream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        iBuf = reader.ReadBlock(buf, 0, buf.Length);
                    }
                }
            }
            catch (Exception ex) //指定的檔案被鎖定，開啟失敗
            {
                MessageBox.Show(ex.Message, @"Error opening file...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "ERROR";
            }

            var crlfs = 0;
            var crs = 0;
            var lfs = 0;

            for (var i = 0; i < iBuf; i++)
            {
                switch (buf[i])
                {
                    case '\r' when i < iBuf - 1 && buf[i + 1] == '\n':
                        ++crlfs; i += 2;
                        break;
                    case '\r':
                        ++crs; i += 1;
                        break;
                    case '\n':
                        ++lfs; i += 1;
                        break;
                }
            }

            if (crlfs > crs && crlfs > lfs)
            {
                sStyle = "Windows (CR LF)";
            }
            else if (lfs > crlfs && lfs > crs)
            {
                sStyle = "Unix (LF)";
            }
            else if (crs > crlfs && crs > lfs)
            {
                sStyle = "MacOs (CR)";
            }
            else
            {
                sStyle = "Unknown";
            }

            return sStyle;
        }

        private static int GetBomLengthFromEncodingMode(MyEncoding encoding)
        {
            int length;

            switch (encoding)
            {
                case MyEncoding.Utf16BeBom:
                case MyEncoding.Utf16LeBom:
                    length = 2;
                    break;
                case MyEncoding.Utf8Bom:
                    length = 3;
                    break;
                default:
                    length = 0;
                    break;
            }

            return length;
        }

        private static MyEncoding CheckBom(IReadOnlyList<byte> buffer, int size)
        {
            if (size >= 2 && buffer[0] == _utf16LeBom[0] && buffer[1] == _utf16LeBom[1])
            {
                return MyEncoding.Utf16LeBom;
            }

            if (size >= 2 && buffer[0] == _utf16BeBom[0] && buffer[1] == _utf16BeBom[1])
            {
                return MyEncoding.Utf16BeBom;
            }

            if (size >= 3 && buffer[0] == _utf8Bom[0] && buffer[1] == _utf8Bom[1] && buffer[2] == _utf8Bom[2])
            {
                return MyEncoding.Utf8Bom;
            }

            return MyEncoding.None;
        }

        public static int Analyze(byte[] data)
        {
            var resBig5 = AnalyzeBig5(data);
            var resGB = AnalyzeGB2312(data);

            if (resBig5.BadSmell < resGB.BadSmell)
                return 1;

            if (resBig5.BadSmell > resGB.BadSmell)
                return -1;

            return 0;

            //return:1表示較可能為繁體, -1表示較可能為簡體, 0表無法識別
        }

        private static Report AnalyzeBig5(IEnumerable<byte> data)
        {
            var res = new Report();
            var isDblBytes = false;
            byte dblByteHi = 0;

            foreach (var b in data)
            {
                if (isDblBytes)
                {
                    if (b >= 0x40 && b <= 0x7e || b >= 0xa1 && b <= 0xfe)
                    {
                        var c = dblByteHi * 0x100 + b;

                        if (c >= 0xa140 && c <= 0xa3bf)
                            res.Symbol++; //符號
                        else if (c >= 0xa440 && c <= 0xc67e)
                            res.Common++; //常用字
                        else if (c >= 0xc940 && c <= 0xf9d5)
                            res.Rare++; //次常用字
                        else
                            res.Unknow++; //無效字元
                    }
                    else
                        res.Unknow++;
                    isDblBytes = false;
                }
                else if (b >= 0x80 && b <= 0xfe)
                {
                    isDblBytes = true;
                    dblByteHi = b;
                }
                else if (b < 0x80)
                    res.Ascii++;
            }
            return res;
        }

        private static Report AnalyzeGB2312(IEnumerable<byte> data)
        {
            var res = new Report();
            var isDblBytes = false;
            byte dblByteHi = 0;

            foreach (var b in data)
            {
                if (isDblBytes)
                {
                    if (b >= 0xa1 && b <= 0xfe)
                    {
                        if (dblByteHi >= 0xa1 && dblByteHi <= 0xa9)
                            res.Symbol++; //符號
                        else if (dblByteHi >= 0xb0 && dblByteHi <= 0xd7)
                            res.Common++; //一級漢字(常用字)
                        else if (dblByteHi >= 0xd8 && dblByteHi <= 0xf7)
                            res.Rare++; //二級漢字(次常用字)
                        else
                            res.Unknow++; //無效字元
                    }
                    else
                        res.Unknow++; //無效字元

                    isDblBytes = false;
                }
                else if (b >= 0xa1 && b <= 0xf7)
                {
                    isDblBytes = true;
                    dblByteHi = b;
                }
                else if (b < 0x80)
                {
                    res.Ascii++;
                }
            }
            return res;
        }

        private static MyEncoding DetectEncoding(byte[] buffer)
        {
            var iSize = buffer.Length;

            var encoding = CheckBom(buffer, iSize);

            if (encoding != MyEncoding.None)
            {
                return encoding;
            }

            if (IsBig5Encoding(buffer) && IsGb2312Encoding(buffer))
            {
                var ii = Analyze(buffer); //1表示較可能為繁體, -1表示較可能為簡體, 0表無法識別

                switch (ii)
                {
                    case 1:
                    case 0:
                        return MyEncoding.Big5;
                    case -1:
                        return MyEncoding.Gb2312;
                }
            }

            if (IsBig5Encoding(buffer))
            {
                return MyEncoding.Big5;
            }

            if (IsGb2312Encoding(buffer))
            {
                return MyEncoding.Gb2312;
            }

            encoding = CheckUtf8(buffer, iSize);

            if (encoding != MyEncoding.None)
            {
                return encoding;
            }

            encoding = CheckUtf16NewlineChars(buffer, iSize);

            if (encoding != MyEncoding.None)
            {
                return encoding;
            }

            encoding = CheckUtf16Ascii(buffer, iSize);

            if (encoding != MyEncoding.None)
            {
                return encoding;
            }

            if (!DoesContainNulls(buffer, iSize))
            {
                return MyEncoding.Ansi;
            }

            return _nullSuggestsBinary ? MyEncoding.None : MyEncoding.Ansi;
        }

        private static string DetectBinaryEncoding(byte[] buffer)
        {
            var size = buffer.Length;

            var sBinaryEncode = "";

            switch (buffer.Length > 5)
            {
                case true when buffer[0] == 0xff && buffer[1] == 0xd8 && buffer[2] == 0xff && buffer[3] == 0xe0:
                    sBinaryEncode = "JPG Image"; //jpg, jpeg
                    break;
                case true when buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4e && buffer[3] == 0x47:
                    sBinaryEncode = "PNG Image"; //png
                    break;
                case true when buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46 && buffer[3] == 0x38:
                    sBinaryEncode = "GIF Image"; //gif
                    break;
                case true when buffer[0] == 0x49 && buffer[1] == 0x49 && buffer[2] == 0x2a && buffer[3] == 0x00:
                    sBinaryEncode = "TIF Image"; //tif
                    break;
                default:
                {
                    if (buffer.Length > 2 && buffer[0] == 0x42 && buffer[1] == 0x4d)
                    {
                        sBinaryEncode = "BMP Image"; //bmp
                    }
                    else if (buffer.Length > 5 && buffer[0] == 0x7b && buffer[1] == 0x5c && buffer[2] == 0x72 && buffer[3] == 0x74)
                    {
                        sBinaryEncode = "Microsoft RTF"; //rtf
                    }
                    else switch (buffer.Length > 10)
                    {
                        case true when buffer[0] == 0xd0 && buffer[1] == 0xcf && buffer[2] == 0x11 && buffer[3] == 0xe0 && buffer[4] == 0xa1 && buffer[5] == 0xb1 && buffer[6] == 0x1a && buffer[7] == 0xe1:
                        {
                            if (buffer.Length > 10 && buffer[26] == 0x04 && buffer[30] == 0x0c)
                            {
                                sBinaryEncode = "Microsoft MSI";
                            }
                            else
                            {
                                sBinaryEncode = "Microsoft Office"; //doc, xls, ppt
                            }

                            break;
                        }
                        case true when buffer[0] == 0x50 && buffer[1] == 0x4b && buffer[2] == 0x03 && buffer[3] == 0x04 && buffer[4] == 0x14 && buffer[5] == 0x00 && buffer[6] == 0x06 && buffer[7] == 0x00 && buffer[8] == 0x08:
                            sBinaryEncode = "Microsoft Office(x)"; //docx, xlsx, pptx
                            break;
                        default:
                        {
                            if (buffer.Length > 20 && buffer[0] == 0x00 && buffer[1] == 0x01 && buffer[2] == 0x00 && buffer[3] == 0x00 && buffer[4] == 0x53 && buffer[13] == 0x41 && buffer[14] == 0x43 && buffer[15] == 0x45 && buffer[16] == 0x20 && buffer[17] == 0x44 && buffer[18] == 0x42)
                            {
                                sBinaryEncode = "Access ACCDB"; //20191111 add accdb
                            }
                            else if (buffer.Length > 10 && buffer[0] == 0x00 && buffer[1] == 0x01 && buffer[2] == 0x00 && buffer[3] == 0x00 && buffer[4] == 0x53 && buffer[5] == 0x74 && buffer[6] == 0x61 && buffer[7] == 0x6e)
                            {
                                sBinaryEncode = "Access MDB"; //mdb
                            }
                            else if (buffer.Length > 5 && buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46)
                            {
                                sBinaryEncode = "PDF"; //pdf
                            }
                            else if (buffer.Length > 10 && buffer[0] == 0x50 && buffer[1] == 0x4b && buffer[2] == 0x03 && buffer[3] == 0x04 && buffer[4] == 0x14 && buffer[5] == 0x00 && buffer[6] == 0x00 && buffer[7] == 0x00 && buffer[8] == 0x08)
                            {
                                sBinaryEncode = "ZIP"; //zip
                            }
                            else switch (buffer.Length > 5)
                            {
                                case true when buffer[0] == 0x50 && buffer[1] == 0x4b && buffer[2] == 0x03 && buffer[3] == 0x04 && buffer[4] == 0x0a:
                                    sBinaryEncode = "ZIP"; //20191111 add zip
                                    break;
                                case true when buffer[0] == 0x52 && buffer[1] == 0x61 && buffer[2] == 0x72 && buffer[3] == 0x21:
                                    sBinaryEncode = "RAR"; //rar
                                    break;
                                case true when buffer[0] == 0x37 && buffer[1] == 0x7a && buffer[2] == 0xbc && buffer[3] == 0xaf:
                                    sBinaryEncode = "7z"; //7z
                                    break;
                            }

                            break;
                        }
                    }

                    break;
                }
            }

            return sBinaryEncode;
        }

        public static string GetTextEncode(string sFilename, ref string sEndOfLineStyle)
        {
            var sTextEncode = "";
            byte[] buffer;

            using (var fileStream = new FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var iLength = fileStream.Length > 5000000 ? 5000000 : fileStream.Length;
                var numBytesToRead = Convert.ToInt32(iLength);
                buffer = new byte[numBytesToRead];
                fileStream.Read(buffer, 0, numBytesToRead);
            }

            sEndOfLineStyle = DetermineEndOfLineStyle(sFilename);

            if (sEndOfLineStyle == "ERROR") //指定的檔案被鎖定，開啟失敗
            {
                return "ERROR";
            }

            var encoding = DetectEncoding(buffer);

            switch (encoding)
            {
                case MyEncoding.None:
                    {
                        sTextEncode = "Binary`" + DetectBinaryEncoding(buffer);

                        break;
                    }
                case MyEncoding.Ascii:
                    sTextEncode = "ASCII";
                    break;
                case MyEncoding.Ansi:
                    sTextEncode = "ANSI"; // ANSI (chars in the range 0-255 range)";
                    break;
                case MyEncoding.Utf8Bom:
                case MyEncoding.Utf8NoBom:
                    sTextEncode = "UTF-8";
                    break;
                case MyEncoding.Utf16LeBom:
                case MyEncoding.Utf16LeNoBom:
                case MyEncoding.Utf16BeBom:
                //UTF-16 Big Endian
                case MyEncoding.Utf16BeNoBom:
                    sTextEncode = "UTF-16"; //UTF-16 Little Endian
                    break;
                case MyEncoding.Big5:
                    sTextEncode = "Big5 (Traditional)";
                    break;
                case MyEncoding.Gb2312:
                    sTextEncode = "GB2312 (Simplified)";
                    break;
            }

            return sTextEncode;
        }

        private static MyEncoding CheckUtf16NewlineChars(byte[] buffer, int size)
        {
            if (size < 2)
            {
                return MyEncoding.None;
            }

            size--;

            var leControlChars = 0;
            var beControlChars = 0;

            uint pos = 0;

            while (pos < size)
            {
                var ch1 = buffer[pos++];
                var ch2 = buffer[pos++];

                if (ch1 == 0)
                {
                    if (ch2 == 0x0a || ch2 == 0x0d)
                    {
                        ++beControlChars;
                    }
                }
                else if (ch2 == 0)
                {
                    if (ch1 == 0x0a || ch1 == 0x0d)
                    {
                        ++leControlChars;
                    }
                }

                if (leControlChars > 0 && beControlChars > 0)
                {
                    return MyEncoding.None;
                }
            }

            if (leControlChars > 0)
            {
                return MyEncoding.Utf16LeNoBom;
            }

            return beControlChars > 0 ? MyEncoding.Utf16BeNoBom : MyEncoding.None;
        }

        private static bool DoesContainNulls(byte[] buffer, int size)
        {
            uint pos = 0;

            while (pos < size)
            {
                if (buffer[pos++] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static MyEncoding CheckUtf16Ascii(byte[] buffer, int size)
        {
            var numOddNulls = 0;
            var numEvenNulls = 0;

            uint pos = 0;

            while (pos < size)
            {
                if (buffer[pos] == 0)
                {
                    numEvenNulls++;
                }

                pos += 2;
            }

            pos = 1;

            while (pos < size)
            {
                if (buffer[pos] == 0)
                {
                    numOddNulls++;
                }

                pos += 2;
            }

            var evenNullThreshold = numEvenNulls * 2.0 / size;
            var oddNullThreshold = numOddNulls * 2.0 / size;
            var expectedNullThreshold = _utf16ExpectedNullPercent / 100.0;
            var unexpectedNullThreshold = _utf16UnexpectedNullPercent / 100.0;

            if (evenNullThreshold < unexpectedNullThreshold && oddNullThreshold > expectedNullThreshold)
            {
                return MyEncoding.Utf16LeNoBom;
            }

            if (oddNullThreshold < unexpectedNullThreshold && evenNullThreshold > expectedNullThreshold)
            {
                return MyEncoding.Utf16BeNoBom;
            }

            return MyEncoding.None;
        }

        private static MyEncoding CheckUtf8(byte[] buffer, int size)
        {
            var onlySawAsciiRange = true;
            uint pos = 0;

            while (pos < size)
            {
                var ch = buffer[pos++];

                if (ch == 0 && _nullSuggestsBinary)
                {
                    return MyEncoding.None;
                }

                int moreChars;

                if (ch <= 127)
                {
                    moreChars = 0;
                }
                else if (ch >= 194 && ch <= 223)
                {
                    moreChars = 1;
                }
                else if (ch >= 224 && ch <= 239)
                {
                    moreChars = 2;
                }
                else if (ch >= 240 && ch <= 244)
                {
                    moreChars = 3;
                }
                else
                {
                    return MyEncoding.None;
                }

                while (moreChars > 0 && pos < size)
                {
                    onlySawAsciiRange = false;

                    ch = buffer[pos++];
                    if (ch < 128 || ch > 191)
                    {
                        return MyEncoding.None;
                    }

                    --moreChars;
                }
            }

            return onlySawAsciiRange ? MyEncoding.Ascii : MyEncoding.Utf8NoBom;
        }

        private static bool IsBig5Encoding(byte[] bytes)
        {
            var big5 = Encoding.GetEncoding(950);

            return bytes.Length == big5.GetByteCount(big5.GetString(bytes));
        }

        private static bool IsGb2312Encoding(byte[] bytes)
        {
            var GB2312 = Encoding.GetEncoding(936);

            return bytes.Length ==
                GB2312.GetByteCount(GB2312.GetString(bytes));
        }
    }
}