using System;
using System.Linq;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.BankCards;

namespace ABC_Banking.Core.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            Branch branch = new Branch
            {
                Name = "Clapham Division",
                AddressLine1 = "79 St John's Rd",
                City = "London",
                County = "Greater London",
                PostCode = "SW11 1QZ"
            };

            context.Branches.AddOrUpdate(b => b.Name, branch);

            if (context.Customers.Any() == false)
            {
                Customer customer = new Customer
                {
                    FirstName = "Luke",
                    MiddleName = "",
                    Surname = "Stoward",
                    AddressLine1 = "42 Madeup Road",
                    City = "London",
                    County = "Greater London",
                    PostCode = "MA4 3UP",


                };
                context.Customers.AddOrUpdate(b => b.FirstName, customer);

                context.SaveChanges();

                //BankAccounts
                DailyCurrentAccount accountOne = new DailyCurrentAccount
                {
                    AccountNumber = "12345678",
                    Balance = 100.00m,
                    Customer = customer,
                    DateOpened = DateTime.UtcNow,
                    SortCode = "445566"
                };

                context.DailyCurrentAccounts.AddOrUpdate(x => x.AccountNumber, accountOne);

                RegularSaverAccount accountTwo = new RegularSaverAccount
                {
                    AccountNumber = "11234567",
                    Balance = 100.00m,
                    Customer = customer,
                    DateOpened = DateTime.UtcNow,
                    SortCode = "445566"
                };

                context.RegularSaverAccounts.AddOrUpdate(x => x.AccountNumber, accountTwo);

                context.SaveChanges();

                //BankCard
                DebitCard debitCard = new DebitCard
                {
                    CardNumber = "4485456965871348",
                    CardType = EnumBankCardType.VisaDebit,
                    ExpiryDate = new DateTime(2018, 11, 24),
                    BankAccountId = accountOne.Id,
                    NameOnCard = "Mr L R Stoward",
                    Pin = 1234,
                    ValidFrom = DateTime.UtcNow,
                    AccountNumber = "12345678",
                    SortCode = "445566"
                };

                context.DebitCards.AddOrUpdate(x => x.CardNumber, debitCard);

            }

        }
    }
}
