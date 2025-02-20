using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.OrgChartDetails;
using System.Collections;
using System.Data;
using static DEMO.Models.DataDL.Interfaces.OrgChartInterface;

namespace DEMO.Models.BusinessDL
{
    public class OrgChartServices
    {
        private readonly OrgChartInterface _dataLayer;  
        public Hashtable ht = new Hashtable();
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public OrgChartServices(OrgChartInterface dataLayer, IConfiguration configuration, ILogger<OrgChartServices> logger)
        {
            _dataLayer = dataLayer;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<OrgChartDetails> GetEmpDetailAsync()
        {
            var empData = new OrgChartDetails
            {
                Empdetails = new List<OrgChartData>(),
                EmpMessage = new LogMsg()
            };

            try
            {
                DataTable dt = await _dataLayer.GetDataTableAsync("GetEmpDetails", ht);

                if (dt.Rows.Count <= 0)
                {
                    empData.EmpMessage = new LogMsg { Success = false, ErrorMsg = "No Record Found" };
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var empDetail = new OrgChartData
                        {
                            ASSO_CODE = row["ASSO_CODE"].ToString().Trim(), // Property name should match class definition
                            Department = row["Department"].ToString().Trim(),
                            Designation = row["Designation"].ToString().Trim()
                        };
                        empData.Empdetails.Add(empDetail);
                    }
                    empData.EmpMessage.Success = true;
                }
            }
            catch (Exception ex)
            {
                empData.EmpMessage.ErrorMsg = $"An error occurred while fetching data: {ex.Message}";
                empData.EmpMessage.Success = false;
            }

            return empData;
        }
    }
}

