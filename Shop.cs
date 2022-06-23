class Shop
{
    private Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
        _warehouse = warehouse;
    }

    public Cart Cart()
    {
        return new Cart(_warehouse);
    }
}
