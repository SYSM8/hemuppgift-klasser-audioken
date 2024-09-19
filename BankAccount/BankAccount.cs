using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class BankAccount
    {
        // FIELDS
        public int AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }

        // CONSTRUCTOR
        public BankAccount(int accNumber, string accHolder, decimal balance)
        {
            AccountNumber = accNumber;
            AccountHolder = accHolder;
            Balance = balance;
        }

        // METHOD - Deposit funds to account
        public void Deposit(decimal deposit)
        {
            Balance += deposit;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nYou have deposited: {deposit} kr!");
            Console.WriteLine($"Your balance: {Balance} kr.");
            Console.ResetColor();

        }

        // METHOD - Withdraw funds if available on account
        public void Withdraw(decimal withdraw)
        {
            if (withdraw > Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nYou dont have enought funds!");
                Console.WriteLine($"Your balance: {Balance} kr.");
            }
            else
            {
                Balance -= withdraw;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nYou have withdrawn: {withdraw} kr!");
                Console.WriteLine($"Your balance: {Balance} kr.");
            }

            Console.ResetColor();
        }

        // METHOD - Display balance on account
        public void DisplayBalance()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"Your balance: {Balance} kr.");
            Console.ResetColor();
        }
    }
}
