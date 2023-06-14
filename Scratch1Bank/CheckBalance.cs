using Scratch1Bank;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Scratch1Bank
{
    public class CheckBalance
    {

        public void PrintBalance(Account account)
        {
            Console.WriteLine($"Your Balance is {account.Balance}");
        }

        public void AccountDetails(Account account)
        {
            //string header = $"FullName\tAccount Number\tAccount Type\tBalance";
            //string details = $"{account.FullName}\t{account.AccountNumber}\t{account.AccountType}\t{account.Balance}";
            //Console.WriteLine(header);
            //Console.WriteLine(details);

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"ACCOUNT DETAILS\n");
            Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|");
            Console.WriteLine("| FULL NAME         | ACCOUNT NUMBER                | ACCOUNT TYPE             | AMOUNT BAL          |");
            Console.WriteLine("|-------------------|-------------------------------|--------------------------|---------------------|");
            Console.WriteLine($"| {account.FullName,-17} | {account.AccountNumber,-29} | {account.AccountType,-24} | {account.Balance.ToString("C", new CultureInfo("ha-Latn-NG")),-19} |");
            Console.WriteLine("|----------------------------------------------------------------------------------------------------|");
        }

        public void PrintStatement(Account account)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"ACCOUNT STATEMENT ON ACCOUNT NO {account.AccountNumber}\n");
            Console.WriteLine("|---------------------|-----------------------------------------------|--------------------------|---------------------|");
            Console.WriteLine("| DATE                | DESCRIPTION                                   | AMOUNT                   | BALANCE             |");
            Console.WriteLine("|---------------------|-----------------------------------------------|--------------------------|---------------------|");

            foreach (Transaction transaction in account.Transactions)
            {
                Console.WriteLine($"| {transaction.Date,-10} | {transaction.Description,-45} | {transaction.Amount,-24} | {transaction.Balance.ToString("C", new CultureInfo("ha-Latn-NG")),-19} |");
            }

            Console.WriteLine("|----------------------------------------------------------------------------------------------------------------------|");
        }
    }
}
