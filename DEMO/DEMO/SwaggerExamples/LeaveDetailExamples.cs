using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveGrantReject;
using Swashbuckle.AspNetCore.Filters;

namespace DEMO.SwaggerExamples
{
    public class LeaveDetailExamples : IMultipleExamplesProvider<LeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<LeaveRequestDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Leave detail 1", new LeaveRequestDto
            {
                FromDate = "2020-01-01",
                ToDate = "2025-03-20",
                Filters = new List<string> { "" }
            });

            yield return SwaggerExample.Create("Leave detail 2", new LeaveRequestDto
            {
                FromDate = "2025-01-01",
                ToDate = "2025-03-20",
                Filters = new List<string> { "" }
            });
        }
    }

}
