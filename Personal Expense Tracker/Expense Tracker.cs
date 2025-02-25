namespace ExpenseTracker
{
    enum Months //This was employed while printing out expenses for any specific month of a particular year
    {
        January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, 
        July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
    }
    public class Expense
    {
        public string? Expenditure { get; set; } //name of the expense
        public int[] Date { get; set; } //date is stored as day, month, and year in an array. (Update to Datetype when familiar)
        public double Amount { get; set; } //cost of expense
        public int ExpenditureNumber {get; set;} //will serve as Primary Key for each expense

        static List<Expense> expenses = new List<Expense>();

        public Expense(string? expenditure, int[] date, double amount, int expenditureNumber)
        {
            Expenditure = expenditure;
            Date = date;
            Amount = amount;
            ExpenditureNumber = expenditureNumber;
        }


        //Takes a date input in string format, converts it to an array. 
        public static int[] DateArray(string dateInput) 
        {
            int[] date = new int[3];
            string[] dateInputArray;

            dateInputArray = dateInput.Split("-");

            int position = 0;
            foreach (string x in dateInputArray)
            {
                date[position] = int.Parse(x); 
                position++;
            }
            return date;
        }

        //the date array of an expense is converted to string for the purpose of display
        public static string DateToString(Expense expense)
        {
            string dateString = "";
            int position = 0;

            foreach (int x in expense.Date)
            {
                dateString += x.ToString();
                if (position == 0 || position == 1) dateString += "-";
                position++;
            }
            return dateString;
        }

        //To add an expense
        public static void AddExpense() 
        {
            string expenditure;
            double amount;
            int expenditureNumber;

            Console.Write("Enter Date ( format: DD-MM-YYYY ): "); //To prompt the user for the date of transaction
            string dateInput = Console.ReadLine();
            int[] date = DateArray(dateInput); //Calls DateArray() method and stores result in this date array 

            if (date.Length == 3 && (date[1] >=1 && date[1] <=12) )//checks if date and month is valid. (Check for month and day validity)
            {  
                Console.Write("Enter Expenditure Number: "); //prompts user for expenditure number
                expenditureNumber = int.Parse(Console.ReadLine());

                Expense checkExpenseNumber = expenses.Find(e => e.ExpenditureNumber == expenditureNumber);//To ensure no two expenses have the same expenditure number
                if(checkExpenseNumber != null)
                {
                    Console.WriteLine("Expenditure number is already assigned to different expense!");
                    AddExpense();
                }
                else
                {
                    Console.Write("Enter Expenditure: "); //prompts user for expenditure name
                    expenditure = Console.ReadLine();

                    Console.Write("Enter Amount: "); //prompts user for expenditure amount
                    amount = double.Parse(Console.ReadLine());

                    expenses.Add(new Expense ( expenditure, date, amount, expenditureNumber) );
                    Console.WriteLine("Expense added!");
                    AddAgain(); 
                }

            }
            else
            {
                Console.WriteLine("Invalid Date entered!");
                AddExpense();
            }

            //If user needs to add another expense immediately after adding one
            static void AddAgain()
            {
                Console.Write("\nDo you want to add another Expense: Enter 1(yes) or 2(No): ");
                int ans = int.Parse(Console.ReadLine());
                switch (ans)
                {
                    case 1:
                        Console.WriteLine("\n");
                        AddExpense();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }


        //To view expenees
        public static void ViewExpense()
        {
            Console.WriteLine("Enter: ");
            Console.WriteLine("1. To view All expenses"); 
            Console.WriteLine("2. To view specific date(DD-MM-YYYY) or month(MM-YYYY) or year(YYYY) expense(s)");
            Console.WriteLine("3. To view specfic expenditure");
            
            int entry = int.Parse(Console.ReadLine());
            double totalExpenses = 0;

            switch (entry)
            {
                //views all expenses
                case 1: 
                    Console.WriteLine("\n\tAll Expenses");
                    Console.WriteLine("S/N \t Date\t Expenditure\t Amount");

                    foreach (Expense expense in expenses)
                    {
                        totalExpenses += expense.Amount;
                        Console.WriteLine($"{expense.ExpenditureNumber}\t {DateToString(expense)}\t {expense.Expenditure}\t {expense.Amount}");
                    }

                    Console.WriteLine($"Sum of all time expenses: {totalExpenses.ToString("C")}");
                    ViewAgain();
                    break;

                //To view expenses by date(DD-MM-YYYY) or month(MM-YYYY) or year(YYYY) of entry
                case 2: 
                    {
                        ViewByDate();
                        ViewAgain();
                    }
                    break;

                //To view partucular expenditure
                case 3: 
                    {
                        Console.WriteLine("Enter expenditure (case sensitive): ");
                        string expenditure = Console.ReadLine();

                        List<Expense> specificExpenditures = new List<Expense>();
                        specificExpenditures = expenses.FindAll(e => e.Expenditure == expenditure);
                        

                        if (specificExpenditures != null)
                        {
                            Console.WriteLine($"All {expenditure} expenses");
                            Console.WriteLine("S/N\t Date\t Expenditure\t Amount");

                            foreach (Expense expense in specificExpenditures)
                            {
                               
                                    totalExpenses += expense.Amount;
                                    Console.WriteLine($"{expense.ExpenditureNumber}\t {DateToString(expense)}\t {expense.Expenditure}\t {expense.Amount}");
                                
                            }
                            Console.WriteLine($"Total expenditure spent on {expenditure} is {totalExpenses.ToString("C")}");
                            ViewAgain();
                        }
                        else
                        {
                            Console.WriteLine("No record found");
                            ViewAgain();
                        }   
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Invalid Entry!");
                        ViewExpense();
                        break;
                    }
            }


            //Implementation of viewing expenses by date(DD-MM-YYYY) or month(MM-YYYY) or year(YYYY) of entry
            static void ViewByDate() 
            {
                double totalExpenses;

                Console.Write("Enter specific date(DD-MM-YYYY) or month(MM-YYYY) or year(YYYY): "); 
                string dateInput = Console.ReadLine();

                if (dateInput.Length == "YYYY".Length)//to search for expenses for a specific year (YYYY)
                {
                    totalExpenses = 0;
                    int year = int.Parse(dateInput);

                    List<Expense> expenseYear = new List<Expense>();
                    expenseYear = expenses.FindAll(e => e.Date[2] == year);

                    Console.WriteLine($"All expenses in the year {dateInput} ");
                    if (expenseYear != null)
                    {
                        Console.WriteLine("S/N\t Date\t Expenditure\t Amount");
                        foreach (Expense expense in expenseYear)
                        {
                            totalExpenses += expense.Amount;
                            Console.WriteLine($"{expense.ExpenditureNumber}\t {DateToString(expense)}\t {expense.Expenditure}\t {expense.Amount}");
                              
                        }
                        Console.WriteLine($"Total expenses in year{dateInput} is {totalExpenses.ToString("C")}");
                    }
                    else Console.WriteLine("No record found");
                }

                else if (dateInput.Length == "MM-YYYY".Length || dateInput.Length == "M-YYYY".Length) // to search for expenses for a specific month and year(MM-YYYY)
                {
                    totalExpenses = 0;
                    int[] monthYear = DateArray(dateInput); 

                    List<Expense> expenseYear = new List<Expense>();
                    expenseYear = expenses.FindAll(e => e.Date[2] == monthYear[1]);
                    
                    if (expenseYear != null)
                    {
                        List<Expense> expenseMonthYear = new List<Expense>();
                        expenseMonthYear = expenseYear.FindAll(e => e.Date[1] == monthYear[0]);

                        if (expenseMonthYear != null)
                        {
                            Console.WriteLine("\nAll expenses in {0} {1}", (Months)monthYear[0], monthYear[1]); //check enum here
                            Console.WriteLine("S/N\t Date\t Expenditure\t Amount");

                            foreach (Expense expense in expenseMonthYear)
                            {
                                totalExpenses += expense.Amount;
                                Console.WriteLine($"{expense.ExpenditureNumber}\t {DateToString(expense)}\t {expense.Expenditure}\t {expense.Amount}");

                            }
                            Console.WriteLine("Total expenses for {0} {1} is {2}", (Months)monthYear[0], monthYear[1], totalExpenses.ToString("C"));
                        }
                        
                    }
                    else Console.WriteLine("No record found");
                }

                else if (dateInput.Length == "DD-MM-YYYY".Length || dateInput.Length == "D-MM-YYYY".Length || dateInput.Length == "D-M-YYYY".Length)//to search for expenses for a specific date (DD-MM-YYYY)
                {
                    totalExpenses = 0;
                    int[] dayMonthYear = DateArray(dateInput);

                    List<Expense> expenseYear = new List<Expense>();
                    expenseYear = expenses.FindAll(e => e.Date[2] == dayMonthYear[2]);
                    
                    if(expenseYear != null)
                    {
                        List<Expense> expenseMonthYear = new List<Expense>();
                        expenseMonthYear = expenseYear.FindAll(e => e.Date[1] == dayMonthYear[1]);
                        
                        if(expenseMonthYear != null)
                        {
                            List<Expense> expenseDayMonthYear = new List<Expense>();
                            expenseDayMonthYear = expenseMonthYear.FindAll(e => e.Date[0] == dayMonthYear[0]);
                            
                            if(expenseDayMonthYear != null)
                            {
                                Console.WriteLine("\nAll expenses on {0}", dateInput);
                                Console.WriteLine("S/N\t Date\t Expenditure\t Amount");

                                foreach (Expense expense in expenseDayMonthYear)
                                {
                                    
                                        totalExpenses += expense.Amount;
                                        Console.WriteLine($"{expense.ExpenditureNumber}\t {DateToString(expense)}\t {expense.Expenditure}\t {expense.Amount}");
                                    

                                }
                                Console.WriteLine($"Total expenses for {dateInput} is {totalExpenses.ToString("C")}");


                            }
                            
                        }
                    }
                    
                    else Console.WriteLine("No record found");

                }
                else
                {
                    Console.WriteLine("Invalid date!");
                }
            }



            //If user needs to view another expense immediately after viewing one
            static void ViewAgain()
            {
                Console.Write("\nDo you want to View another expense(s): Enter 1(yes) or 2(No): ");
                int ans = int.Parse(Console.ReadLine());
                switch (ans)
                {
                    case 1:
                        Console.WriteLine("\n");
                        ViewExpense();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }

        //to update expenses
        public static void UpdateExpense()
        {
            Console.WriteLine("Before updating any expense, kindly view the expense(s) either by expenditure name or date to confirm the expense number");
            ViewExpense();

            Console.Write("\nExpenditure number: ");
            int number = int.Parse(Console.ReadLine());

            Expense expense = expenses.Find(e => e.ExpenditureNumber == number);

            if (expense != null )
            {
                Console.WriteLine("To update:");
                Console.WriteLine("Expenditure date enter '1'\nExpenditure amount enter '2'\nExpenditure name enter '3'\n All enter '4' ");
                Console.Write("Update: ");

                int update = int.Parse(Console.ReadLine());

                Console.WriteLine("Are you sure you want to update? Y(yes)/N(no): ");
                Console.WriteLine("S/N\t Date\t Expenditure\t Amount");
                Console.WriteLine($"{expense.ExpenditureNumber} \t {DateToString(expense)} \t {expense.Expenditure} \t {expense.Amount}");

                char ans = char.Parse(Console.ReadLine());

                if (ans == 'Y')
                {
                    switch (update)
                    {
                        case 1:
                            {
                                expense.Date = UpdateDate();
                                UpdateAgain();
                            }
                            break;
                        case 2:
                            {
                                expense.Amount = UpdateAmount();
                                UpdateAgain();
                            }
                            break;
                        case 3:
                            {
                                expense.Expenditure = UpdateExpenditure();
                                UpdateAgain();
                            }
                            break;
                        case 4:
                            {
                                expense.Date = UpdateDate();
                                expense.Amount = UpdateAmount();
                                expense.Expenditure = UpdateExpenditure();
                                UpdateAgain();
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Invalid entry!");
                                UpdateAgain();
                            }
                            break;
                    }

                }
                else UpdateAgain();

               
            }
            else Console.WriteLine("No record found");

            //Update Date 
            static int[] UpdateDate()
            {
                Console.Write("Enter new date( DD-MM-YYYY ): ");
                string dateInput = Console.ReadLine();

                int[] date = DateArray(dateInput);
                return date;
            }

            //Update Amount
            static double UpdateAmount()
            {
                Console.WriteLine("Enter new amount: ");
                double amount = double.Parse(Console.ReadLine());

                return amount;
                
            }
            
            static string UpdateExpenditure()
            {
                Console.Write("Enter expenditure's new name: ");
                string nameInput = Console.ReadLine();

                return nameInput;
            }
            //If user needs to update another expense immediately after updating one
            static void UpdateAgain()
            {
                Console.Write("Do you want to update another expense(s): Enter 1(yes) or 2(No): ");
                int ans = int.Parse(Console.ReadLine());
                switch (ans)
                {
                    case 1:
                        Console.WriteLine("\n");
                        UpdateExpense();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }

        //to delete expenses
        public static void DeleteExpense()
        {
            Console.WriteLine("Before deleting any expense, kindly view the expense(s) either by expenditure name or date to confirm the expense number");
            ViewExpense();

            Console.Write("Enter expenditure number: ");
            int number = int.Parse(Console.ReadLine());

            Expense expense = expenses.Find(e => e.ExpenditureNumber == number);

            if (expense != null)
            {
                Console.WriteLine("Are you sure you want to delete? Y(yes)/N(no): ");
                Console.WriteLine("S/N\t Date\t Expenditure\t Amount");
                Console.WriteLine($"{expense.ExpenditureNumber} \t {DateToString(expense)} \t {expense.Expenditure} \t {expense.Amount}");

                char ans = char.Parse(Console.ReadLine());

                if (ans == 'Y')
                {
                    expenses.Remove(expense);
                    DeleteAgain();
                }
                else DeleteAgain();

            }
            else Console.WriteLine("No record found");

            //If user needs to another delete immediately after adding one
            static void DeleteAgain()
            {
                Console.Write("Do you want to delete another expense(s): (Enter 1(yes) or 2(No): ");
                int ans = int.Parse(Console.ReadLine());
                switch (ans)
                {
                    case 1:
                        Console.WriteLine("\n");
                        DeleteExpense();
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
