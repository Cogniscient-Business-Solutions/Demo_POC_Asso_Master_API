using DEMO.Models.DataDL.Interfaces;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;


namespace DEMO.Models.DataDL.Classes
{
    public class SQLData : IData
    {

        SqlTransaction sqlTransaction;
        private SqlConnection Conn;

        private static string ConnectionString = string.Empty;

        public SQLData(IConfiguration _Confg)
        {
            var connString = _Confg.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentNullException("ConnectionStrings");
            }
            ConnectionString = connString;

            Conn = new SqlConnection(ConnectionString);
        }

        public SqlConnection Connect()
        {
            try
            {
                Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                sqlTransaction = Conn.BeginTransaction();
                return Conn;
            }
            catch (SqlException ex)
            {
                if (sqlTransaction != null)
                    sqlTransaction.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public SqlCommand BuildQueryCommand(string storedProcName, Hashtable rec)
        {
            try
            {
                SqlCommand command = new SqlCommand(storedProcName, Conn, sqlTransaction);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                SqlParameter sprm = command.Parameters.Add("@returnValue", SqlDbType.Decimal);
                sprm.Direction = ParameterDirection.Output;

                IDictionaryEnumerator myEnumerator = rec.GetEnumerator();
                while (myEnumerator.MoveNext())
                {

                    if (myEnumerator.Key.ToString() == "timestamp")
                    {
                        //command.Parameters.Add(new SqlParameter("@" + (myEnumerator.Key).ToString(), myEnumerator.Value));
                        SqlParameter pm = new SqlParameter();
                        pm = command.Parameters.Add("@timestamp", SqlDbType.Timestamp);
                        pm.Value = myEnumerator.Value;
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@" + myEnumerator.Key.ToString(), myEnumerator.Value.ToString()));
                    }
                }

                return command;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetDataDataset(string procedureName, Hashtable rec)
        {
            try
            {
                DataSet ds = new DataSet();
                Conn = Connect();
                SqlDataAdapter sqlDa = new SqlDataAdapter();
                sqlDa.SelectCommand = BuildQueryCommand(procedureName, rec);
                sqlDa.Fill(ds);
                Conn.Close();
                return ds;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataTable(string procedureName, Hashtable rec)
        {
            try
            {
                DataTable dt = new DataTable();
                Conn = Connect();
                SqlDataAdapter sqlDa = new SqlDataAdapter
                {
                    SelectCommand = BuildQueryCommand(procedureName, rec)
                };
                sqlDa.Fill(dt);
                sqlTransaction.Commit();
                return dt;
            }
            catch (SqlException ex)
            {
                sqlTransaction?.Rollback();
                throw ex;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }


        public decimal SaveData(string procedureName, Hashtable rec)
        {
            try
            {
                decimal result = 0;
                Conn = Connect();
                SqlCommand cmd = BuildQueryCommand(procedureName, rec);
                cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                if (Convert.IsDBNull(cmd.Parameters["@returnValue"].Value) == true)
                {
                    result = 0;
                }
                else
                    result = (decimal)cmd.Parameters["@returnValue"].Value;
                Conn.Close();
                return result;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public decimal ValidateData(string procedureName, Hashtable rec)
        {
            try
            {
                decimal result = 0;
                Conn = Connect();
                SqlCommand cmd = BuildQueryCommand(procedureName, rec);
                cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                if (Convert.IsDBNull(cmd.Parameters["@returnValue"].Value) == true)
                {
                    result = 0;
                }
                else
                    result = (decimal)cmd.Parameters["@returnValue"].Value;
                Conn.Close();
                return result;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DataTable> GetDataTableAsync(string procedureName, Hashtable rec)
        {
            try
            {
                DataTable dt = new DataTable();
                Conn = Connect();
                SqlCommand command = BuildQueryCommand(procedureName, rec);

                using (SqlDataAdapter sqlDa = new SqlDataAdapter(command))
                {
                    await Task.Run(() => sqlDa.Fill(dt));
                }

                sqlTransaction.Commit();
                return dt;
            }
            catch (SqlException ex)
            {
                sqlTransaction?.Rollback();
                throw ex;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public async Task<DataSet> GetDataSetAsync(string procedureName, Hashtable rec)
        {
            try
            {
                DataSet ds = new DataSet();
                Conn = Connect();
                SqlCommand command = BuildQueryCommand(procedureName, rec);

                using (SqlDataAdapter sqlDa = new SqlDataAdapter(command))
                {
                    await Task.Run(() => sqlDa.Fill(ds));
                }

                sqlTransaction.Commit();
                return ds;
            }
            catch (SqlException ex)
            {
                sqlTransaction?.Rollback();
                throw ex;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }


        public decimal SaveMultiLineData(string procedureName, ArrayList arrayLst)
        {
            try
            {
                decimal result = 0;
                Conn = Connect();

                foreach (Hashtable ht in arrayLst)
                {
                    using (SqlCommand cmd = BuildQueryCommandMultiLine(procedureName, ht))
                    {
                        cmd.ExecuteNonQuery();

                        result = (decimal)cmd.Parameters["@returnValue"].Value;
                    }
                }
                sqlTransaction.Commit();
                return result;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close();
                throw new Exception("SQL Error: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("General Error: " + ex.Message, ex);
            }
        }


        public SqlCommand BuildQueryCommandMultiLine(string storedProcName, Hashtable rec)
        {
            SqlCommand command = new SqlCommand(storedProcName, Conn, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;

            // Output parameter for return value
            command.Parameters.Add("@returnValue", SqlDbType.Decimal).Direction = ParameterDirection.Output;

            foreach (DictionaryEntry entry in rec)
            {
                string paramName = "@" + entry.Key.ToString();
                SqlParameter param;

                // Handle special cases
                if (entry.Key.ToString().ToUpper() == "TIMESTAMP")
                {
                    param = new SqlParameter(paramName, SqlDbType.Timestamp) { Value = entry.Value };
                }
                else if (entry.Key.ToString().ToUpper() == "FILE")
                {
                    param = new SqlParameter(paramName, SqlDbType.Image) { Value = entry.Value };
                }
                else
                {
                    // Use DBNull.Value for nulls
                    param = new SqlParameter(paramName, entry.Value ?? DBNull.Value);
                }

                command.Parameters.Add(param);
            }

            return command;
        }


        public string[] data_procStringTwoOutput(string procedureName, Hashtable rec)
        {
            try
            {
                string[] result = new string[2];

                Conn = Connect();
                SqlCommand cmd = BuildQueryCommandStringTwoOutput(procedureName, rec);
                cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                result[0] = (string)cmd.Parameters["@returnValue"].Value;
                result[1] = (string)cmd.Parameters["@returnValue2"].Value;

                Conn.Close();
                return result;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SqlCommand BuildQueryCommandStringTwoOutput(string storedProcName, Hashtable rec)
        {
            try
            {
                SqlCommand command = new SqlCommand(storedProcName, Conn, sqlTransaction);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                SqlParameter sprm = command.Parameters.Add("@returnValue", SqlDbType.VarChar, 60);
                sprm.Direction = ParameterDirection.Output;
                SqlParameter sprm2 = command.Parameters.Add("@returnValue2", SqlDbType.VarChar, 60);
                sprm2.Direction = ParameterDirection.Output;

                IDictionaryEnumerator myEnumerator = rec.GetEnumerator();
                while (myEnumerator.MoveNext())
                {

                    if (myEnumerator.Key.ToString().ToUpper() == "IMAGE")
                    {
                        SqlParameter pm = new SqlParameter();
                        pm = command.Parameters.Add("@Image", SqlDbType.Image);
                        pm.Value = myEnumerator.Value;
                    }
                    else if (myEnumerator.Key.ToString() == "timestamp")
                    {
                        //command.Parameters.Add(new SqlParameter("@" + (myEnumerator.Key).ToString(), myEnumerator.Value));
                        SqlParameter pm = new SqlParameter();
                        pm = command.Parameters.Add("@timestamp", SqlDbType.Timestamp);
                        pm.Value = myEnumerator.Value;
                    }
                    else if (myEnumerator.Key.ToString() == "output")
                    {
                        SqlParameter pm = new SqlParameter();
                        pm = command.Parameters.Add("@output", SqlDbType.VarChar);
                        pm.Direction = ParameterDirection.Output;
                        pm.Value = myEnumerator.Value;
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter("@" + (myEnumerator.Key).ToString(), myEnumerator.Value.ToString()));
                    }
                }

                return command;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                Conn.Close(); throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
