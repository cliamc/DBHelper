using System;
using System.Data;
using System.Data.SqlClient;

namespace DBHelper
{
    public class AccessSQL : IDisposable
    {
        private string _connStr = null;
        private SqlConnection _Conn = null;
        private string _queryCmd = null;

        public void SetConnStr(string connStr)
        {
            _connStr = connStr;
            _Conn = new SqlConnection(_connStr);
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
                _Conn.Open();

                SqlCommand sCmd = new SqlCommand(_queryCmd, _Conn);

                SqlDataAdapter sDa = new SqlDataAdapter(sCmd);
                sDa.Fill(dt);
                _Conn.Close();
                sDa.Dispose();

            }
            catch (SqlException)
            {
                throw;                          // to be caught at the calling place
            }
            finally
            {
                _Conn.Close();
            }

            return dt;
        }

        public DataTable LoadDbData()
        {
            DataTable dt = new DataTable();

            try
            {
                _Conn.Open();

                SqlCommand sCmd = new SqlCommand(_queryCmd, _Conn);
                dt.Load(sCmd.ExecuteReader());

                _Conn.Close();
            }
            catch (SqlException)
            {
                throw;                          // to be caught at the calling place
            }
            finally
            {
                _Conn.Close();
            }

            return dt;
        }

        public void RunSQLcmd()
        {
            try
            {
                _Conn.Open();

                SqlCommand sCmd = new SqlCommand(_queryCmd, _Conn);
                // Default SqlCommand.CommandTimeout value is 30 seconds.
                //XLiUtilLog.LogMessage(sCmd.CommandTimeout.ToString());
                sCmd.CommandTimeout = 600;                                  // Make it 10 minutes

                sCmd.ExecuteNonQuery();
                sCmd.Dispose();
                _Conn.Close();
            }
            catch (SqlException)
            {
                throw;                          // to be caught and treated at the calling place
            }
            finally
            {
                _Conn.Close();
            }
        }

        public object GetASingleValue()
        {
            object ob = null;

            try
            {
                _Conn.Open();
                SqlCommand sCmd = new SqlCommand(_queryCmd, _Conn);
                ob = (object)sCmd.ExecuteScalar();
                _Conn.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                _Conn.Close();
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
