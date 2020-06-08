using System;
using System.Collections.Generic;

namespace Insurance.Models
{
    public partial class Contracts
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerCountry { get; set; }
        public DateTime CustomerDateOfBirth { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal NetPrice { get; set; }
        public string CoveragePlan { get; set; }
        public int ContractId { get; set; }

        public virtual CoveragePlan CoveragePlanNavigation { get; set; }
    }
}
