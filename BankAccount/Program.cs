using System.CodeDom.Compiler;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Xml.Serialization;

namespace BankAccount
{
    internal class Program
    {
        // USER ACCOUNT TO BE ACCESSIBLE IN ALL METHODS
        private static BankAccount? account;
        private static BankAccount? currentAccount;

        // LIST TO STORE NEW ACCOUNTS
        private static List<BankAccount> accountList = new List<BankAccount>();

        // USER PROFILE PARAMETERS TO BE ACCESSIBLE IN ALL METHODS
        private static string? accountHolder { get; set; }
        private static int accountNumber { get; set; }
        private static int accountBalance { get; set; }

        // START OF PROGRAM
        static void WelcomeScreen()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("============ SUPERIOR TRANSACTIONS BANK ============");
            Console.WriteLine("====================================================");

            Console.WriteLine("\nWelcome to Superior Transactions Bank. ");
            Console.WriteLine("Create a new account or sign in to an existing one.");
            Console.WriteLine("We hope you find our services useful!");
        }

        // START MENU BEFORE SIGNED IN
        static bool StartMenu()
        {
            bool correctInput = false;
            bool exitApplication = false;

            while (!correctInput)
            {
                Console.WriteLine("\n==================== START MENU ====================");

                Console.WriteLine("\n[1] Sign in to an existing account");
                Console.WriteLine("[2] Create a new account");
                Console.WriteLine("[3] Admin Account List");

                Console.WriteLine("\n[ESC] Exit Application");

                // Reads key input for use in menu
                ConsoleKeyInfo keyInput = Console.ReadKey(true);

                switch (keyInput.Key)
                {
                    case ConsoleKey.D1: SignIn(); correctInput = true; break; // Calls method for sign in process
                    case ConsoleKey.D2: CreateAccount(); correctInput = true; break; // Possibility for user to create a new bank account
                    case ConsoleKey.D3: AdminAccountList(); break; // Admin access to print all exisiting bank accounts
                    case ConsoleKey.Escape: return exitApplication = true;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPress 1, 2, 3 or Escape");
                        Console.ResetColor();
                        break;
                }
            }

            return exitApplication;
        }

        static void SignIn() // FIX THIS METHOD
        {
            Console.WriteLine("Enter your name to sign in..");
            string signInUsername = Console.ReadLine();

            foreach (BankAccount account in accountList)
            {
                if (account.AccountHolder == signInUsername)
                {
                    // Copy found useraccount to temporary account
                    currentAccount = account;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nACCOUNT INFO:");
                    Console.WriteLine($"Name: {account.AccountHolder}\nAccount: {account.AccountNumber}\nBalance: {account.AccountBalance} kr");
                    Console.ResetColor();
                    break;
                }

            }
        }

        static void CreateAccount()
        {
            Console.WriteLine("\n====================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnter your name and surname to generate an account.");
            Console.ResetColor();

            // Individual parameters for user account
            accountHolder = NameInput();
            accountBalance = 0; 
            accountNumber = AccountGenerator(); // Get random account number for user profile

            // Create new bank account for user based on parameters
            account = new BankAccount(accountNumber, accountHolder, accountBalance);

            // Adds account to list to help with "sign in name-matching" and admin printouts
            accountList.Add(account);

            // Copy the account to a temporary one to control which use profile can be edited at sign in and account creation
            currentAccount = account;

            Console.WriteLine("\n====================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYou successfully created a new account!");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nName: {account.AccountHolder}\nAccount: {account.AccountNumber}\nBalance: {account.AccountBalance} kr");
            Console.ResetColor();
        }

        static string NameInput()
        {
            bool correctInput = false;
            string name = " ";
            string surname = " ";

            while (!correctInput)
            {
                Console.Write("\nFirst name: ");
                string? userInput = Console.ReadLine();

                // Convert to correct capitalization and check if input is ok
                (name, correctInput) = CapitalizeAndLower(userInput!); // Nullforgiving sign "!" since nullvalue is controlled in method
            }

            correctInput = false;

            while (!correctInput)
            {
                Console.Write("\nSurname: ");
                string? userInput = Console.ReadLine();

                (surname, correctInput) = CapitalizeAndLower(userInput!);
            }

            return name + " " + surname;
        }

        // ERROR HANDLING AND CONVERTION OF LETTERS
        static (string, bool) CapitalizeAndLower(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || !userInput.All(Char.IsLetter) || userInput.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOnly letters allowed. Minimum 2 characters..");
                Console.ResetColor();

                return (userInput, false);

            }
            else
            {
                return (userInput = Char.ToUpper(userInput[0]) + userInput.Substring(1).ToLower(), true);
            }
        }

        // GENERATES RANDOM ACCOUNT NUMBER FOR NEW ACCOUNTS
        static int AccountGenerator()
        {
            // Generate random account number
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }

        // ADMIN PRINOUT OF ALL ACTIVE BANK ACCOUNTS
        static void AdminAccountList()
        {
            Console.WriteLine("\n====================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nALL ACCOUNTS ({accountList.Count}) AT SUPERIOR TRANSACTIONS BANK:");

            foreach (BankAccount account in accountList)
            {
                Console.WriteLine($"\nName: {account.AccountHolder}\nAccount: {account.AccountNumber}\nBalance: {account.AccountBalance} kr");
            }

            Console.ResetColor ();
        }

        // USER MENU WHEN SIGNED IN
        static bool AccountMenu()
        {
            bool signOut = false;

            while (!signOut)
            {
                Console.WriteLine("\n=================== ACCOUNT MENU ===================");

                // Display who is signed in for clarity
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nSigned in as: {currentAccount.AccountHolder}");
                Console.ResetColor();

                Console.WriteLine("\n[1] Deposit");
                Console.WriteLine("[2] Withdraw");
                Console.WriteLine("[3] Check Balance");

                Console.WriteLine("\n[ESCAPE] Sign Out");

                ConsoleKeyInfo keyInput = Console.ReadKey(true);

                switch (keyInput.Key)
                {
                    case ConsoleKey.D1: Deposit(); break;
                    case ConsoleKey.D2: Withdraw(); break;
                    case ConsoleKey.D3: currentAccount.DisplayBalance(); break;
                    case ConsoleKey.Escape: return signOut = true;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou have to enter a valid number..");
                        Console.ResetColor();
                        break;
                }

                account = currentAccount;
            } 

            return signOut;
        }

        static void Deposit()
        {
            bool correctInput = false;

            do
            {
                Console.Write("\nDeposit ammount: ");

                // Checking correct input
                if (currentAccount != null && decimal.TryParse(Console.ReadLine(), out decimal deposit) && deposit > 0)
                {
                    correctInput = true;
                    currentAccount.Deposit(deposit); // Sends a valid deposit amount as an argument to method within BankAccount class
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

                if (currentAccount != null && decimal.TryParse(Console.ReadLine(), out decimal withdraw) && withdraw > 0)
                {
                    correctInput = true;
                    currentAccount.Withdraw(withdraw);
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

        // MAIN
        static void Main(string[] args)
        {
            // Controlls menu loops
            bool signOut;
            bool exitApplication;

            WelcomeScreen();

            do
            {
                exitApplication = StartMenu();

                if (exitApplication)
                {
                    break;
                }

                signOut = AccountMenu();

            } while (signOut);
        }
    }
}
