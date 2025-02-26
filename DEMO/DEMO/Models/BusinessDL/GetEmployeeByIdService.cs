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

        //abhay21backup
        //public async Task<GetEmpData> GetEmpDetailAsync()
        //{
        //    var empData = new GetEmpData
        //    {
        //        EDetails = new List<EmpDetail>(),
        //        EmpMessage = new EmpMsg()
        //    };

        //    try
        //    {
        //        DataTable dt = await _dataLayer.GetDataTableAsync("GET_EMPDETAIL_ID_RTR", ht);

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
        //                    Name = row["Name"].ToString().Trim(),  // ✅ Corrected
        //                    Asso_Code = row["Asso_Code"].ToString().Trim(),
        //                    Designation = row["Designation"].ToString().Trim(),
        //                    Department = row["Department"].ToString().Trim(),
        //                    // EmailId = row["EmailId"].ToString().Trim(),
        //                    // ContactNo = row["ContactNo"].ToString().Trim(),

        //                    reporting_person = new PersonDetail
        //                    {
        //                        Name = row["Name"]?.ToString().Trim(),  // ✅ Fetch from correct column
        //                        Asso_Code = row["reporting_person"]?.ToString().Trim(),
        //                        Designation = row["Designation"]?.ToString().Trim(),
        //                        Department = row["Department"]?.ToString().Trim()
        //                    },

        //                    Manager = new PersonDetail
        //                    {
        //                        Name = row["Name"]?.ToString().Trim(),  // ✅ Fetch from correct column
        //                        Asso_Code = row["Manager"]?.ToString().Trim(),
        //                        Designation = row["Designation"]?.ToString().Trim(),
        //                        Department = row["Department"]?.ToString().Trim()
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


        //21febbackup
        //public async Task<GetEmpData> GetEmpDetailAsync()
        //{
        //    var empData = new GetEmpData
        //    {
        //        EDetails = new List<EmpDetail>(),
        //        EmpMessage = new EmpMsg()
        //    };

        //    try
        //    {
        //        DataTable dt = await _dataLayer.GetDataTableAsync("GET_EMPDETAIL_ID_RTR_21_FEB_2025", ht);

        //        if (dt.Rows.Count <= 0)
        //        {
        //            empData.EmpMessage = new EmpMsg
        //            {
        //                Success = false,
        //                ErrorMsg = "No Record Found"
        //            };
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

        //                    reporting_person = new PersonDetail
        //                    {
        //                        Name = row["Name"]?.ToString().Trim(),
        //                        Asso_Code = row["Asso_Code"]?.ToString().Trim(),
        //                        Designation = row["Designation"]?.ToString().Trim(),
        //                        Department = row["Department"]?.ToString().Trim()
        //                    },

        //                    Manager = new PersonDetail
        //                    {
        //                        Name = row["Name"]?.ToString().Trim(),
        //                        Asso_Code = row["Asso_Code"]?.ToString().Trim(),
        //                        Designation = row["Designation"]?.ToString().Trim(),
        //                        Department = row["Department"]?.ToString().Trim()
        //                    }
        //                };

        //                empData.EDetails.Add(empDetail);
        //            }

        //            empData.EmpMessage = new EmpMsg
        //            {
        //                Success = true
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        empData.EmpMessage = new EmpMsg
        //        {
        //            Success = false,
        //            ErrorMsg = $"An unexpected error occurred: {ex.Message}"
        //        };
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
