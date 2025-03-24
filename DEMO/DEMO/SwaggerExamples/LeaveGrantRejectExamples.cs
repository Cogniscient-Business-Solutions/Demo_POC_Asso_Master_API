using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using static DEMO.Models.DTO.LeaveGrantReject.LeaveGrantReject;

public class LeaveGrantRejectExamples : IMultipleExamplesProvider<LeaveGrantRejectRequest>
{
    public IEnumerable<SwaggerExample<LeaveGrantRejectRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("GRANTED", new LeaveGrantRejectRequest
        {
            UserId = "EMP001",
            LeaveType = "CL",
            LeaveStatus = "GRANTED",
            LeaveTransactionNo = 234,
            ApprovalReason = "I am approving because ..."
        });

        yield return SwaggerExample.Create("APPROVAL REJECTED", new LeaveGrantRejectRequest
        {
            UserId = "EMP002",
            LeaveType = "CL",
            LeaveStatus = "APPROVAL REJECTED",
            LeaveTransactionNo = 234,
            ApprovalReason = "I am rejecting because ..."
        });

        yield return SwaggerExample.Create("CANCELLATION REJECTED", new LeaveGrantRejectRequest
        {
            UserId = "EMP003",
            LeaveType = "CL",
            LeaveStatus = "CANCELLATION REJECTED",
            LeaveTransactionNo = 234,
            ApprovalReason = "I am rejecting cancellation because ..."
        });

        yield return SwaggerExample.Create("LEAVE CANCELLED", new LeaveGrantRejectRequest
        {
            UserId = "EMP003",
            LeaveType = "CL",
            LeaveStatus = "LEAVE CANCELLED",
            LeaveTransactionNo = 234,
            ApprovalReason = "I am canceling because ..."
        });
    }
}
