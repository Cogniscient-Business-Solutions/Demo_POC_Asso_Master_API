using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.EmpDetail;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<IActionResult> GetEmpDetailAsync()
        {
            try
            {
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_GET_EMP_DETAIL_RTR", ht);

                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count <= 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No records found");
                }

                DataTable empTable = ds.Tables[0]; // Employee Details         
                DataTable reportingTable = ds.Tables[1]; // Reporting Person
                DataTable managerTable = ds.Tables[2]; // Manager Details

                var empDetails = new List<EmpDetail>();

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
                        Status = row["Status"].ToString().Trim(),

                        reporting_person = reportingTable.AsEnumerable()
                            .Where(r => r["Asso_Code"].ToString().Trim() == reportingPersonCode)
                            .Select(r => new PersonDetail
                            {
                                Name = r["Name"].ToString().Trim(),
                                Asso_Code = r["Asso_Code"].ToString().Trim(),
                                Designation = r["Designation"].ToString().Trim(),
                                Department = r["Department"].ToString().Trim(),
                                Status = row["Status"].ToString().Trim()
                            }).FirstOrDefault(),

                        Manager = managerTable.AsEnumerable()
                            .Where(m => m["Asso_Code"].ToString().Trim() == managerCode)
                            .Select(m => new PersonDetail
                            {
                                Name = m["Name"].ToString().Trim(),
                                Asso_Code = m["Asso_Code"].ToString().Trim(),
                                Designation = m["Designation"].ToString().Trim(),
                                Department = m["Department"].ToString().Trim(),
                                Status = row["Status"].ToString().Trim()
                            }).FirstOrDefault()
                    };

                    empDetails.Add(empDetail);
                }

                return ApiResponseHelper.SuccessResponse(empDetails);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred", ex.Message);
            }
        }


        public static class EmployeeDataStore
        {
            public static List<EmployeeRequestModel> Employees = new List<EmployeeRequestModel>();
        }




    }
}
