using Battleships.Models.Enums;

namespace Battleships.Models;

public interface IGameBoard
{
    GameSquareStatus[,] GameSquareStatuses { get; }

    ShotResult ShootAt(ShotCoordinates shotCoordinates);
}