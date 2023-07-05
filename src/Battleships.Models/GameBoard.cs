using Battleships.Models.Enums;

namespace Battleships.Models;

public class GameBoard : IGameBoard
{
    private readonly IEnumerable<IGameShip> _gameShips;
    private readonly IGameBoardSquare[,] _gameBoardSquares;

    public GameBoard(IEnumerable<IGameShip> gameShips, IGameBoardSquare[,] gameBoardSquares)
    {
        _gameShips = gameShips;
        _gameBoardSquares = gameBoardSquares;
    }

    public GameSquareStatus[,] GameSquareStatuses
    {
        get
        {
            var gameSquareStatuses = new GameSquareStatus[_gameBoardSquares.GetLength(0), _gameBoardSquares.GetLength(1)];

            for (int i = 0; i < gameSquareStatuses.GetLength(0); i++)
            {
                for (int j = 0; j < gameSquareStatuses.GetLength(1); j++)
                {
                    gameSquareStatuses[i, j] = _gameBoardSquares[i, j].Status;
                }
            }

            return gameSquareStatuses;
        }
    }

    public ShotResult ShootAt(ShotCoordinates shotCoordinates)
    {
        if (shotCoordinates.Row < 0 || shotCoordinates.Row >= _gameBoardSquares.Length)
            throw new ArgumentOutOfRangeException("Row number is outside of board size");

        if (shotCoordinates.Column < 0 || shotCoordinates.Column >= _gameBoardSquares.Length)
            throw new ArgumentOutOfRangeException("Column number is outside of board size");

        var squareShootAtResult = _gameBoardSquares[shotCoordinates.Row, shotCoordinates.Column].ShootAt();

        if (_gameShips.All(x => x.HealthPoints <= 0))
        {
            return ShotResult.AllShipsSank;
        }

        return squareShootAtResult;
    }
}
