using Insurance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Manager
{
    public class ContractManager
    {
        // create new contract
        public async Task<bool> SaveContract(Contracts newContract)
        {
            newContract = CreateContractDetails(newContract);
            try
            {
                if (newContract != null)
                {
                    _InsuranceDBContext.Contracts.Add(newContract);
                    await _InsuranceDBContext.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // update contract
        public async Task<bool> UpdateContract(Contracts oldContract)
        {
           
            try
            {

                Contracts contract = CreateContractDetails(oldContract);
                if (contract != null)
                {
                    _InsuranceDBContext.Contracts.Update(contract);
                    await _InsuranceDBContext.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete contract
        public async Task<bool> DeleteContract(int contractToDeleteId)
        {
            try
            {
                var contract = _InsuranceDBContext.Contracts;
               var contractToDelete = contract.Where(a => a.ContractId == contractToDeleteId).FirstOrDefault();
                contract.Remove(contractToDelete);

              await  _InsuranceDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Helpers
        private static InsuranceDBContext insuranceDBContext;

        // plan and rate calculation
        private Contracts CreateContractDetails(Contracts newContract)
        {
            try
            {
                CoveragePlan planType;
                var customerAge = GetCustmerAge(newContract);
                if (newContract.CustomerCountry.ToUpper().Equals("USA") || newContract.CustomerCountry.ToUpper().Equals("CAN"))
                    planType = _InsuranceDBContext.CoveragePlan.Where(a => a.EligibilityCountry == newContract.CustomerCountry && a.EligibilityDateFrom <= newContract.SaleDate && a.EligibilityDateTo >= newContract.SaleDate).FirstOrDefault();
                else
                    planType = _InsuranceDBContext.CoveragePlan.Where(a => a.EligibilityCountry.ToUpper().Equals("ALL") && a.EligibilityDateFrom <= newContract.SaleDate && a.EligibilityDateTo >= newContract.SaleDate).FirstOrDefault();

                var rateData = _InsuranceDBContext.RateChart.Where(a => a.StartAge < customerAge && a.EndAge >= customerAge && a.CustomerGender == newContract.CustomerGender && a.CoveragePlan == planType.CoveragePlan1).FirstOrDefault();
                newContract.CoveragePlan = planType.CoveragePlan1;
                newContract.NetPrice = rateData.NetPrice;
                return newContract;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // DB context creation
        public static InsuranceDBContext _InsuranceDBContext 
        {
            get
            {
                if (insuranceDBContext == null)
                {
                    insuranceDBContext= new InsuranceDBContext();
                    return insuranceDBContext;
                }
                else
                    return insuranceDBContext;
            }
        }

        // Age calculation
        private int GetCustmerAge(Contracts newContract)
        {
            var ageSpan = DateTime.Now - newContract.CustomerDateOfBirth;
            return (int)ageSpan.TotalDays / 365;
        }
        #endregion
    }
}
