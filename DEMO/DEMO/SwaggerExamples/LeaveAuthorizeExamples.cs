using DEMO.Models.DTO.ApplyLeave;
using static DEMO.Models.Generic.Enums;
using Swashbuckle.AspNetCore.Filters;
using DEMO.Models.DTO.LeaveAuthorizeCancel;

namespace DEMO.SwaggerExamples
{
    public class LeaveAuthorizeExamples : IMultipleExamplesProvider<LeaveRequest>
    {
        public IEnumerable<SwaggerExample<LeaveRequest>> GetExamples()
        {
            yield return SwaggerExample.Create("MODIFY LEAVE", new LeaveRequest
            {
                LeaveType = "CL",
                Action = "MODIFY",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO .......",
                LeaveTransactionNo=1


            });

            yield return SwaggerExample.Create("CANCEL LEAVE", new LeaveRequest
            {
                LeaveType = "SPL",
                Action = "CANCEL",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO .......",
                LeaveTransactionNo = 1
            });

            yield return SwaggerExample.Create("AUTHORIZE LEAVE", new LeaveRequest
            {
                LeaveType = "CL",
                Action ="AUTHORIZE",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "F",
                ToDateSession = "F",
                EmployeeReason = "DUE TO .......",
                LeaveTransactionNo = 1
            });

            yield return SwaggerExample.Create("CANCEL LEAVE", new LeaveRequest
            {
                LeaveType = "CL",
                Action = "CANCEL",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO .......",
                LeaveTransactionNo = 1
            });

            yield return SwaggerExample.Create("MODIFY LEAVE", new LeaveRequest
            {
                LeaveType = "CL",
                Action = "MODIFY",
                FromDate = "2025-03-21",
                ToDate = "2025-03-21",
                FromDateSession = "W",
                ToDateSession = "W",
                EmployeeReason = "DUE TO .......",
                LeaveTransactionNo = 1
            });


        }
    }

}
