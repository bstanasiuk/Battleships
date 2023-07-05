namespace Battleships.Common;

// A simple wrapper around Random class that allows mocking behavior in tests
public class RandomGenerator : IRandomGenerator
{
    private readonly Random _random;

    public RandomGenerator()
    {
        _random = new Random();
    }

    public int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }

    public bool NextBool()
    {
        return _random.Next(2) == 0;
    }
}
