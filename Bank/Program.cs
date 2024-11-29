
using Bank.Entities;
using Bank.Service;
CardService cardService = new CardService();
TrasactionService trasactionService = new TrasactionService();
bool isfinished = false;
int loginCount = 0;
do
{
    Console.Clear();
    Console.WriteLine("*****Welcome To The Library*****");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("1.Login");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("3.Logout");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("******************************");
    int choice = 0;
    try
    {
        choice = Int32.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid format entered.Try again.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key...");
        Console.ReadKey();
    }
    switch (choice)
    {
        case 1:
            Console.Clear();
            Login();
            break;
        case 3:
            Console.Clear();
            isfinished = true;
            break;
    }
} while (!isfinished);
void Login()
{
    Console.Clear();
    Console.Write("Please enter your Card Numebr: ");
    var Cardnumebr = Console.ReadLine();
    Console.Write("Please enter your password: ");
    var password = Console.ReadLine();
    var res = cardService.IsCardValid(Cardnumebr, password);
    ResultMessage(res);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Press any key...");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.ReadKey();
    if (res.IsDone == true)
    {
        membermenu(Cardnumebr);
    }
    else
    {
        loginCount++;
        if (loginCount == 3)
        {
            Console.WriteLine("You have entered wrong numbers 3 times, your card is now deactivated.");
            ResultMessage(cardService.DeActivateCard(Cardnumebr));
        }
    }
}
void ResultMessage(Result result)
{
    if (result.IsDone)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(result.Message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
void membermenu(string cardnumber)
{
    bool isfinished = false;
    do
    {
        Console.Clear();
        Console.WriteLine("******Welcome******");
        Console.WriteLine("1.Transfer money.");
        Console.WriteLine("2.Transfer history.");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("5.Peace out.");
        Console.ForegroundColor = ConsoleColor.Gray;
        if (!Int32.TryParse(Console.ReadLine(), out int res))
        {
            Console.WriteLine("Invalid format please try again.");
        }
        switch (res)
        {
            case 1:
                Console.Clear();
                Console.Write("Enter amount: ");
                var amount = float.Parse(Console.ReadLine());
                Console.Write("Enter The Destination card number: ");
                var destinationcardnumber = Console.ReadLine();
                var result = trasactionService.Transfer(cardnumber,destinationcardnumber, amount);
                ResultMessage(result);
                break;
            case 2:
                Console.Clear();
                var transactionlist = trasactionService.CardTransactionList(cardnumber);
                transactionlist.ForEach(t =>Console.WriteLine(t.ToString()));
                break;
            case 5:
                Console.Clear();
                isfinished = true;
                break;
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Press any key...");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.ReadKey();
    }
    while (!isfinished);
}
