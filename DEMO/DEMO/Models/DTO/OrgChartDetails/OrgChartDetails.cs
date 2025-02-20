namespace DEMO.Models.DTO.OrgChartDetails
{
    public class OrgChartDetails
    {
        public LogMsg EmpMessage { get; set; }
        public List<OrgChartData> Empdetails { get; set; } = new List<OrgChartData>();
    }
}
