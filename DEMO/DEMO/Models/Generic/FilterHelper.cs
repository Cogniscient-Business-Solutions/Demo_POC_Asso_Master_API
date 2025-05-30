using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Attributes;
using static DEMO.Models.Generic.Enums;

namespace DEMO.Models.Generic
{
    public static class FilterHelper
    {
        public static string GetLeaveStatusName(object statusObj)
        {
            if (statusObj == DBNull.Value || statusObj == null)
                return "UNKNOWN";

            if (int.TryParse(statusObj.ToString(), out int statusValue) &&
                Enum.IsDefined(typeof(LeaveAppDetailEnum), statusValue))
            {
                return Enum.GetName(typeof(LeaveAppDetailEnum), statusValue);
            }

            return "UNKNOWN";
        }

        //public static int convertstatustonumber2(LeaveStatusEnum status)
        //{

        //    LeaveStatusEnum enm = (LeaveStatusEnum)enum.parse(typeof(LeaveStatusEnum), status.tostring());
        //    return (int) enm;

        //}

        //public static int ConvertEnumToNumber<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        //{
        //    return Convert.ToInt32(enumValue);
        //}

        public static string ConvertStatusToValue<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            // Get the enum type
            Type type = typeof(TEnum);
            FieldInfo field = type.GetField(enumValue.ToString());

            // Check if the enum has a [Description] attribute
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            if (attribute != null)
            {
                return attribute.Description; // Return the description string
            }

            return Convert.ToInt32(enumValue).ToString(); // Return the explicit integer value
        }


        public static TEnum ConvertValueToStatus<TEnum>(string value) where TEnum : struct, Enum
        {
            // Get the enum type
            Type type = typeof(TEnum);

            // Try to find an enum field with the given description
            foreach (var field in type.GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == value)
                {
                    return (TEnum)field.GetValue(null); // Return the enum value that matches the description
                }
            }


            // If no description matches, try parsing it as an integer value
            if (int.TryParse(value, out int intValue))
            {
                if (Enum.IsDefined(typeof(TEnum), intValue))
                {
                    return (TEnum)(object)intValue; // Convert integer to enum
                }
            }

            throw new ArgumentException($"Invalid value '{value}' for enum {typeof(TEnum).Name}");
        }

        //public static TEnum ConvertNumberToEnum<TEnum>(int number) where TEnum : struct, Enum
        //{
        //    if (Enum.IsDefined(typeof(TEnum), number))
        //    {
        //        return (TEnum)Enum.ToObject(typeof(TEnum), number);
        //    }
        //    throw new ArgumentException($"Invalid value '{number}' for enum type {typeof(TEnum).Name}");
        //}

        //public static string ConvertNumberToStatus(int statusNumber)
        //{
        //    if (Enum.IsDefined(typeof(LeaveStatusEnum), statusNumber))
        //    {
        //        return ((LeaveStatusEnum)statusNumber).ToString();
        //    }
        //    return "UNKNOWN_STATUS"; 
        //}



        public static readonly Dictionary<string, string> LeaveStatusMapping = new Dictionary<string, string>
        {
         
         { "LEAVE_CANCELLED", "1" },
         { "CANCELLATION_REJECTED", "2" },
         { "GRANTED", "3" },
         { "APPROVAL_REJECTED", "4" },
        };

        public static readonly Dictionary<string, string> LeaveActionMapping = new Dictionary<string, string>
        {

         { "MODIFY", "0" },
         { "CANCEL", "1" },
         { "AUTHORIZE", "2" },
         
        };


        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute?.Description ?? value.ToString();
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
