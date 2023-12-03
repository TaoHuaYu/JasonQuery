using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Data.SQLite;
using System.Drawing;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public class clsSQLiteReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title);

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

        private string sName;
        private static SQLiteConnection _mConn;
        private SQLiteTransaction txn;
        private SQLiteCommand oCommand;
        private SQLiteDataAdapter oDataAdapter;
        private DataSet oDataset;
        private DataTable oDataTable;

        public delegate void QueryCompletedEventHandler();
        public event QueryCompletedEventHandler QueryCompleted;

        private DataTable Datatable { set => oDataTable = value; }
        public SQLiteDataReader Datareader { get; private set; }

        public string ConnectTo(string strConnectionString, string sMode = "", string sPW = "", string sFilename = "")
        {
            var sErrMsg = "";

            oCommand = new SQLiteCommand();
            _mConn = new SQLiteConnection(strConnectionString);

            try
            {
                if (sMode == "NEW")
                {
                    _mConn.Open();
                    _mConn.ChangePassword(sPW);
                }
                else if (sMode == "ENCRYPT")
                {
                    _mConn.Open();
                    _mConn.ChangePassword(sPW);
                }
                else if (sMode == "REMOVE")
                {
                    _mConn.SetPassword(sPW);
                    _mConn.Open();
                    _mConn.ChangePassword("");
                }
                else
                {
                    _mConn.SetPassword(sPW);
                    _mConn.Open();

                    var da = new SQLiteDataAdapter("SELECT sql FROM sqlite_master ", _mConn);
                    var ds = new DataSet();

                    try
                    {
                        ds.Clear();
                        da.Fill(ds);
                        txn = _mConn.BeginTransaction(IsolationLevel.ReadCommitted);
                    }
                    catch (Exception ex)
                    {
                        sErrMsg = ex.Message;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                sErrMsg = ex.Message;
            }

            var sConnectTo = sErrMsg;

            return sConnectTo;
        }

        public string oCommit()
        {
            var sResult = "";

            try
            {
                txn.Commit();
            }
            catch (SQLiteException ex)
            {
                var sErrorCode = "ErrorCode1: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "(oCommit1)ErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                sResult = "(oCommit2)ErrorMsg: " + ex.Message;
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
            catch (SQLiteException ex)
            {
                var sErrorCode = "ErrorCode2: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "ErrorMsg: " + ex.Message;
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
            var iPosAdjust = 1;
            var iPosOffset = 0;
            var iPosition = 0;
            var sSql = "";
            var iCRLF = 0;
            var sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 25)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    iCRLF = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3].TrimEnd(" ;\r\n".ToCharArray());
                    iStartRow = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4]);
                    iPageLength = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5]);
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                sSql = script.Statements[0].Text.Replace("\r\n", "!r!n").Replace("\n", "\r\n").Replace("!r!n", "\r\n"); //經過 OracleScript(sSql) 解析的 SQL 指令，中間會有 \n 的存在，故要經過多次的 Replace
                iPosOffset = script.Statements[0].Offset; //去掉註解後，真正要執行的 SQL 指令

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;

                oDataTable = new DataTable();

                //Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SQLiteException ex)
            {
                Datatable = new DataTable();

                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = ex.Message;
                var sErrorMsg2 = "";

                //if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                //{
                //    iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                //}

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql + MyGlobal.sSeparator5 + sErrorMsg2;
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

        public SQLiteDataReader ExecuteQueryPaged100Rows(string sSql, int iStartRow, int iPageLength)
        {
            try
            {
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;
                //Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (SQLiteException)
            {
                //
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
            var iPosAdjust = 1;
            var iPosOffset = 0;
            var iPosition = 0;
            var sSql = "";
            var iCRLF = 0;
            var sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 15)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    iCRLF = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3].TrimEnd(" ;\r\n".ToCharArray());
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                sSql = script.Statements[0].Text.Replace("\r\n", "!r!n").Replace("\n", "\r\n").Replace("!r!n", "\r\n"); //經過 OracleScript(sSql) 解析的 SQL 指令，中間會有 \n 的存在，故要經過多次的 Replace
                iPosOffset = script.Statements[0].Offset; //去掉註解後，真正要執行的 SQL 指令

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;

                oDataTable = new DataTable();

                Datareader = oCommand.ExecuteReader();
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SQLiteException ex)
            {
                Datatable = new DataTable();

                if (ex.Message.StartsWith("Parameter") && ex.Message.IndexOf("' is missing", StringComparison.Ordinal) != -1)
                {
                    var sWord = ":" + MyGlobal.GetStringBetween(ex.Message, "'", "'");
                    iPosition = sSql.IndexOf(sWord, StringComparison.Ordinal) + 1;
                }

                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = ex.Message;

                //if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                //{
                //    iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                //}

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql + MyGlobal.sSeparator5 + "";
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
            var i = 0;
            var iSelectionStart = 0;
            var iPosAdjust = 1;
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
            var iCRLF = 0;

            try
            {
                Datatable = new DataTable();
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 15)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    iCRLF = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3].TrimEnd(" ;\r\n".ToCharArray());
                }

                var script = new Devart.Data.Oracle.OracleScript(sSql);
                string sResult;

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
                        sExecutedResult = sExecutedResult.Substring(0, sExecutedResult.Length - 2);
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
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SQLiteException ex)
            {
                if (iBatchRunQty > 0)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    MyGlobal.iProgressInsertInto = iBatchRunQty; //for Batch Run
                }

                sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);

                Datatable = new DataTable();

                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = ex.Message;
                var sErrorMsg2 = "";

                //錯誤的定位點
                iSelectionStart += iPosOffset + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sExecutedResult + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSqlExecuted + MyGlobal.sSeparator5 + sErrorMsg2;

                MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '" + sQueryTime + "', '" + sQueryTime + "', " + iCount + ", 'Error', 'ErrorMsg: " + sMessage.Replace("'", "''") + "', '" + (iBatchRunQty == 0 ? "" : "/*Batch " + (i + 1) + " of " + iBatchRunQty + " */") + sSqlExecuted.Replace("'", "''") + "');";
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
                case "EXEC":
                    sResult = "Exec script - process OK!";
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
                            sResult = "Grant script - process OK!";
                            break;
                        case "USE":
                            sResult = "Use script - process OK!";
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
                default:
                    sResult = iCount + " row" + (iCount > 1 ? "s" : "") + " " + sMode + "(undefineMode@JasonQuery).";
                    break;
            }

            return sResult;
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sSql">SQL Query string</param>
        /// <remarks></remarks>
        public DataTable ExecuteQueryToDataTable(string sSQL, bool bShowMsg = true)
        {
            //var iCount;
            var iError = 0;

            try
            {
                Datatable = new DataTable(); //加上這個，第一個 SQL Statement 執行有誤時，才不會帶出前一次的 Table data

                oCommand.CommandText = sSQL;
                oCommand.Connection = _mConn;
                oCommand.ExecuteNonQuery(); //iCount = oCommand.ExecuteNonQuery();

                oDataAdapter = new SQLiteDataAdapter(oCommand);
                oDataTable = new DataTable();
                oDataAdapter.Fill(oDataTable);
            }
            catch (ThreadAbortException)
            {
                iError = 1;
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SQLiteException ex)
            {
                iError = 1;

                //如果沒有權限，會跑來這裡
                if (bShowMsg)
                {
                    MessageBox.Show(ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return iError == 0 ? oDataTable : null;
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