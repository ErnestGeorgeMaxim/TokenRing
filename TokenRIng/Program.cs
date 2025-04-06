using TokenRIng;

namespace TokenRIng;

class Program
{
    private const int NUM_COMPUTERS = 10;
    private static readonly Random Random = new();
    
    private static string GenerateIpAddress()
    {
        return $"{Random.Next(256)}.{Random.Next(256)}.{Random.Next(256)}.{Random.Next(256)}";
    }

    public static void Main()
    {
        Console.WriteLine("Alegeti sensul de mers:");
        Console.WriteLine("1. Dreapta (Clockwise)");
        Console.WriteLine("2. Stanga (Anti-Clockwise)");

        int directionValue;
        while (true)
        {
            Console.Write("Introduceti 1 sau 2: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1)
                {
                    directionValue = 1;
                    Console.WriteLine("Sensul de mers ales: dreapta (Clockwise)");
                    break;
                }
                else if (choice == 2)
                {
                    directionValue = -1;
                    Console.WriteLine("Sensul de mers ales: stanga (Anti-Clockwise)");
                    break;
                }
            }
            Console.WriteLine("Optiune invalida. Introduceti 1 sau 2.");
        }

        Console.WriteLine();

        List<Calculator> calculators = new();
        HashSet<string> usedIps = new HashSet<string>();
        
        for (int i = 0; i < NUM_COMPUTERS; i++)
        {
            string ip = GenerateIpAddress();
            while (usedIps.Contains(ip))
            {
                ip = GenerateIpAddress();
            }
            usedIps.Add(ip);
            calculators.Add(new Calculator(ip));
        }

        Token token = new Token();
        
        int currentTokenIndex = 0;

        for (int step = 1; step <= 10; step++)
        {
            Console.WriteLine($"PAS {step}\n");
            
            for (int i = 0; i < NUM_COMPUTERS; i++)
            {
                Console.WriteLine($"C{i}({calculators[i].IpAddress}) -> {calculators[i].Buffer ?? "null"}");
            }
            Console.WriteLine();

            int sourceIndex = Random.Next(NUM_COMPUTERS);
            int destIndex;
            do
            {
                destIndex = Random.Next(NUM_COMPUTERS);
            } while (sourceIndex == destIndex);

            Console.WriteLine($"Sursa: C{sourceIndex}  Destinatia: C{destIndex}");
            Console.WriteLine();

            while (currentTokenIndex != sourceIndex)
            {
                currentTokenIndex = (currentTokenIndex + directionValue + NUM_COMPUTERS) % NUM_COMPUTERS;
                Console.WriteLine($"C{currentTokenIndex}: Muta jetonul");
            }

            Console.WriteLine($"C{sourceIndex}: Am preluat jetonul");
            token.SourceIp = calculators[sourceIndex].IpAddress;
            token.DestinationIp = calculators[destIndex].IpAddress;
            token.Message = "mesaj de test";
            token.IsFree = false;
            token.ReachedDestination = false;

            while (currentTokenIndex != destIndex)
            {
                currentTokenIndex = (currentTokenIndex + directionValue + NUM_COMPUTERS) % NUM_COMPUTERS;
                if (currentTokenIndex == destIndex)
                {
                    calculators[destIndex].Buffer = token.Message;
                    Console.WriteLine($"C{currentTokenIndex}: Am ajuns la destinatie");
                    token.ReachedDestination = true;
                }
                else
                {
                    Console.WriteLine($"C{currentTokenIndex}: Muta jetonul");
                }
            }

            while (currentTokenIndex != sourceIndex)
            {
                currentTokenIndex = (currentTokenIndex + directionValue + NUM_COMPUTERS) % NUM_COMPUTERS;
                if (currentTokenIndex == sourceIndex)
                {
                    Console.WriteLine($"C{currentTokenIndex}: Am ajuns inapoi");
                }
                else
                {
                    Console.WriteLine($"C{currentTokenIndex}: Muta jetonul");
                }
            }
            
            token.SourceIp = null;
            token.DestinationIp = null;
            token.Message = null;
            token.IsFree = true;
            token.ReachedDestination = false;
            
            Console.WriteLine("\n" + new string('-', 40) + "\n");
        }
    }
}