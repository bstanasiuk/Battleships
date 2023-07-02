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

    public static GameBoard CreateNewGameBoard(int gameBoardSize, int battleshipHp, int destroyerHp, int battleshipsAmount, int destroyersAmount)
    {
        if (gameBoardSize <= 0) throw new ArgumentOutOfRangeException("Game board size must be larger than 0");
        if (battleshipHp <= 0) throw new ArgumentOutOfRangeException("Battleship hp must be larger than 0");
        if (destroyerHp <= 0) throw new ArgumentOutOfRangeException("Destroyer hp must be larger than 0");
        if (battleshipHp < gameBoardSize) throw new ArgumentOutOfRangeException("Battleship hp must be smaller than the game board size");
        if (destroyerHp < gameBoardSize) throw new ArgumentOutOfRangeException("Battleship hp must be smaller than the game board size");

        var gameShips = new List<IGameShip>();
        for (int i = 0; i < battleshipsAmount; i++)
        {
            gameShips.Add(new GameShip(battleshipHp));
        }
        for (int i = 0; i < destroyersAmount; i++)
        {
            gameShips.Add(new GameShip(destroyerHp));
        }

        var gameBoardSquares = new GameBoardSquare[gameBoardSize, gameBoardSize];
        // random squares

        return new GameBoard(gameShips, gameBoardSquares);
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

    public ShotResult ShootAt(int row, int column)
    {
        if (row < 0 || row >= _gameBoardSquares.Length) throw new ArgumentOutOfRangeException("Row number is outside of board size");
        if (column < 0 || column >= _gameBoardSquares.Rank) throw new ArgumentOutOfRangeException("Column number is outside of board size");

        var squareShootAtResult = _gameBoardSquares[row, column].ShootAt();

        if (_gameShips.All(x => x.HealthPoints <= 0))
        {
            return ShotResult.AllShipsSunk;
        }

        return squareShootAtResult;
    }
}
