using DEMO.Models.DTO.LeaveAppDetail;
using Swashbuckle.AspNetCore.Filters;
using static DEMO.Models.DTO.UserLogin.UserLoginInfo;

namespace DEMO.SwaggerExamples
{
    public class LoginExamples : IMultipleExamplesProvider<LoginRequest>
    {
        public IEnumerable<SwaggerExample<LoginRequest>> GetExamples()
        {
            yield return SwaggerExample.Create("LOGIN USER 1", new LoginRequest
            {
                LOGIN_NAME = "Admin",
                PASSWORD = "abcdc"
            });

            yield return SwaggerExample.Create("LOGIN USER 2", new LoginRequest
            {
                LOGIN_NAME = "dt1037",
                PASSWORD = "tyuio"
            });
        }
    }

}
