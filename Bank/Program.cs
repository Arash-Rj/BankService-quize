
using Bank.Entities;
using Bank.Interfaces;
using Bank.Service;
using System.Reflection.Emit;
using System.Xml.Serialization;
CardService cardService = new CardService();
TrasactionService trasactionService = new TrasactionService();
UserService userService = new UserService();
Dictionary<int, DateTime> Codes = new Dictionary<int, DateTime>();
bool isfinished = false;
int loginCount = 0;
do
{
    Console.Clear();
    Console.WriteLine("*****Welcome To The Library*****");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("1.Login");
    Console.WriteLine("2.Register");
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
        case 2:
            Console.Clear();
            Register();
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
    Console.Write("Please enter your CardNumber: ");
    var Cardnumber = Console.ReadLine();
    Console.Write("Please enter your password: ");
    var password = Console.ReadLine();
    var res = cardService.IsCardValid(Cardnumber, password);
    ResultMessage(res);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Press any key...");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.ReadKey();
    if (res.IsDone == true)
    {
        membermenu(Cardnumber);
        loginCount = 0;
    }
    else
    {
        loginCount++;
        if (loginCount == 3)
        {
            Console.WriteLine("You have entered wrong numbers 3 times, your card is now deactivated.");
            loginCount = 0;
            ResultMessage(cardService.DeActivateCard(Cardnumber));
            Console.ReadKey();
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
        Console.WriteLine("3.Card balance.");
        Console.WriteLine("4.Change card password.");
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
                var isdone = false;
                do
                {
                    Console.Clear();
                    Console.Write("Enter amount: ");
                    var amount = float.Parse(Console.ReadLine());
                    Console.Write("Enter The Destination card number: ");
                    var destinationcardnumber = Console.ReadLine();
                    Console.Write("The random code: ");
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    var randomcode = cardService.GenerateRandomCode();
                    Console.WriteLine(randomcode);
                    Codes[randomcode] = DateTime.Now.AddMinutes(5);
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("!Note:The random code is only valid for 5 minutes. ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    var holdername = cardService.GetCardHoldername(destinationcardnumber);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Press any key to proceed to the transfer page...");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadKey();
                    Console.Clear();
                    if (holdername.IsDone)
                    {
                        Console.WriteLine("Destination card number: " + destinationcardnumber);
                        Console.WriteLine($"Amount: {amount}");
                        ResultMessage(holdername);
                        Console.Write("1.Continue   2.Back:  ");
                        var choice = int.Parse(Console.ReadLine());
                        switch(choice)
                        {
                            case 1:
                                Console.Clear();
                                Console.Write("Enter the code: ");
                                var rcode = int.Parse(Console.ReadLine());
                                var iscodevslid = IsCodeValid(rcode);
                                if(iscodevslid.IsDone)
                                {

                                    var result = trasactionService.Transfer(cardnumber, destinationcardnumber, amount);
                                    ResultMessage(result);
                                    isdone = true;
                                }                                
                                else
                                {
                                    ResultMessage(iscodevslid);
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Press any key...");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.ReadKey();
                                }
                                break;
                        }
                    }
                    else
                    {
                        ResultMessage(holdername);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Press any key...");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                    }
                } while(!isdone);
                break;
            case 2:
                Console.Clear();
                var transactionlist = trasactionService.CardTransactionList(cardnumber);
                transactionlist.ForEach(t =>Console.WriteLine(t.ToString()));
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("Enter your card number: ");
                var cardnumber1 = Console.ReadLine();
                ResultMessage(cardService.CardBalance(cardnumber1));
                break;
            case 4:
                Console.Clear();
                Console.Write("Enter the current password: ");
                var currentpass = Console.ReadLine();
                Console.Write("Enter the new password: ");
                var newpassword = Console.ReadLine();
                ResultMessage(cardService.ChangecardPass(cardnumber, currentpass, newpassword));
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
Result IsCodeValid(int code)
{
    if(Codes.ContainsKey(code) && Codes[code] > DateTime.Now )
    {
        return new Result(true);
    }
    if(Codes.ContainsKey(code) && Codes[code] < DateTime.Now)
    {
        Codes.Remove(code);
        return new Result(false,"The Code has expired.");
    }
    return new Result(false,"The Entered code is wrong.");
}
void Register()
{
    Console.Write("Please enter your name: ");
    var name = Console.ReadLine();
    Console.Write("Please enter your email: ");
    var email = Console.ReadLine();
    Console.Write("Please enter your password: ");
    var password = Console.ReadLine();
    var res = userService.Register(name, email, password);
    ResultMessage(res);
    if (res.IsDone == true)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1.Advance to menu");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("2.Peace out");
        Console.ForegroundColor = ConsoleColor.Gray;
        int res1 = 0;
        try
        { res1 = Int32.Parse(Console.ReadLine()); }
        catch (FormatException)
        {
            Console.Clear();
            Console.WriteLine("Invalid format entered.Try again.");
            Console.WriteLine("Press any key...");
        }
        if (res1 == 1)
        {
            Login();
        }
    }
    else
    {
        Console.WriteLine("Press any key...");
    }
}
