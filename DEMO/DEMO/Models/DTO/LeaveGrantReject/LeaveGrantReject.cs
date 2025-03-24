using System.Text.Json.Serialization;
using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.DTO.LeaveGrantReject
{
    
    public class DateRangeDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class LeaveRequestDto
    {
       
        public DateRangeDto DateRange { get; set; } = new DateRangeDto();

        public List<LeaveStatusEnum> Status { get; set; } = new List<LeaveStatusEnum>(); 

    }

    public class LeaveDto
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
        public string Status { get; set; } // open, completed, disabled
        public int? DueDays { get; set; }
    }
    public class EmployeeDto
    {
        //public string ASSO_CODE { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
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

