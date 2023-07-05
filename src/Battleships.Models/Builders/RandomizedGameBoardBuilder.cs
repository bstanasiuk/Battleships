using Battleships.Common;

namespace Battleships.Models.Builders;

public class RandomizedGameBoardBuilder : IRandomizedGameBoardBuilder
{
    private readonly IRandomGenerator _randomGenerator;

    private int _gameBoardSize;
    private int _battleshipHp;
    private int _destroyerHp;
    private int _battleshipsAmount;
    private int _destroyersAmount;

    private IGameBoardSquare[,]? _builtGameBoardSquares;
    private IList<IGameShip>? _builtGameShips;

    public RandomizedGameBoardBuilder(IRandomGenerator randomGenerator)
    {
        _randomGenerator = randomGenerator;

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
        ValidateBuildParameters();

        BuildGameShips();
        BuildEmptyGameBoardSquares();

        PlaceBuiltShipsOnGameBoardSquares();

        Reset();

        return new GameBoard(_builtGameShips!, _builtGameBoardSquares!);
    }

    private void ValidateBuildParameters()
    {
        if (_gameBoardSize <= 0) throw new ArgumentException("Game board size must be larger than 0");
        if (_battleshipHp <= 0) throw new ArgumentException("Battleship hp must be larger than 0");
        if (_destroyerHp <= 0) throw new ArgumentException("Destroyer hp must be larger than 0");
        if (_battleshipHp > _gameBoardSize) throw new ArgumentException("Battleship hp must be smaller than the game board size");
        if (_destroyerHp > _gameBoardSize) throw new ArgumentException("Battleship hp must be smaller than the game board size");
    }

    private void BuildGameShips()
    {
        _builtGameShips = new List<IGameShip>();

        for (int i = 0; i < _battleshipsAmount; i++)
        {
            _builtGameShips.Add(new GameShip(_battleshipHp));
        }

        for (int i = 0; i < _destroyersAmount; i++)
        {
            _builtGameShips.Add(new GameShip(_destroyerHp));
        }
    }

    private void BuildEmptyGameBoardSquares()
    {
        _builtGameBoardSquares = new GameBoardSquare[_gameBoardSize, _gameBoardSize];

        for (var i = 0; i < _builtGameBoardSquares.GetLength(0); i++)
        {
            for (var j = 0; j < _builtGameBoardSquares.GetLength(1); j++)
            {
                _builtGameBoardSquares[i, j] = new GameBoardSquare();
            }
        }
    }

    private void PlaceBuiltShipsOnGameBoardSquares()
    {
        const int maxPossibleTriesToPlaceShip = 1000000;

        foreach (var gameShip in _builtGameShips!)
        {
            var shipPlacementTries = 0;
            var isShipPlaced = false;

            while (!isShipPlaced)
            {
                isShipPlaced = TryPlaceShipRandomlyOnBoard(gameShip);

                if (++shipPlacementTries == maxPossibleTriesToPlaceShip)
                {
                    throw new ArgumentException("Ships cannot be placed randomly on the board with set parameters");
                }
            }
        }
    }

    private bool TryPlaceShipRandomlyOnBoard(IGameShip gameShip)
    {
        var isHorizontal = _randomGenerator.NextBool();
        var firstPositionStartsAt = _randomGenerator.Next(_gameBoardSize);
        var secondPositionStartsAt = _randomGenerator.Next(_gameBoardSize - gameShip.HealthPoints);

        var canShipBePlaced = isHorizontal ?
            CanShipBePlacedHorizontaly(firstPositionStartsAt, secondPositionStartsAt, gameShip.HealthPoints) :
            CanShipBePlacedVerticaly(firstPositionStartsAt, secondPositionStartsAt, gameShip.HealthPoints);

        if (canShipBePlaced)
        {
            if (isHorizontal)
            {
                PlaceShipHorizontaly(firstPositionStartsAt, secondPositionStartsAt, gameShip);
            }
            else
            {
                PlaceShipVerticaly(firstPositionStartsAt, secondPositionStartsAt, gameShip);
            }
        }

        return canShipBePlaced;
    }

    private bool CanShipBePlacedVerticaly(int shipRow, int shipColumnStartAt, int shipSize)
    {
        for (var i = shipColumnStartAt; i < shipColumnStartAt + shipSize; i++)
        {
            if (_builtGameBoardSquares![shipRow, i].HasGameShip)
            {
                return false;
            }
        }

        return true;
    }

    private bool CanShipBePlacedHorizontaly(int shipColumn, int shipRowStartAt, int shipSize)
    {
        for (var i = shipRowStartAt; i < shipRowStartAt + shipSize; i++)
        {
            if (_builtGameBoardSquares![i, shipColumn].HasGameShip)
            {
                return false;
            }
        }

        return true;
    }

    private void PlaceShipVerticaly(int shipRow, int shipColumnStartAt, IGameShip gameShip)
    {
        for (var i = shipColumnStartAt; i < shipColumnStartAt + gameShip.HealthPoints; i++)
        {
            _builtGameBoardSquares![shipRow, i] = new GameBoardSquare(gameShip);
        }
    }

    private void PlaceShipHorizontaly(int shipColumn, int shipRowStartAt, IGameShip gameShip)
    {
        for (var i = shipRowStartAt; i < shipRowStartAt + gameShip.HealthPoints; i++)
        {
            _builtGameBoardSquares![i, shipColumn] = new GameBoardSquare(gameShip);
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
