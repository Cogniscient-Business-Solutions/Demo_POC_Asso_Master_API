using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.LeaveDetail;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL
{
    public class LeaveStatusService
    {
        private readonly IData _dataLayer;
        public Hashtable ht = new Hashtable();

        public LeaveStatusService(IData dataLayer)
        {
            _dataLayer = dataLayer;
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

        //public async Task<IActionResult> GetLeaveStatusDetailAsync(Hashtable ht)
        //{
        //    try
        //    {
        //        DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_Leave_Details_For_Dashboard", ht);

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            return ApiResponseHelper.ErrorResponse("404", "No records found");
        //        }

        //        var leaveStatusDetails = dt.AsEnumerable().Select(row => new LeaveStatusDetail
        //        {
        //            Leave_type = row["Leave_type"].ToString().Trim(),
        //            Leave_Decs = row["Leave_Decs"].ToString().Trim(),
        //            Opening_bal = row["Opening_bal"].ToString().Trim(),
        //            Entitled = row["Entitled"].ToString().Trim(),
        //            Availed = row["Availed"].ToString().Trim(),
        //            Balance = row["Balance"].ToString().Trim()
        //        }).ToList();

        //        return ApiResponseHelper.SuccessResponse(leaveStatusDetails);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred", ex.Message);
        //    }
        //}


    }
}
