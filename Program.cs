internal class Program
{
    private static void Main(string[] args) { }

    public void Enable()
    {
        _effects.StartEnableAnimation();
    }

    public void Disable()
    {
        _pool.Free(this);
    }
}
