using System.Text.Json.Serialization;
using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.DTO.LeaveApproval
{

    public class DateRangeDto
    {
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
    public class LeaveRequestDto
    {

        public DateRangeDto? DateRange { get; set; } = new DateRangeDto();

        public List<LeaveApprovalEnum>? filters { get; set; } 
    }

    public class LeaveDto
    {
        public string LeaveType { get; set; }
        //public int NoOfDays { get; set; }
        public string LeaveStatus { get; set; }
        //public int LeaveTransactionNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromDateSession { get; set; }
        public string ToDateSession { get; set; }
        public string EmployeeReason { get; set; }
        public string LeaveApplicationDate { get; set; }
        //public string ApprovalDate { get; set; }
        public string ApprovalReason { get; set; }
        public string Status { get; set; } 
        public int? DueDays { get; set; }
    }
    public class EmployeeDto
    {

        public string userName { get; set; }
        public string userId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public int OpenLeaves { get; set; }
        public List<LeaveDto> Leaves { get; set; } = new List<LeaveDto>();
    }

    public class LeaveResponseDataDto
    {
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }










}

