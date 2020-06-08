using System;
using System.Collections.Generic;

namespace Insurance.Models
{
    public partial class CoveragePlan
    {
        public CoveragePlan()
        {
            Contracts = new HashSet<Contracts>();
        }

        public string CoveragePlan1 { get; set; }
        public DateTime EligibilityDateFrom { get; set; }
        public DateTime EligibilityDateTo { get; set; }
        public string EligibilityCountry { get; set; }

        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
