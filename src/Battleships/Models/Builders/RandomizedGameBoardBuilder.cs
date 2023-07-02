namespace Battleships.Models.Builders;

public class RandomizedGameBoardBuilder : IRandomizedGameBoardBuilder
{
    private int _gameBoardSize;
    private int _battleshipHp;
    private int _destroyerHp;
    private int _battleshipsAmount;
    private int _destroyersAmount;

    private Random _random;

    public RandomizedGameBoardBuilder()
    {
        _random = new Random();
        Reset();
    }

    public IRandomizedGameBoardBuilder SetGameBoardSize(int gameBoardSize)
    {
        _gameBoardSize = gameBoardSize;
        return this;
    }

    public IRandomizedGameBoardBuilder SetBattleshipHp(int battleshipHp)
    {
        _battleshipHp = battleshipHp;
        return this;
    }

    public IRandomizedGameBoardBuilder SetDestroyerHp(int destroyerHp)
    {
        _destroyerHp = destroyerHp;
        return this;
    }

    public IRandomizedGameBoardBuilder SetBattleshipsAmount(int battleshipsAmount)
    {
        _battleshipsAmount = battleshipsAmount;
        return this;
    }

    public IRandomizedGameBoardBuilder SetDestroyersAmount(int destroyersAmount)
    {
        _destroyersAmount = destroyersAmount;
        return this;
    }

    public IGameBoard Build()
    {
        ValidateBuilderParameters();

        var gameShips = BuildGameShips();
        var gameBoardSquares = BuildEmptyGameBoardSquares();

        PlaceShipsOnGameBoardSquares(gameShips, gameBoardSquares);

        Reset();

        return new GameBoard(gameShips, gameBoardSquares);
    }

    private void ValidateBuilderParameters()
    {
        if (_gameBoardSize <= 0) throw new ArgumentException("Game board size must be larger than 0");
        if (_battleshipHp <= 0) throw new ArgumentException("Battleship hp must be larger than 0");
        if (_destroyerHp <= 0) throw new ArgumentException("Destroyer hp must be larger than 0");
        if (_battleshipHp < _gameBoardSize) throw new ArgumentException("Battleship hp must be smaller than the game board size");
        if (_destroyerHp < _gameBoardSize) throw new ArgumentException("Battleship hp must be smaller than the game board size");
    }

    private IList<IGameShip> BuildGameShips()
    {
        var gameShips = new List<IGameShip>();

        for (int i = 0; i < _battleshipsAmount; i++)
        {
            gameShips.Add(new GameShip(_battleshipHp));
        }

        for (int i = 0; i < _destroyersAmount; i++)
        {
            gameShips.Add(new GameShip(_destroyerHp));
        }

        return gameShips;
    }

    private IGameBoardSquare[,] BuildEmptyGameBoardSquares()
    {
        var gameBoardSquares = new GameBoardSquare[_gameBoardSize, _gameBoardSize];

        for (var i = 0; i < gameBoardSquares.GetLength(0); i++)
        {
            for (var j = 0; j < gameBoardSquares.GetLength(1); j++)
            {
                gameBoardSquares[i, j] = new GameBoardSquare();
            }
        }

        return gameBoardSquares;
    }

    private void PlaceShipsOnGameBoardSquares(IList<IGameShip> gameShips, IGameBoardSquare[,] gameBoardSquares)
    {
        const int maxPossibleTriesToPlaceShip = 1000000;

        foreach (var gameShip in gameShips)
        {
            var shipPlacementTries = 0;
            var isShipPlaced = false;

            while (!isShipPlaced)
            {
                isShipPlaced = TryPlaceShipRandomlyOnBoard(gameShip, gameBoardSquares);

                if (++shipPlacementTries == maxPossibleTriesToPlaceShip)
                {
                    throw new ArgumentException("Ships cannot be placed randomly on the board with set parameters");
                }
            }
        }
    }

    private bool TryPlaceShipRandomlyOnBoard(IGameShip gameShip, IGameBoardSquare[,] gameBoardSquares)
    {
        var isHorizontal = _random.Next(2) == 0;
        var firstPositionStartsAt = _random.Next(_gameBoardSize);
        var secondPositionStartsAt = _random.Next(_gameBoardSize - gameShip.HealthPoints);

        var canShipBePlaced = isHorizontal ?
            CanShipBePlacedHorizontaly(gameBoardSquares, firstPositionStartsAt, secondPositionStartsAt, gameShip.HealthPoints) :
            CanShipBePlacedVerticaly(gameBoardSquares, secondPositionStartsAt, firstPositionStartsAt, gameShip.HealthPoints);

        if (canShipBePlaced)
        {
            if (isHorizontal)
            {
                PlaceShipHorizontaly(gameBoardSquares, firstPositionStartsAt, secondPositionStartsAt, gameShip);
            }
            else
            {
                PlaceShipVerticaly(gameBoardSquares, secondPositionStartsAt, firstPositionStartsAt, gameShip);
            }
        }

        return canShipBePlaced;
    }

    private bool CanShipBePlacedHorizontaly(IGameBoardSquare[,] gameBoardSquares, int shipRow, int shipColumnStartAt, int shipSize)
    {
        for (var i = shipColumnStartAt; i < shipColumnStartAt + shipSize; i++)
        {
            if (gameBoardSquares[shipRow, i].ContainsGameShip)
            {
                return false;
            }
        }

        return true;
    }

    private bool CanShipBePlacedVerticaly(IGameBoardSquare[,] gameBoardSquares, int shipColumn, int shipRowStartAt, int shipSize)
    {
        for (var i = shipRowStartAt; i < shipRowStartAt + shipSize; i++)
        {
            if (gameBoardSquares[i, shipColumn].ContainsGameShip)
            {
                return false;
            }
        }

        return true;
    }

    private void PlaceShipHorizontaly(IGameBoardSquare[,] gameBoardSquares, int shipRow, int shipColumnStartAt, IGameShip gameShip)
    {
        for (var i = shipColumnStartAt; i < shipColumnStartAt + gameShip.HealthPoints; i++)
        {
            gameBoardSquares[shipRow, i] = new GameBoardSquare(gameShip);
        }
    }

    private void PlaceShipVerticaly(IGameBoardSquare[,] gameBoardSquares, int shipColumn, int shipRowStartAt, IGameShip gameShip)
    {
        for (var i = shipRowStartAt; i < shipRowStartAt + gameShip.HealthPoints; i++)
        {
            gameBoardSquares[i, shipColumn] = new GameBoardSquare(gameShip);
        }
    }

    private void Reset()
    {
        _gameBoardSize = 0;
        _battleshipHp = 0;
        _destroyerHp = 0;
        _battleshipsAmount = 0;
        _destroyersAmount = 0;
    }
}
