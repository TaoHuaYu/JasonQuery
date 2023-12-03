using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using Devart.Data.Oracle;
using System.Drawing;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public class clsOracleReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title);

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

        private static OracleConnection _mConn;
        private OracleTransaction txn;
        private string sLangText;

        private OracleCommand oCommand;
        private OracleDataAdapter oDataAdapter;
        private DataSet oDataset;
        public delegate void QueryCompletedEventHandler();
        public event QueryCompletedEventHandler QueryCompleted;

        public DataSet Dataset => oDataset;
        private DataTable Datatable { get; set; }
        public OracleDataReader Datareader { get; private set; }

        public string ConnectTo()
        {
            var sErrMsg = "";

            _mConn = new OracleConnection
            {
                UserId = MyGlobal.sDBUser,
                Password = MyGlobal.sDBPW,
                Server = MyGlobal.sDBConnectionServer,
                Port = MyGlobal.iDBConnectionPort,
                Unicode = MyGlobal.bDBUnicode
            };
            
            if (MyGlobal.bDBDirectMode)
            {
                _mConn.Direct = true;
                _mConn.Sid = MyGlobal.sDBConnectionSid;
            }

            if (MyGlobal.bDBPooling == false)
            {
                _mConn.ConnectionString += "Pooling=false;"; //預設值=true
            }

            switch (MyGlobal.sDBConnectionConnectAs.ToUpper())
            {
                case "SYSDBA":
                    _mConn.ConnectMode = OracleConnectMode.SysDba;
                    break;
                case "SYSOPER":
                    _mConn.ConnectMode = OracleConnectMode.SysOper;
                    break;
            }

            oCommand = new OracleCommand();

            try
            {
                _mConn.Open();
                txn = _mConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (OracleException ex)
            {
                sErrMsg = ex.Message;
            }

            var sConnectTo = _mConn.State == ConnectionState.Open ? "" : sErrMsg;

            return sConnectTo;
        }

        public string oCommit()
        {
            var sResult = "";

            try
            {
                txn.Commit();
            }
            catch (OracleException ex)
            {
                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
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
            catch (OracleException ex)
            {
                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "ErrorMsg: " + ex.Message;
            }

            Thread.Sleep(100);

            return sResult;
        }

        public string oSavePoint(string sSavePoint)
        {
            var sResult = "";

            try
            {
                txn.Save(sSavePoint);
            }
            catch (OracleException ex)
            {
                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "ErrorMsg: " + ex.Message;
            }
            catch (Exception)
            {
                //
            }

            Thread.Sleep(100);

            return sResult;
        }

        public string oRollbackPoint(string sSavePoint)
        {
            var sResult = "";

            try
            {
                txn.Rollback(sSavePoint);
            }
            catch (OracleException ex)
            {
                var sErrorCode = "ErrorCode: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "ErrorMsg: " + ex.Message;
            }
            catch (Exception)
            {
                //
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

                if (_mConn != null)
                {
                    if (_mConn.State == ConnectionState.Open)
                    {
                        _mConn.Close();
                    }

                    _mConn.Dispose();
                }

                bValue = true;
            }
            catch (Exception)
            {
                //
            }

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

                var script = new OracleScript(sSql);
                sSql = script.Statements[0].Text.Replace("\r\n", "!r!n").Replace("\n", "\r\n").Replace("!r!n", "\r\n"); //經過 OracleScript(sSql) 解析的 SQL 指令，會出現 \n
                iPosOffset = script.Statements[0].Offset; //去掉註解後，真正要執行的 SQL 指令

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.FetchSize = 1000;
                oCommand.Connection = _mConn;
                Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OracleException ex)
            {
                var iOffset = ex.Offset;
                var sErrorCode = "";
                var sErrorHint = "";
                var sMessage = ex.Message;

                if (sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal) != -1)
                {
                    sMessage = sMessage.Substring(0, sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal));
                }

                if (sMessage.StartsWith("ORA-01008"))
                {
                    //有變數未指定值，例如 :name，Oracle 的定位點有錯，要再往後找到 : 才算定位OK
                    for (var i = ex.Offset; i < sSql.Length; i++)
                    {
                        if (sSql.Substring(i, 1) != ":")
                        {
                            continue;
                        }

                        iOffset = i;
                        break;
                    }
                }

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iOffset + (sMessage.IndexOf("missing expression", StringComparison.Ordinal) > 0 ? -1 : 0);
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

        public OracleDataReader ExecuteQueryPaged100Rows(string sSql, int iStartRow, int iPageLength, out bool bPermissionDenied)
        {
            bPermissionDenied = false;

            if (GetState() == ConnectionState.Closed)
            {
                ConnectTo();
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
            catch (OracleException)
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
            var iPosOffset = 0;
            var sSql = "";
            var sAccessibleDescription = ""; //識別從哪一個 QueryEditor 傳過來的 SQL

            try
            {
                var sTemp = Convert.ToString(sqlQuery);

                if (sTemp.Length > 10 && sTemp.Length - sTemp.Replace(MyGlobal.sSeparator5, "").Length == 10)
                {
                    sAccessibleDescription = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
                    iSelectionStart = Convert.ToInt32(sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1]);
                    sSql = sTemp.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator).Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2].TrimEnd(" ;\r\n".ToCharArray());
                }

                var script = new OracleScript(sSql);
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
                sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OracleException ex)
            {
                var iOffset = ex.Offset;
                var sErrorCode = "";
                var sErrorHint = "";

                if (ex.Message.StartsWith("ORA-01008"))
                {
                    //有變數未指定值，例如 :name，Oracle 的定位點有錯，要再往後找到 : 才算定位OK
                    for (var i = ex.Offset; i < sSql.Length; i++)
                    {
                        if (sSql.Substring(i, 1) != ":")
                        {
                            continue;
                        }

                        iOffset = i;
                        break;
                    }
                }

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iOffset + (ex.Message.IndexOf("missing expression", StringComparison.Ordinal) > 0 ? -1 : 0);
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql;
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

                var script = new OracleScript(sSql, _mConn);
                var sResult = "";

                if (script.Statements.Count > 1)
                {
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

                        iCount = 0; //reset to zero

                        if (script.Statements[i].StatementType.ToString() != "Select")
                        {
                            script.ExecuteNext(out var dr);
                            iCount = dr.RecordsAffected;

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
                sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OracleException ex)
            {
                if (iBatchRunQty > 0)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    MyGlobal.iProgressInsertInto = iBatchRunQty; //for Batch Run
                }

                sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);
                Datatable = new DataTable();
                var sErrorCode = "";
                var sErrorHint = "";

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + ex.Offset; //錯誤的定位點
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sExecutedResult + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSqlExecuted;

                MyGlobal.sExecuteNonQuerySQLHistoryScript = "OK";
                sSqlScript = "Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "', '" + sQueryTime + "', '" + sQueryTime + "', " + iCount + ", 'Error', 'ErrorMsg: " + ex.Message.Replace("'", "''") + "', '" + (iBatchRunQty == 0 ? "" : "/*Batch " + (i + 1) + " of " + iBatchRunQty + " */") + sSqlExecuted.Replace("'", "''") + "');";
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
                            sResult = "Grant script - process OK!"; //Oracle 不會回傳 Grant 筆數
                            break;
                        case "COMMENT":
                            sResult = "Comment script - process OK!";
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
                Datatable = new DataTable();

                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;

                var dataReader = oCommand.ExecuteReader();
                Datatable.Load(dataReader);
            }
            catch (ThreadAbortException)
            {
                iError = 1;

                if (bShowErrMsg)
                {
                    sLangText = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OracleException ex)
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