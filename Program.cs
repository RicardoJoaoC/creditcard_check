using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a credit card number:");
        string input = Console.ReadLine()?.Replace(" ", "").Replace("-", "") ?? "";

        if (string.IsNullOrEmpty(input) || !long.TryParse(input, out _))
        {
            Console.WriteLine("Invalid input. Please enter a valid credit card number.");
            return;
        }

        string bandeira = GetBandeira(input);
        Console.WriteLine($"Bandeira: {bandeira}");
    }

    static string GetBandeira(string number)
    {
        // Visa: Starts with 4, 16 digits
        if (number.Length == 16 && number.StartsWith("4"))
            return "Visa";

        // MasterCard: Starts with 51-55 or 2221-2720, 16 digits
        int prefix2 = int.TryParse(number.Substring(0, 2), out var p2) ? p2 : -1;
        int prefix4 = int.TryParse(number.Substring(0, 4), out var p4) ? p4 : -1;
        if (number.Length == 16 && ((prefix2 >= 51 && prefix2 <= 55) || (prefix4 >= 2221 && prefix4 <= 2720)))
            return "MasterCard";

        // Elo: Starts with 4011, 4312, 4389, etc. (add more as needed)
        string[] eloPrefixes = { "4011", "4312", "4389" };
        foreach (var prefix in eloPrefixes)
            if (number.StartsWith(prefix))
                return "Elo";

        // American Express: Starts with 34 or 37, 15 digits
        if (number.Length == 15 && (number.StartsWith("34") || number.StartsWith("37")))
            return "American Express";

        // Discover: Starts with 6011, 65, or 644-649, 16 digits
        if (number.Length == 16 && (number.StartsWith("6011") || number.StartsWith("65") ||
            (int.TryParse(number.Substring(0, 3), out var p3) && p3 >= 644 && p3 <= 649)))
            return "Discover";

        // Hipercard: Usually starts with 6062
        if (number.StartsWith("6062"))
            return "Hipercard";

        return "Unknown";
    }
}

