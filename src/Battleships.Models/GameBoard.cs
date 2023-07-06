using Battleships.Models.Enums;

namespace Battleships.Models;

public class GameBoard : IGameBoard
{
    public GameBoard(IEnumerable<IGameShip> gameShips, IGameBoardSquare[,] gameBoardSquares)
    {
        GameShips = gameShips;
        GameBoardSquares = gameBoardSquares;
    }

    public IEnumerable<IGameShip> GameShips { get; }

    public IGameBoardSquare[,] GameBoardSquares { get; }

    public bool AreAllShipsSunk => GameShips.All(x => x.HealthPoints <= 0);

    public ShotResult ShootAt(ShotCoordinates shotCoordinates)
    {
        if (shotCoordinates.Row < 0 || shotCoordinates.Row >= GameBoardSquares.GetLength(0))
            throw new ArgumentOutOfRangeException("Row number is outside of board size");

        if (shotCoordinates.Column < 0 || shotCoordinates.Column >= GameBoardSquares.GetLength(1))
            throw new ArgumentOutOfRangeException("Column number is outside of board size");

        return GameBoardSquares[shotCoordinates.Row, shotCoordinates.Column].ShootAt();
    }
}
