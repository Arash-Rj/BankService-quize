namespace Bank.Connections;

public class ConnectionStrings
{
    public static string Connection1 { get; set; }

    static ConnectionStrings()
    {
        Connection1 = @"Data Source=DESKTOP-7648UU0\SQLEXPRESS; Initial Catalog=Bank1; User Id=sa; Password=123456; TrustServerCertificate=True;";
    }
}
