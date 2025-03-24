using System.ComponentModel;

namespace DEMO.Models.Generic
{
    public class Enums
    {
        public enum LeaveStatusEnum
        {

            [Description("Approval Request")]
            APPROVAL_REQUEST = 2,

            DIRECT = 0,

            [Description("Cancellation Request")]
            CANCELLATION_REQUEST = 5,

            [Description("Already Rejected")]
            ALREADY_REJECTED = 4,

            [Description("Already Approved")]
            ALREADY_APPROVED = 1

            

        }
        public enum LeaveAppDetailEnum
        {

            Fresh = 0,
            Leave_Cancelled=1,
            Pending_Approval=2,
            Grant = 3,
            Approval_Reject = 4,
            Cancellation_Rejected=3

        }

       
    }
}
