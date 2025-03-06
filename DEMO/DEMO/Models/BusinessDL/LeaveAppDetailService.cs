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
                if (dt == null || dt.Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "No leave application records found.");
                }

                // Initialize list to store leave details
                List<LeaveAppDetail> leaveDetails = new List<LeaveAppDetail>();

                // Loop through each row and populate list
                foreach (DataRow row in dt.Rows)
                {
                    LeaveAppDetail leave = new LeaveAppDetail
                    {
                        LeaveType = row["LeaveType"].ToString().Trim(),
                        //NoOfDays = Convert.ToInt32(row["NoOfDays"]),
                        LeaveStatus = row["LeaveStatus"].ToString().Trim(),
                        LeaveTransactionNo = Convert.ToInt32(row["LeaveTransactionNo"]),
                        FromDate = row["FromDate"].ToString().Trim(),
                        ToDate = row["ToDate"].ToString().Trim(),
                        FromDateSession = row["FromDateSession"].ToString().Trim(),
                        ToDateSession = row["ToDateSession"].ToString().Trim(),
                        EmployeeReason = row["EmployeeReason"].ToString().Trim(),
                        LeaveApplicationDate = row["LeaveApplicationDate"].ToString().Trim(),
                        ApprovalDate = row.Table.Columns.Contains("ApprovalDate") && row["ApprovalDate"] != DBNull.Value
                            ? row["ApprovalDate"].ToString().Trim()
                            : null,
                        ApprovalReason = row.Table.Columns.Contains("ApprovalReason") && row["ApprovalReason"] != DBNull.Value
                            ? row["ApprovalReason"].ToString().Trim()
                            : null
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
