using DEMO.Models.DTO.ApplyLeave;
using DEMO.Models.DTO.LeaveAppDetail;
using Swashbuckle.AspNetCore.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DEMO.SwaggerExamples
{
    public class ApplyLeave : IMultipleExamplesProvider<ApplyLeaveRequestDto>
    {
        public IEnumerable<SwaggerExample<ApplyLeaveRequestDto>> GetExamples()
        {
            yield return SwaggerExample.Create("FRESH CL", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = "FRESH",
                FromDate = "2025-03-20",
                ToDate = "2025-03-20",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO ......."
            });



            yield return SwaggerExample.Create("PENDING APPROVAL", new ApplyLeaveRequestDto
            {
                LeaveType = "SPL",
                LeaveStatus = "PENDING APPROVAL",
                FromDate = "2025-03-20",
                ToDate = "2025-03-20",
                FromDateSession = "F",
                ToDateSession = "F",
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("FRESH Half CL [ERROR]", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = "FRESH",
                FromDate = "2025-03-20",
                ToDate = "2025-03-20",
                FromDateSession = "F",
                ToDateSession = "F",
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("GRANTED [ERROR]", new ApplyLeaveRequestDto
            {
                LeaveType = "CL",
                LeaveStatus = "GRANTED",
                FromDate = "2025-03-20",
                ToDate = "2025-03-20",
                FromDateSession = "F",
                ToDateSession = "F",
                EmployeeReason = "DUE TO ......."
            });

            yield return SwaggerExample.Create("LWP FRESH", new ApplyLeaveRequestDto
            {
                LeaveType = "LWP",
                LeaveStatus = "FRESH",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO ......."
            });

        }
    }


}
