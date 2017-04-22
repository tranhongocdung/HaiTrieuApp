using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWeb.AppDataLayer
{
    public class FilterParams
    {
        public string Keyword { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        
        //For CarTracking
        public DateTime? RunDate { get; set; }
        public int DriverId { get; set; }
        public int CarId { get; set; }
        public int TripType { get; set; }

        //For Employee
        public int WorkPos { get; set; }

        //For Pagination
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortField { get; set; }
        public bool SortASC { get; set; }

        public FilterParams()
        {
            Keyword = "";
            Month = 0;
            Year = 0;
            DriverId = 0;
            CarId = 0;
            PageSize = 10;
            PageNumber = 0;
            SortField = "Id";
            SortASC = false;
            TripType = 0;
            WorkPos = 0;
        }
    }
}