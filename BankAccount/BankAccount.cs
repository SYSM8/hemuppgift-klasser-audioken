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
        }

        //Lycka till! :)
    }
}
