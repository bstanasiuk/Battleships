namespace Battleships.Models;

public interface IGameShip
{
    int HealthPoints { get; }

    void ShootAt();
}