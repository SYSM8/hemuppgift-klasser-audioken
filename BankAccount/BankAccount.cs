using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class BankAccount
    {
        //Lägg till Egenskaper (fields)
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }

        //Lägg till Konstruktor
        public BankAccount(string accNumber, string accHolder, decimal balance)
        {
            AccountNumber = accNumber;
            AccountHolder = accHolder;
            Balance = balance;
        }

        //Lägg till Metoder
        public void Deposit(decimal deposit)
        {
            Balance += deposit;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDu har satt in: {deposit} kr!");
            Console.WriteLine($"Ditt nya saldo är: {Balance} kr.");
            Console.ResetColor();

        }
        public void Withdraw(decimal withdraw)
        {
            if (withdraw > Balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nDu har för lite pengar på kontot!");
                Console.WriteLine($"Du kan som mest ta ut: {Balance} kr.");
            }
            else
            {
                Balance -= withdraw;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDu har tagit ut: {withdraw} kr!");
                Console.WriteLine($"Ditt nya saldo är: {Balance} kr.");
            }

            Console.ResetColor();
        }

        public void DisplayBalance()
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"\nDitt saldo är: {Balance} kr");
            Console.ResetColor();
        }

        //Lycka till! :)
    }
}
