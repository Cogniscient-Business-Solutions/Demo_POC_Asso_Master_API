namespace DEMO.Models.DTO.EmpDetail
{
    public class GetEmpData
    {

        public EmpMsg EmpMessage { get; set; }
        public List<EmpDetail> EDetails { get; set; } = new List<EmpDetail>();
    }
}
