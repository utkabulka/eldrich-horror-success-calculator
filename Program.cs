using System;

namespace EldrichHorrorSuccessCalculator
{
  internal class Program
  {
    static void Main(string[] args)
    {
      // table should look like this
      // normally 5 and 6 is a success
      // if player is blessed, then 4 is also a success
      // if player is cursed, then only 6 is a success
      // 
      // X is success count
      // Y is cube count
      /*
      ~   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10  | 12  | 13  | 14  | 15
      1   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      2   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      3   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      4   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      5   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      6   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      7   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      8   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      9   | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      10  | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0   | 0   | 0   | 0   | 0
      */

      CubeSimulator cubeSimulator = new CubeSimulator(cubeSides: 6);

      int cubes = 10;
      int iterations = 1000000;
      int[,] results = cubeSimulator.Simulate(cubes, iterations);

      Console.WriteLine($"SR for 1 cube, 1 success: {GetSuccessRate(results, successCriteria: 4, successesNeeded: 1, cubeCount: 1)}%");
      Console.WriteLine($"SR for 2 cubes, 1 success: {GetSuccessRate(results, successCriteria: 4, successesNeeded: 1, cubeCount: 2)}%");
      Console.WriteLine($"SR for 2 cubes, 2 successes: {GetSuccessRate(results, successCriteria: 4, successesNeeded: 2, cubeCount: 2)}%");
    }

    static double GetSuccessRate(int[,] results, int cubeCount, int successesNeeded, int successCriteria)
    {
      int successCount = 0;
      for (int i = 0; i < results.GetLength(1); i++)
      {
        int succeessForThisRow = 0;
        for (int j = 0; j < cubeCount; j++)
        {
          if (results[j, i] >= successCriteria)
          {
            succeessForThisRow++;
          }
        }
        if (succeessForThisRow >= successesNeeded)
        {
          successCount++;
        }
      }
      return (double)successCount / (double)results.GetLength(1) * 100;
    }
  }
}
