class Cart
{
    private Dictionary<Good, int> _cart;
    private IReadOnlyDictionary<Good, int> _readOnlyCart => _cart;
    private Warehouse _warehouse;

    public Cart(Warehouse warehouse)
    {
        _cart = new Dictionary<Good, int>();
        _warehouse = warehouse;
    }

    public void ShowGoods()
    {
        foreach (var good in _cart)
        {
            Console.WriteLine($"Good: {good.Key.Name}  amount: {good.Value}");
        }
    }

    public void Add(Good good, int amount)
    {
        if (_warehouse.HasEnoughGoods(good, amount))
        {
            if (_cart.ContainsKey(good))
                _cart[good] += amount;
            else
                _cart.Add(good, amount);
        }
        else
        {
            Console.Error.WriteLine("Warehouse does not have enough Goods");
        }
    }

    public Order Order()
    {
        _warehouse.Ship(_readOnlyCart);
        _cart = new Dictionary<Good, int>();

        return new Order();
    }
}

class Order
{
    public string Paylink { get; private set; }

    public Order()
    {
        Paylink = GeneratePaylink();
    }

    private string GeneratePaylink()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);

        return finalString;
    }
}
