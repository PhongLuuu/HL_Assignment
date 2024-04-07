namespace algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int[] arr = InputArray();
        SortArray(arr);
        Console.WriteLine(CalculateMinValue(arr) + " " + CalculateMaxValue(arr));
    }

    static int[] InputArray()
    {
        int[] arr = new int[5];
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write($"Enter number {i + 1}: ");
            string? input = Console.ReadLine();
            int number;
            if (int.TryParse(input, out number))
            {
                arr[i] = number;
            }
            else
            {
                i--;
            }
        }
        return arr;
    }

    static int[] SortArray(int[] arr)
    {
        Array.Sort(arr);
        return arr;
    }

    static int CalculateMinValue(int[] arr)
    {
        int value = 0;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            value += arr[i];
        }
        return value;
    }

    static int CalculateMaxValue(int[] arr)
    {
        int value = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            value += arr[i];
        }
        return value;
    }
}
