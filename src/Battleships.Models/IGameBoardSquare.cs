using Battleships.Models.Enums;

namespace Battleships.Models;

public interface IGameBoardSquare
{
    bool HasGameShip { get; }

    GameSquareStatus Status { get; }

    public ShotResult ShootAt();
}