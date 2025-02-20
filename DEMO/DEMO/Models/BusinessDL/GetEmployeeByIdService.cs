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
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public GetEmployeeByIdService(IData dataLayer, IConfiguration configuration, ILogger<GetEmployeeByIdService> logger)
        {
            _dataLayer = dataLayer;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        //public async Task<GetEmpData> GetEmpDetailAsync()
        //{
        //    var empData = new GetEmpData
        //    {
        //        EDetails = new List<EmpDetail>(),
        //        EmpMessage = new EmpMsg()
        //    };

        //    try
        //    {
        //        DataTable dt = await _dataLayer.GetDataTableAsync("GetEmpDetails", ht);

        //        if (dt.Rows.Count <= 0)
        //        {
        //            empData.EmpMessage = new EmpMsg { Success = false, ErrorMsg = "No Record Found" };
        //        }
        //        else
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                var empDetail = new EmpDetail
        //                {
        //                    Asso_Code = row["ASSO_CODE"].ToString().Trim(), // Property name should match class definition
        //                    Department = row["Department"].ToString().Trim(),
        //                    Designation = row["Designation"].ToString().Trim()
        //                };
        //                empData.EDetails.Add(empDetail);
        //            }
        //            empData.EmpMessage.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        empData.EmpMessage.ErrorMsg = $"An error occurred while fetching data: {ex.Message}";
        //        empData.EmpMessage.Success = false;
        //    }

        //    return empData;
        //}

        //public async Task<GetEmpData> GetEmpDetailAsync()
        //{
        //    var empData = new GetEmpData
        //    {
        //        EDetails = new List<EmpDetail>(),
        //        EmpMessage = new EmpMsg()
        //    };

        //    try
        //    {
        //        DataTable dt = await _dataLayer.GetDataTableAsync("GetEmpDetails", ht);

        //        if (dt.Rows.Count <= 0)
        //        {
        //            empData.EmpMessage = new EmpMsg { Success = false, ErrorMsg = "No Record Found" };
        //        }
        //        else
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                var empDetail = new EmpDetail
        //                {
        //                    Name = row["Name"].ToString().Trim(),
        //                    Asso_Code = row["Asso_Code"].ToString().Trim(),
        //                    Designation = row["Designation"].ToString().Trim(),
        //                    Department = row["Department"].ToString().Trim(),
        //                    //EmailId = row["EmailId"].ToString().Trim(),
        //                    //ContactNo = row["ContactNo"].ToString().Trim(),

        //                    reporting_person = new PersonDetail
        //                    {
        //                        Name = row["Name"].ToString().Trim(),
        //                        Asso_Code = row["Asso_Code"].ToString().Trim(),
        //                        Designation = row["Designation"].ToString().Trim(),
        //                        Department = row["Department"].ToString().Trim()
        //                    },

        //                    Manager = new PersonDetail
        //                    {
        //                        Name = row["Name"].ToString().Trim(),
        //                        Asso_Code = row["Asso_Code"].ToString().Trim(),
        //                        Designation = row["Designation"].ToString().Trim(),
        //                        Department = row["Department"].ToString().Trim()
        //                    }
        //                };

        //                empData.EDetails.Add(empDetail);
        //            }
        //            empData.EmpMessage.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        empData.EmpMessage.ErrorMsg = $"An error occurred while fetching data: {ex.Message}";
        //        empData.EmpMessage.Success = false;
        //    }

        //    return empData;
        //}
        public async Task<GetEmpData> GetEmpDetailAsync()
        {
            var empData = new GetEmpData
            {
                EDetails = new List<EmpDetail>(),
                EmpMessage = new EmpMsg()
            };

            try
            {
                DataTable dt = await _dataLayer.GetDataTableAsync("GetEmpDetails", ht);

                if (dt.Rows.Count <= 0)
                {
                    empData.EmpMessage = new EmpMsg { Success = false, ErrorMsg = "No Record Found" };
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var empDetail = new EmpDetail
                        {
                            Name = row["EmployeeName"].ToString().Trim(),  // ✅ Corrected
                            Asso_Code = row["EmployeeAssoCode"].ToString().Trim(),
                            Designation = row["EmployeeDesignation"].ToString().Trim(),
                            Department = row["EmployeeDepartment"].ToString().Trim(),
                            // EmailId = row["EmailId"].ToString().Trim(),
                            // ContactNo = row["ContactNo"].ToString().Trim(),

                            reporting_person = new PersonDetail
                            {
                                Name = row["ReportingPersonName"]?.ToString().Trim(),  // ✅ Fetch from correct column
                                Asso_Code = row["ReportingPersonAssoCode"]?.ToString().Trim(),
                                Designation = row["ReportingPersonDesignation"]?.ToString().Trim(),
                                Department = row["ReportingPersonDepartment"]?.ToString().Trim()
                            },

                            Manager = new PersonDetail
                            {
                                Name = row["ManagerName"]?.ToString().Trim(),  // ✅ Fetch from correct column
                                Asso_Code = row["ManagerAssoCode"]?.ToString().Trim(),
                                Designation = row["ManagerDesignation"]?.ToString().Trim(),
                                Department = row["ManagerDepartment"]?.ToString().Trim()
                            }
                        };

                        empData.EDetails.Add(empDetail);
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
