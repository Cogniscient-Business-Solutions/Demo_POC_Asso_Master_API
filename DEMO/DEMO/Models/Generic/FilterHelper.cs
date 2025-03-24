using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Attributes;
using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.Generic
{
    public static class FilterHelper
    {

        //public static int convertstatustonumber2(LeaveStatusEnum status)
        //{

        //    LeaveStatusEnum enm = (LeaveStatusEnum)enum.parse(typeof(LeaveStatusEnum), status.tostring());
        //    return (int) enm;

        //}

        public static int ConvertEnumToNumber<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            return Convert.ToInt32(enumValue);
        }

        public static TEnum ConvertNumberToEnum<TEnum>(int number) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), number))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), number);
            }
            throw new ArgumentException($"Invalid value '{number}' for enum type {typeof(TEnum).Name}");
        }

        public static int ConvertStatusToNumber(LeaveStatusEnum status)
        {
            LeaveStatusEnum enm = (LeaveStatusEnum)Enum.Parse(typeof(LeaveStatusEnum), status.ToString());
            return (int)enm;
        }

        //public static List<int> ConvertStatusToNumber(List<LeaveStatusEnum> statuses)
        //{
        //    List<int> statusNumbers = new List<int>();

        //    foreach (var status in statuses)
        //    {
        //        statusNumbers.Add((int)status);
        //    }

        //    return statusNumbers;
        //}

        public static List<int> ConvertLeaveStatusToNumber(List<LeaveAppDetailEnum> statuses)
        {
            List<int> statusNumbers = new List<int>();

            foreach (var status in statuses)
            {
                statusNumbers.Add((int)status);
            }

            return statusNumbers;
        }



        //public static List<string> ConvertNumberToLeaveStatus(List<int> statusNumbers)
        //{
        //    List<string> statusStrings = new List<string>();

        //    foreach (var number in statusNumbers)
        //    {
        //        if (Enum.IsDefined(typeof(LeaveAppDetailEnum), number))
        //        {
        //            statusStrings.Add(((LeaveAppDetailEnum)number).ToString());
        //        }
        //        else
        //        {
        //            statusStrings.Add("InvalidStatus"); 
        //        }
        //    }

        //    return statusStrings;
        //}

        public static string ConvertNumberToStatus(int statusNumber)
        {
            if (Enum.IsDefined(typeof(LeaveStatusEnum), statusNumber))
            {
                return ((LeaveStatusEnum)statusNumber).ToString();
            }
            return "UNKNOWN_STATUS"; 
        }

    }

    //public static class EnumHelper
    //{
    //    public static string GetDescription(Enum value)
    //    {
    //        FieldInfo field = value.GetType().GetField(value.ToString());
    //        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

    //        return attribute != null ? attribute.Description : value.ToString();
    //    }
    //}
}
