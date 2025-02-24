namespace DEMO.Models.DTO.OrgChartDetails
{
    public class OrgChartResponse
    {
        public string Status { get; set; } = "SUCCESS";
        public OrgChartData Data { get; set; }
        public ErrorDetails Error { get; set; }
    }

    public class OrgChartData
    {
        public EmployeeDetails SelectedUser { get; set; }
        public List<EmployeeDetails> Reportees { get; set; }
        public List<ManagerDetails> Managers { get; set; }
    }

    public class EmployeeDetails
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }

    public class ManagerDetails : EmployeeDetails
    {
        public int Level { get; set; }
    }

    public class ErrorResponse
    {
        public string Status { get; set; } = "FAIL";
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
