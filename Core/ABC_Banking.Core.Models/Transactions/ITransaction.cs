using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models.Transactions
{
    public interface ITransaction
    {
        Guid Id { get; set; }
        DateTime DateRequested { get; set; }
        Guid BankAccountId { get; set; }

        Task<bool> Process();
    }
}
