using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.EmpDetail;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Data;

namespace DEMO.Models.BusinessDL
{
    public class UserPictureService
    {
        private readonly IData _dataLayer;

        public UserPictureService(IData dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<IActionResult> GetProfilePictureAsync(string assoCode)
        {
            try
            {
                if (string.IsNullOrEmpty(assoCode))
                {
                    return ApiResponseHelper.ErrorResponse("400", "Asso_Code is required");
                }

               
                Hashtable parameters = new Hashtable
                {
                    { "Asso_Code", assoCode }
                };

      
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_GET_EMP_DETAIL_RTR", parameters);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse("404", "Profile picture not found for the given Asso_Code");
                }

             
                DataRow pictureRow = ds.Tables[0].Rows[0];

                var userPicture = new UserPicture
                {
                    FileId = pictureRow["FileId"].ToString().Trim(),
                    FileType = pictureRow["FileType"].ToString().Trim(),
                    FileName = pictureRow["FileName"].ToString().Trim()
                };

                return ApiResponseHelper.SuccessResponse(userPicture);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorResponse("500", "An unexpected error occurred", ex.Message);
            }
        }
    }
}
