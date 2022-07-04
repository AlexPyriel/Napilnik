internal class Program
{
    public static int Search(int[] array, int element)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == element)
                return i;

        return -1;
    }

    private static void Main(string[] args)
    {

    }
}