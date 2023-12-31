﻿using System.Diagnostics;

namespace EldrichHorrorSuccessCalculator
{
  internal class Program
  {
    static int Main(string[] args)
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
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      CubeSimulator cubeSimulator = new CubeSimulator(cubeSides: 6);

      int cubes = 10;
      int iterations = 1000000;
      int[,] results = cubeSimulator.Simulate(cubes, iterations);
      stopwatch.Stop();
      Console.WriteLine($"Simulated throwing {cubes} cubes {iterations} times in {stopwatch.ElapsedMilliseconds}ms");

      const string HTML_FILE_OUTPUT_PATH = "./index.html";
      StreamWriter fileWriter = new StreamWriter(HTML_FILE_OUTPUT_PATH);

      fileWriter.WriteLine(@"<!DOCTYPE html>
        <html lang=""en"">
        <head>
          <meta charset=""UTF-8"">
          <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
          <title>Eldrich Horror Success Rates</title>
          <link rel=""stylesheet"" href=""./styles.css"" defer />
        </head><body>");

      int tableWidth = 10;

      stopwatch.Restart();
      string tableTitle = "Normal throws";
      fileWriter.WriteLine(BuildHTMLTable(results, tableTitle, cubes, successCriteria: 4, tableWidth));
      stopwatch.Stop();
      Console.WriteLine($"Built table '{tableTitle}' in {stopwatch.ElapsedMilliseconds}ms");

      stopwatch.Restart();
      tableTitle = "Blessed throws";
      fileWriter.WriteLine(BuildHTMLTable(results, tableTitle, cubes, successCriteria: 3, tableWidth));
      stopwatch.Stop();
      Console.WriteLine($"Built table '{tableTitle}' in {stopwatch.ElapsedMilliseconds}ms");

      stopwatch.Restart();
      tableTitle = "Cursed throws";
      fileWriter.WriteLine(BuildHTMLTable(results, tableTitle, cubes, successCriteria: 5, tableWidth));
      stopwatch.Stop();
      Console.WriteLine($"Built table '{tableTitle}' in {stopwatch.ElapsedMilliseconds}ms");

      fileWriter.WriteLine("</body></html>");
      fileWriter.Flush();
      fileWriter.Close();

      const string CSS_FILE_OUTPUT_PATH = "./styles.css";
      fileWriter = new StreamWriter(CSS_FILE_OUTPUT_PATH);
      fileWriter.WriteLine(Styles.StylesheetString);
      fileWriter.Flush();
      fileWriter.Close();

      Console.WriteLine($"Done!");
      return 0;
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

    static string BuildHTMLTable(int[,] results, string title, int cubes, int successCriteria, int tableWidth)
    {
      string table = $@"<div class=""table-block"">
      <table>
        <h1>{title}</h1>
        <tbody>";

      table += "<tr>";
      for (int j = 0; j < tableWidth + 1; j++)
      {
        if (j == 0)
        {
          table += @"<th><div class=""table-corner"">Successes<hr>Cubes</div></th>";
        }
        else
        {
          table += $"<th>{j}</th>";
        }
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
            table += $@"<td class=""cubes-column"">{i}</td>";
          }
          else
          {
            if (i + 1 > j)
            {
              double successRate = GetSuccessRate(results, cubeCount: i, successesNeeded: j, successCriteria);
              int severity = 0;
              if (successRate < 90)
              {
                severity = 1;
              }
              if (successRate < 80)
              {
                severity = 2;
              }
              if (successRate < 70)
              {
                severity = 3;
              }
              if (successRate < 60)
              {
                severity = 4;
              }
              if (successRate < 50)
              {
                severity = 5;
              }
              if (successRate < 40)
              {
                severity = 6;
              }
              if (successRate < 30)
              {
                severity = 7;
              }
              if (successRate < 20)
              {
                severity = 8;
              }
              if (successRate < 10)
              {
                severity = 9;
              }
              table += $@"<td class=""severity-{severity}"">{successRate.ToString("0.00")}</td>";
            }
            else
            {
              table += $"<td></td>";
            }
          }
        }
        table += "</tr>";
      }

      table += "</tbody></table></div>";
      return table;
    }
  }
}
