using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace JasonQuery
{
    public static class DBCommon
    {
        private static string _sLangText = "";

        //資料庫初始化
        private static SQLiteConnection OleDbOpenConn(string sPW = "", string sNewPW = "", bool bUseDefaultPW = false)
        {
            sPW = string.IsNullOrEmpty(sPW) ? MyGlobal.sMyDBConnectionPassword : sPW;

            var mdbConn = new SQLiteConnection {ConnectionString = MyGlobal.sMyDBConnectionString};

            try
            {
                if (mdbConn.State == ConnectionState.Open) mdbConn.Close();
                mdbConn.SetPassword(sPW);
                mdbConn.Open();

                if (!sNewPW.Equals(""))
                {
                    mdbConn.ChangePassword(MyGlobal.sMyDBConnectionPasswordPrefix + sNewPW + MyGlobal.sMyDBConnectionPasswordSuffix);
                    MyGlobal.sMyDBConnectionPassword = MyGlobal.sMyDBConnectionPasswordPrefix + sNewPW + MyGlobal.sMyDBConnectionPasswordSuffix;
                }
                else if (bUseDefaultPW && sNewPW.Equals(""))
                {
                    mdbConn.ChangePassword("ytec1688");
                    MyGlobal.sMyDBConnectionPassword = "ytec1688";
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return mdbConn;
        }

        //是否使用預設密碼？
        public static bool CheckDBPassword(string sPW)
        {
            var bResult = true;
            sPW = string.IsNullOrEmpty(sPW) ? "" : MyGlobal.sMyDBConnectionPasswordPrefix + sPW + MyGlobal.sMyDBConnectionPasswordSuffix;
            var mdbConn = OleDbOpenConn(sPW); //IsUsingDefaultPassword

            var myDataTable = new DataTable();
            var da = new SQLiteDataAdapter("SELECT * FROM SystemConfig WHERE 1 = 2", mdbConn);
            var ds = new DataSet();

            try
            {
                ds.Clear();
                da.Fill(ds);
                myDataTable = ds.Tables[0];
            }
            catch (Exception)
            {
                //密碼錯誤！
                bResult = false;
            }

            if (mdbConn.State == ConnectionState.Open) mdbConn.Close();

            return bResult;
        }

        //變更密碼！
        public static string ResetDBPassword(string sOldPW, string sNewPW, bool bUseDefaultPW)
        {
            var sResult = "";
            var mdbConn = OleDbOpenConn(sOldPW, sNewPW, bUseDefaultPW);

            var myDataTable = new DataTable();
            var da = new SQLiteDataAdapter("SELECT * FROM SystemConfig WHERE 1 = 2", mdbConn);
            var ds = new DataSet();

            try
            {
                ds.Clear();
                da.Fill(ds);
                myDataTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //密碼錯誤！
                sResult = ex.Message;
            }

            if (mdbConn.State == ConnectionState.Open) mdbConn.Close();

            return sResult;
        }

        //取得資料表:
        public static DataTable ExecQuery(string sSQL)
        {
            var mdbConn = OleDbOpenConn(); //ExecQuery

            var myDataTable = new DataTable();
            var da = new SQLiteDataAdapter(sSQL, mdbConn);
            var ds = new DataSet();

            try
            {
                ds.Clear();
                da.Fill(ds);
                myDataTable = ds.Tables[0];
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (mdbConn.State == ConnectionState.Open) mdbConn.Close();

            return myDataTable;
        }

        //對資料表進行新增、修改及刪除等功能:
        public static void ExecNonQuery(string sSQL)
        {
            var mdbConn = OleDbOpenConn(); //ExecNonQuery

            try
            {
                var cmd = new SQLiteCommand(sSQL, mdbConn);
                cmd.ExecuteNonQuery();

                if (mdbConn.State == ConnectionState.Open) mdbConn.Close();
            }
            catch (Exception ex)
            {
                var sMsg = ex.Message;
                var parts = sMsg.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    if (parts[0] == parts[1])
                    {
                        sMsg = parts[0]; //過濾重複的錯誤訊息
                    }
                }

                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string BatchDeleteRecord(string sSQLs)
        {
            var sResult = "";
            var mdbConn = OleDbOpenConn(); //BatchDeleteRecord

            try
            {
                var sSql = sSQLs.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in sSql)
                {
                    var cmd = new SQLiteCommand(t, mdbConn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                sResult = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text") + ex.Message;
            }

            if (mdbConn.State == ConnectionState.Open) mdbConn.Close();

            return sResult;
        }
    }
}