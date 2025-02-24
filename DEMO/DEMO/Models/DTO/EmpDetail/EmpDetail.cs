namespace DEMO.Models.DTO.EmpDetail
{
    //public class EmpDetail
    //{
    //    public string Asso_Code { get; set; }
    //    public string Department { get; set; }
    //    public string Designation { get; set; }
    //}

    public class EmpDetail
    {
        public string Name { get; set; }
        public string Asso_Code { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        //public string EmailId { get; set; }
        //public string ContactNo { get; set; }
        public PersonDetail reporting_person { get; set; }
        public PersonDetail Manager { get; set; }
    }

    public class PersonDetail
    {
        public string Name { get; set; }
        public string Asso_Code { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }


}
