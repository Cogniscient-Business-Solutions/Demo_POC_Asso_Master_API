using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.Generic
{
    public static class FilterHelper
    {

        //public static int convertstatustonumber(leavestatusenum status)
        //{

        //    leavestatusenum enm = (leavestatusenum)enum.parse(typeof(leavestatusenum), status.tostring());
        //    return (int)enm;

        //}

        public static List<int> ConvertStatusToNumber(List<LeaveStatusEnum> statuses)
        {
            List<int> statusNumbers = new List<int>();

            foreach (var status in statuses)
            {
                statusNumbers.Add((int)status);
            }

            return statusNumbers;
        }

        public static List<int> ConvertLeaveStatusToNumber(List<LeaveAppDetailEnum> statuses)
        {
            List<int> statusNumbers = new List<int>();

            foreach (var status in statuses)
            {
                statusNumbers.Add((int)status);
            }

            return statusNumbers;
        }


        //public static int ConvertStatus1ToNumber(LeaveAppDetailEnum status)
        //{
        //    LeaveAppDetailEnum enm = (LeaveAppDetailEnum)Enum.Parse(typeof(LeaveAppDetailEnum), status.ToString());
        //    return (int)enm;
        //}

        public static string ConvertNumberToStatus(int statusNumber)
        {
            if (Enum.IsDefined(typeof(LeaveStatusEnum), statusNumber))
            {
                return ((LeaveStatusEnum)statusNumber).ToString();
            }
            return "UNKNOWN_STATUS"; // Default if the status is not defined
        }

        //public static List<string> ConvertNumberToStatus(List<int> statusNumbers)
        //{
        //    List<string> statusNames = new List<string>();

        //    foreach (var num in statusNumbers)
        //    {
        //        if (Enum.IsDefined(typeof(LeaveStatusEnum), num))
        //        {
        //            statusNames.Add(((LeaveStatusEnum)num).ToString());
        //        }
        //        else
        //        {
        //            statusNames.Add("UNKNOWN_STATUS"); 
        //        }
        //    }

        //    return statusNames;
        //}










    }
}
