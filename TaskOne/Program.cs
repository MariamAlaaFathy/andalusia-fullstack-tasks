using TaskOne;

BankAccount[] bankAccounts = new BankAccount[] {
    new BankAccount("Shahd", 1000),
    new SavingsAccount("Mariam", 2000, 0.05m),
    new PremiumSavingsAccount("Alaa", 3000, 0.05m)
};

foreach (BankAccount bankAccount in bankAccounts)
{
    Console.WriteLine($"Account Owner: {bankAccount.Owner}, Account Type: {bankAccount.GetAccountType()}, Balance: {bankAccount.Balance}");
    bankAccount.Deposit(1000);
    bankAccount.Withdraw(500);
    if (bankAccount is SavingsAccount savingsAccount)
    {
        savingsAccount.ApplyInterest();
    }
    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
}

//bankAccounts[0].Balance = 1500;
//               |
//               V
// This line is commented out because the Balance property has a private setter (read-only),
// so it cannot be modified directly from outside the BankAccount class.