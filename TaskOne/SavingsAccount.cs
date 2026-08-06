using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOne
{
    internal class SavingsAccount : BankAccount
    {
        public decimal InterestRate { get; }

        public SavingsAccount(string owner, decimal initialBalance, decimal interestRate)
            : base(owner, initialBalance)
        {
            InterestRate = interestRate;
        }

        public virtual void ApplyInterest()
        {
            decimal interest = Balance * InterestRate;
            Console.WriteLine($"Interest of {interest} applied.");
            Deposit(interest);
        }

        public override string GetAccountType()
        {
            return "Savings";
        }
    }
}
