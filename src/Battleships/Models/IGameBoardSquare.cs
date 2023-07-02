using Battleships.Models.Enums;

namespace Battleships.Models;

public interface IGameBoardSquare
{
    bool ContainsGameShip { get; }

    GameSquareStatus Status { get; }

    public ShotResult ShootAt();
}