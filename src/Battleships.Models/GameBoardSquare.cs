using Battleships.Models.Enums;

namespace Battleships.Models;

public class GameBoardSquare : IGameBoardSquare
{
    private readonly IGameShip? _gameShip;
    private bool _hasAlreadyBeenShotAt;

    public GameBoardSquare(IGameShip? gameShip = null)
    {
        _gameShip = gameShip;
        _hasAlreadyBeenShotAt = false;
    }

    public bool HasGameShip => _gameShip != null;

    public GameSquareStatus Status
    {
        get
        {
            if (!_hasAlreadyBeenShotAt)
            {
                return GameSquareStatus.NotShotAtYet;
            }

            if (HasGameShip)
            {
                return _gameShip?.HealthPoints > 0 ? GameSquareStatus.ShotAtAndHit : GameSquareStatus.ShotAtAndSunk;
            }

            return GameSquareStatus.ShotAtAndMissed;
        }
    }

    public ShotResult ShootAt()
    {
        if (_hasAlreadyBeenShotAt)
        {
            return ShotResult.SquareAlreadyShotAt;
        }

        _hasAlreadyBeenShotAt = true;

        if (!HasGameShip)
        {
            return ShotResult.ShotMissed;
        }

        _gameShip?.ShootAt();

        return _gameShip?.HealthPoints > 0 ? ShotResult.ShipHit : ShotResult.ShipSank;
    }
}
