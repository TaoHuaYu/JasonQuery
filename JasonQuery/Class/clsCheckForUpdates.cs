using System;
using System.IO;
using System.Net;

namespace JasonQuery
{
    internal delegate void BytesDownloadedEventHandler(ByteArgs e);

    public static class CheckForUpdates
    {
        /// <summary>Get update and version information from specified online file - returns a List</summary>
        /// <param name="downloadsUrl">URL to download file from</param>
        /// <param name="versionFile">Name of the pipe| delimited version file to download</param>
        /// <param name="resourceDownloadFolder">Folder on the local machine to download the version file to</param>
        /// <param name="sTargetFilename"></param>
        /// <returns>List containing the information from the pipe delimited version file</returns>
        public static string GetUpdateInfo(string downloadsUrl, string versionFile, string resourceDownloadFolder, string sTargetFilename)
        {
            //let's try and download update information from the web
            var sUpdateCheck = WebData.DownloadFromWeb(downloadsUrl, versionFile, resourceDownloadFolder, sTargetFilename);

            //if the download of the file was successful
            if (!string.IsNullOrEmpty(sUpdateCheck))
            {
                return "";
            }

            var text = File.ReadAllText(resourceDownloadFolder + sTargetFilename);

            try
            {
                var iFrom = text.IndexOf("```", StringComparison.Ordinal) + 3;
                var iTo = text.LastIndexOf("```", StringComparison.Ordinal);

                text = text.Substring(iFrom, iTo - iFrom);
            }
            catch (Exception)
            {
                //
            }

            //get information out of download info file
            //return populateInfoFromWeb(versionFile, resourceDownloadFolder, startLine);
            return text;

            //there is a chance that the download of the file was not successful

        }

        //public static string DownloadFile(string downloadsURL, string versionFile, string resourceDownloadFolder, string sTargetFilename)
        //{
        //    var sResult = "";
        //    int iFrom = 0, iTo = 0;

        //    //let's try and download update information from the web
        //    sResult = webdata.downloadFromWeb(downloadsURL, versionFile, resourceDownloadFolder, sTargetFilename);

        //    return "";
        //}
    }

    internal class ByteArgs : EventArgs
    {
        public int iDownloaded { get; set; }

        public int iTotal { get; set; }
    }

    internal static class WebData
    {
        public static event BytesDownloadedEventHandler BytesDownloaded;

        public static string DownloadFromWeb(string sURL, string sFilename, string sTargetFolder, string sTargetFilename)
        {
            try
            {
                var webReq = WebRequest.Create(sURL + sFilename);
                var webResponse = webReq.GetResponse();
                var dataStream = webResponse.GetResponseStream();

                //Download the data in chuncks
                var dataBuffer = new byte[1024];

                //Get the total size of the download
                var dataLength = (int)webResponse.ContentLength;

                //lets declare our downloaded bytes event args
                var byteArgs = new ByteArgs {iDownloaded = 0, iTotal = dataLength};

                //we need to test for a null as if an event is not consumed we will get an exception
                BytesDownloaded?.Invoke(byteArgs);

                var memoryStream = new MemoryStream();

                while (true)
                {
                    if (dataStream == null)
                    {
                        continue;
                    }

                    var bytesFromStream = dataStream.Read(dataBuffer, 0, dataBuffer.Length);

                    if (bytesFromStream == 0)
                    {
                        byteArgs.iDownloaded = dataLength;
                        byteArgs.iTotal = dataLength;
                        BytesDownloaded?.Invoke(byteArgs);

                        //Download complete
                        break;
                    }

                    //Write the downloaded data
                    memoryStream.Write(dataBuffer, 0, bytesFromStream);

                    byteArgs.iDownloaded = bytesFromStream;
                    byteArgs.iTotal = dataLength;

                    BytesDownloaded?.Invoke(byteArgs);
                }

                //Convert the downloaded stream to a byte array
                var downloadedData = memoryStream.ToArray();

                dataStream.Close();
                memoryStream.Close();

                //寫入檔案
                var newFile = new FileStream(sTargetFolder + sTargetFilename, FileMode.Create);
                newFile.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();

                return "";
            }
            catch (Exception ex)
            {
                //網址錯誤或是網路不通？
                return ex.Message;
            }
        }
    }
}