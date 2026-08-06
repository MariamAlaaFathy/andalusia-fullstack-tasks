# BankAccount Class Hierarchy

This task is part of my **Full Stack Development (.NET + React) — ITIDA Training to Hire** coursework by **Andalusia Academy**.

## Objective

Build a `BankAccount` class hierarchy that exercises **encapsulation**, **properties**, **inheritance**, and **method overriding**.

## Requirements

- `BankAccount` with:
  - a `private decimal _balance` field
  - a read-only `Balance` property
  - an `Owner` string
  - `Deposit` / `Withdraw` methods that validate their arguments
- A `virtual string GetAccountType()` method that returns `"Standard"`
- `SavingsAccount : BankAccount`:
  - adds `InterestRate` (decimal) and `ApplyInterest()`
  - overrides `GetAccountType()` to return `"Savings"`
- A `BankAccount[]` storing one `BankAccount` and one `SavingsAccount`, printing `GetAccountType()` and `Balance` for each
- A demonstration (in a comment) that setting `Balance` directly from outside the class does not compile

### Bonus

`PremiumSavingsAccount : SavingsAccount` that doubles the interest rate on each `ApplyInterest()` call and overrides `GetAccountType()` to return `"Premium Savings"`.

## Concepts Practiced

- Encapsulation with private fields and read-only properties
- Constructor validation
- Inheritance and `virtual` / `override` method polymorphism
- Working with arrays of a base type holding derived-type instances
