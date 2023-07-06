namespace Battleships.Models;

public class GameShip : IGameShip
{
    public int HealthPoints
    {
        get; private set;
    }

    public GameShip(int healthPoints)
    {
        HealthPoints = healthPoints;
    }

    public void ShootAt()
    {
        if (HealthPoints > 0)
        {
            HealthPoints--;
        }
    }
}
