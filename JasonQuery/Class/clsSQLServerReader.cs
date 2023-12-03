using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using Devart.Data.SqlServer;
using System.Drawing;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public class clsSQLServerReader
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title);

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

        private string sName;
        private static SqlConnection _mConn;
        private SqlTransaction txn;
        private SqlCommand oCommand;
        private SqlDataAdapter oDataAdapter;
        private DataSet oDataset;
        private DataTable oDataTable;

        public delegate void QueryCompletedEventHandler();
        public event QueryCompletedEventHandler QueryCompleted;

        private DataTable Datatable { set => oDataTable = value; }
        public SqlDataReader Datareader { get; private set; }

        public string ConnectTo(string strConnectionString)
        {
            var sErrMsg = "";

            oCommand = new SqlCommand();
            _mConn = new SqlConnection(strConnectionString);

            try
            {
                _mConn.Open();
                txn = _mConn.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (SqlException ex)
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
            catch (SqlException ex)
            {
                var sErrorCode = "ErrorCode1: " + ex.ErrorCode + "\r\n";
                sResult = sErrorCode + "(oCommit1)\r\nErrorMsg: " + ex.Message;
            }
            catch (Exception ex)
            {
                sResult = "(oCommit2)\r\nErrorMsg: " + ex.Message;
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
            catch (SqlException ex)
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
            catch (SqlException ex)
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
            catch (SqlException ex)
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

                Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                Datatable = new DataTable();

                var sErrorCode = ""; //20220820 拿掉 ErrorCode，因為它看起來像是一串無意義的負數。 //"ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = "\r\n" + ex.Message.Replace("\r\n資料指標並未宣告。", "").Replace("The cursor was not declared.", "");

                if (sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal) != -1)
                {
                    sMessage = sMessage.Substring(0, sMessage.IndexOf("\r\n ) AS PAGE_READ_T\r\n LIMIT", StringComparison.Ordinal));
                }

                var sNumber = MyGlobal.GetLanguageString("Number", "Global", "Global", "msg", "Number", "Text");
                var sClass = MyGlobal.GetLanguageString("Class", "Global", "Global", "msg", "Class", "Text");
                var sState = MyGlobal.GetLanguageString("State", "Global", "Global", "msg", "State", "Text");
                var sLineNumber = MyGlobal.GetLanguageString("LineNumber", "Global", "Global", "msg", "LineNumber", "Text");

                string sErrorMsg2;
                var sErrorFull = AnalysisErrorMessageWithLineNumber(sSql, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteQueryPaged

                if (string.IsNullOrEmpty(sErrorMsg2))
                {
                    sErrorFull = AnalysisErrorMessageWithoutLineNumber(sSql, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteQueryPaged
                }

                if (string.IsNullOrEmpty(sErrorMsg2))
                {
                    var iFrom = sMessage.IndexOf("'", StringComparison.Ordinal) + 1;
                    var iTo = sMessage.LastIndexOf("'", StringComparison.Ordinal);

                    if (iFrom != -1 && iTo != -1)
                    {
                        iTo = sMessage.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                        var sTempWord = sMessage.Substring(iFrom, iTo - iFrom);
                        var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);

                        if (ex.LineNumber > 0 && ex.LineNumber <= parts.Length && parts[ex.LineNumber - 1].IndexOf(sTempWord, StringComparison.Ordinal) != -1)
                        {
                            var iTemp0 = 0;
                            var iTemp9 = 0;
                            var iPosPerErrLine = parts[ex.LineNumber - 1].IndexOf(sTempWord, StringComparison.Ordinal);

                            foreach (var t in parts)
                            {
                                if (ex.LineNumber - 1 == iTemp0)
                                {
                                    iTemp9 += iPosPerErrLine;
                                    break;
                                }
                                else
                                {
                                    iTemp9 += t.Length + 2;
                                }

                                iTemp0++;
                            }

                            iPosition = iTemp9;
                        }
                        else
                        {
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }
                    }
                }
                else
                {
                    sMessage = sErrorFull;
                }

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sMessage + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql + MyGlobal.sSeparator5 + sErrorMsg2;
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

        public SqlDataReader ExecuteQueryPaged100Rows(string sSql, int iStartRow, int iPageLength)
        {
            if (GetState() == ConnectionState.Closed)
            {
                ConnectTo(MyGlobal.sDBConnectionString);
            }

            try
            {
                oCommand.CommandType = CommandType.Text;
                oCommand.CommandText = sSql;
                oCommand.Connection = _mConn;
                Datareader = oCommand.ExecutePageReader(CommandBehavior.Default, iStartRow, iPageLength);
            }
            catch (ThreadAbortException)
            {
                //
            }
            catch (SqlException)
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
            catch (SqlException ex)
            {
                Datatable = new DataTable();

                if (ex.Message.StartsWith("Parameter") && ex.Message.IndexOf("' is missing", StringComparison.Ordinal) != -1)
                {
                    var sWord = ":" + MyGlobal.GetStringBetween(ex.Message, "'", "'");
                    iPosition = sSql.IndexOf(sWord, StringComparison.Ordinal) + 1;
                }

                var sErrorCode = ""; //20220820 拿掉 ErrorCode，因為它看起來像是一串無意義的負數。 //"ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = "\r\n" + ex.Message.Replace("\r\n資料指標並未宣告。", "").Replace("The cursor was not declared.", "");

                var sNumber = MyGlobal.GetLanguageString("Number", "Global", "Global", "msg", "Number", "Text");
                var sClass = MyGlobal.GetLanguageString("Class", "Global", "Global", "msg", "Class", "Text");
                var sState = MyGlobal.GetLanguageString("State", "Global", "Global", "msg", "State", "Text");
                var sLineNumber = MyGlobal.GetLanguageString("LineNumber", "Global", "Global", "msg", "LineNumber", "Text");

                string sErrorMsg2;
                AnalysisErrorMessageWithLineNumber(sSql, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteQuery2

                if (string.IsNullOrEmpty(sErrorMsg2))
                {
                    AnalysisErrorMessageWithoutLineNumber(sSql, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteQuery2
                }

                if (string.IsNullOrEmpty(sErrorMsg2))
                {
                    var iFrom = sMessage.IndexOf("'", StringComparison.Ordinal) + 1;
                    var iTo = sMessage.LastIndexOf("'", StringComparison.Ordinal);

                    if (iFrom != -1 && iTo != -1)
                    {
                        iTo = sMessage.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                        var sTempWord = sMessage.Substring(iFrom, iTo - iFrom);
                        var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);

                        if (ex.LineNumber > 0 && ex.LineNumber <= parts.Length && parts[ex.LineNumber - 1].IndexOf(sTempWord, StringComparison.Ordinal) != -1)
                        {
                            var iTemp0 = 0;
                            var iTemp9 = 0;
                            var iPosPerErrLine = parts[ex.LineNumber - 1].IndexOf(sTempWord, StringComparison.Ordinal);

                            foreach (var t in parts)
                            {
                                if (ex.LineNumber - 1 == iTemp0)
                                {
                                    iTemp9 += iPosPerErrLine;
                                    break;
                                }
                                else
                                {
                                    iTemp9 += t.Length + 2;
                                }

                                iTemp0++;
                            }

                            iPosition = iTemp9;
                        }
                        else
                        {
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }
                    }
                }

                //傳回 SQL 錯誤的字串位置、錯誤訊息，由「該 QueryEditor」將錯誤的字串標示波浪底線
                iSelectionStart = iSelectionStart + iPosOffset + iPosition + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sMessage + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSql + MyGlobal.sSeparator5 + sErrorMsg2;
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

                if (script.Statements.Count > 1) //ExecuteNonQuery, 多個 SQL
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
                            if (script.Statements[i].StatementType.ToString() != "Unknown" && MyGlobal.GetFirstWord(sSqlExecuted.ToLower()) != "go")
                            {
                                oCommand.CommandText = sSqlExecuted;
                                iCount = oCommand.ExecuteNonQuery();
                            }

                            sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);
                        }

                        if (script.Statements[i].StatementType.ToString() == "Unknown" && MyGlobal.GetFirstWord(sSqlExecuted.ToLower()) == "go")
                        {
                            sResult = "GO script - process ignored!";
                        }
                        else
                        {
                            sResult = GetResultString(script.Statements[i].StatementType.ToString(), MyGlobal.GetFirstWord(script.Statements[i].Text.ToUpper()), iCount);
                        }

                        sExecutedResult += sResult + "\r\n";

                        if (sResult == "Use script - Database changed!")
                        {
                            var sDB = MyGlobal.GetFirstWord(script.Statements[i].Text.Substring(4).Trim());
                            sExecutedResult = "Database (" + sDB + ") changed!\r\n";
                            MyGlobal.sDBConnectionDatabase = sDB;
                            MyGlobal.sGlobalTemp2 = "sqlserverswitchdatabase" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sDB;
                            MyGlobal.sGlobalTemp3 = "sqlserverswitchdatabasebyusing`" + sDB;
                        }

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
                else //ExecuteNonQuery, 單一 SQL
                {
                    dtStartTime = DateTime.Now;

                    sSqlExecuted = sSql;
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = sSql;

                    if (script.Statements[0].StatementType.ToString() == "Unknown" && MyGlobal.GetFirstWord(script.Statements[0].Text.ToLower()) == "go")
                    {
                        sResult = "GO script - process ignored!";
                    }
                    else
                    {
                        oCommand.Connection = _mConn;
                        iCount = oCommand.ExecuteNonQuery();
                        sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);
                        sResult = GetResultString(script.Statements[0].StatementType.ToString(), MyGlobal.GetFirstWord(script.Statements[0].Text.ToUpper()), iCount);
                    }

                    sExecutedResult = sResult;

                    if (sExecutedResult == "Use script - Database changed!")
                    {
                        var sDB = MyGlobal.GetFirstWord(script.Statements[i].Text.Substring(4).Trim());
                        sExecutedResult = "Database (" + sDB + ") changed!";
                        MyGlobal.sDBConnectionDatabase = sDB;
                        MyGlobal.sGlobalTemp2 = "sqlserverswitchdatabase" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sDB;
                        MyGlobal.sGlobalTemp3 = "sqlserverswitchdatabasebyusing`" + sDB;
                    }

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
            catch (SqlException ex)
            {
                if (iBatchRunQty > 0)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    MyGlobal.iProgressInsertInto = iBatchRunQty; //for Batch Run
                }

                sQueryTime = MyGlobal.DateDiff(dtStartTime, DateTime.Now);

                Datatable = new DataTable();

                var sErrorCode = ""; //20220820 拿掉 ErrorCode，因為它看起來像是一串無意義的負數。 //"ErrorCode: " + ex.ErrorCode + "\r\n";
                var sErrorHint = "";
                var sMessage = ex.Message;
                var sErrorMsg2 = "";

                var sNumber = MyGlobal.GetLanguageString("Number", "Global", "Global", "msg", "Number", "Text");
                var sClass = MyGlobal.GetLanguageString("Class", "Global", "Global", "msg", "Class", "Text");
                var sState = MyGlobal.GetLanguageString("State", "Global", "Global", "msg", "State", "Text");
                var sLineNumber = MyGlobal.GetLanguageString("LineNumber", "Global", "Global", "msg", "LineNumber", "Text");

                var sErrorFull = AnalysisErrorMessageWithLineNumber(sSqlExecuted, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteNonQuery

                if (string.IsNullOrEmpty(sErrorMsg2))
                {
                    sErrorFull = AnalysisErrorMessageWithoutLineNumber(sSqlExecuted, ex.Errors, out sErrorMsg2, sNumber, sClass, sState, sLineNumber); //ExecuteNonQuery
                }

                if (!string.IsNullOrEmpty(sErrorFull))
                {
                    sMessage = sErrorFull;
                }

                //錯誤的定位點
                iSelectionStart += iPosOffset + iPosAdjust;
                MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + sAccessibleDescription + ";" + sExecutedResult + MyGlobal.sSeparator5 + sErrorCode + MyGlobal.sSeparator5 + sMessage + MyGlobal.sSeparator5 + sErrorHint + MyGlobal.sSeparator5 + iSelectionStart + MyGlobal.sSeparator5 + sSqlExecuted + MyGlobal.sSeparator5 + sErrorMsg2;

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
                    sResult = "Select script - process ignored! (ExecuteNonQuery)";
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
                            sResult = "Grant script - process OK!"; //SQL Server 不會回傳 Grant 筆數
                            break;
                        case "USE":
                            sResult = "Use script - Database changed!";
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

                oDataAdapter = new SqlDataAdapter(oCommand);
                oDataTable = new DataTable();
                oDataAdapter.Fill(oDataTable);
            }
            catch (ThreadAbortException)
            {
                iError = 1;
                sName = MyGlobal.GetLanguageString("Current operation was aborted.", "Global", "Global", "msg", "OperationAborted", "Text");
                MessageBox.Show(sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
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

        //使用 Error Line Number 定位字串位置，抓出錯誤字串出現的位置
        private static string AnalysisErrorMessageWithLineNumber(string sSql, SqlErrorCollection sqlError, out string sErrorMsg2, string sNumber, string sClass, string sState, string sLineNumber)
        {
            var iTemp = 0;
            var sTemp = "";
            var sTempWord = "";
            var iFrom = 0;
            var iTo = 0;
            var iStartPos = 0;
            var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);
            DataRow row;
            sErrorMsg2 = "";
            DataTable dtLines = new DataTable();
            dtLines.Columns.Add("Line", typeof(int));
            dtLines.Columns.Add("Count", typeof(int));
            dtLines.Columns.Add("Pos", typeof(int));
            var sErrorFull = "";

            if (sqlError.Count >= 1)
            {
                var sSymbol = "'";

                for (var j = 0; j < sqlError.Count; j++)
                {
                    if (sqlError[j].Number != 16945 && sqlError[j].State != 2)
                    {
                        sErrorFull += sNumber + " " + sqlError[j].Number + ", " + sClass + " " + sqlError[j].Class + ", " + sState + " " + sqlError[j].State + ", " + sLineNumber + " " + sqlError[j].LineNumber + "\r\n" + sqlError[j].Message + "\r\n";
                    }

                    DataRow[] drFilter = dtLines.Select("Line=" + sqlError[j].LineNumber);

                    if (drFilter.Length > 0)
                    {
                        iStartPos = (int)drFilter[0]["Pos"];
                    }
                    else
                    {
                        iStartPos = 0;
                        row = dtLines.NewRow();
                        row["Line"] = sqlError[j].LineNumber;
                        row["Count"] = 0;
                        row["Pos"] = 0;
                        dtLines.Rows.Add(row);

                        drFilter = dtLines.Select("Line=" + sqlError[j].LineNumber);
                    }

                    for (var i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            sSymbol = "'";
                        }
                        else
                        {
                            sSymbol = "\"";
                        }

                        iFrom = sqlError[j].Message.IndexOf(sSymbol, 0, StringComparison.Ordinal) + 1;
                        iTo = sqlError[j].Message.IndexOf(sSymbol, iFrom, StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1 && iTo > iFrom)
                        {
                            sTempWord = sqlError[j].Message.Substring(iFrom, iTo - iFrom);

                            if (sqlError[j].LineNumber - 1 >= 0 && sqlError[j].LineNumber - 1 <= parts.Length)
                            {
                                sTemp = parts[sqlError[j].LineNumber - 1];
                            }
                            else
                            {
                                sTemp = "";
                            }

                            iTemp = sTemp.IndexOf(sTempWord, iStartPos, StringComparison.Ordinal);

                            if (iTemp != -1)
                            {
                                for (var x = 0; x < 10; x++)
                                {
                                    if (iTemp != -1 && (sTemp.Substring((iTemp == 0 ? 0 : iTemp - 1), 1) == "'" || sTemp.Substring((iTemp == 0 ? 0 : iTemp - 1), 1) == "\"")) //避開單、雙引號框起來的字串
                                    {
                                        iTemp = sTemp.IndexOf(sTempWord, iTemp + sTempWord.Length, StringComparison.Ordinal);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (iTemp != -1)
                                {
                                    drFilter[0]["Pos"] = iTemp + sTempWord.Length;
                                    sErrorMsg2 += sqlError[j].LineNumber + MyGlobal.sSeparatorPlus2 + sTempWord + MyGlobal.sSeparatorPlus2 + iTemp + MyGlobal.sSeparatorPlus1;
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(sErrorMsg2))
            {
                dtLines = new DataTable();
                dtLines.Columns.Add("Line", typeof(int));
                dtLines.Columns.Add("Data");

                parts = sErrorMsg2.Split(new[] { MyGlobal.sSeparatorPlus1 }, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < parts.Length; i++)
                {
                    row = dtLines.NewRow();
                    row["Line"] = parts[i].Split(new[] { MyGlobal.sSeparatorPlus2 }, StringSplitOptions.None)[0];
                    row["Data"] = parts[i];
                    dtLines.Rows.Add(row);
                }

                var dv = dtLines.DefaultView;
                dv.Sort = "Line, Data";
                dtLines = dv.ToTable();
                sErrorMsg2 = "";

                for (var i = 0; i < dtLines.Rows.Count; i++)
                {
                    sErrorMsg2 += dtLines.Rows[i]["Data"] + MyGlobal.sSeparatorPlus1;
                }
            }

            if (sErrorFull.Length > 2 && sErrorFull.EndsWith("\r\n"))
            {
                sErrorFull = sErrorFull.Substring(0, sErrorFull.Length - 2);
            }

            return sErrorFull;
        }

        //無法透過 Error Line Number 定位字串位置，改抓錯誤字串第一次出現的位置
        private static string AnalysisErrorMessageWithoutLineNumber(string sSql, SqlErrorCollection sqlError, out string sErrorMsg2, string sNumber, string sClass, string sState, string sLineNumber)
        {
            var iTemp = 0;
            var sTempWord = "";
            var iFrom = 0;
            var iTo = 0;
            var iStartPos = 0;
            var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);
            sErrorMsg2 = "";
            var sErrorFull = "";

            if (sqlError.Count >= 1)
            {
                for (var j = 0; j < sqlError.Count; j++)
                {
                    if (sqlError[j].Number != 16945 && sqlError[j].State != 2)
                    {
                        sErrorFull += sNumber + " " + sqlError[j].Number + ", " + sClass + " " + sqlError[j].Class + ", " + sState + " " + sqlError[j].State + ", " + sLineNumber + " " + sqlError[j].LineNumber + "\r\n" + sqlError[j].Message + "\r\n";
                    }

                    for (var i = 0; i < 2; i++)
                    {
                        var sSymbol = "";

                        if (i == 0)
                        {
                            sSymbol = "'";
                        }
                        else
                        {
                            sSymbol = "\"";
                        }

                        iFrom = sqlError[j].Message.IndexOf(sSymbol, 0, StringComparison.Ordinal) + 1;
                        iTo = sqlError[j].Message.IndexOf(sSymbol, iFrom, StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1 && iTo > iFrom)
                        {
                            sTempWord = sqlError[j].Message.Substring(iFrom, iTo - iFrom);

                            for (var k = 0; k < parts.Length; k++)
                            {
                                iTemp = parts[k].IndexOf(sTempWord, iStartPos, StringComparison.Ordinal);

                                if (iTemp != -1)
                                {
                                    for (var x = 0; x < 10; x++)
                                    {
                                        if (iTemp != -1 && (parts[k].Substring((iTemp == 0 ? 0 : iTemp - 1), 1) == "'" || parts[k].Substring((iTemp == 0 ? 0 : iTemp - 1), 1) == "\"")) //避開單、雙引號框起來的字串
                                        {
                                            iTemp = parts[k].IndexOf(sTempWord, iTemp + sTempWord.Length, StringComparison.Ordinal);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    if (iTemp != -1)
                                    {
                                        sErrorMsg2 += (k + 1) + MyGlobal.sSeparatorPlus2 + sTempWord + MyGlobal.sSeparatorPlus2 + iTemp + MyGlobal.sSeparatorPlus1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (sErrorFull.Length > 2 && sErrorFull.EndsWith("\r\n"))
            {
                sErrorFull = sErrorFull.Substring(0, sErrorFull.Length - 2);
            }

            return sErrorFull;
        }
    }
}