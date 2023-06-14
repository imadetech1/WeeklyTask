using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Scratch1Bank
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileLocation = "accountDB.txt";
            Bank bank = new Bank(fileLocation);
            string choice;

            Thread startThread = new Thread(() =>  // This starts the thread 
            {
                do
                {
                    Console.WriteLine("1. Create Account\n2. Login\n3. Exit");

                    do
                    {
                        Console.WriteLine("Enter your choice");
                        choice = Console.ReadLine();

                    }
                    while (!Validate.Choice(choice));


                    switch (choice)
                    {
                        case "1":

                            Thread createAccountThread = new Thread(() =>
                            {
                                bank.CreateAccount(bank);

                            });
                            createAccountThread.Start();
                            createAccountThread.Join();
                           
                            break;
                        case "2":
                            Thread loginThread = new Thread(() =>
                            {
                                bank.Log(bank);

                            });
                            loginThread.Start();
                            loginThread.Join();
                            
                            break;
                        case "3":
                            Console.WriteLine("Exit..");
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                    //choice = Console.ReadLine();
                } while (choice != "3");


            });
            startThread.Start();
            startThread.Join();



        }



    }

}
