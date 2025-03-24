using System.ComponentModel;

namespace DEMO.Models.Generic
{
    public class Enums
    {
        public enum LeaveStatusEnum
        {
            [Description("Approval Request")]
            APPROVAL_REQUEST = 2,

            [Description("Cancellation Request")]
            CANCELLATION_REQUEST = 5,

            [Description("Already Rejected")]
            ALREADY_REJECTED = 4
        }
        public enum LeaveAppDetailEnum
        {
            Fresh = 0,
            Grant = 3,
            Rejected = 1
        }

       
    }
}
