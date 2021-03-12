using System;

namespace Lab2
{
    public class HemmingNetwork
    {
        private Neuron[] firstLayer;
        private double[][] weightsMatrix;
        private readonly double[][] sampleVectors;
        private int SampleVectorsCount => sampleVectors.GetLength(0);

        private double[] secondLayerActivations;

        private const double Epsilon = 1.0 / NetworkConfig.VectorLength;
        
        public HemmingNetwork(double[][] sampleVectors)
        {
            this.sampleVectors = sampleVectors;
            
            InitializeWeights();

            firstLayer = new Neuron[SampleVectorsCount];
            secondLayerActivations = new double[SampleVectorsCount];
            for (int i = 0; i < SampleVectorsCount; i++)
            {
                firstLayer[i] = new Neuron(weightsMatrix[i], 0.5 * NetworkConfig.VectorLength);
            }
        }

        private void InitializeWeights()
        {
            this.weightsMatrix = new double[SampleVectorsCount][];

            for (int i = 0; i < SampleVectorsCount; i++)
            {
                weightsMatrix[i] = new double[NetworkConfig.VectorLength];
                for (int j = 0; j < NetworkConfig.VectorLength; j++)
                {
                    weightsMatrix[i][j] = 0.5 * sampleVectors[i][j];
                }
            }
        }

        private double[] CalculateFirstLayerActivations(double[] inputVector)
        {
            double[] activations = new double[SampleVectorsCount];
            
            for (int i = 0; i < SampleVectorsCount; i++)
            {
                activations[i] = firstLayer[i].calculateActivation(inputVector);
            }

            return activations;
        }
        
        private double activationFunction(double x)
        {
            double T = NetworkConfig.VectorLength / 2.0;
            
            if (x <= 0) return 0;
            if (x <= T) return x;
            
            return T;
        }

        private double hemmingDistance(double[] x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("Cannot calculate hemming distance of vectors of different length");
            }

            double distance = 0;
            for (int i = 0; i < x.Length; i++)
            {
                distance += Math.Abs(x[i] - y[i]);
            }

            return distance;
        }

        void printActivations(double[] arr)
        {
            foreach (var element in arr)
            {
                Console.Write(element + " ");
            }
        }
        
        public double[] ClassifyInput(double[] inputVector, double E_max)
        {
            var firstLayerActivations = CalculateFirstLayerActivations(inputVector);
            for (int i = 0; i < SampleVectorsCount; i++)
            {
                secondLayerActivations[i] = firstLayerActivations[i];
            }

            double[] prevSecondLayerActivations = new double[SampleVectorsCount];
            double[] newSecondLayerActivations = secondLayerActivations;

            int currentEpoch = 1;
            do
            {
                prevSecondLayerActivations = (double[])newSecondLayerActivations.Clone();
                
                Console.WriteLine($"\nEpoch #{currentEpoch}:");
                printActivations(prevSecondLayerActivations);

                for (int i = 0; i < SampleVectorsCount; i++)
                {
                    newSecondLayerActivations[i] = prevSecondLayerActivations[i];
                    for (int j = 0; j < SampleVectorsCount; j++)
                    {
                        if (j != i)
                        {
                            newSecondLayerActivations[i] -= prevSecondLayerActivations[j] * Epsilon;
                        }
                    }

                    newSecondLayerActivations[i] = activationFunction(newSecondLayerActivations[i]);
                }
                
                currentEpoch++;
            } while (hemmingDistance(prevSecondLayerActivations, newSecondLayerActivations) > E_max);

            secondLayerActivations = newSecondLayerActivations;
            
            Console.WriteLine("\nFinal epoch values:");
            printActivations(secondLayerActivations);

            return secondLayerActivations;
        }
    }
}