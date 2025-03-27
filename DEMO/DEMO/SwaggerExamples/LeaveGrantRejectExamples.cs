using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using static DEMO.Models.DTO.LeaveGrantReject.LeaveGrantReject;

public class LeaveGrantRejectExamples : IMultipleExamplesProvider<LeaveGrantRejectRequest>
{
    public IEnumerable<SwaggerExample<LeaveGrantRejectRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("LEAVES LIST FOR LEAVE CANCELLED", new LeaveGrantRejectRequest
        {
            Leaves = new List<LeaveDetail>
            {
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "GRANTED",
                    leaveTransactionNo = 4,
                    approvalReason = "Leave granted as per company policy."
                },
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "APPROVAL_REJECTED",
                    leaveTransactionNo = 5,
                    approvalReason = "Rejected due to insufficient leave balance."
                },
                new LeaveDetail
                {
                    userId = "DT125",
                    leaveStatus = "GRANTED",
                    leaveTransactionNo = 13,
                    approvalReason = "Cancellation not permitted after approval."
                },
                new LeaveDetail
                {
                    userId = "DT125",
                    leaveStatus = "LEAVE_CANCELLED",
                    leaveTransactionNo = 12,
                    approvalReason = "Leave cancelled due to project urgency."
                }
            }
        });

        yield return SwaggerExample.Create("LEAVES LIST FOR APPROVAL REJECTED AND GRANTED", new LeaveGrantRejectRequest
        {
            Leaves = new List<LeaveDetail>
            {
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "GRANTED",
                    leaveTransactionNo =4,
                    approvalReason = "Leave granted as per company policy."
                },
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "APPROVAL_REJECTED",
                    leaveTransactionNo = 5,
                    approvalReason = "Rejected due to insufficient leave balance."
                }
            }
        });

        yield return SwaggerExample.Create("LEAVES LIST FOR GRANTED AND LEAVE_CANCELLED", new LeaveGrantRejectRequest
        {
            Leaves = new List<LeaveDetail>
            {
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "GRANTED",
                    leaveTransactionNo = 4,
                    approvalReason = "Leave granted as per company policy."
                },
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "APPROVAL_REJECTED",
                    leaveTransactionNo = 5,
                    approvalReason = "Rejected due to insufficient leave balance."
                },
                new LeaveDetail
                {
                    userId = "DT124",
                    leaveStatus = "LEAVE_CANCELLED",
                    leaveTransactionNo = 4,
                    approvalReason = "Leave cancelled due to project urgency."
                }
            }
        });
    }
}
