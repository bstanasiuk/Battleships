using Battleships.Models.Enums;

namespace Battleships.Models.UnitTests;

public class GameBoardTests
{
    [Theory]
    [InlineData(-1, 0)]
    [InlineData(1, 0)]
    [InlineData(100, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 2)]
    [InlineData(0, 100)]
    [InlineData(100, 100)]
    public void ShootAt_WithCoordinatesOutOfRange_ThrowsArgumentOutOfRangeException(int row, int column)
    {
        var gameBoardSquares = new IGameBoardSquare[,]
        {
            { Mock.Of<IGameBoardSquare>(), Mock.Of<IGameBoardSquare>() }
        };

        var gameBoard = new GameBoard(
            new List<IGameShip> { Mock.Of<IGameShip>() },
            gameBoardSquares);

        gameBoard.Invoking(x => x.ShootAt(new ShotCoordinates(row, column)))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void AreAllShipsSunk_WithAllShipsBeingSunk_ReturnsTrue()
    {
        var firstGameShip = new Mock<IGameShip>();
        firstGameShip.Setup(x => x.HealthPoints).Returns(0);

        var secondGameShip = new Mock<IGameShip>();
        secondGameShip.Setup(x => x.HealthPoints).Returns(0);

        var gameBoardSquares = new IGameBoardSquare[,]
        {
            { Mock.Of<IGameBoardSquare>(), Mock.Of<IGameBoardSquare>() }
        };

        var gameBoard = new GameBoard(
            new List<IGameShip> { firstGameShip.Object, secondGameShip.Object },
            gameBoardSquares);

        gameBoard.AreAllShipsSunk.Should().Be(true);
    }

    [Fact]
    public void AreAllShipsSunk_WithNotAllShipsBeingSunk_ReturnsFalse()
    {
        var firstGameShip = new Mock<IGameShip>();
        firstGameShip.Setup(x => x.HealthPoints).Returns(2);

        var secondGameShip = new Mock<IGameShip>();
        secondGameShip.Setup(x => x.HealthPoints).Returns(0);

        var gameBoardSquares = new IGameBoardSquare[,]
        {
            { Mock.Of<IGameBoardSquare>(), Mock.Of<IGameBoardSquare>() }
        };

        var gameBoard = new GameBoard(
            new List<IGameShip> { firstGameShip.Object, secondGameShip.Object },
            gameBoardSquares);

        gameBoard.AreAllShipsSunk.Should().Be(false);
    }

    [Fact]
    public void ShootAt_WithCoordinatesInRange_ReturnsShotResult()
    {
        var gameBoardSquare = new Mock<IGameBoardSquare>();
        gameBoardSquare.Setup(x => x.ShootAt()).Returns(ShotResult.ShipHit);

        var gameBoardSquares = new IGameBoardSquare[,]
        {
            { gameBoardSquare.Object, Mock.Of<IGameBoardSquare>() }
        };

        var gameBoard = new GameBoard(
            new List<IGameShip> { Mock.Of<IGameShip>() },
            gameBoardSquares);

        var shotResult = gameBoard.ShootAt(new ShotCoordinates(0, 0));

        shotResult.Should().Be(ShotResult.ShipHit);
    }
}
