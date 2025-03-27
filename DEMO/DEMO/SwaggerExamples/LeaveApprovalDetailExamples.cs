using DEMO.Models.DTO.LeaveAppDetail;
using DEMO.Models.DTO.LeaveApproval;
using DEMO.Models.Generic;
using Swashbuckle.AspNetCore.Filters;
using static DEMO.Models.Generic.Enums;
using static DEMO.Models.Generic.FilterHelper;

namespace DEMO.SwaggerExamples
{
    public class LeaveApprovalDetailExamples : IMultipleExamplesProvider<LeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<LeaveRequestDto>> GetExamples()
        {

            foreach (LeaveApprovalEnum status in Enum.GetValues(typeof(LeaveApprovalEnum)))
            {

                //string description = EnumHelper.GetDescription(status);

                yield return SwaggerExample.Create($"LEAVE - {status}", new LeaveRequestDto
                {
                    DateRange = new DateRangeDto
                    {
                        FromDate = "2020-01-01",
                        ToDate = "2025-03-20"
                    },
                    filters = new List<LeaveApprovalEnum> { status }
                });
            }
        }




        }
    }

