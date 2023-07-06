using Battleships.Models.Enums;

namespace Battleships.Models;

public interface IGameBoard
{
    IEnumerable<IGameShip> GameShips { get; }

    IGameBoardSquare[,] GameBoardSquares { get; }

    bool AreAllShipsSunk { get; }

    ShotResult ShootAt(ShotCoordinates shotCoordinates);
}