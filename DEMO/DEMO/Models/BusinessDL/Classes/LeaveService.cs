using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveDetail;
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
                // Fetch data from database
                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_ADD_LEAVE_APPLICATION_PRV_PRI", ht);

                // Check if data is empty
                if (dt == null || dt.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
                }

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

    }
}

