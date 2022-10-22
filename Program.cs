﻿using NeuralNetwork;

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


            Console.WriteLine("Please input a size for the population of the neural network");
            int size = int.Parse(Console.ReadLine());

            NeuralNetwork.NeuralNetwork[] population = new NeuralNetwork.NeuralNetwork[size];
            for (int i = 0; i < size; i++)
            {
                population[i] = new NeuralNetwork.NeuralNetwork(ActivationAndErrorFunctions.TanHActivationFunction, ActivationAndErrorFunctions.MeanSqauredErrorFunction, 2, 2, 1);
                population[i].Randomize(random, -1, 1);
            }

            RunTraining(population, random, 2, inputs);


            //for (int j = 0; j < 5; j++)
            //{
            //    double[] testers = new double[4];
            //    Console.WriteLine("Here are the outputs: ");

            //    for (int k = 0; k < 4; k++)
            //    {
            //        var temp = random.NextDouble();
            //        testers[k] = temp;
            //        Console.Write(temp + ", ");
            //    }

            //    Console.WriteLine("Fitness: " + Fitness(testers));
            //}
            /*while (true)
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
            }*/
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

        //public static double Fitness(NeuralNetwork.NeuralNetwork network, double[][] inputs)
        //{
        //    double fitness = 0;

        //    for (int i = 0; i < inputs.Length; i++)
        //    {
        //        double[] outputs = network.Compute(inputs[i]);
        //        //For output 1
        //        fitness += (outputs[0]);

        //        fitness += (network.Compute(inputs[i]))[0];

        //        //For output 2
        //        fitness += Math.Abs(1 - outputs[1]);

        //        //For output 3
        //        fitness += Math.Abs(1 - outputs[2]);

        //        //For output 4
        //        fitness += outputs[3];
        //    }

        //    return fitness;
        //}

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

                var fitnesS = Fitness(outputs);

                fitnessRecords[i].fitness = fitnesS;
                Console.WriteLine("Fitness: " + fitnesS + "\n");
            }
        }
    }
}