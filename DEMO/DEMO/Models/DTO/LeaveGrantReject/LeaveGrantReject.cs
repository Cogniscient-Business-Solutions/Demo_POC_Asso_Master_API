using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.DTO.LeaveGrantReject
{
    public class LeaveGrantReject
    {
        public class LeaveDetail
        {
            public string userId { get; set; }
            public string leaveStatus { get; set; }
            public int leaveTransactionNo { get; set; }
            public string approvalReason { get; set; }
        }

        public class LeaveGrantRejectRequest
        {
            public List<LeaveDetail> Leaves { get; set; }
        }

    }


}
