using DEMO.Models.DTO.EmpDetail;

namespace DEMO.Models.DTO.OrgChartDetails
{
    public class OrgChartResponse
    {
        public string Status { get; set; } = "SUCCESS";
        public OrgChartData Data { get; set; }
        public ErrorDetails Error { get; set; }
        public bool Success { get; internal set; }
    }

    public class OrgChartData
    {
        public SelectedUserDetails SelectedUser { get; set; }
        public List<EmployeeDetails> Reportees { get; set; }
        public List<ManagerDetails> Managers { get; set; }
    }

    public class SelectedUserDetails
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }

        //public UserPicture UserPictureId { get; set; } = new UserPicture();
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
    }
    public class EmployeeDetails
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Reportees { get; set; }
        //public UserPicture UserPictureId { get; set; } = new UserPicture();
    }

    //public class UserPicture
    //{
    //    public string FileId { get; set; } = "pictureuniqueId";
    //    public string FileType { get; set; } = ".jpg";
    //    public string FileName { get; set; } = "somename.jpg";
    //}

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
