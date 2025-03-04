﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;
using DEMO.Models.DTO.OrgChartDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DEMO.Models.BusinessDL
{
    public class OrgChartServices
    {
        private readonly IData _dataLayer;
        private readonly string _connectionString;
        private readonly ILogger<OrgChartServices> _logger;

        public OrgChartServices(IData dataLayer, IConfiguration configuration, ILogger<OrgChartServices> logger)
        {
            _dataLayer = dataLayer;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<IActionResult> GetEmpDetailAsync(Hashtable parameters)
        {
            try
            {
                // Fetch data from stored procedure
                DataSet ds = await _dataLayer.GetDataSetAsync("CBS_HR_Organisation_Chart_RTR", parameters);

                // Ensure the dataset contains at least 3 tables
                if (ds.Tables.Count < 3 || ds.Tables[0].Rows.Count == 0)
                {
                    return ApiResponseHelper.ErrorResponse(
                        "USER_NOT_FOUND",
                        "The employee ID provided does not exist in the system.",
                        "Please check the employee ID and try again."
                    );
                }

                // Extract tables
                DataTable empTable = ds.Tables[0];      
                DataTable reporteeTable = ds.Tables[1]; 
                DataTable managerTable = ds.Tables[2];  

                // Create response object
                var responseData = new OrgChartData
                {
                    SelectedUser = empTable.Rows.Count > 0 ? new EmployeeDetails
                    {
                        UserName = empTable.Rows[0]["Name"].ToString().Trim(),
                        UserId = empTable.Rows[0]["Asso_Code"].ToString().Trim(),
                        Designation = empTable.Rows[0]["Designation"].ToString().Trim(),
                        Department = empTable.Rows[0]["Department"].ToString().Trim(),
                        Status = empTable.Rows[0]["Status"].ToString().Trim()
                    } : new EmployeeDetails(),

                    Reportees = reporteeTable.AsEnumerable().Select(row => new EmployeeDetails
                    {
                        UserName = row["Name"].ToString().Trim(),
                        UserId = row["Asso_Code"].ToString().Trim(),
                        Designation = row["Designation"].ToString().Trim(),
                        Department = row["Department"].ToString().Trim(),
                        Status = row["Status"].ToString().Trim()
                    }).ToList(),

                    Managers = managerTable.AsEnumerable().Select((row, index) => new ManagerDetails
                    {
                        UserName = row["Name"].ToString().Trim(),
                        UserId = row["Asso_Code"].ToString().Trim(),
                        Designation = row["Designation"].ToString().Trim(),
                        Department = row["Department"].ToString().Trim(),
                        Status = row["Status"].ToString().Trim(),
                        Level = index + 1
                    }).ToList()
                };

                return ApiResponseHelper.SuccessResponse(responseData);
            }
            catch (Exception ex)
            {

                return ApiResponseHelper.ErrorResponse(
                    "INTERNAL_ERROR",
                    "An error occurred while fetching data.",
                    ex.Message
                );
            }
        }


    }
}
