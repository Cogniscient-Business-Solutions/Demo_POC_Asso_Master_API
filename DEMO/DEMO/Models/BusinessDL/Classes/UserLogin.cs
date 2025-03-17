using static DEMO.Models.DTO.UserLogin.UserLoginInfo;
using System.Collections;
using System.Data;
using DEMO.Models.BusinessDL.Interfaces;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DataDL.Classes;


namespace DEMO.Models.BusinessDL.Classes
{
    public class UserLogin : IUserInterface
    {
        private readonly IData _dataLayer;

        public UserLogin(IData dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<User?> ValidateUserAsync(string LOGIN_NAME, string PASSWORD)
        {
            try
            {
                Hashtable parameters = new Hashtable
            {
                { "LOGIN_NAME", LOGIN_NAME },
                { "PASSWORD", PASSWORD }
            };

                DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_VALIDATEUSER_test", parameters);

                if (dt == null || dt.Rows.Count == 0)
                {
                    return null; //(User?)ApiResponseHelper.ErrorResponse("400", "Invalid request payload.");
                }

                DataRow row = dt.Rows[0];

                return new User
                {
                    UserId = row["LOGIN_NAME"].ToString().Trim(),
                    Company = row["COMPANY_NO"].ToString().Trim(),
                    Location = row["LOCATION_NO"].ToString().Trim(),
                    User_Id = row["User_Id"].ToString().Trim(),
                    Role = row["Role"].ToString().Trim()
                };
            }
            catch (Exception ex)
            {
                // Log error if needed
                throw new Exception("Error validating user", ex);
            }
        }

    }
}





 
