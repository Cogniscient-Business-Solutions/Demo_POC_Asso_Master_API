﻿namespace DEMO.Models.DTO.UserLogin
{
    public class UserLoginInfo
    {
        public class LoginRequest
        {
            public string LOGIN_NAME { get; set; } 
            public string PASSWORD { get; set; } 
        }

        public class User
        {


            public string ASSO_CODE { get; set; }
            public string Company { get; set; }
            public string Location { get; set; }
            public string User_Id { get; set; }
            public string Role { get; set; }

        }

        public class LoginResponseDto
        {
            public string Status { get; set; }
            public string Token { get; set; }
        }


    }
}




