namespace Battleships.Common;

public interface IRandomGenerator
{
    int Next(int maxValue);

    bool NextBool();
}
