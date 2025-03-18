using DEMO.Models.DTO.EmpDetail;
using DEMO.Models.DTO.LeaveAppDetail;
using Swashbuckle.AspNetCore.Filters;

namespace DEMO.Models.DataDL.Classes
{
    public class LeaveMultiple : IMultipleExamplesProvider<GetLeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<GetLeaveRequestDto>> GetExamples()
        {
            yield return SwaggerExample.Create("FRESH CL", new GetLeaveRequestDto
            {
                FromDate = "2020-01-01",
                ToDate = "2025-03-20",
                LeaveStatus = new List<string> { "FRESH" }
            });

            yield return SwaggerExample.Create("PENDING APPROVAL", new GetLeaveRequestDto
            {
                FromDate = "2020-01-01",
                ToDate = "2025-03-20",
                LeaveStatus = new List<string> { "GRANTED" }
            });
        }
    }

}
