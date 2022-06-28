using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //Выведите платёжные ссылки для трёх разных систем платежа: 
        //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
        //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
        //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

        Order testOrder = new Order(1, 12000);
        PaymentSystemProvider system1 = new PaymentSystemProvider(new PaymentSystem1(), testOrder);
        PaymentSystemProvider system2 = new PaymentSystemProvider(new PaymentSystem2(), testOrder);
        PaymentSystemProvider system3 = new PaymentSystemProvider(new PaymentSystem3(), testOrder);

        Console.WriteLine(system1.GetPayingLink());
        Console.WriteLine(system2.GetPayingLink());
        Console.WriteLine(system3.GetPayingLink());
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);
}

public static class HashGenerator
{
    public static string GetMD5Hash(string input)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

        return Convert.ToBase64String(hash);
    }

    public static string GetSHA1Hash(string input)
    {
        var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

        return Convert.ToBase64String(hash);
    }
}

public class PaymentSystemProvider
{
    private IPaymentSystem _paymentSystem;
    private Order _order;

    public PaymentSystemProvider(IPaymentSystem paymentSystem, Order order) => (_paymentSystem, _order) = (paymentSystem, order);

    public string GetPayingLink()
    {
        return _paymentSystem != null ? _paymentSystem.GetPayingLink(_order) : "";
    }
}

public class PaymentSystem1 : IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        string baseLink = "pay.system1.ru/order?amount=12000RUB&hash=";
        string stringToHash = order.Id.ToString();
        string hash = HashGenerator.GetMD5Hash(stringToHash);

        return baseLink + hash;
    }
}

public class PaymentSystem2 : IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        string baseLink = "order.system2.ru/pay?hash=";
        string stringToHash = order.Id.ToString() + order.Amount;
        string hash = HashGenerator.GetMD5Hash(stringToHash);

        return baseLink + hash;
    }
}

public class PaymentSystem3 : IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        string baseLink = "system3.com/pay?amount=12000&curency=RUB&hash=";
        string secretKey = "secret key";
        string stringToHash = order.Amount.ToString() + order.Id + secretKey;
        string hash = HashGenerator.GetMD5Hash(stringToHash);

        return baseLink + hash;
    }
}
