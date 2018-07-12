using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODPizzaUtils
{
    public class MODReportLogs
    {
        private int _jobExecutionID;

        public MODReportLogs()
        {

        }

        public void JobError(string connStringLOG, string logSource, string logEntry, string entryType, int jobExecutionID, DateTime executedDateTime, int jobTerminated)
        {
            if (string.IsNullOrEmpty(connStringLOG))
            {
                throw new ArgumentException("message", nameof(connStringLOG));
            }

            SqlConnection oConn = new SqlConnection(connStringLOG);
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.CommandText = "logs.uspJob_Error";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;

                SqlParameter pLogSource = new SqlParameter
                {
                    ParameterName = "@LogSource",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 400,
                    Direction = ParameterDirection.Input,
                    Value = logSource
                };
                oCmd.Parameters.Add(pLogSource);

                SqlParameter pLogEntry = new SqlParameter
                {
                    ParameterName = "@LogEntry",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 4000,
                    Direction = ParameterDirection.Input,
                    Value = logEntry
                };
                oCmd.Parameters.Add(pLogEntry);

                SqlParameter pEntryType = new SqlParameter
                {
                    ParameterName = "@EntryType",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Direction = ParameterDirection.Input,
                    Value = entryType
                };
                oCmd.Parameters.Add(pEntryType);

                oConn.Open();

                oCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
        }

        public int JobLoggingInit(string connStringLOG, int parentID, string jobName, string description, string machineName, DateTime logicalDate, string userOperator)
        {
            SqlConnection oConn = new SqlConnection(connStringLOG);
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.CommandText = "logs.uspJob_Logging_Init";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;

                SqlParameter pParentID = new SqlParameter
                {
                    ParameterName = "@ParentID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = parentID
                };
                oCmd.Parameters.Add(pParentID);

                SqlParameter pJobName = new SqlParameter
                {
                    ParameterName = "@JobName",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 80,
                    Direction = ParameterDirection.Input,
                    Value = jobName
                };
                oCmd.Parameters.Add(pJobName);

                SqlParameter pDescription = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input,
                    Value = description
                };
                oCmd.Parameters.Add(pDescription);

                SqlParameter pMachineName = new SqlParameter
                {
                    ParameterName = "@MachineName",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    Value = machineName
                };
                oCmd.Parameters.Add(pMachineName);

                SqlParameter pLogicalDate = new SqlParameter
                {
                    ParameterName = "@LogicalDate",
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input,
                    Value = logicalDate
                };
                oCmd.Parameters.Add(@pLogicalDate);

                SqlParameter pOperator = new SqlParameter
                {
                    ParameterName = "@Operator",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    Value = userOperator
                };
                oCmd.Parameters.Add(pOperator);

                SqlParameter pJobExecutionID = new SqlParameter
                {
                    ParameterName = "@JobExecutionID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.InputOutput
                };
                oCmd.Parameters.Add(pJobExecutionID);

                oConn.Open();

                oCmd.ExecuteNonQuery();

                _jobExecutionID = (int)pJobExecutionID.Value;

                return (int)pJobExecutionID.Value;
            }
            catch (Exception)
            {
                return 0;
                //MessageBox.Show(ex.Message);
            }

        }

        public void JobLoggingFinalize(string connStringLOG, string notificationRecipients = "analytics@modpizza.com", string dbMailProfile = "outlook.office365.com")
        {
            SqlConnection oConn = new SqlConnection(connStringLOG);
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.CommandText = "logs.uspJob_Logging_Finalize";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;

                SqlParameter pJobExecutionID = new SqlParameter
                {
                    ParameterName = "@JobExecutionID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = _jobExecutionID
                };
                oCmd.Parameters.Add(pJobExecutionID);

                SqlParameter pNotificationRecipients = new SqlParameter
                {
                    ParameterName = "@NotificationRecipients",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 800,
                    Direction = ParameterDirection.Input,
                    Value = notificationRecipients
                };
                oCmd.Parameters.Add(pNotificationRecipients);

                SqlParameter pDBMailProfile = new SqlParameter
                {
                    ParameterName = "@DBMailProfile",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 128,
                    Direction = ParameterDirection.Input,
                    Value = dbMailProfile
                };
                oCmd.Parameters.Add(pDBMailProfile);

                oConn.Open();

                oCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        public void JobNotificationSend() { }

        public void JobSessionCtrlGet() { }

        public void JobStepDetailSend() { }

        public void JobStepINS(string connStringLOG, string logSource, string logEntry, string entryType, int jobExecutionID, DateTime executedDateTime, int rowCount)
        {
            SqlConnection oConn = new SqlConnection(connStringLOG);
            SqlCommand oCmd = new SqlCommand();

            try
            {
                oCmd.CommandText = "logs.uspJob_Step_INS";
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;

                SqlParameter pLogSource = new SqlParameter
                {
                    ParameterName = "@LogSource",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 400,
                    Direction = ParameterDirection.Input,
                    Value = logSource
                };
                oCmd.Parameters.Add(pLogSource);

                SqlParameter pLogEntry = new SqlParameter
                {
                    ParameterName = "@LogEntry",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 4000,
                    Direction = ParameterDirection.Input,
                    Value = logEntry
                };
                oCmd.Parameters.Add(pLogEntry);

                SqlParameter pEntryType = new SqlParameter
                {
                    ParameterName = "@EntryType",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 40,
                    Direction = ParameterDirection.Input,
                    Value = entryType
                };
                oCmd.Parameters.Add(pEntryType);

                oConn.Open();

                oCmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
        }

        public void ValidationErrorLogINS() { }

        public void ValidationErrorSend() { }

        public int JobExecutionID
        {
            get { return _jobExecutionID; }
            set { _jobExecutionID = value; }
        }
    }
}
