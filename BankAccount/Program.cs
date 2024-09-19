namespace BankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("123456789", "John Doe", 1000);

            account.Deposit(500);
            account.Withdraw(2000);
            //account.DisplayBalance();
        }
    }
}
