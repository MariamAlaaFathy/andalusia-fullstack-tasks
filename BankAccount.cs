using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOne
{
    internal class BankAccount
    {
        private decimal _balance;
        public decimal Balance { get { return _balance; } }
        public string Owner { get; }

        public BankAccount(string owner, decimal initialBalance)
        {
            Owner = owner;
            _balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0) {
                Console.WriteLine("Deposit amount cannot be negative.");
                
            } else {
                _balance += amount;
                Console.WriteLine($"{amount} was deposited in your account. Total balance: {_balance}");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0) {
                Console.WriteLine("Withdrawal amount cannot be negative.");
            } else if (amount > _balance) {
                Console.WriteLine("Insufficient funds.");
            } else {
                _balance -= amount;
                Console.WriteLine($"{amount} was withdrawn from your account. Total balance: {_balance}");
            }
        }

        public virtual string GetAccountType()
        {
            return "Standard";
        }
    }
}
