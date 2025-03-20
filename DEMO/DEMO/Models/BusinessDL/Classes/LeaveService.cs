using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveDetail;
using DEMO.Models.DTO.LeaveGrantReject;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL.Classes
{
    public class LeaveService : ILeaveService
    {
        private readonly IData _dataLayer;

        public LeaveService(IData dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<IActionResult> ApplyLeaveDetailAsync(Hashtable ht)
        {
            try
            {
                // Fetch data from database using DataSet
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_ADD_LEAVE_APPLICATION_PRV_PRI", ht);

                // Check if DataSet is null or contains no tables
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
                }

                // Get the first DataTable from DataSet
                DataTable dt = ds.Tables[1];

                // Initialize list to store leave details
                List<ApplyLeaveDetail> leaveDetails = new List<ApplyLeaveDetail>();

                // Loop through each row and populate list
                foreach (DataRow row in dt.Rows)
                {
                    ApplyLeaveDetail leave = new ApplyLeaveDetail
                    {
                        LeaveTransactionNo = Convert.ToInt32(row["@returnValue"])
                    };

                    leaveDetails.Add(leave);
                }

                // Create response DTO
                var response = new ApplyLeaveResponseDto { Leaves = leaveDetails };

                return ApiResponseHelper.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }


        //public async Task<IActionResult> ApplyLeaveDetailAsync(Hashtable ht)
        //{
        //    try
        //    {
        //        // Fetch data from database
        //        //DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_ADD_LEAVE_APPLICATION_PRV_PRI", ht);
        //        DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_ADD_LEAVE_APPLICATION_PRV_PRI", ht);
        //        // Check if data is empty
        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
        //        }

        //        // Initialize list to store leave details
        //        List<ApplyLeaveDetail> leaveDetails = new List<ApplyLeaveDetail>();

        //        // Loop through each row and populate list
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            ApplyLeaveDetail leave = new ApplyLeaveDetail
        //            {
        //                LeaveTransactionNo = Convert.ToInt32(row["@returnValue"])
        //            };

        //            leaveDetails.Add(leave);
        //        }

        //        var response = new ApplyLeaveResponseDto { Leaves = leaveDetails };

        //        return ApiResponseHelper.SuccessResponse(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
        //    }
        //}

        //public async Task<IActionResult> ApplyLeaveDetailAsync(Hashtable ht)
        //{
        //    try
        //    {
        //        // Call the data_procStringTwoOutput method
        //        string[] result = await Task.Run(() => _dataLayer.data_procStringTwoOutput("CBS_HR_ADD_LEAVE_APPLICATION_PRV_PRI", ht));

        //        // Check if result is empty or null
        //        if (result == null || result.Length == 0)
        //        {
        //            return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
        //        }

        //        // Process each result separately
        //        List<ApplyLeaveDetail> leaveDetails = new List<ApplyLeaveDetail>();
        //        foreach (string value in result)
        //        {
        //            leaveDetails.Add(new ApplyLeaveDetail
        //            {
        //                LeaveTransactionNo = int.TryParse(value, out int parsedValue) ? parsedValue : 0
        //            });
        //        }

        //        var response = new ApplyLeaveResponseDto { Leaves = leaveDetails };
        //        return ApiResponseHelper.SuccessResponse(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
        //    }
        //}


        public async Task<IActionResult> GetLeaveAppDetailAsync(Hashtable ht)
        {
            try
            {
                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_APP_CODE_CCH_DATE_FILTER", ht);

                if (dt == null || dt.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
                }

                var leaveDetails = new List<LeaveAppDetail>();

                foreach (DataRow row in dt.Rows)
                {
                    int numericStatus = row.Table.Columns.Contains("leaveStatus") && row["leaveStatus"] != DBNull.Value
                        ? Convert.ToInt32(row["leaveStatus"])
                        : 0;

                    leaveDetails.Add(new LeaveAppDetail
                    {
                        LeaveType = row["leaveType"].ToString().Trim(),
                        NoOfDays = Convert.ToInt32(row["noOfDays"]),
                        LeaveStatus = StatusHelper.ConvertStatus(numericStatus).ToString(),
                        LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
                        FromDate = row["fromDate"].ToString().Trim(),
                        ToDate = row["toDate"].ToString().Trim(),
                        FromDateSession = row["fromDateSession"].ToString().Trim(),
                        ToDateSession = row["toDateSession"].ToString().Trim(),
                        EmployeeReason = row["employeeReason"].ToString().Trim(),
                        LeaveApplicationDate = row["leaveApplicationDate"].ToString().Trim(),
                        ApprovalDate = row.Table.Columns.Contains("ApprovalDate") && row["ApprovalDate"] != DBNull.Value
                            ? row["ApprovalDate"].ToString().Trim()
                            : null,
                        ApprovalReason = row.Table.Columns.Contains("ApprovalReason") && row["ApprovalReason"] != DBNull.Value
                            ? row["ApprovalReason"].ToString().Trim()
                            : null
                    });
                }

                var response = new LeaveResponseDto { Leaves = leaveDetails };
                return ApiResponseHelper.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }


        }

        public async Task<IActionResult> GetLeaveStatusDetailAsync(Hashtable ht)
        {
            try
            {
                // Fetch data from database
                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_Leave_Details_For_Dashboard", ht);

                // Check if data is empty
                if (dt == null || dt.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No records found");
                }

                // Initialize list to store leave details
                List<LeaveStatusDetail> leaveStatusDetails = new List<LeaveStatusDetail>();

                // Loop through each row and populate list
                foreach (DataRow row in dt.Rows)
                {
                    LeaveStatusDetail detail = new LeaveStatusDetail
                    {
                        Leave_type = row["Leave_type"].ToString().Trim(),
                        Leave_Decs = row["Leave_Decs"].ToString().Trim(),
                        Opening_bal = row["Opening_bal"].ToString().Trim(),
                        Entitled = row["Entitled"].ToString().Trim(),
                        Availed = row["Availed"].ToString().Trim(),
                        Balance = row["Balance"].ToString().Trim()
                    };

                    leaveStatusDetails.Add(detail);
                }

                // Return response with populated list
                return ApiResponseHelper.SuccessResponse(leaveStatusDetails);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred", ex.Message);
            }
        }

        //public async Task<IActionResult> GetEmployeeLeaveDetailsAsync(Hashtable ht)
        //{
        //    try
        //    {
        //        // Fetch data from database using DataSet
        //        DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_LEAVE_APPROVAL_19_march_2025", ht);

        //        // Check if DataSet is null, empty, or has no rows
        //        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        //        {
        //            return ApiResponseHelper.ErrorResponse("404", "No leave data found.");
        //        }

        //        // Get the first DataTable from DataSet
        //        DataTable dt = ds.Tables[0];
        //        DataTable dt1 = ds.Tables[1];
        //        DataTable dt2 = ds.Tables[2];

        //        var employees = new Dictionary<string, EmployeeDto>();

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            string empId = row["empCode"].ToString().Trim();

        //            if (!employees.ContainsKey(empId))
        //            {
        //                employees[empId] = new EmployeeDto
        //                {
        //                    UserId = empId,
        //                    ASSO_CODE = row["empName"].ToString().Trim(),
        //                    Designation = row["designation"].ToString().Trim(),
        //                    Department = row["department"].ToString().Trim(),
        //                    Status = row["status"].ToString().Trim(),
        //                    OpenLeaves = Convert.ToInt32(row["openLeaves"]),
        //                    Leaves = new List<LeaveDto>()
        //                };
        //            }

        //            employees[empId].Leaves.Add(new LeaveDto
        //            {
        //                LeaveType = row["leaveType"].ToString().Trim(),
        //                NoOfDays = Convert.ToInt32(row["noOfDays"]),
        //                LeaveStatus = row["leaveStatus"].ToString().Trim(),
        //                LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
        //                FromDate = row["fromDate"].ToString().Trim(),
        //                ToDate = row["toDate"].ToString().Trim(),
        //                FromDateSession = row["fromDateSession"].ToString().Trim(),
        //                ToDateSession = row["toDateSession"].ToString().Trim(),
        //                EmployeeReason = row["employeeReason"].ToString().Trim(),
        //                LeaveApplicationDate = row["leaveApplicationDate"].ToString().Trim(),
        //                ApprovalDate = row.Table.Columns.Contains("ApprovalDate") && row["ApprovalDate"] != DBNull.Value ? row["ApprovalDate"].ToString().Trim() : null,
        //                ApprovalReason = row.Table.Columns.Contains("ApprovalReason") && row["ApprovalReason"] != DBNull.Value ? row["ApprovalReason"].ToString().Trim() : null,
        //                Status = row["status"].ToString().Trim(),
        //                DueDays = row.Table.Columns.Contains("DueDays") && row["DueDays"] != DBNull.Value ? Convert.ToInt32(row["DueDays"]) : (int?)null
        //            });
        //        }

        //        var response = new LeaveResponseDataDto { Employees = employees.Values.ToList() };
        //        return ApiResponseHelper.SuccessResponse(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
        //    }
        //}

        public async Task<IActionResult> GetEmployeeLeaveDetailsAsync(Hashtable parameters)
        {
            try
            {
                // Fetch data from the database using DataSet
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_LEAVE_APPROVAL", parameters);

                // Validate dataset
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave data found.");
                }

                // Extract tables
                DataTable dt1 = ds.Tables.Count > 1 ? ds.Tables[1] : null; // Employee General Details
                DataTable dt2 = ds.Tables.Count > 2 ? ds.Tables[2] : null; // Leave & Open Leaves Data

                if (dt1 == null || dt1.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No employee details found.");
                }

                if (dt2 == null || dt2.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave data found.");
                }

                var employees = new Dictionary<string, EmployeeDto>();

                // Step 1: Populate employee details from dt1
                foreach (DataRow row in dt1.Rows)
                {
                    string empId = row["ASSO_CODE"].ToString().Trim();

                    if (!employees.ContainsKey(empId))
                    {
                        employees[empId] = new EmployeeDto
                        {
                            UserId = empId,
                            ASSO_CODE = row["ASSO_CODE"].ToString().Trim(),
                          
                            Designation = row["designation"].ToString().Trim(),
                            Department = row["department"].ToString().Trim(),
                            Status = "",  // Will be updated from dt2
                            OpenLeaves = 0, // Will be updated from dt2
                            Leaves = new List<LeaveDto>()
                        };
                    }
                }

                // Step 2: Populate leave details from dt2 and update employee status & openLeaves
                foreach (DataRow row in dt2.Rows)
                {
                    string empId = row["Emp_code"].ToString().Trim();

                    if (employees.ContainsKey(empId))
                    {
                        employees[empId].Status = row["status"].ToString().Trim(); // Update status
                        //employees[empId].OpenLeaves = Convert.ToInt32(row["openLeaves"]); // Update openLeaves count

                        employees[empId].Leaves.Add(new LeaveDto
                        {
                            LeaveType = row["leave_Type"].ToString().Trim(),
                            NoOfDays = Convert.ToInt32(row["DUEDAYS"]),
                            LeaveStatus = row["STATUS1"].ToString().Trim(),
                            //LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
                            FromDate = row["From_date"].ToString().Trim(),
                            ToDate = row["To_date"].ToString().Trim(),
                            FromDateSession = row["From_session"].ToString().Trim(),
                            ToDateSession = row["To_session"].ToString().Trim(),
                            EmployeeReason = row["employee_Reason"].ToString().Trim(),
                            LeaveApplicationDate = row["Notified_date"].ToString().Trim(),
                            //ApprovalDate = row.Table.Columns.Contains("approvalDate") && row["approvalDate"] != DBNull.Value ? row["approvalDate"].ToString().Trim() : null,
                            ApprovalReason = row.Table.Columns.Contains("employer_reason") && row["employer_reason"] != DBNull.Value ? row["employer_reason"].ToString().Trim() : null,
                            //Status = row["status"].ToString().Trim(),
                            //DueDays = row.Table.Columns.Contains("dueDays") && row["dueDays"] != DBNull.Value ? Convert.ToInt32(row["dueDays"]) : (int?)null
                        });
                    }
                }

                var response = new LeaveResponseDataDto { Employees = employees.Values.ToList() };
                return ApiResponseHelper.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }


    }
}


