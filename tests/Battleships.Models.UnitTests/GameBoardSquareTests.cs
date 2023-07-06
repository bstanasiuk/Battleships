using Battleships.Models.Enums;

namespace Battleships.Models.UnitTests;

public class GameBoardSquareTests
{
    [Fact]
    public void HasGameShip_WithBoardSquareInitializedWithoutAShip_ReturnsFalse()
    {
        var gameSquare = new GameBoardSquare();

        gameSquare.HasGameShip.Should().BeFalse();
    }

    [Fact]
    public void HasGameShip_WithBoardSquareInitializedWithAShip_ReturnsTrue()
    {
        var gameShipMock = new Mock<IGameShip>();
        var gameSquare = new GameBoardSquare(gameShipMock.Object);

        gameSquare.HasGameShip.Should().BeTrue();
    }

    [Fact]
    public void Status_WithBoardSquareNotYetShotAt_ReturnsStatusNotShotAtYet()
    {
        var gameSquare = new GameBoardSquare();

        gameSquare.Status.Should().Be(GameSquareStatus.NotShotAtYet);
    }

    [Fact]
    public void Status_WithBoardSquareAlreadyShotAtAndHavingNonSunkShip_ReturnsStatusShotAtAndHit()
    {
        var gameShipMock = new Mock<IGameShip>();
        gameShipMock.Setup(x => x.HealthPoints).Returns(2);

        var gameSquare = new GameBoardSquare(gameShipMock.Object);

        gameSquare.ShootAt();

        gameSquare.Status.Should().Be(GameSquareStatus.ShotAtAndHit);
    }

    [Fact]
    public void Status_WithBoardSquareAlreadyShotAtAndHavingSunkShip_ReturnsStatusShotAtAndSank()
    {
        var gameShipMock = new Mock<IGameShip>();
        gameShipMock.Setup(x => x.HealthPoints).Returns(0);

        var gameSquare = new GameBoardSquare(gameShipMock.Object);

        gameSquare.ShootAt();

        gameSquare.Status.Should().Be(GameSquareStatus.ShotAtAndSank);
    }

    [Fact]
    public void Status_WithBoardSquareAlreadyShotAtAndNotHavingAShip_ReturnsStatusShotAtAndMiss()
    {
        var gameSquare = new GameBoardSquare();

        gameSquare.ShootAt();

        gameSquare.Status.Should().Be(GameSquareStatus.ShotAtAndMissed);
    }

    [Fact]
    public void ShootAt_WithBoardSquareAlreadyShotAt_ReturnsShotResultSquareAlreadyShotAt()
    {
        var gameSquare = new GameBoardSquare();

        gameSquare.ShootAt();
        var shotResult = gameSquare.ShootAt();

        shotResult.Should().Be(ShotResult.SquareAlreadyShotAt);
    }

    [Fact]
    public void ShootAt_WithBoardSquareNotYetShotAtAndHavingNonSunkShip_ReturnsShotResultShipHit()
    {
        var gameShipMock = new Mock<IGameShip>();
        gameShipMock.Setup(x => x.HealthPoints).Returns(5);

        var gameSquare = new GameBoardSquare(gameShipMock.Object);

        var shotResult = gameSquare.ShootAt();

        shotResult.Should().Be(ShotResult.ShipHit);
    }

    [Fact]
    public void ShootAt_WithBoardSquareNotYetShotAtAndHavingSunkShip_ReturnsShotResultShipSank()
    {
        var gameShipMock = new Mock<IGameShip>();
        gameShipMock.Setup(x => x.HealthPoints).Returns(0);

        var gameSquare = new GameBoardSquare(gameShipMock.Object);

        var shotResult = gameSquare.ShootAt();

        shotResult.Should().Be(ShotResult.ShipSank);
    }

    [Fact]
    public void ShootAt_WithBoardSquareNotYetShotAtAndNotHavingAShip_ReturnsShotResultShotMissed()
    {
        var gameSquare = new GameBoardSquare();

        var shotResult = gameSquare.ShootAt();

        shotResult.Should().Be(ShotResult.ShotMissed);
    }
}
