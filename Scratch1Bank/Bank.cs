using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scratch1Bank
{
    public class Bank
    { 

        private CheckBalance _checker;

        private List<Account> accounts { get; set; }

        private string fileLocation { get; set; }

        public Bank (string fileLocation)
        {
            accounts = new List<Account>();
            _checker = new CheckBalance();
            this.fileLocation = fileLocation;

            // Check if file exist 
            if (File.Exists(fileLocation))
            {
                ReadFromFile();
            }
        }

        public List<Account> GetAccounts()
        {
            return accounts; // finder
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account); // collector
        }

        public void SaveToFile()
        {
            try
            {

                List<string> lines = new List<string>();


                foreach (Account account in accounts)

                {

                    string line = $"{account.FirstName},{account.LastName},{account.AccountNumber},{account.AccountType},{account.Password},{account.Email},{account.Balance}";


                    foreach (Transaction transaction in account.Transactions)

                    {

                        line += $",{transaction.Date},{transaction.Description},{transaction.Amount},{transaction.Balance}";

                    }


                    lines.Add(line);

                }


                File.WriteAllLines(fileLocation, lines);


                Console.WriteLine("Accounts saved to file.");

            }
            catch (Exception ex)

            {

                Console.WriteLine($"Error saving accounts to file: {ex.Message}");

            }

        }

        public void ReadFromFile()
        {
            if (File.Exists(fileLocation))

            {

                try
                {

                    string[] lines = File.ReadAllLines(fileLocation);


                    foreach (string line in lines)

                    {

                        string[] accountData = line.Split(',');


                        if (accountData.Length >= 7)

                        {

                            Account account = new Account()

                            {

                                FirstName = accountData[0],

                                LastName = accountData[1],

                                AccountNumber = accountData[2],

                                AccountType = accountData[3],

                                Password = accountData[4],

                                Email = accountData[5],

                                Balance = decimal.Parse(accountData[6])

                            };


                            for (int i = 7; i < accountData.Length; i += 4)

                            {

                                DateTime transactionDate;

                                if (DateTime.TryParse(accountData[i], out transactionDate))

                                {

                                    Transaction transaction = new Transaction()

                                    {

                                        Date = transactionDate,

                                        Description = accountData[i + 1],

                                        Amount = decimal.Parse(accountData[i + 2]),

                                        Balance = decimal.Parse(accountData[i + 3])

                                    };


                                    account.Transactions.Add(transaction);

                                }

                            }


                            accounts.Add(account);

                        }

                    }


                    Console.WriteLine("Accounts loaded from file.");

                }

                catch (Exception ex)

                {

                    Console.WriteLine($"Error loading accounts from file: {ex.Message}");

                }

            }


        }


        public void Deposits(Account account)
        {
            Console.WriteLine("Enter the amount");
            string amountInput = Console.ReadLine();

            while (!Validate.Amount(amountInput))
            {
                Console.WriteLine("Invalid input");
                amountInput = Console.ReadLine();
            }
            decimal amount = Convert.ToDecimal(amountInput);


            Console.WriteLine($"{account.Balance}");
            account.Balance += amount;

            Console.WriteLine($"{account.Balance}");

            account.Transactions.Add(new Transaction()
            {
                Date = DateTime.Now,
                Description = "Deposit",
                Amount = amount,
                Balance = account.Balance
            });
            Console.WriteLine("deposit Successful");


        }

        public void CreateAccount(Bank bank)
        {
            Console.WriteLine("Enter your First Name");
            string firstName = Console.ReadLine();
            while (!Validate.Input(firstName, @"^[A-Z][a-z]+$"))
            {
                Console.WriteLine("Invalid input, please input a valid firstname");
                firstName = Console.ReadLine();
            }
            Console.WriteLine("Enter your Last Name");
            string lastName = Console.ReadLine();
            while (!Validate.Input(lastName, @"^[A-Z][a-z]+$"))
            {
                Console.WriteLine("Invalid input, please input a valid lastname");
                lastName = Console.ReadLine();
            }
            Console.WriteLine("Enter your Email");
            string email = Console.ReadLine();
            while (!Validate.Input(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                Console.WriteLine("Invalid input, please input a valid email. eg imade@gmail.com");
                email = Console.ReadLine();
            }
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            while (!Validate.Input(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
            {
                Console.WriteLine("Invalid input, please input a valid password. eg Imade123!");
                password = Console.ReadLine();
            }

            Console.WriteLine("Enter 1 for Saving or 2 for Current");
            string accounttype = Console.ReadLine();

            while (!Validate.Choice(accounttype))
            {
                Console.WriteLine("Invalid input");
                accounttype = Console.ReadLine();
            }

            Random random = new Random();
            string accountNumber = "";
            switch (accounttype)
            {
                case "1":
                    accounttype = "Savings";
                    accountNumber = "0" + random.Next().ToString().Substring(0, 9);
                    break;
                case "2":
                    accounttype = "Current";
                    accountNumber = "1" + random.Next().ToString().Substring(0, 9);
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            Console.WriteLine("Make initial deposit");
            string amountInput = Console.ReadLine();

            while (!Validate.Amount(amountInput))
            {
                Console.WriteLine("Invalid input");
                amountInput = Console.ReadLine();
            }
            decimal amount = Convert.ToDecimal(amountInput);

            Account account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                AccountType = accounttype,
                AccountNumber = accountNumber,
                Balance = amount
            };

            bank.AddAccount(account);

            Console.WriteLine($"Your account number is {account.AccountNumber}");


            account.Transactions.Add(new Transaction()
            {
                Date = DateTime.Now,
                Description = "initial Deposit",
                Amount = amount,
                Balance = account.Balance
            });
            Console.WriteLine("Acccount created successfully");
            SaveToFile();



        }

        public void Transfers(Account account, Bank bank)
        {
            Console.WriteLine("Enter the destination account number");
            string destinationAccountNumber = Console.ReadLine();

            while (!Validate.Input(destinationAccountNumber, @"^\d{10}$"))
            {
                Console.WriteLine("Invalid input, please input a valid Account Number");
                destinationAccountNumber = Console.ReadLine();
            }

            Account destinationAccount = bank.GetAccounts().Find(x => x.AccountNumber == destinationAccountNumber);

            if (destinationAccount != null)
            {
                Console.WriteLine("Enter the amount");
                string amountInput = Console.ReadLine();

                while (!Validate.Amount(amountInput))
                {
                    Console.WriteLine("Invalid input");
                    amountInput = Console.ReadLine();
                }
                decimal amount = Convert.ToDecimal(amountInput);


                Console.WriteLine($"My Balance before: {account.Balance}");
                Console.WriteLine($"Receipient Balance before: {destinationAccount.Balance}");
                account.Balance -= amount;
                destinationAccount.Balance += amount;
                Console.WriteLine("Transfer Successful");
                Console.WriteLine($"My Balance after: {account.Balance}");
                Console.WriteLine($"Receipient Balance after: {destinationAccount.Balance}");

                account.Transactions.Add(new Transaction()
                {
                    Date = DateTime.Now,
                    Description = $"Transfer to {destinationAccount.AccountNumber}",
                    Amount = amount,
                    Balance = account.Balance
                });

                destinationAccount.Transactions.Add(new Transaction()
                {
                    Date = DateTime.Now,
                    Description = $"Transfer from {account.AccountNumber}",
                    Amount = amount,
                    Balance = account.Balance
                });
                Console.WriteLine("Transfer Successful");
                SaveToFile();
            }
            else
            {
                Console.WriteLine("Destination Account number not found");
            }



        }


        public void Withdraw(Account account)
        {
            Console.WriteLine("Enter the amount");
            string amountInput = Console.ReadLine();

            while (!Validate.Amount(amountInput))
            {
                Console.WriteLine("Invalid input");
                amountInput = Console.ReadLine();
            }
            decimal amount = Convert.ToDecimal(amountInput);


            Console.WriteLine($"{account.Balance}");
            account.Balance -= amount;

            Console.WriteLine($"{account.Balance}");


            account.Transactions.Add(new Transaction()
            {
                Date = DateTime.Now,
                Description = "Withdrawal",
                Amount = amount,
                Balance = account.Balance
            });
            Console.WriteLine("Withdrawal Successful");

        }

        public void Log(Bank bank)
        {
            Console.WriteLine("Enter your Email");
            string email = Console.ReadLine();

            while (!Validate.Input(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                Console.WriteLine("Invalid input, please input a valid email. eg imade@gmail.com");
                email = Console.ReadLine();
            }

            Console.WriteLine("Enter your Password");
            string password = Console.ReadLine();

            while (!Validate.Input(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
            {
                Console.WriteLine("Invalid input, please input a valid password. eg Imade123!");
                password = Console.ReadLine();
            }

            Account account = bank.GetAccounts().Find(x => x.Email == email && x.Password == password);

            if (account != null)
            {
                Console.WriteLine("Login Successful");
                string choice;
                do
                {
                    BankOperationMenu();
                    do
                    {
                        Console.WriteLine("Enter your choice");
                        choice = Console.ReadLine();

                    }
                    while (!Validate.Choice(choice));
                    switch (choice)
                    {
                        case "1":

                            Deposits(account);
                            break;
                        case "2":                        
                            Withdraw(account);
                            break;
                        case "3":                    
                            Transfers(account, bank);
                            break;
                        case "4":
                           // CheckBalance checkbalance = new CheckBalance();
                            _checker.PrintBalance(account);
                            break;
                        case "5":
                          //  CheckBalance accountdetails = new CheckBalance();
                            _checker.AccountDetails(account);
                            break;
                        case "6":
                          //  CheckBalance statement = new CheckBalance();
                            _checker.PrintStatement(account);
                            break;
                        case "7":
                            Console.WriteLine("Exit");
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;


                    }
                } while (choice != "7");

            }
            else
            {
                Console.WriteLine("Invalid Email or Password");
            }
        }

        public void BankOperationMenu()
        {
            string message = $"1. Deposit\n2. Withdrawal\n" +
                $"3. Transfer\n4.Check Balance\n5.Account Details\n6.Statement Of Account\n7. Exit";
            Console.WriteLine(message);
        }


    }



}

