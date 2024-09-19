using System.Security.Principal;

namespace BankAccount
{
    internal class Program
    {
        // Declaring variable in the class field so it can be used over several methods
        private static BankAccount account;
        static void Start()
        {
            // Welcome screen with instructions
            Console.WriteLine("================================================");
            Console.WriteLine("========== SUPERIOR TRANSACTIONS BANK ==========");
            Console.WriteLine("================================================");

            Console.WriteLine("\nWelcome to our bank.");
            Console.WriteLine("Enter your name below to generate a new account.");

            // User enters name
            Console.Write("\nName: ");
            string? accountHolder = Console.ReadLine();

            // Generate random account number
            Random random = new Random();
            int accountNumber = random.Next(100000000, 999999999);

            // Initiate 0 balance for new account
            decimal balance = 0;

            // Creates a new account based on previous data
            account = new BankAccount(accountNumber, accountHolder, balance);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYou successfully created a new account.");
            Console.ResetColor();

            Console.WriteLine("\nAccount info:");
            Console.WriteLine($"\nName: {account.AccountHolder}\nAccount number: {account.AccountNumber}\nBalance: {account.Balance} kr");
        }
        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nPlease make a selection from the menu below.");

                Console.WriteLine("\n[1] Deposit");
                Console.WriteLine("[2] Withdraw");
                Console.WriteLine("[3] Check Balance");

                Console.WriteLine("\n[4] Exit Application");

                Console.Write("\nYour choice: ");

                switch (Console.ReadLine())
                {
                    case "1": Deposit(); break;
                    case "2": Withdraw(); break;
                    case "3": account.DisplayBalance(); break;
                    case "4": /* Avsluta programmet naturligt */
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou have to enter a valid number..");
                        Console.ResetColor();
                        break;
                }
            } 
        }
        static void Deposit()
        {
            bool correctInput = false;

            do
            {
                Console.Write("\nDeposit ammount: ");

                // Checking correct input
                if (decimal.TryParse(Console.ReadLine(), out decimal deposit) && deposit > 0)
                {
                    correctInput = true;
                    account.Deposit(deposit); // Sends a valid deposit amount as an argument to method within BankAccount class
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have to enter a positive number..");
                    Console.ResetColor();
                }

            } while (!correctInput);
        }
        static void Withdraw()
        {
            bool correctInput;

            do
            {
                Console.Write("\nWithdraw ammount: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal withdraw) && withdraw > 0)
                {
                    correctInput = true;
                    account.Withdraw(withdraw);
                }
                else
                {
                    correctInput = false;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have to enter a positive number..");
                    Console.ResetColor();
                }

            } while (!correctInput);
        }
        static void Main(string[] args)
        {
            Start();
            Menu();
        }
    }
}
