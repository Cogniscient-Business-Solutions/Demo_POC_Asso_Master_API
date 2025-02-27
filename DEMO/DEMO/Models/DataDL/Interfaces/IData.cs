using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DEMO.Models.DataDL.Interfaces
{
    public interface IData
    {
        public SqlConnection Connect();
        public DataTable GetDataTable(string procedureName, Hashtable rec);
        public DataSet GetDataDataset(string procedureName, Hashtable rec);
        public SqlCommand BuildQueryCommand(string storedProcName, Hashtable rec);
        public decimal SaveData(string procedureName, Hashtable rec);
        Task<DataTable> GetDataTableAsync(string storedProcedure, Hashtable parameters);
        Task<DataSet> GetDataSetAsync(string procedureName, Hashtable rec);
        decimal SaveMultiLineData(string procedureName, ArrayList parameters);

        public decimal ValidateData(string procedureName, Hashtable rec);

    }
}
