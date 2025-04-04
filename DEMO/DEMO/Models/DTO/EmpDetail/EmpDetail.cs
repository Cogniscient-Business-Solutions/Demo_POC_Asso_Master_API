namespace DEMO.Models.DTO.EmpDetail
{
    public class EmpDetail
    {
        public string Name { get; set; }
        public string Asso_Code { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }

        public string Email { get; set; }
        public string Mobile_No { get; set; }

        //public UserPicture UserPictureId { get; set; } = new UserPicture();

        public PersonDetail ReportingPerson { get; set; }
        public PersonDetail Manager { get; set; }


    }

    public class PersonDetail
    {
        public string Name { get; set; }
        public string Asso_Code { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }

        public string Status { get; set; }

        //public UserPicture UserPictureId { get; set; } = new UserPicture();


    }

    public class EmployeeRequestModel
    {
        public int EmployeeId { get; set; } // Auto-assigned
        public string ASSO_CODE { get; set; }
        public string COMPANY_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public string EMP_NAME { get; set; }
        public string EMP_EMAIL { get; set; }
        public string EMP_PHONE { get; set; }
    }

    //public class UserPicture
    //{
    //    public string FileId { get; set; } = "pictureuniqueId";
    //    public string FileType { get; set; } = ".jpg";
    //    public string FileName { get; set; } = "somename.jpg";
    //}

    //public class EmpMsg
    //{
    //    public string ErrorMsg { get; set; }
    //    public bool Success { get; set; }
    //}

    //public class GetEmpData
    //{

    //    public EmpMsg EmpMessage { get; set; }
    //    public List<EmpDetail> EDetails { get; set; } = new List<EmpDetail>();

    //}


}
