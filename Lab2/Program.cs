using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new NetworkConfig();
            
            HemmingNetwork network = new HemmingNetwork(config.sampleVectors);
            
            var finalActivations = network.ClassifyInput(config.inputVector, NetworkConfig.E_max);
            var activationDifference = finalActivations[0] - finalActivations[1];

            if (Math.Abs(activationDifference) < Double.Epsilon)
            {
                Console.WriteLine("\nThe network could not determine, which sample the input vector matches");
            }
            else if (activationDifference > 0)
            {
                Console.WriteLine("\nThe input vector matches the first sample");
            }
            else
            {
                Console.WriteLine("\nThe input vector matches the second sample");
            }
        }
    }
}