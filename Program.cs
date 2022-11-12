using NeuralNetwork;
using System.Linq;

namespace XOR_Neural_Network
{
    public class Program
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


            Console.WriteLine("Please input a size for the population of the neural network");
            int size = int.Parse(Console.ReadLine());

            NeuralNetwork.NeuralNetwork[] population = new NeuralNetwork.NeuralNetwork[size];
            for (int i = 0; i < size; i++)
            {
                population[i] = new NeuralNetwork.NeuralNetwork(ActivationAndErrorFunctions.TanHActivationFunction, ActivationAndErrorFunctions.MeanSqauredErrorFunction, 2, 2, 1);
                population[i].Randomize(random, -1, 1);
            }


            RunTraining(population, random, 2, inputs);
        }

        public static double Fitness(double[][] outputs)
        {
            double fitness = 0;

            //For output 1
            fitness += Math.Pow((outputs[0][0]), 2);

            //For output 2
            fitness += Math.Pow(1 - outputs[1][0], 2);

            //For output 3
            fitness += Math.Pow(1 - outputs[2][0], 2);

            //For output 4
            fitness += Math.Pow(outputs[3][0], 2);

            return fitness / 4;
        }

        public static void RunTraining(NeuralNetwork.NeuralNetwork[] population, Random random, double mutationRate, double[][] inputs)
        {
            (NeuralNetwork.NeuralNetwork network, double fitness)[] fitnessRecords = new (NeuralNetwork.NeuralNetwork network, double fitness)[population.Length];

            for (int i = 0; i < population.Length; i++)
            {
                fitnessRecords[i].network = population[i];
                Console.WriteLine("Outputs: ");
                var outputs = fitnessRecords[i].network.Compute(inputs);

                for (int j = 0; j < outputs.Length; j++)
                {
                    Console.WriteLine(outputs[j][0] + ", ");
                }

                var temporaryFitness = Fitness(outputs);

                fitnessRecords[i].fitness = temporaryFitness;
                Console.WriteLine("Fitness: " + temporaryFitness + "\n");

                if (temporaryFitness == 0)
                {
                    Console.WriteLine("Eureka, I have found it!");
                    //Add code for it to be the network.
                    return;
                }
            }

            fitnessRecords = fitnessRecords.OrderBy(networkTuple => networkTuple.fitness).ToArray();

            int amountTrained = 0;
            while (fitnessRecords[0].fitness != 0)
            {
                GeneticAlgorithm.Train(fitnessRecords, random, mutationRate, -1, 1, true);
                amountTrained++;

                if (amountTrained % 100 == 0)
                {

                    Console.WriteLine("Outputs: " + amountTrained + " Times trained");

                    for (int i = 0; i < population.Length; i++)
                    {
                        var outputs = fitnessRecords[i].network.Compute(inputs);
                        fitnessRecords[i].fitness = Fitness(outputs);

                        //Prints the best neural network fitness
                        if (i == 0)
                        {
                            for (int j = 0; j < outputs.Length; j++)
                            {
                                Console.WriteLine(outputs[j][0] + ", ");
                            }
                        }
                    }
                    Console.WriteLine("Fitness: " + fitnessRecords[0].fitness + "\n");
                }
            }

            Console.WriteLine("Sorted");
        }
    }
}