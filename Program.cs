internal class Program
{
    public static int ValidateValueWithRange(int a, int b, int c)
    {
        if (a < b)
            return b;
        else if (a > c)
            return c;
        else
            return a;
    }

    private static void Main(string[] args)
    {

    }
}
