using NeuralNetwork;

namespace XOR_Neural_Network
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] inputs = new double[][]
            {
                new double[]{0,0},
                new double[]{1,0},
                new double[]{0,1},
                new double[]{1,1}
            };
            double[][] outputs = new double[][]
            {
                new double[]{0},
                new double[]{1},
                new double[]{1},
                new double[]{0}
            };

            Random random = new Random();

            NeuralNetwork.NeuralNetwork neuralNetwork = new NeuralNetwork.NeuralNetwork(ActivationAndErrorFunctions.TanHActivationFunction, ActivationAndErrorFunctions.MeanSqauredErrorFunction, 2, 2, 1);
            neuralNetwork.Randomize(random, -1, 1);
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < inputs.Length; i++)
                {
                    Console.Write("Inputs: ");
                    for (int j = 0; j < inputs[i].Length; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(inputs[i][j]);
                    }

                    Console.Write(" Output: " + Math.Round(neuralNetwork.Compute(inputs[i])[0], 3));
                    Console.WriteLine();
                }

                Console.WriteLine("Hello, World!");
            }
        }
    }
}