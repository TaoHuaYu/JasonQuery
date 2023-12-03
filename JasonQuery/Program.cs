using System;
using System.IO;
using System.Windows.Forms;

namespace JasonQuery
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            MyGlobal.sOpenFileFromExternal = "";

            if (args.Length == 1)
            {
                //只取第一個參數即可 (只有在命令列模式才有機會在執行檔後面帶多個參數，例如 JasonQuery.exe c:\temp\123.sql c:\temp\456.sql)
                //如果是透過檔案總案在檔名上快按兩次左鍵(呼叫 JasonQuery.exe)，一次選多個檔案，就會執行多個 JasonQuery.exe (一對一)
                MyGlobal.sOpenFileFromExternal = args[0];

                if (File.Exists(MyGlobal.sOpenFileFromExternal) == false)
                {
                    MyGlobal.sOpenFileFromExternal = "";
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}