using System;
using System.IO;

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

      StreamWriter htmlFile = new StreamWriter("./index.html");

      htmlFile.WriteLine(@"<!DOCTYPE html>
        <html lang=""en"">
        <head>
          <meta charset=""UTF-8"">
          <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
          <title>Eldrich Horror Success Rates</title>
          <link rel=""stylesheet"" href=""./styles.css"" defer />
        </head><body>");

      int tableWidth = 10;
      htmlFile.WriteLine(BuildTable(results, cubes, successCriteria: 4, tableWidth));

      htmlFile.WriteLine("</body></html>");
      htmlFile.Flush();
      htmlFile.Close();
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

    static string BuildTable(int[,] results, int cubes, int successCriteria, int tableWidth)
    {
      string table = "<table><tbody>";

      table += "<tr>";
      for (int j = 0; j < tableWidth + 1; j++)
      {
        // Console.Write($" {j + 1}\t\t|");
        table += $"<th>{j}</th>";
      }
      table += "</tr>";

      for (int i = 1; i < cubes + 1; i++)
      {
        // row
        table += "<tr>";
        for (int j = 0; j < tableWidth + 1; j++)
        {
          if (j == 0)
          {
            table += $"<td>{i}</td>";
          }
          else
          {
            if (i + 1 > j)
            {
              table += $"<td>{GetSuccessRate(results, cubeCount: i, successesNeeded: j, successCriteria).ToString("0.00")}</td>";
            }
            else
            {
              table += $"<td>N/A</td>";
            }
          }
        }
        table += "</tr>";
      }

      table += "</table></tbody>";
      return table;
    }
  }
}
