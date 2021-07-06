using System;
namespace DIO.Bank
{
    public class BankMenu : Menu
    {
        static  AccountRepository repository = new AccountRepository();
        public BankMenu()
        {
            Headers.Add("Dio.Bank Demo");
            Headers.Add("Developed by Ricardo Medeiros");
            Headers.Add("https://github.com/jjackbauer/DIO.Bank.Transfer");

            Options.Add(setOption("List Accounts", '1', true, FunctionListAccounts));
            Options.Add(setOption("Insert Account", '2', true, FunctionInsertAccount));
            Options.Add(setOption("Transfer", '3', true, FunctionTransfer));
            Options.Add(setOption("Withdraw", '4', true, FunctionWithdraw));
            Options.Add(setOption("Deposit", '5', true, FunctionDeposit));
            Options.Add(setOption("Clear Screen",'C',true,Clear));
        }

        static void FunctionListAccounts()
        {
            Clear();
            if((repository.GetList()).Count == 0 )
            {
                Console.WriteLine("There is no register stored");
                WaitForKey();
                return;
            }
            Console.Write(repository);
            WaitForKey();
        }
        static void FunctionInsertAccount()
        {
            Account newAccount = GetAccountFromInput();
            repository.Insert(newAccount);
            Console.WriteLine("Account added sucessfully");
            Console.WriteLine(newAccount);
            WaitForKey();
        }
        static void FunctionTransfer()
        {
            int indexAccountClient = GetValidAccountIndex("Client");
            int indexAccountDestiny = GetValidAccountIndex("Destiny");
            
            if(indexAccountClient == indexAccountDestiny)
            {
                
                Console.WriteLine("You must transfer to a Account different than yours");
                WaitForKey();
                return;
            }

            Console.Write("Type the value to transfer:");
            double inputValue = double.Parse(Console.ReadLine());
            Clear();

            if(!repository.Get(indexAccountClient).Transfer(inputValue,repository.Get(indexAccountDestiny)))
            {
                Console.WriteLine("Invalid Transfer, please try again later...");
                WaitForKey();
                return;
            }
                
            
            Console.WriteLine("Sucessfull transfer!");
            WaitForKey();
            return;
        }
        static void FunctionWithdraw()
        {
            int indexAccountClient = GetValidAccountIndex("Client");
            
            Console.Write("Type the value to Withdraw:");
            double inputValue = double.Parse(Console.ReadLine());
            Clear();
            
            if(!repository.Get(indexAccountClient).Withdraw(inputValue))
            {   
                
                Console.WriteLine("Invalid Withdraw, please try again later...");
                WaitForKey();
                return;
            }

            Console.WriteLine("Sucessfull Withdraw!");
            WaitForKey();
            return;


        }
        static void FunctionDeposit()
        {
            int indexAccountClient = GetValidAccountIndex("Client");
            
            Console.Write("Type the value to Deposit:");
            double inputValue = double.Parse(Console.ReadLine());
            Clear();
            
            if(!repository.Get(indexAccountClient).Deposit(inputValue))
            {   
                
                Console.WriteLine("Invalid Deposit, please try again later...");
                WaitForKey();
                return;
            }

            Console.WriteLine("Sucessfull Deposit!");
            WaitForKey();
            return;
        }
        static Account GetAccountFromInput()
        {   
            int AccountKindCounter = 0;
            foreach(int i in Enum.GetValues(typeof(AccountKind)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(AccountKind), i));
                AccountKindCounter++;
            }
                
            int inputAccountKind;

            do
            {
                Console.Write("Select the Account Kind from the options above > ");
                inputAccountKind = int.Parse(Console.ReadLine());

                if(inputAccountKind < 1 || inputAccountKind > AccountKindCounter)
                    Console.WriteLine("Invalid Account Kind");

            }while(inputAccountKind < 1 || inputAccountKind > AccountKindCounter);
           

            Console.Write("Type the name of the client >");
            string inputClientName = Console.ReadLine();

            Console.Write("Type the initial balance of the account:");
            double inputBalance = double.Parse(Console.ReadLine());

            Console.Write("Type the initial credit of the account:");
            double inputCredit = double.Parse(Console.ReadLine());
            
            Account newAccount = new Account(accountKind: (AccountKind)inputAccountKind, balance: inputBalance, credit: inputCredit, clientName: inputClientName, false);
            WaitForKey();
            return newAccount;
        }
         static  int GetValidAccountIndex(string argument)
        {
            bool done = false;
            int inputId;

            do
            {
                Clear();
                Console.Write($"Type the {argument} Account id > ");
                inputId = int.Parse(Console.ReadLine());
                
                if(inputId > repository.GetNextId())
                {
                    Console.WriteLine("Invalid id {0}. Repository has only {1} Accounts.",inputId,repository.GetNextId());
                    WaitForKey();
                }
                else
                {
                    Console.WriteLine(repository.Get(inputId-1));
                    Console.Write($"Is this the wanted {argument} Account <Y/N> > ");
                    string inputOption = Console.ReadLine();
                    done = inputOption.ToUpper() == "Y" ? true :false;
                }
               
              
            }while(!done);

            WaitForKey();

            return inputId-1;
        }
    }
}