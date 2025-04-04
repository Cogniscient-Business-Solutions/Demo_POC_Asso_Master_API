using DEMO.Models.DTO.EmpDetail;

namespace DEMO.Models.DTO.OrgChartDetails
{
  

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

}
