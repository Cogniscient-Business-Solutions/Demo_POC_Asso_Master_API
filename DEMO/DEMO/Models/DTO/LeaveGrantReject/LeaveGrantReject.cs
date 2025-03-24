namespace DEMO.Models.DTO.LeaveGrantReject
{
    public class LeaveGrantReject
    {
        public class LeaveGrantRejectRequest
        {
            public string UserId { get; set; }
            public string LeaveType { get; set; }
            public string LeaveStatus { get; set; }
            public int LeaveTransactionNo { get; set; }
            public string ApprovalReason { get; set; }
        }

        public class LeaveRequestDto
        {
            public List<LeaveGrantRejectRequest> Leaves { get; set; }
        }

    }


}
