using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    public class AccessFoxProODBC : IDisposable
    {
        private string _connStr = null;
        private OdbcConnection _Conn = null;

        private string _queryCmd = null;

        public void SetConnStr(string connStr)
        {
            _connStr = connStr;
            _Conn = new OdbcConnection(_connStr);

            _Conn.Open();
            try
            {
                OdbcCommand adoCmd = new OdbcCommand("set null off", _Conn);
                adoCmd.ExecuteNonQuery();
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

                OdbcCommand adoCmd = new OdbcCommand(_queryCmd, _Conn);

                OdbcDataAdapter adoDa = new OdbcDataAdapter(adoCmd);
                adoDa.Fill(dt);
                adoDa.Dispose();
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

                OdbcCommand adoCmd = new OdbcCommand(_queryCmd, _Conn);
                adoCmd.CommandTimeout = 600;                                  // Make it 10 minutes

                adoCmd.ExecuteNonQuery();
                adoCmd.Dispose();
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

                OdbcCommand adoCmd = new OdbcCommand(_queryCmd, _Conn);
                ob = (object)adoCmd.ExecuteScalar();
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

    } // class
}
