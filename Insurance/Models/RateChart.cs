using System;
using System.Collections.Generic;

namespace Insurance.Models
{
    public partial class RateChart
    {
        public string CoveragePlan { get; set; }
        public string CustomerGender { get; set; }
        public decimal StartAge { get; set; }
        public decimal EndAge { get; set; }
        public decimal NetPrice { get; set; }

        public virtual CoveragePlan CoveragePlanNavigation { get; set; }
    }
}
