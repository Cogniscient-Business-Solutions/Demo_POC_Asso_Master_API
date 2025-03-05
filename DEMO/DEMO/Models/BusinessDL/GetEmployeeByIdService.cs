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
        //private readonly UserPictureService _userPictureService;

        public GetEmployeeByIdService(IData dataLayer, ILogger<GetEmployeeByIdService> logger)
        {
            _dataLayer = dataLayer;
           
            _logger = logger;

            //_userPictureService = userPictureService;
        }


        //public async Task<IActionResult> GetEmpDetailAsync()
        //{
        //    try
        //    {
        //        DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_GET_EMP_DETAIL_RTR", new Hashtable());

        //        if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count <= 0)
        //        {
        //            return ApiResponseHelper.ErrorResponse("404", "No records found");
        //        }

        //        DataTable empTable = ds.Tables[0];
        //        DataTable reportingTable = ds.Tables[1];
        //        DataTable managerTable = ds.Tables[2];

        //        var empDetails = new List<EmpDetail>();

        //        foreach (DataRow row in empTable.Rows)
        //        {
        //            string assoCode = row["Asso_Code"].ToString().Trim();
        //            string reportingPersonCode = row["Reporting_Person"].ToString().Trim();
        //            string managerCode = row["Manager"].ToString().Trim();

        //            // Fetch Profile Picture for Employee
        //            var empPictureResult = await _userPictureService.GetProfilePictureAsync(assoCode) as ObjectResult;
        //            var empPicture = empPictureResult?.Value as UserPicture;

        //            // Fetch Reporting Person Details
        //            var reportingPerson = reportingTable.AsEnumerable()
        //                .Where(r => r["Asso_Code"].ToString().Trim() == reportingPersonCode)
        //                .Select(async r =>
        //                {
        //                    var reportingPictureResult = await _userPictureService.GetProfilePictureAsync(reportingPersonCode) as ObjectResult;
        //                    var reportingPicture = reportingPictureResult?.Value as UserPicture;

        //                    return new PersonDetail
        //                    {
        //                        Name = r["Name"].ToString().Trim(),
        //                        Asso_Code = r["Asso_Code"].ToString().Trim(),
        //                        Designation = r["Designation"].ToString().Trim(),
        //                        Department = r["Department"].ToString().Trim(),
        //                        Status = r["Status"].ToString().Trim(),
        //                        UserPictureId = reportingPicture
        //                    };
        //                }).FirstOrDefault();

        //            // Fetch Manager Details
        //            var manager = managerTable.AsEnumerable()
        //                .Where(m => m["Asso_Code"].ToString().Trim() == managerCode)
        //                .Select(async m =>
        //                {
        //                    var managerPictureResult = await _userPictureService.GetProfilePictureAsync(managerCode) as ObjectResult;
        //                    var managerPicture = managerPictureResult?.Value as UserPicture;

        //                    return new PersonDetail
        //                    {
        //                        Name = m["Name"].ToString().Trim(),
        //                        Asso_Code = m["Asso_Code"].ToString().Trim(),
        //                        Designation = m["Designation"].ToString().Trim(),
        //                        Department = m["Department"].ToString().Trim(),
        //                        Status = m["Status"].ToString().Trim(),
        //                        UserPictureId = managerPicture
        //                    };
        //                }).FirstOrDefault();

        //            var empDetail = new EmpDetail
        //            {
        //                Name = row["Name"].ToString().Trim(),
        //                Asso_Code = assoCode,
        //                Designation = row["Designation"].ToString().Trim(),
        //                Department = row["Department"].ToString().Trim(),
        //                Status = row["Status"].ToString().Trim(),
        //                Email = row["Email"].ToString().Trim(),
        //                Mobile_No = row["Mobile_No"].ToString().Trim(),
        //                UserPictureId = empPicture,
        //                ReportingPerson = await reportingPerson,
        //                Manager = await manager
        //            };

        //            empDetails.Add(empDetail);
        //        }

        //        return ApiResponseHelper.SuccessResponse(empDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred", ex.Message);
        //    }
        //    }

        public async Task<IActionResult> GetEmpDetailAsync()
        {
            try
            {
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_GET_EMP_DETAIL_RTR", ht);

                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count <= 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No records found");
                }

                DataTable empTable = ds.Tables[0];
                DataTable reportingTable = ds.Tables[1];
                DataTable managerTable = ds.Tables[2];

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
                        Email = row["Email"].ToString().Trim(),
                        Mobile_No = row["Mobile_No"].ToString().Trim(),

                        ReportingPerson = reportingTable.AsEnumerable()
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
