using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ABC_Banking.Core;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices
{
    public class CustomerServices
    {
        public async Task<ValidationResult> CreateCustomer(CustomerDTO model)
        {
            ValidationResult vResult = new ValidationResult();

            //1. Validate customer values
            //[Validate here]

            //2. Create new customer object
            Customer customer = new Customer
            {
                BankAccounts = new List<BankAccount>(),
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                County = model.County,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                Surname = model.Surname,
                PostCode = model.PostCode
            };

            //3. Check a customer doesn't already exist with those matching details
            bool existing = await CheckForExistingCustomer(customer);

            if (existing)
            {
                vResult.AddError("A customer with this name and address already exists");
                return vResult;
            }

            //4. Pass to 'core' to validate and insert into DB
            bool success = await InsertNewCustomer(customer);

            if (success == false)
            {
                vResult.AddError("Unable to create customer at this time.");
            }

            return vResult;
        }

        private async Task<bool> InsertNewCustomer(Customer customer)
        {
            //Check customer entity is valid

            using (CustomerAccountManager manager = new CustomerAccountManager())
            {
                ValidationResult vResult = await manager.CreateCustomer(customer);

                if (vResult.HasError())
                {
                    //TODO: do something with error
                    return false;
                }

                return true;
            }
        }

        private async Task<bool> CheckForExistingCustomer(Customer customer)
        {
            try
            {
                using (CustomerAccountManager manager = new CustomerAccountManager())
                {
                    bool exists = await manager.CustomerAlreadyExist(customer);

                    return exists;
                }
            }
            catch (Exception ex)
            {
                //LogError
                throw;
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            try
            {
                //1. Simple, just get the customers
                using (CustomerAccountManager manager = new CustomerAccountManager())
                {
                    List<Customer> customers = await manager.GetAllCustomers();
                    return customers;
                }
            }
            catch (Exception ex)
            {
                //LogError
                throw;
            }
        }

        public async Task<Customer> GetCustomerDetails(Guid id)
        {
            try
            {
                if(id == Guid.Empty)
                    return null;

                using (CustomerAccountManager manager = new CustomerAccountManager())
                {
                    Customer customer = await manager.GetCustomerDetails(id);

                    return customer;
                }
            }
            catch (Exception)
            {
                //Log error
                throw;
            }
        }
    }
}