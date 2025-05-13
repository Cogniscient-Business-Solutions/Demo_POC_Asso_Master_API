using DEMO.Models.DTO.EmpDetail;
using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveApproval;
using Swashbuckle.AspNetCore.Filters;
using static DEMO.Models.Generic.Enums;

namespace DEMO.SwaggerExamples
{
    public class LeaveAppDetailExamples : IMultipleExamplesProvider<GetLeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<GetLeaveRequestDto>> GetExamples()
        {

            //foreach (LeaveAppDetailEnum LeaveStatus in Enum.GetValues(typeof(LeaveAppDetailEnum)))
            //{

            //    yield return SwaggerExample.Create($"Leave {LeaveStatus}", new GetLeaveRequestDto
            //    {
            //        DateRange = new DateRange
            //      {
            //          FromDate = "2020-01-01",
            //          ToDate = "2025-03-20"
            //      },
            //        LeaveStatus = LeaveStatus

            //    });

            //}
            foreach (LeaveAppDetailEnum LeaveStatus in Enum.GetValues(typeof(LeaveAppDetailEnum)))
            {
                yield return SwaggerExample.Create($"LEAVE - {LeaveStatus}", new GetLeaveRequestDto
                {
                    DateRange = new DateRange
                    {
                        FromDate = "2020-01-01",
                        ToDate = "2025-03-20"
                    },
                    LeaveStatus = new List<LeaveAppDetailEnum> { LeaveStatus }
                });
            }

        }
    }

}
