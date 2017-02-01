using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ABC_Banking.Core;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices
{
    public class StatementServices
    {
        public async Task<List<MiniStatementTransaction>> GetMiniStatement(StatementRequest model)
        {
            //1. Load an account Transactions
            List<ITransaction> transactions = await GetAccountTransactions(model);

            if (transactions == null || transactions.Any() == false)
                return null;

            //2. Create Ministatement object
            List<MiniStatementTransaction> miniStatement = new List<MiniStatementTransaction>();

            //Convert the transactions to the mini statement view
            foreach (var t in transactions)
            {
                var miniT = new MiniStatementTransaction
                {
                    DateTime = t.DateRequested,
                    Amount = t.TransactionAmount,
                    Description = t.Description
                }; 

                miniStatement.Add(miniT);
            }

            return miniStatement;
        }

        private async Task<List<ITransaction>> GetAccountTransactions(StatementRequest request)
        {
            using (var manager = new StatementManager())
            {
                var transactions = await manager.GetAccountTransactions(request.AccountNumber, request.SortCode,
                    request.DateFrom, request.DateTo, request.MaxTransactionCount);

                return transactions;
            }
        }

        public async Task<List<FullStatementTransaction>> GetFullStatement(StatementRequest model)
        {
            try
            {
                //1. Load an account Transactions
                List<ITransaction> transactions = await GetAccountTransactions(model);

                if (transactions == null || transactions.Any() == false)
                    return null;

                List<FullStatementTransaction> fullStatement = new List<FullStatementTransaction>();

                //Loop through and identify the type of transaction
                foreach (var t in transactions)
                {
                    var fullTransaction = new FullStatementTransaction
                    {
                        DateTime = t.DateRequested,
                        Description = t.Description,
                    };

                    if (t is DepositTransaction)
                    {
                        fullTransaction.Type = "Deposit";
                        fullTransaction.PaidIn = t.TransactionAmount;
                    }
                    else if (t is WithdrawTransaction)
                    {
                        fullTransaction.Type = "Withdrawal";
                        fullTransaction.PaidOut = t.TransactionAmount;
                    }
                    else if (t is TransferTransaction)
                    {
                        fullTransaction.Type = "Bank Tranfer";
                        fullTransaction.PaidOut = t.TransactionAmount;
                    }
                    else
                    {
                        fullTransaction.Type = "Other";
                    }

                    fullStatement.Add(fullTransaction);
                }

                return fullStatement;
            }
            catch (Exception ex)
            {
                //Log error
                throw;
            }
        }
    }
}