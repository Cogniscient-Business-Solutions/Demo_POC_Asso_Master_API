using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveApproval;
using DEMO.Models.DTO.LeaveAuthorizeCancel;
using DEMO.Models.DTO.LeaveDetail;
using DEMO.Models.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;
using static DEMO.Models.Generic.Enums;

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
                    return ApiResponseHelper.ErrorResponse("No Record Found", "No leave application records found.");
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


      
        public async Task<IActionResult> GetLeaveAppDetailAsync(Hashtable ht)
        {
            try
            {
                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_APP_CODE_CCH_DATE_FILTER", ht);

                if (dt == null || dt.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("No leave data found", "No leave application records found.");
                }

                var leaveDetails = new List<LeaveAppDetail>();



                foreach (DataRow row in dt.Rows)
                {
                    //int numericStatus = row.Table.Columns.Contains("leaveStatus") && row["leaveStatus"] != DBNull.Value
                    //    ? Convert.ToInt32(row["leaveStatus"])
                    //    : 0;

                    leaveDetails.Add(new LeaveAppDetail
                    {
                        LeaveType = row["leaveType"].ToString().Trim(),
                        NoOfDays = Convert.ToInt32(row["noOfDays"]),
                        LeaveStatus = row["LeaveStatus"].ToString().Trim(),
                        LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
                        FromDate = row["fromDate"].ToString().Trim(),
                        ToDate = row["toDate"].ToString().Trim(),
                        FromDateSession = row["fromDateSession"].ToString().Trim(),
                        ToDateSession = row["toDateSession"].ToString().Trim(),
                        EmployeeReason = row["employeeReason"].ToString().Trim(),
                        LeaveApplicationDate = row["leaveApplicationDate"].ToString().Trim(),
                        //ApprovalDate = row.Table.Columns.Contains("ApprovalDate") && row["ApprovalDate"] != DBNull.Value
                        //    ? row["ApprovalDate"].ToString().Trim()
                        //    : null,
                        //ApprovalReason = row.Table.Columns.Contains("ApprovalReason") && row["ApprovalReason"] != DBNull.Value
                        //    ? row["ApprovalReason"].ToString().Trim()
                        //    : null
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
                    return ApiResponseHelper.ErrorResponse("No Data", "No records found");
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

        

        public async Task<IActionResult> GetEmployeeLeaveApprovalDetailsAsync(Hashtable parameters)
        {
            try
            {
                // Fetch data from the database using DataSet
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_LEAVE_APPROVAL", parameters);

               // Validate dataset
                if (ds == null || ds.Tables.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("No data", "No leave data found.");
                }

                // Extract tables
                DataTable dt1 = ds.Tables.Count > 1 ? ds.Tables[1] : null; 
                DataTable dt2 = ds.Tables.Count > 2 ? ds.Tables[2] : null; 

                if (dt1 == null || dt1.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("No data", "No employee details found.");
                }

                if (dt2 == null || dt2.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("No data", "No leave data found.");
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
                            userId = empId,
                            userName = row["NAME"].ToString().Trim(),
                            Designation = row["designation"].ToString().Trim(),
                            Department = row["department"].ToString().Trim(),
                            Status = row["STATUS"].ToString().Trim(), 
                            OpenLeaves = 0, 
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

                        //var leaveStatus = ConvertNumberToEnum<LeaveApprovalEnum>(row["STATUS1"].ToString()).ToString(); // Convert Status

                        //employees[empId].Status = row["STATUS"].ToString().Trim(); // Update status
                        //employees[empId].OpenLeaves = Convert.ToInt32(row["openLeaves"]); // Update openLeaves count

                        var leaveStatus = row["STATUS1"].ToString().Trim();

                        employees[empId].Leaves.Add(new LeaveDto
                        {
                            LeaveType = row["leave_Type"].ToString().Trim(),
                            //NoOfDays = Convert.ToInt32(row["DUEDAYS"]),
                            LeaveStatus = leaveStatus,
                            //LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
                            FromDate = Convert.ToDateTime(row["From_date"]).ToString("yyyy-MM-dd"),
                            ToDate = Convert.ToDateTime(row["To_date"]).ToString("yyyy-MM-dd"),
                            FromDateSession = row["From_session"].ToString().Trim(),
                            ToDateSession = row["To_session"].ToString().Trim(),
                            EmployeeReason = row["employee_Reason"].ToString().Trim(),
                            //LeaveApplicationDate = row["Notified_date"].ToString().Trim(),
                            LeaveApplicationDate = Convert.ToDateTime(row["Notified_date"]).ToString("yyyy-MM-dd"),
                            //ApprovalDate = row.Table.Columns.Contains("approvalDate") && row["approvalDate"] != DBNull.Value ? row["approvalDate"].ToString().Trim() : null,
                            ApprovalReason = row.Table.Columns.Contains("employer_reason") && row["employer_reason"] != DBNull.Value ? row["employer_reason"].ToString().Trim() : null,
                            Status = row["Status"].ToString().Trim(),
                            DueDays = row.Table.Columns.Contains("DUEDAYS") && row["DUEDAYS"] != DBNull.Value ? Convert.ToInt32(row["DUEDAYS"]) : (int?)null
                        });

                        if (leaveStatus.Equals("Open", StringComparison.OrdinalIgnoreCase)) 
                        {
                            employees[empId].OpenLeaves++; // Increment OpenLeaves count if status is "Open"
                        }
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

        public async Task<IActionResult> LeaveGrantRejectDetailAsync(Hashtable parameters)
        {
            try
            {
                // Fetch data from the database using DataSet
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_GRANT_REJECT_LEAVES", parameters);
                

                // Check if DataSet is null or contains no valid data
                if (ds == null || ds.Tables.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave records found.");
                }

                // Get the first DataTable from DataSet
                DataTable dt = ds.Tables[2];

                // Initialize counters
                int requestApproved = 0;
                int requestRejected = 0;

                // Loop through each row and count approvals and rejections
                foreach (DataRow row in dt.Rows)
                {
                    string leaveStatus = row["TRANSACTION_MODE"].ToString();

                    leaveStatus = FilterHelper.LeaveStatusMapping.FirstOrDefault(x => x.Value == leaveStatus).Key ?? leaveStatus;

                    if (leaveStatus == "GRANTED" || leaveStatus == "LEAVE_CANCELLED")
                    {
                        requestApproved++;
                    }
                    else if (leaveStatus == "APPROVAL_REJECTED" || leaveStatus == "CANCELLATION_REJECTED")
                    {
                        requestRejected++;
                    }
                }

                // Create response object
                var result = new
                {
                    requestApproved,
                    requestRejected
                };

                return ApiResponseHelper.SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }

        public async Task<IActionResult> LeaveAuthorizeDetailAsync(Hashtable ht)
        {
            try
            {
                // Fetch data from database using DataSet
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_ADD_LEAVE_APPLICATION_MODIFY_AUTHORIZE_CANCEL", ht);

                // Check if DataSet is null or contains no tables
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("No Record Found", "No leave application records found.");
                }

                // Get the first DataTable from DataSet
                DataTable dt = ds.Tables[1];

                // Initialize list to store leave details
                List<LeaveData> leaveDetails = new List<LeaveData>();

                // Loop through each row and populate list
                foreach (DataRow row in dt.Rows)
                {
                    LeaveData leave = new LeaveData
                    {
                        LeaveTransactionNo = Convert.ToInt32(row["@returnValue"])
                    };

                    leaveDetails.Add(leave);
                }

                
                var response = new
                {
                    leaveTransactionNo = leaveDetails.FirstOrDefault()?.LeaveTransactionNo
                };

                return ApiResponseHelper.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }

    }
}


