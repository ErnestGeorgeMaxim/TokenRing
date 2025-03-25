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
        List<Calculator> calculators = new List<Calculator>();
        for (int i = 0; i < NUM_COMPUTERS; i++)
        {
            calculators.Add(new Calculator(GenerateIpAddress()));
        }
        
        Token token = new Token();

        for (int step = 0; step < 10; step++)
        {
            Console.WriteLine($"\n--- Step {step + 1} ---");

            for (int i = 0; i < calculators.Count - 1; i++)
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

            for (int i = 0; i < calculators.Count; i++)
            {
                Calculator currentComputer = calculators[i];
                Console.WriteLine($"C{i}: Move the token");

                if (currentComputer.IpAddress == token.DestinationIp && !token.ReachedDestination)
                {
                    Console.WriteLine($"C{i}: Reached the destination");
                    currentComputer.Buffer = token.Message;
                    token.ReachedDestination = true;
                }

                if (currentComputer.IpAddress == token.DestinationIp && token.ReachedDestination)
                {
                    Console.WriteLine($"C{i}: Back to the destination");
                    token.DestinationIp = null;
                    token.Message = null;
                    token.IsFree = true;
                    token.ReachedDestination = false;
                }
            }
        }
    }
}