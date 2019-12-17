using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Modern_Chart_Client.ModelClasses
{
    public partial class SalesTerritory
    {
        public int TerritoryID { get; set; }
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public decimal SalesYTD { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYTD { get; set; }
        public decimal CostLastYear { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }

    public partial class SalesInfo
    {
        public string Name { get; set; }
        public decimal Sales { get; set; }
    }

    public class CountryRegionCode
    {
        public string CountryRegion { get; set; }
    }
}
