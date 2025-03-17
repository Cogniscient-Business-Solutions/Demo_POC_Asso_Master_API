using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.LeaveAppDetail;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL
{
    public class LeaveAppDetailService
    {

        private readonly IData _dataLayer;
        public Hashtable ht = new Hashtable();

        public LeaveAppDetailService(IData dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public async Task<IActionResult> GetLeaveAppDetailAsync(Hashtable ht)
        {
            try
            {
                // Fetch data from database
                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_APP_CODE_CCH_DATE_FILTER", ht);

                // Check if data is empty
                if (dt == null)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
                }

                // Initialize list to store leave details
                List<LeaveAppDetail> leaveDetails = new List<LeaveAppDetail>();

                // Loop through each row and populate list
                foreach (DataRow row in dt.Rows)
                {
                    int numericStatus = row.Table.Columns.Contains("leaveStatus") && row["leaveStatus"] != DBNull.Value
                                                    ? Convert.ToInt32(row["leaveStatus"]) : 0;

                    string leaveStatusText = StatusHelper.ConvertStatus(numericStatus).ToString();

                    LeaveAppDetail leave = new LeaveAppDetail
                    {
                        LeaveType = row["leaveType"].ToString().Trim(),
                        NoOfDays =  Convert.ToInt32(row["noOfDays"]),
                        LeaveStatus = leaveStatusText, //row["leaveStatus"].ToString().Trim(),
                        LeaveTransactionNo = Convert.ToInt32(row["leaveTransactionNo"]),
                        FromDate = row["fromDate"].ToString().Trim(),
                        ToDate = row["toDate"].ToString().Trim(),
                        FromDateSession = row["fromDateSession"].ToString().Trim(),
                        ToDateSession = row["toDateSession"].ToString().Trim(),
                        EmployeeReason = row["employeeReason"].ToString().Trim(),
                        LeaveApplicationDate = row["leaveApplicationDate"].ToString().Trim(),
                        ApprovalDate = row.Table.Columns.Contains("ApprovalDate") && row["ApprovalDate"] != DBNull.Value
                                                    ? row["ApprovalDate"].ToString().Trim() : null,
                        ApprovalReason = row.Table.Columns.Contains("ApprovalReason") && row["ApprovalReason"] != DBNull.Value ?
                                                    row["ApprovalReason"].ToString().Trim() : null
                    };

                    leaveDetails.Add(leave);
                }

                
                var response = new LeaveResponseDto { Leaves = leaveDetails };

                return ApiResponseHelper.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred.", ex.Message);
            }
        }


    }
}
