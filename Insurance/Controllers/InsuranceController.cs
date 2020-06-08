using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Insurance.Models;
using Insurance.Manager;

namespace Insurance.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        // Get list of Contract
        [HttpGet]
        public IEnumerable<Contracts> Get()
        {
            using (var context = new InsuranceDBContext())
            {
                return context.Contracts.ToList();
            }
        }

        // create a new contract
        [HttpPost]
        [Route("/api/insurance/new")]
        public async Task<bool> CreateContract(Contracts newContract)
        {
            try
            {
                var contractManager = new ContractManager();
                return await contractManager.SaveContract(newContract);
            }
            catch (Exception)
            {
                return false;
            }
          
        }

        // update a contract
        [HttpPatch]
        [Route("/api/insurance/update")]
        public async Task<bool> UpdateContract(Contracts oldContract)
        {
            try
            {
                var contractManager = new ContractManager();
                return await contractManager.UpdateContract(oldContract);
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        // Delete a contract
        [HttpDelete]
        [Route("/api/insurance/{contractID}/delete")]
        public async Task<bool> Delete(int contractID)
        {
            try
            {
                var contractManager = new ContractManager();
                return await contractManager.DeleteContract(contractID);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}