using System.ComponentModel;

namespace DEMO.Models.Generic
{
    public class Enums
    {

        public enum LeaveApprovalEnum
        {
            [Description("2")]
            APPROVAL_REQUEST,

            [Description("-1")]
            DIRECT,

            [Description("5")]
            CANCELLATION_REQUEST,

            [Description("4")]
            ALREADY_REJECTED,

            [Description("1,3")]
            ALREADY_APPROVED
        }
        public enum LeaveAppDetailEnum
        {

            [Description("0")]
            FRESH,

            [Description("1")]
            LEAVE_CANCELLED ,

            [Description("2")]
            PENDING_APPROVAL,

            [Description("3")]
            GRANTED ,

            [Description("4")]
            APPROVAL_REJECTED,

            [Description("3")]
            PENDING_CANCELLATION 

        }

        public enum SessionEnum
        {
            [Description("W")]
            WHOLE_DAY ,

            [Description("F")]
            FIRST_HALF ,

            [Description("S")]
            SECOND_HALF 
        }

       
    }
}
