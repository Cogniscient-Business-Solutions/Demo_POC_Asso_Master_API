using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DEMO.Models.BusinessDL.Interfaces
{
    public interface ILeaveService
    {
        Task<IActionResult> ApplyLeaveDetailAsync(Hashtable ht);

        Task<IActionResult> GetLeaveAppDetailAsync(Hashtable ht);

        Task<IActionResult> GetLeaveStatusDetailAsync(Hashtable ht);

        Task<IActionResult> GetEmployeeLeaveDetailsAsync(Hashtable ht);


    }
}
