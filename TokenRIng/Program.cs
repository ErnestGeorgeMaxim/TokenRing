namespace TokenRIng;

class Program
{
    private const int NUM_COMPUTERS = 10;
    private static readonly Random Random = new();
    
    private enum Direction
    {
        Clockwise,
        CounterClockwise
    }

    private static string GenerateIpAddress()
    {
        return $"{Random.Next(256)}.{Random.Next(256)}.{Random.Next(256)}.{Random.Next(256)}";
    }

    public static void Main()
    {
        Console.WriteLine("Choose token movement direction:");
        Console.WriteLine("1. Clockwise");
        Console.WriteLine("2. Counter-Clockwise");

        Direction direction;
        while (true)
        {
            Console.Write("Enter your choice (1 or 2): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && (choice == 1 || choice == 2))
            {
                direction = choice == 1 ? Direction.Clockwise : Direction.CounterClockwise;
                break;
            }

            Console.WriteLine("Invalid input. Please enter 1 or 2.");
        }

        Console.WriteLine($"\nToken will move in {direction} direction.\n");

        List<Calculator> calculators = new List<Calculator>();
        for (int i = 0; i < NUM_COMPUTERS; i++)
        {
            calculators.Add(new Calculator(GenerateIpAddress()));
        }

        Token token = new Token();

        for (int step = 0; step < 10; step++)
        {
            Console.WriteLine($"\n--- Step {step + 1} ---");

            for (int i = 0; i < calculators.Count; i++)
            {
                Calculator calculator = calculators[i];
                Console.WriteLine($"C{i}({calculator.IpAddress}) -> {calculator.Buffer ?? "null"}");
            }

            if (token.IsFree)
            {
                int sourceIndex, destIndex;
                do
                {
                    sourceIndex = Random.Next(NUM_COMPUTERS);
                    destIndex = Random.Next(NUM_COMPUTERS);
                } while (sourceIndex == destIndex);

                Calculator source = calculators[sourceIndex];
                Calculator destination = calculators[destIndex];

                Console.WriteLine($"Source: C{sourceIndex} Destination: C{destIndex}");

                token.SourceIp = source.IpAddress;
                token.DestinationIp = destination.IpAddress;
                token.Message = "Test message";
                token.IsFree = false;
                token.ReachedDestination = false;
            }

            if (direction == Direction.Clockwise)
            {
                ProcessTokenMovementClockwise(calculators, token);
            }
            else
            {
                ProcessTokenMovementCounterClockwise(calculators, token);
            }
        }
    }

    private static void ProcessTokenMovementClockwise(List<Calculator> calculators, Token token)
    {
        for (int i = 0; i < calculators.Count; i++)
        {
            Calculator currentComputer = calculators[i];
            Console.WriteLine($"C{i}: Move the token (Clockwise)");

            if (currentComputer.IpAddress == token.DestinationIp && !token.ReachedDestination)
            {
                Console.WriteLine($"C{i}: Reached the destination");
                currentComputer.Buffer = token.Message;
                token.ReachedDestination = true;
            }

            if (currentComputer.IpAddress == token.SourceIp && token.ReachedDestination)
            {
                Console.WriteLine($"C{i}: Back to the source");
                token.SourceIp = null;
                token.DestinationIp = null;
                token.Message = null;
                token.IsFree = true;
                token.ReachedDestination = false;
            }
        }
    }

    private static void ProcessTokenMovementCounterClockwise(List<Calculator> calculators, Token token)
    {
        for (int i = calculators.Count - 1; i >= 0; i--)
        {
            Calculator currentComputer = calculators[i];
            Console.WriteLine($"C{i}: Move the token (Counter-Clockwise)");

            if (currentComputer.IpAddress == token.DestinationIp && !token.ReachedDestination)
            {
                Console.WriteLine($"C{i}: Reached the destination");
                currentComputer.Buffer = token.Message;
                token.ReachedDestination = true;
            }

            if (currentComputer.IpAddress == token.SourceIp && token.ReachedDestination)
            {
                Console.WriteLine($"C{i}: Back to the source");
                token.SourceIp = null;
                token.DestinationIp = null;
                token.Message = null;
                token.IsFree = true;
                token.ReachedDestination = false;
            }
        }
    }
}