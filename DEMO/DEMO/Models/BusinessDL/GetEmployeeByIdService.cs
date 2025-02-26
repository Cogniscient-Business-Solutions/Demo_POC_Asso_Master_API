using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.EmpDetail;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL
{
    public class GetEmployeeByIdService
    {
        private readonly IData _dataLayer;
        public Hashtable ht = new Hashtable();
       
        private readonly ILogger _logger;

        public GetEmployeeByIdService(IData dataLayer, ILogger<GetEmployeeByIdService> logger)
        {
            _dataLayer = dataLayer;
           
            _logger = logger;
        }

        

        public async Task<GetEmpData> GetEmpDetailAsync()
        {
            var empData = new GetEmpData
            {
                EDetails = new List<EmpDetail>(),
                EmpMessage = new EmpMsg()
            };

            try
            {
                DataSet ds = await _dataLayer.GetDataSetAsync("GET_EMPDETAIL_ID_RTR", ht);

                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count <= 0)
                {
                    empData.EmpMessage = new EmpMsg
                    {
                        Success = false,
                        ErrorMsg = "No Record Found"
                    };
                }
                else
                {
                    DataTable empTable = ds.Tables[0]; // Employee Details
                    DataTable reportingTable = ds.Tables[1]; // Reporting Person
                    DataTable managerTable = ds.Tables[2]; // Manager Details

                    foreach (DataRow row in empTable.Rows)
                    {
                        string assoCode = row["Asso_Code"].ToString().Trim();
                        string reportingPersonCode = row["Reporting_Person"].ToString().Trim();
                        string managerCode = row["Manager"].ToString().Trim();

                        var empDetail = new EmpDetail
                        {
                            Name = row["Name"].ToString().Trim(),
                            Asso_Code = assoCode,
                            Designation = row["Designation"].ToString().Trim(),
                            Department = row["Department"].ToString().Trim(),

                            // Find Reporting Person using employee's `Reporting_Person` field
                            reporting_person = reportingTable.AsEnumerable()
                                .Where(r => r["Asso_Code"].ToString().Trim() == reportingPersonCode)
                                .Select(r => new PersonDetail
                                {
                                    Name = r["Name"].ToString().Trim(),
                                    Asso_Code = r["Asso_Code"].ToString().Trim(),
                                    Designation = r["Designation"].ToString().Trim(),
                                    Department = r["Department"].ToString().Trim()
                                }).FirstOrDefault(),

                            // Find Manager using employee's `Manager` field
                            Manager = managerTable.AsEnumerable()
                                .Where(m => m["Asso_Code"].ToString().Trim() == managerCode)
                                .Select(m => new PersonDetail
                                {
                                    Name = m["Name"].ToString().Trim(),
                                    Asso_Code = m["Asso_Code"].ToString().Trim(),
                                    Designation = m["Designation"].ToString().Trim(),
                                    Department = m["Department"].ToString().Trim()
                                }).FirstOrDefault()
                        };

                        empData.EDetails.Add(empDetail);
                    }

                    empData.EmpMessage = new EmpMsg
                    {
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                empData.EmpMessage = new EmpMsg
                {
                    Success = false,
                    ErrorMsg = $"An unexpected error occurred: {ex.Message}"
                };
            }

            return empData;
        }


        public static class EmployeeDataStore
        {
            public static List<EmployeeRequestModel> Employees = new List<EmployeeRequestModel>();
        }


    }
}
