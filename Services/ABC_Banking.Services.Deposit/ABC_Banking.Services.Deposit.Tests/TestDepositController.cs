using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABC_Banking.Services.Deposit.Controllers;
using ABC_Banking.Services.Deposit.Models;

namespace ABC_Banking.Services.Deposit.Tests
{
    [TestClass]
    public class TestDepositController
    {

        [TestMethod]
        public async Task ProcessATMDeposit_AccountNotFound()
        {
            var testDeposit = new DepositRequest
            {
                AccountHolderName = "Mr Luke R Stoward",
                AccountNumber = "10456235",
                DateRequested = DateTime.Now,
                SortCode = "102040",
                TotalCashValue = 50
            };
            
            var controller = new DepositController();

            var result = await controller.ProcessDepositATM(testDeposit);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

    }
}
