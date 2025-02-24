using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.OrgChartDetails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DEMO.Models.BusinessDL
{
    public class OrgChartServices
    {
        private readonly OrgChartInterface _dataLayer;
        private readonly string _connectionString;
        private readonly ILogger<OrgChartServices> _logger;

        public OrgChartServices(OrgChartInterface dataLayer, IConfiguration configuration, ILogger<OrgChartServices> logger)
        {
            _dataLayer = dataLayer;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<OrgChartResponse> GetEmpDetailAsync(Hashtable parameters)
        {
            var response = new OrgChartResponse
            {
                Status = "SUCCESS",
                Data = new OrgChartData
                {
                    SelectedUser = new EmployeeDetails(),
                    Reportees = new List<EmployeeDetails>(),
                    Managers = new List<ManagerDetails>()
                }
            };

            try
            {
                // Fetch data from stored procedure
                DataSet ds = await _dataLayer.GetDataSetAsync("Organisation_Chart_RTR", parameters);

                // Ensure the dataset contains at least 3 tables
                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count == 0)
                {
                    response.Status = "FAIL";
                    response.Error = new ErrorDetails
                    {
                        Code = "USER_NOT_FOUND",
                        Message = "The employee ID provided does not exist in the system.",
                        Details = "Please check the employee ID and try again."
                    };
                    return response;
                }

                // Extract tables
                DataTable empTable = ds.Tables[0];      // Selected Employee Details
                DataTable reporteeTable = ds.Tables[1]; // Reportees
                DataTable managerTable = ds.Tables[2];  // Managers

                // Extract Selected User
                if (empTable.Rows.Count > 0)
                {
                    DataRow empRow = empTable.Rows[0];
                    response.Data.SelectedUser = new EmployeeDetails
                    {
                        UserName = empRow["Name"].ToString().Trim(),
                        UserId = empRow["Asso_Code"].ToString().Trim(),
                        Designation = empRow["Designation"].ToString().Trim(),
                        Department = empRow["Department"].ToString().Trim()
                    };
                }

                // Extract Reportees
                response.Data.Reportees = reporteeTable.AsEnumerable()
                    .Select(row => new EmployeeDetails
                    {
                        UserName = row["Name"].ToString().Trim(),
                        UserId = row["Asso_Code"].ToString().Trim(),
                        Designation = row["Designation"].ToString().Trim(),
                        Department = row["Department"].ToString().Trim()
                    }).ToList();

                // Extract Managers
                response.Data.Managers = managerTable.AsEnumerable()
                    .Select(row => new ManagerDetails
                    {
                        UserName = row["Name"].ToString().Trim(),
                        UserId = row["Asso_Code"].ToString().Trim(),
                        Designation = row["Designation"].ToString().Trim(),
                        Department = row["Department"].ToString().Trim(),
                        Level = Convert.ToInt32(row["Level"])
                    }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetEmpDetailAsync: {ex.Message}");

                response.Status = "FAIL";
                response.Error = new ErrorDetails
                {
                    Code = "INTERNAL_ERROR",
                    Message = "An error occurred while fetching data.",
                    Details = ex.Message
                };
            }

            return response;
        }

    }
}
