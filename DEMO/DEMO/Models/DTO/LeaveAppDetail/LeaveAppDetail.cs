namespace DEMO.Models.DTO.LeaveAppDetail
{
    public class LeaveAppDetail
    {
            public string LeaveType { get; set; }
            public int NoOfDays { get; set; }
            public string LeaveStatus { get; set; }
            public int LeaveTransactionNo { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string FromDateSession { get; set; }
            public string ToDateSession { get; set; }
            public string EmployeeReason { get; set; }
            public string LeaveApplicationDate { get; set; }
            public string ApprovalDate { get; set; } 
            public string ApprovalReason { get; set; } 
        }
    public class LeaveResponseDto
    {
        public List<LeaveAppDetail> Leaves { get; set; }
    }
    public class GetLeaveRequestDto
    {
        
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public List<string>? LeaveStatus { get; set; }
    }

    public static class StatusHelper
    {
        private static readonly Dictionary<string, int> StatusMapping = new()
    {
        { "FRESH", 0 },
        { "LEAVE CANCELLED", 1 },
        { "PENDING APPROVAL", 2 },
        { "GRANTED", 3 },
        { "APPROVAL REJECTED", 4 },
        { "PENDING CANCELLATION", 5 },
        { "CANCELLATION REJECTED", 6 }
    };

        private static readonly Dictionary<int, string> ReverseStatusMapping = StatusMapping.ToDictionary(kv => kv.Value, kv => kv.Key);

        public static object ConvertStatus(object input)
        {
            if (input is string status && StatusMapping.TryGetValue(status.ToUpper(), out int numericValue))
            {
                return numericValue;
            }
            else if (input is int numericStatus && ReverseStatusMapping.TryGetValue(numericStatus, out string statusValue))
            {
                return statusValue;
            }

            return null;
        }


        public static List<int> ConvertStatusList(IEnumerable<object> inputs)
        {
            return inputs.Select(input => ConvertStatus(input))
                         .Where(result => result is int)
                         .Cast<int>()
                         .ToList();
        }
    }



}
