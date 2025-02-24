using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace DEMO.Models.DataDL.Interfaces
{
    public interface OrgChartInterface
    {       
            public SqlConnection Connect();
            public DataTable GetDataTable(string procedureName, Hashtable rec);
            public DataSet GetDataDataset(string procedureName, Hashtable rec);
            public SqlCommand BuildQueryCommand(string storedProcName, Hashtable rec);
            public decimal SaveData(string procedureName, Hashtable rec);
            Task<DataTable> GetDataTableAsync(string storedProcedure, Hashtable parameters);
            Task<DataSet> GetDataSetAsync(string procedureName, Hashtable rec);
            decimal SaveMultiLineData(string procedureName, ArrayList parameters);
            Task<int> ExecuteNonQueryAsync(string procedureName, Hashtable parameters);
        
    }
}
