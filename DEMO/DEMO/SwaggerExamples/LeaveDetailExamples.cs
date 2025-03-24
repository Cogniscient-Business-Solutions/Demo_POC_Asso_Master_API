using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveGrantReject;
using Swashbuckle.AspNetCore.Filters;
using static DEMO.Models.Generic.Enums;

namespace DEMO.SwaggerExamples
{
    public class LeaveDetailExamples : IMultipleExamplesProvider<LeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<LeaveRequestDto>> GetExamples()
        {

            foreach (LeaveStatusEnum status in Enum.GetValues(typeof(LeaveStatusEnum)))
            {
                yield return SwaggerExample.Create($"LEAVE - {status}", new LeaveRequestDto
                {
                    DateRange = new DateRangeDto
                    {
                        FromDate = "2020-01-01",
                        ToDate = "2025-03-20"
                    },
                    Status = new List<LeaveStatusEnum> { status }
                });
            }

            //// Example with multiple statuses
            //yield return SwaggerExample.Create("Multiple Statuses", new LeaveRequestDto
            //{
            //    FromDate = "2020-01-01",
            //    ToDate = "2025-03-20",
            //    Status = new List<LeaveStatusEnum> { LeaveStatusEnum.APPROVAL_REQUEST, LeaveStatusEnum.CANCELLATION_REQUEST }
            //});
        }




        }
    }

