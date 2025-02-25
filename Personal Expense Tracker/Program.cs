using System;
using ExpenseTracker;
using static ExpenseTracker.Expense; //LAST BUS STOP


class Program
{

    static void Main()
    {

        Console.WriteLine("Welcome to your Personal Expense Tracker\n");
        bool cont = true;

        do
        {
            //A loop can start from here
            Console.WriteLine("\nEnter: ");
            Console.WriteLine("1. To Add an expense");
            Console.WriteLine("2. To View expense(s)");
            Console.WriteLine("3. To Update an expense");
            Console.WriteLine("4. To Delete an expense\n");
            
            int entry = int.Parse(Console.ReadLine()); //remember to add the are you done or do you want to perform another function

            switch (entry)
            {
                case 1:
                    AddExpense();
                    break;
                case 2:
                    ViewExpense();
                    break;
                case 3:
                    UpdateExpense();
                    break;
                case 4:
                    DeleteExpense();
                    break;
            }

            Console.Write("\nDo you want to perform another action? Yes(enter 1)/No(enter 2): ");
            int ans = int.Parse(Console.ReadLine());

            if (ans == 2)
            {
                Console.WriteLine("Goodbye!");
                cont = false; 
            }
            else cont = true;
            //complete this
        }while ( cont == true);
        
    }


   
}
