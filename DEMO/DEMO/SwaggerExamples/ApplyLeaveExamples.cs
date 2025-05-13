using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using Swashbuckle.AspNetCore.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static DEMO.Models.Generic.Enums;

namespace DEMO.SwaggerExamples
{
    public class ApplyLeaveExamples : IMultipleExamplesProvider<ApplyLeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<ApplyLeaveRequestDto>> GetExamples()
        {
            yield return SwaggerExample.Create("FRESH CL", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",  
                LeaveStatus = LeaveAppDetailEnum.FRESH, 
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = SessionEnum.WHOLE_DAY,
                ToDateSession = SessionEnum.WHOLE_DAY,
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("PENDING APPROVAL", new ApplyLeaveRequestDto
            {
                LeaveType = "SPL",
                LeaveStatus = LeaveAppDetailEnum.PENDING_APPROVAL,
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = SessionEnum.WHOLE_DAY,
                ToDateSession = SessionEnum.WHOLE_DAY,
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("FRESH Half CL [ERROR]", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = LeaveAppDetailEnum.FRESH,
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = SessionEnum.FIRST_HALF,
                ToDateSession = SessionEnum.FIRST_HALF,
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("GRANTED [ERROR]", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = LeaveAppDetailEnum.GRANTED,
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = SessionEnum.FIRST_HALF,
                ToDateSession = SessionEnum.FIRST_HALF,
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("PENDING CANCELLATION", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = LeaveAppDetailEnum.GRANTED,
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = SessionEnum.FIRST_HALF,
                ToDateSession = SessionEnum.FIRST_HALF,
                EmployeeReason = "DUE TO ......."
            });


        }
    }



}
