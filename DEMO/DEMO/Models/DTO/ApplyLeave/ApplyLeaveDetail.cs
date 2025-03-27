using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.DTO.ApplyLeave
{
    public class ApplyLeaveDetail
    {
        public int LeaveTransactionNo { get; set; }

    }

    public class ApplyLeaveResponseDto
    {
        public List<ApplyLeaveDetail> Leaves { get; set; }
    }
    public class ApplyLeaveRequestDto
    {

        public string LeaveType { get; set; }
        public LeaveAppDetailEnum LeaveStatus { get; set; }       
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public SessionEnum FromDateSession { get; set; }
        public SessionEnum ToDateSession { get; set; }
        public string EmployeeReason { get; set; }

    }

}



