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

    public bool ContainsGameShip => _gameShip != null;

    public GameSquareStatus Status
    {
        get
        {
            if (!_hasAlreadyBeenShotAt)
            {
                return GameSquareStatus.NotShotAtYet;
            }

            return !ContainsGameShip ? GameSquareStatus.ShotAtAndMissed : GameSquareStatus.ShotAtAndHit;
        }
    }

    public ShotResult ShootAt()
    {
        if (_hasAlreadyBeenShotAt)
        {
            return ShotResult.SquareAlreadyShotAt;
        }

        if (!ContainsGameShip)
        {
            return ShotResult.ShotMissed;
        }

        _gameShip?.ShootAt();
        _hasAlreadyBeenShotAt = true;
        return _gameShip?.HealthPoints > 0 ? ShotResult.ShipHit : ShotResult.ShipSunk;
    }
}
