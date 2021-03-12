using System;

namespace Lab2
{
    public class Neuron
    {
        private double[] inputWeights;
        private double bias;

        public Neuron(double[] inputWeights, double bias)
        {
            this.inputWeights = inputWeights;
            this.bias = bias;
        }

        public double calculateActivation(double[] inputVector)
        {
            double weightedSum = bias;
            
            try
            {
                if (inputVector.Length != inputWeights.Length)
                {
                    throw new Exception("Invalid number of input values passed.");
                }
            
                for (int i = 0; i < inputVector.Length; i++)
                {
                    weightedSum += inputVector[i] * inputWeights[i];
                }

                return activationFunction(weightedSum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private double activationFunction(double x)
        {
            if (x <= 0) return 0;
            if (x <= bias) return x;
            
            return bias;
        }
    }
}