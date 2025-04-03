

namespace DEMO.Models.DTO.LeaveAuthorizeCancel
{
    public class LeaveRequest
    {
        public string LeaveType { get; set; }
        public string Action { get; set; } // MODIFY, CANCEL, AUTHORIZE
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string FromDateSession { get; set; } // Whole Day, Half Day
        public string ToDateSession { get; set; } // Whole Day, Half Day
        public string EmployeeReason { get; set; }
        public int LeaveTransactionNo { get; set; }
    }

    public class LeaveData
    {
        public int LeaveTransactionNo { get; set; }
    }

    public class LeaveAuthorizeResponseDto
    {
        public List<LeaveData> Leaves { get; set; }
    }
}
