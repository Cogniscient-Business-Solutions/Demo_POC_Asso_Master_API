using DEMO.Models.DTO.EmpDetail;
using Swashbuckle.AspNetCore.Filters;

namespace DEMO.SwaggerExamples
{
    public class EmployeeMultipleExamples : IMultipleExamplesProvider<EmployeeRequestModel>
    {
        public IEnumerable<SwaggerExample<EmployeeRequestModel>> GetExamples()
        {
            yield return SwaggerExample.Create("Example 1", new EmployeeRequestModel
            {
                EmployeeId = 1,
                ASSO_CODE = "ABC123",
                COMPANY_NO = "COMP001",
                LOCATION_NO = "LOC789",
                EMP_NAME = "John Doe",
                EMP_EMAIL = "john.doe@example.com",
                EMP_PHONE = "1234567890"
            });

            yield return SwaggerExample.Create("Example 2", new EmployeeRequestModel
            {
                EmployeeId = 2,
                ASSO_CODE = "XYZ456",
                COMPANY_NO = "COMP002",
                LOCATION_NO = "LOC123",
                EMP_NAME = "Jane Smith",
                EMP_EMAIL = "jane.smith@example.com",
                EMP_PHONE = "9876543210"
            });
        }
    }

}
