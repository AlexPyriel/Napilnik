using System;

class Warehouse
{
    private Dictionary<Good, int> _goods;

    public Warehouse()
    {
        _goods = new Dictionary<Good, int>();
    }

    public void ShowGoods()
    {
        foreach (var good in _goods)
        {
            Console.WriteLine($"Good: {good.Key.Name}  amount: {good.Value}");
        }
    }

    public void Deliver(Good good, int amount)
    {
        if (_goods.ContainsKey(good))
            _goods[good] += amount;
        else
            _goods.Add(good, amount);
    }

    public void Ship(IReadOnlyDictionary<Good, int> cart)
    {
        foreach (var good in cart)
        {
            if (HasEnoughGoods(good.Key, good.Value))
            {
                _goods[good.Key] -= good.Value;
            }
        }
    }

    public bool HasEnoughGoods(Good good, int amount)
    {
        if (_goods.ContainsKey(good))
        {
            return _goods[good] - amount >= 0;
        }
        else
        {
            return false;
        }
    }
}
