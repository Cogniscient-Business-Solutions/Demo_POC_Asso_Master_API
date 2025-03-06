namespace DEMO.Models.DTO.LeaveAppDetail
{
    public class LeaveAppDetail
    {
            public string LeaveType { get; set; }
            public int NoOfDays { get; set; }
            public string LeaveStatus { get; set; }
            public int LeaveTransactionNo { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string FromDateSession { get; set; }
            public string ToDateSession { get; set; }
            public string EmployeeReason { get; set; }
            public string LeaveApplicationDate { get; set; }
            public string ApprovalDate { get; set; } 
            public string ApprovalReason { get; set; } 
        }


    public class LeaveResponseDto
    {
        public List<LeaveAppDetail> Leaves { get; set; }
    }

    public class GetLeaveRequestDto
    {
        
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

}
