using System;
using System.Data;
using System.Data.OleDb;
//using System.Data.Odbc;

namespace DBHelper
{
    public class AccessFoxProOLE : IDisposable
    {
        private string _connStr = null;
        private OleDbConnection _Conn = null;

        private string _queryCmd = null;

        public void SetConnStr(string connStr)
        {
            _connStr = connStr;
            _Conn = new OleDbConnection(_connStr);

            _Conn.Open();
            try
            {
                OleDbCommand oleCmd = new OleDbCommand("set null off", _Conn);
                oleCmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new System.ArgumentException("Test DB Connection Failed", connStr);
            }
        }

        public void SetQueryCmd(string queryCmd)
        {
            _queryCmd = queryCmd;
        }

        public DataTable ReadDbData()
        {
            DataTable dt = new DataTable();

            try
            {
                //_Conn.Open();

                OleDbCommand oleCmd = new OleDbCommand(_queryCmd, _Conn);

                OleDbDataAdapter oleDa = new OleDbDataAdapter(oleCmd);
                oleDa.Fill(dt);
                oleDa.Dispose();
            }
            catch (Exception)
            {
                throw;                          // to be caught at the calling place
            }

            return dt;
        }

        public void RunSQLcmd()
        {
            try
            {
                //_Conn.Open();

                OleDbCommand oleCmd = new OleDbCommand(_queryCmd, _Conn);
                oleCmd.CommandTimeout = 600;                                  // Make it 10 minutes

                oleCmd.ExecuteNonQuery();
                oleCmd.Dispose();
            }
            catch (Exception)
            {
                throw;                          // to be caught and treated at the calling place
            }
        }

        public object GetASingleValue()
        {
            object ob = null;

            try
            {
                //_Conn.Open();

                OleDbCommand oleCmd = new OleDbCommand(_queryCmd, _Conn);
                ob = (object)oleCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }

            return ob;
        }

        public void Dispose()
        {
            if (_Conn != null)
            {
                _Conn.Dispose();
                _Conn = null;
            }
        }

    }
}
