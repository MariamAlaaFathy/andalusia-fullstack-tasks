using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOne
{
    internal class PremiumSavingsAccount : SavingsAccount
    {
        public PremiumSavingsAccount(string owner, decimal initialBalance, decimal interestRate)
            : base(owner, initialBalance, interestRate)
        {
        }

        public override void ApplyInterest()
        {
            decimal interest = Balance * (InterestRate * 2);
            Console.WriteLine($"Premium interest of {interest} applied.");
            Deposit(interest);
        }

        public override string GetAccountType()
        {
            return "Premium Savings";
        }
    }
}
