using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Models.BankAccounts;
using ValidationResult = ABC_Banking.Core.Validation.ValidationResult;

namespace ABC_Banking.Core
{
    public class CustomerAccountManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<BankAccount> GetCustomerAccount(string accountNumber, string sortCode)
        {
            //Find the bank account
            BankAccount bankAccount = await GetBankAccount(accountNumber, sortCode);

            return bankAccount;
        }

        /// <summary>
        /// Method will check the balance of an account against a desired minimum amount
        /// and will add an error only if the desired amount exceeds the account balance.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="sortCode"></param>
        /// <param name="desiredMoney"></param>
        /// <returns></returns>
        public async Task<ValidationResult> AccountHasFunds(string accountNumber, string sortCode,
            decimal desiredMoney)
        {
            ValidationResult vResult = new ValidationResult();

            try
            {
                BankAccount bankAccount = await GetBankAccount(accountNumber, sortCode);

                if (bankAccount == null)
                {
                    vResult.AddException(new KeyNotFoundException("Bank Account not be found"));
                    return vResult;
                }

                //Check balance
                var fundsAvailable = bankAccount.Balance >= desiredMoney;

                //Add an error if funds are insufficient
                if (fundsAvailable == false)
                {
                    vResult.AddError("Insufficient funds");
                }

                return vResult;
            }
            catch (Exception ex)
            {
                //Log Error
                vResult.AddException(ex);
                return vResult;
            }
        }

        /// <summary>
        /// Searches the database to try and find a customer with matching details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<bool> CustomerAlreadyExist(Customer customer)
        {
            Customer c = await _unitOfWork.CustomerRepository.GetFirst(x => x.FirstName == customer.FirstName
                                                                            && x.Surname == customer.Surname
                                                                            && x.AddressLine1 == customer.AddressLine1
                                                                            && x.City == customer.City
                                                                            && x.County == customer.County
                                                                            && x.PostCode == customer.PostCode);

            //Return true if exists
            return c != null;
        }

        /// <summary>
        /// Inserts a new customer in to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<ValidationResult> CreateCustomer(Customer customer)
        {
            ValidationResult vResult = new ValidationResult();

            try
            {
                _unitOfWork.CustomerRepository.Insert(customer);
                await _unitOfWork.SaveAsync();
                return vResult;
            }
            catch (ValidationException vex)
            {
                vResult.AddException(vex);
                return vResult;
            }
            catch (Exception ex)
            {
                vResult.AddError("An error occurred");
                return vResult;
            }
        }

        /// <summary>
        /// Retrieves every customer from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = await _unitOfWork.CustomerRepository.Get();

                return customers?.ToList();
            }
            catch (Exception ex)
            {
                //Log error
                throw;
            }
        }

        /// <summary>
        /// Retrieves a single customer using the ID primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerDetails(Guid id)
        {
            Customer customer = await _unitOfWork.CustomerRepository.GetFirst(x => x.Id == id);
            return customer;
        }

        private async Task<BankAccount> GetBankAccount(string accountNumber, string sortCode)
        {
            try
            {
                 return await _unitOfWork.BankAccountRepository.GetFirst(
                                    x => x.AccountNumber == accountNumber &&
                                         x.SortCode == sortCode);
            }
            catch (Exception ex)
            {
                //Log Error
                return null;
            }
           
        }

        private bool disposed = false;

        /// <summary>
        /// Handle the disposal of the unit of work obj
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
