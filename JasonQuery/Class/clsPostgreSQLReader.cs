using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using Devart.Data.PostgreSql;
using System.Drawing;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public class clsPostgreSQLReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title);

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool rePaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

        private string _sLangText;
        private static PgSqlConnection _mConn;
        private PgSqlTransaction txn;
        private PgSqlCommand oCommand;
        private PgSqlDataAdapter oDataAdapter;
        private DataSet oDataset;

        public delegate void QueryCompletedEventHandler();
        public event QueryCompletedEventHandler QueryCompleted;

        private DataTable Datatable { get; set; }
        public PgSqlDataReader Datareader { get; set; }

        public string ConnectTo(string strConnectionString)
        {
            var sErrMsg = "";
            oCommand = new PgSqlCommand();
            _mConn = new PgSqlConnection(strConnectionString);

            try
            {
                _mConn.Open();
                txn = _mConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (PgSqlException ex)
            {
                sErrMsg = ex.Message;
            }

            var sConnectTo = _mConn.State == ConnectionState.Open ? "" : sErrMsg;

            return sConnectTo;
        }

        public string oSavePoint(string sSavePoint)
        {
            var sResult = "";

            try
            {
                txn.Save(sSavePoint);
            }
            catch (PgSqlException ex)
            {
                sResult = "ErrorCode: " + ex.ErrorCode + "\r\nErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                if (ex.Message != "This PgSqlTransaction has completed; it is no longer usable.") //出現此訊息，實際還是有 commit 成功！
                {
                    sResult = "ErrorMsg: " + ex.Message;
                }
            }

            Thread.Sleep(10);

            return sResult;
        }

        public string oRollbackPoint(string sSavePoint)
        {
            var sResult = "";

            try
            {
                txn.Rollback(sSavePoint);
            }
            catch (PgSqlException ex)
            {
                sResult = "ErrorCode: " + ex.ErrorCode + "\r\nErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                if (ex.Message != "This PgSqlTransaction has completed; it is no longer usable.") //出現此訊息，實際還是有 commit 成功！
                {
                    sResult = "ErrorMsg: " + ex.Message;
                }
                else
                {
                    sResult = "ERROR: no such savepoint\r\nSQL state: 3B001";
                }
            }

            Thread.Sleep(10);

            return sResult;
        }

        public string oReleasePoint(string sSavePoint)
        {
            var sResult = "";

            try
            {
                txn.Release(sSavePoint);
            }
            catch (PgSqlException ex)
            {
                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "ErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                if (ex.Message != "This PgSqlTransaction has completed; it is no longer usable.") //出現此訊息，實際還是有 commit 成功！
                {
                    sResult = "ErrorMsg: " + ex.Message;
                }
            }

            Thread.Sleep(10);

            return sResult;
        }

        public string oCommit()
        {
            var sResult = "";

            try
            {
                txn.Commit();
            }
            catch (PgSqlException ex)
            {
                sResult = "ErrorCode: " + ex.ErrorCode + "\r\nErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                if (ex.Message != "This PgSqlTransaction has completed; it is no longer usable.") //出現此訊息，實際還是有 commit 成功！
                {
                    sResult = "ErrorMsg: " + ex.Message;
                }
            }

            Thread.Sleep(10);

            return sResult;
        }

        public string oRollback()
        {
            var sResult = "";

            try
            {
                txn.Rollback();
            }
            catch (PgSqlException ex)
            {
                sResult = "ErrorCode: " + ex.ErrorCode + "\r\nErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                if (ex.Message != "This PgSqlTransaction has completed; it is no longer usable.")
                {
                    sResult = "ErrorMsg: " + ex.Message;
                }
            }

            Thread.Sleep(100);

            return sResult;
        }

        public bool oDisconnect()
        {
            var bValue = false;

            try
            {
                txn.Dispose();
                oCommand.Dispose();

                _mConn.Dispose();
                bValue = true;
            }
            catch (Exception)
            {
                //
            }

            Thread.Sleep(100);

            return bValue;
        }

        public static ConnectionState GetState()
        {
            return _mConn.State;
        }

        public string InterruptQuery()
        {
            var sResult = "";

            try
            {
                oCommand.Cancel();
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
            }

            Thread.Sleep(100);

            return sResult;
        }

        public void ExecuteQueryPaged(object sqlQuery)
        {
            var iStartRow = 0;
            var iPageLength = 0;
            var iSelectionStart = 0;
            var iPosAdjust = 0;
            var iPosOffset = 0;
            var sSql = "";
            var sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 20)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2].TrimEnd(" ;\r\n".ToCharArray());
                    iStartRow = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3]);
                    iPageLength = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4]);
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                sSql = script.Statements[0].Text.Replace("\r\n", "!r!n").Replace("\n", "\r\n").Replace("!r!n", "\r\n"); //經過 OracleScript(sSql) 解析的 SQL 指令，中間會有 \n 的存在，故要經過多次的 Replace
                iPosOffset = script.Statements[0].Offset; //去掉註解後，真正要執行的 SQL 指令

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.FetchSize = 1000;
                oCommand.Connection = _mConn;

                Datatable = new DataTable();

                Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                _sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (PgSqlException ex)
            {
                var sMessage = ex.Message;
                var iPosition = ex.Position - 15;
                Datatable = new DataTable(); //加上這個，當 SQL Statement 執行有誤時，才不會執行 ArrangeDataTable()

                if (sMessage == "Unexpected server response.")
                {
                    sMessage += " If you keep getting this error message, please reopen JasonQuery and try again.";
                }

                if (iPosition == -15 && (sMessage.StartsWith("permission denied for relation") || sMessage.StartsWith("current transaction is aborted, commands ignored")))
                {
                    iPosition = 0;
                }

                //判斷是否有變數沒有指定？此處 PostgreSQL 並不會回傳 ErrorCode/Hint/Position，所以只能透過字串判斷正確的位置
                if (sMessage.StartsWith("Parameter") && sMessage.IndexOf("' is missing", StringComparison.Ordinal) != -1)
                {
                    var sWord = ":" + MyGlobal.GetStringBetween(sMessage, "'", "'");
                    iPosition = sSql.IndexOf(sWord, StringComparison.Ordinal) + 1;
                }

                if (sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal) != -1)
                {
                    sMessage = sMessage.Substring(0, sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal));
                }

                var sErrorCode = string.IsNullOrEmpty(ex.ErrorCode) ? "" : "ErrorCode: " + ex.ErrorCode + "\r\nErrorPosition: " + iPosition + "\r\n";
                var sErrorHint = string.IsNullOrEmpty(ex.Hint) ? "" : "\r\n\r\n" + ex.Hint;

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sMessage + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql;
            }
            catch (Exception ex)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                MessageBox.Show(ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                QueryCompleted?.Invoke();
            }
        }

        public PgSqlDataReader ExecuteQueryPaged100Rows(string sSql, int iStartRow, int iPageLength, out bool bRollback, out bool bPermissionDenied)
        {
            bRollback = false;
            bPermissionDenied = false;

            if (GetState() == ConnectionState.Closed)
            {
                ConnectTo(MyGlobal.sDBConnectionString);
            }

            try
            {
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.FetchSize = 1000;
                oCommand.Connection = _mConn;
                Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (PgSqlException ex)
            {
                if (ex.Message == "current transaction is aborted, commands ignored until end of transaction block")
                {
                    bRollback = true;
                    oRollback();
                }
                else if (ex.Message.StartsWith("permission denied for relation"))
                {
                    bPermissionDenied = true;
                }
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                //
            }

            return Datareader;
        }

        public void ExecuteQuery2(object sqlQuery)
        {
            var iSelectionStart = 0;
            var iPosAdjust = 0;
            var iPosOffset = 0;
            var sSql = "";
            var  sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 10)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2].TrimEnd(" ;\r\n".ToCharArray());
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                sSql = script.Statements[0].Text.Replace("\r\n", "!r!n").Replace("\n", "\r\n").Replace("!r!n", "\r\n"); //經過 OracleScript(sSql) 解析的 SQL 指令，中間會有 \n 的存在，故要經過多次的 Replace
                iPosOffset = script.Statements[0].Offset; //去掉註解後，真正要執行的 SQL 指令

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.FetchSize = 1000;
                oCommand.Connection = _mConn;

                Datatable = new DataTable();

                Datareader = oCommand.ExecuteReader();
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                _sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (PgSqlException ex)
            {
                var sMessage = ex.Message;
                var iPosition = ex.Position;
                Datatable = new DataTable(); //加上這個，當 SQL Statement 執行有誤時，才不會執行 ArrangeDataTable()

                if (sMessage == "Unexpected server response.")
                {
                    sMessage += " If you keep getting this error message, please reopen JasonQuery and try again.";
                }

                //錯誤的定位點若往前抓，就要調整這個地方
                if (iPosition == -15 && (sMessage.StartsWith("permission denied for relation") || sMessage.StartsWith("current transaction is aborted, commands ignored")))
                {
                    iPosition = 0;
                }

                //判斷是否有變數沒有指定？此處 PostgreSQL 並不會回傳 ErrorCode/Hint/Position，所以只能透過字串判斷正確的位置
                if (sMessage.StartsWith("Parameter") && sMessage.IndexOf("' is missing", StringComparison.Ordinal) != -1)
                {
                    var sWord = ":" + MyGlobal.GetStringBetween(sMessage, "'", "'");
                    iPosition = sSql.IndexOf(sWord, StringComparison.Ordinal) + 1;
                }

                var sErrorCode = string.IsNullOrEmpty(ex.ErrorCode) ? "" : "ErrorCode: " + ex.ErrorCode + "\r\nErrorPosition: " + iPosition + "\r\n";
                var sErrorHint = string.IsNullOrEmpty(ex.Hint) ? "" : "\r\n\r\n" + ex.Hint;

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sMessage + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql;
            }
            catch (Exception ex)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                MessageBox.Show(ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                QueryCompleted?.Invoke();
            }
        }

        public void ExecuteNonQuery(object sqlQuery)
        {
            var  i = 0;
            var iSelectionStart = 0;
            var iCount = 0;
            var iPosOffset = 0;
            var iBatchRunQty = 0;
            var dtStartTime = DateTime.Now;
            var sQueryTime = "";
            var sSql = "";
            var sSqlScript = "";
            var sSqlExecuted = "";
            var sExecutedResult = "";
            var sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                Datatable = new DataTable();
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 10)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2].TrimEnd(" ;\r\n".ToCharArray());
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                var sResult = "";

                if (script.Statements.Count > 1)
                {
                    oCommand.CommandType = CommandType.Text;
                    oCommand.Connection = _mConn;

                    //顯示進度視窗
                    MyGlobal.iProgressInsertInto = 0;
                    MyGlobal.bProgressCancel = false;

                    var sLangText = MyGlobal.GetLanguageString("Execute non-query SQL scripts", "Global", "Global", "msg", "ExecuteNonQueryScripts", "Text");

                    var myForm = new frmProgressInfo
                    {
                        sTitleName = sLangText,
                        iTotalQty = script.Statements.Count,
                        iInterval = 500,
                        FormBorderStyle = FormBorderStyle.None,
                        ShowInTaskbar = false,
                        TopMost = true,
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    myForm.Show();

                    iBatchRunQty = script.Statements.Count;

                    for (i = 0; i < iBatchRunQty; i++)
                    {
                        dtStartTime = DateTime.Now;
                        iPosOffset = script.Statements[i].Offset; //去掉註解後，真正要執行的 SQL 指令
                        sSqlExecuted = script.Statements[i].Text;

                        iCount = 0;

                        if (script.Statements[i].StatementType.ToString() != "Select")
                        {
                            oCommand.CommandText = sSqlExecuted;
                            iCount = oCommand.ExecuteNonQuery();

                            sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);
                        }

                        sResult = GetResultString(script.Statements[i].StatementType.ToString(), script.Statements[i].Text.Split(' ')[0].ToUpper(), iCount); //ExecuteNonQuery, 多個 SQL
                        sExecutedResult += sResult + "\r\n";

                        MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                        sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '" + sQueryTime + "', '" + sQueryTime + "', " + iCount + ", 'Complete', '" + sResult + "', '/*Batch " + (i + 1) + " of " + iBatchRunQty + " */" + sSqlExecuted.Replace("'", "''") + "');";
                        DBCommon.ExecNonQuery(sSqlScript);

                        MyGlobal.iProgressInsertInto = i + 1;
                        Thread.Sleep(1);
                        Application.DoEvents();

                        if (MyGlobal.bProgressCancel)
                        {
                            break;
                        }
                    }

                    if (MyGlobal.bProgressCancel)
                    {
                        sTemp = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                        sExecutedResult += sTemp + "\r\n"; //ExecuteNonQuery, 多個 SQL

                        MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                        sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '00:00.0001', '00:00.0001', " + iCount + ", 'Cancel', '" + sTemp.Replace("'", "''") + "', '/*Batch " + (i + 1) + " of " + iBatchRunQty + " */');";
                        DBCommon.ExecNonQuery(sSqlScript);
                    }
                    else
                    {
                        myForm.Close();
                    }

                    if (sExecutedResult.Substring(sExecutedResult.Length - 2, 2) == "\r\n")
                    {
                        sExecutedResult = sExecutedResult.Substring(0, sExecutedResult.Length - 2); //正常結束，去掉尾部的換行符號
                    }
                }
                else
                {
                    dtStartTime = DateTime.Now;

                    sSqlExecuted = sSql;
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = sSql;
                    oCommand.Connection = _mConn;
                    iCount = oCommand.ExecuteNonQuery();
                    
                    sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);

                    sResult = GetResultString(script.Statements[i].StatementType.ToString(), script.Statements[i].Text.Split(' ')[0].ToUpper(), iCount); //ExecuteNonQuery, 單一 SQL
                    sExecutedResult = sResult;

                    MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                    sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '" + sQueryTime + "', '" + sQueryTime + "', " + iCount + ", 'Complete', '" + sResult + "', '" + sSqlExecuted.Replace("'", "''") + "');";
                    DBCommon.ExecNonQuery(sSqlScript);
                }

                //正常結束 or 取消結束：回傳異動筆數
                MyGlobal.sGlobalTemp = "sqlexecuteaffected" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sExecutedResult;
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                _sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (PgSqlException ex)
            {
                if (iBatchRunQty > 0)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    MyGlobal.iProgressInsertInto = iBatchRunQty; //for Batch Run
                }

                sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);

                Datatable = new DataTable(); //加上這個，當 SQL Statement 執行有誤時，才不會執行 ArrangeDataTable()

                var sErrorCode = string.IsNullOrEmpty(ex.ErrorCode) ? "" : "ErrorCode: " + ex.ErrorCode + "\r\nErrorPosition: " + ex.Position + "\r\n";
                var sErrorHint = string.IsNullOrEmpty(ex.Hint) ? "" : (string.IsNullOrEmpty(sErrorCode) ? "" : "\r\n") + ex.Hint;
                var sErrorMsg = ex.Message + (!string.IsNullOrEmpty(ex.DetailMessage) ? "\r\nDetailMsg: " + ex.DetailMessage : "");

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + ex.Position; //錯誤的定位點
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sExecutedResult + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sErrorMsg + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSqlExecuted;

                MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '" + sQueryTime + "', '" + sQueryTime + "', " + iCount + ", 'Error', 'ErrorMsg: " + sErrorMsg.Replace("'", "''") + "', '" + (iBatchRunQty == 0 ? "" : "/*Batch " + (i + 1) + " of " + iBatchRunQty + " */") + sSqlExecuted.Replace("'", "''") + "');";
                DBCommon.ExecNonQuery(sSqlScript);
            }
            finally
            {
                QueryCompleted?.Invoke();
            }
        }

        private static string GetResultString(string sMode, string sFirstWord, int iCount)
        {
            string sResult;

            switch (sMode.ToUpper())
            {
                case "SELECT":
                    sResult = "Select script - process ignored!";
                    break;
                case "WITH":
                    sResult = "With script - process ignored!";
                    break;
                case "CREATE":
                    sResult = "Create script - process OK!";
                    break;
                case "ALTER":
                    sResult = "Alter script - process OK!";
                    break;
                case "BATCH":
                    sResult = "Batch script - process OK!";
                    break;
                case "DROP":
                    sResult = "Drop script - process OK!";
                    break;
                case "EXECUTE":
                    sResult = "Execute script - process OK!";
                    break;
                case "EXTENDED":
                    sResult = "Extended script - process OK!";
                    break;
                case "TRUNCATE":
                    sResult = "Truncate script - process OK!";
                    break;
                case "ROLLBACK":
                    sResult = "Rollback script - process OK!";
                    break;
                case "COMMIT":
                    sResult = "Commit script - process OK!";
                    break;
                case "UNKNOWN":
                    switch (sFirstWord)
                    {
                        case "GRANT":
                            sResult = "Grant script - process OK!"; //PostgreSQL 不會回傳 Grant 筆數
                            break;
                        default:
                            sResult = sFirstWord + " script - process OK!";
                            break;
                    }

                    break;
                case "INSERT":
                    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " created.";
                    break;
                case "UPDATE":
                    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " updated.";
                    break;
                case "DELETE":
                    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " deleted.";
                    break;
                //case "GRANT": //Devart 元件沒有這一個
                //    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " granted.";
                //    break;
                default:
                    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " " + sMode + "(undefineMode@JasonQuery).";
                    break;
            }

            return sResult;
        }

        public DataTable ExecuteQueryToDataTable(string sSql, bool bShowErrMsg = true)
        {
            var iError = 0;

            try
            {
                Datatable = new DataTable(); //加上這個，第一個 SQL Statement 執行有誤時，才不會帶出前一次的 Table data

                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;
                oCommand.ExecuteNonQuery();

                oDataAdapter = new PgSqlDataAdapter(oCommand);
                Datatable = new DataTable();
                oDataAdapter.Fill(Datatable);
            }
            catch (ThreadAbortException)
            {
                iError = 1;

                if (bShowErrMsg)
                {
                    _sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (PgSqlException ex)
            {
                iError = 1;

                if (bShowErrMsg)
                {
                    MessageBox.Show(ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return iError == 0 ? Datatable : null;
        }

        public void Clear()
        {
            oDataAdapter?.Dispose();
            oDataset?.Dispose();
        }

        public void Disconnect()
        {
            try
            {
                if (_mConn != null)
                {
                    if (_mConn.State == ConnectionState.Open)
                    {
                        _mConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Thread.Sleep(100);
        }

        private static void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            var thr = new Thread(() =>
            {
                IntPtr msgBox;

                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero)
                { }

                GetWindowRect(msgBox, out var r);

                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });

            thr.Start();
        }
    }
}