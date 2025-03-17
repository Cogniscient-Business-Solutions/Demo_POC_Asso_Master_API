using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using DEMO.Models.DataDL.Interfaces;
using static DEMO.Models.DTO.UserLogin.UserLoginInfo;

public class UserService
{
    private readonly IData _dataLayer;

    public UserService(IData dataLayer)
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

            DataTable dt = await _dataLayer.GetDataTableAsync("CBS_HR_EYE_MOBILE_APP_VALIDATEUSER", parameters);

            if (dt == null || dt.Rows.Count == 0)
            {
                return null; // No matching user found
            }

            DataRow row = dt.Rows[0];

            return new User
            {
                UserId = row["UserId"].ToString().Trim(),
                Company = row["Company"].ToString().Trim(),
                Location = row["Location"].ToString().Trim()
            };
        }
        catch (Exception ex)
        {
            // Log error if needed
            throw new Exception("Error validating user", ex);
        }
    }    

}
