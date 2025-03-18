using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL
{
    public class ApplyLeaveService
    {
        private readonly IData _dataLayer;
        public Hashtable ht = new Hashtable();

        public ApplyLeaveService(IData dataLayer)
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
                if (dt == null)
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

    }
}
