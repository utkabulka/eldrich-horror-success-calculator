namespace EldrichHorrorSuccessCalculator
{
  public class CubeSimulator
  {
    private int _sides = 6;
    public int Sides { get { return _sides; } }

    public CubeSimulator(int cubeSides)
    {
      _sides = cubeSides;
    }

    public int[,] Simulate(int cubes, int iterations)
    {
      int[,] results = new int[cubes, iterations];
      Random random = new();
      for (int i = 0; i < cubes; i++)
      {
        for (int j = 0; j < iterations; j++)
        {
          results[i, j] = random.Next(0, _sides);
        }
      }
      return results;
    }
  }
}
