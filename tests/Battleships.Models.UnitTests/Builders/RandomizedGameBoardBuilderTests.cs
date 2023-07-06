using Battleships.Common;
using Battleships.Models.Builders;

namespace Battleships.Models.UnitTests.Builders;

public class RandomizedGameBoardBuilderTests
{
    [Fact]
    public void Build_WithGameBoardSizeEqualToZero_ThrowsArgumentException()
    {
        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>());

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetGameBoardSize(0);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Game board size must be larger than 0");
    }

    [Fact]
    public void Build_WithBattleshipHpEqualToZero_ThrowsArgumentException()
    {
        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>());

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetBattleshipHp(0);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Battleship hp must be larger than 0");
    }

    [Fact]
    public void Build_WithDestroyerHpEqualToZero_ThrowsArgumentException()
    {
        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>());

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetDestroyerHp(0);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Destroyer hp must be larger than 0");
    }

    [Fact]
    public void Build_WithBattleshipHpLargerThanGameBoardSize_ThrowsArgumentException()
    {
        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>());

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetBattleshipHp(11)
            .SetGameBoardSize(10);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Battleship hp must be smaller than the game board size");
    }

    [Fact]
    public void Build_WithDestroyerHpLargerThanGameBoardSize_ThrowsArgumentException()
    {
        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>());

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetDestroyerHp(11)
            .SetGameBoardSize(10);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Destroyer hp must be smaller than the game board size");
    }

    [Fact]
    public void Build_WithValidBattleshipsAmountAndHp_ReturnsRandomizedGameBoardWithSetBattleshipsAmount()
    {
        const int battleshipHp = 2;
        const int battlehipsAmount = 2;

        var randomGeneratorMock = new Mock<IRandomGenerator>();
        randomGeneratorMock.SetupSequence(x => x.Next(It.IsAny<int>()))
            .Returns(0).Returns(0)
            .Returns(1).Returns(1);

        var gameBoardBuilder = new RandomizedGameBoardBuilder(randomGeneratorMock.Object);
        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetBattleshipHp(battleshipHp)
            .SetBattleshipsAmount(battlehipsAmount)
            .SetDestroyersAmount(0);

        var gameBoard = gameBoardBuilder.Build();

        gameBoard.GameShips.Where(x => x.HealthPoints == battleshipHp).Count()
            .Should().Be(battlehipsAmount);
    }

    [Fact]
    public void Build_WithValidDestroyersAmountAndHp_ReturnsRandomizedGameBoardWithSetDestroyersAmount()
    {
        const int destroyerHp = 1;
        const int destroyersAmount = 2;

        var randomGeneratorMock = new Mock<IRandomGenerator>();
        randomGeneratorMock.SetupSequence(x => x.Next(It.IsAny<int>()))
            .Returns(0).Returns(0)
            .Returns(1).Returns(1);
        var gameBoardBuilder = new RandomizedGameBoardBuilder(randomGeneratorMock.Object);

        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetDestroyerHp(destroyerHp)
            .SetDestroyersAmount(destroyersAmount)
            .SetBattleshipsAmount(0);

        var gameBoard = gameBoardBuilder.Build();

        gameBoard.GameShips.Where(x => x.HealthPoints == destroyerHp).Count()
            .Should().Be(destroyersAmount);
    }

    [Fact]
    public void Build_WithValidBuildParameters_ReturnsGameBoardWithSquaresHavingHorizontalShip()
    {
        const bool isHorizontal = true;
        var randomGeneratorMock = new Mock<IRandomGenerator>();
        randomGeneratorMock.SetupSequence(x => x.Next(It.IsAny<int>()))
            .Returns(1).Returns(1);
        randomGeneratorMock.Setup(x => x.NextBool())
            .Returns(isHorizontal);

        var gameBoardBuilder = new RandomizedGameBoardBuilder(randomGeneratorMock.Object);
        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetBattleshipsAmount(1)
            .SetDestroyersAmount(0)
            .SetBattleshipHp(2)
            .SetGameBoardSize(3);

        var gameBoard = gameBoardBuilder.Build();

        gameBoard.GameBoardSquares[0, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[0, 1].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[0, 2].HasGameShip.Should().Be(false);

        gameBoard.GameBoardSquares[1, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[1, 1].HasGameShip.Should().Be(true);
        gameBoard.GameBoardSquares[1, 2].HasGameShip.Should().Be(true);

        gameBoard.GameBoardSquares[2, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[2, 1].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[2, 2].HasGameShip.Should().Be(false);
    }

    [Fact]
    public void Build_WithValidBuildParameters_ReturnsGameBoardWithSquaresHavingVerticalShip()
    {
        const bool isHorizontal = false;
        var randomGeneratorMock = new Mock<IRandomGenerator>();
        randomGeneratorMock.SetupSequence(x => x.Next(It.IsAny<int>()))
            .Returns(1).Returns(1);
        randomGeneratorMock.Setup(x => x.NextBool())
            .Returns(isHorizontal);

        var gameBoardBuilder = new RandomizedGameBoardBuilder(randomGeneratorMock.Object);
        SetDefaultValidTestParameters(gameBoardBuilder)
            .SetBattleshipsAmount(1)
            .SetDestroyersAmount(0)
            .SetBattleshipHp(2)
            .SetGameBoardSize(3);

        var gameBoard = gameBoardBuilder.Build();

        gameBoard.GameBoardSquares[0, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[0, 1].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[0, 2].HasGameShip.Should().Be(false);

        gameBoard.GameBoardSquares[1, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[1, 1].HasGameShip.Should().Be(true);
        gameBoard.GameBoardSquares[1, 2].HasGameShip.Should().Be(false);

        gameBoard.GameBoardSquares[2, 0].HasGameShip.Should().Be(false);
        gameBoard.GameBoardSquares[2, 1].HasGameShip.Should().Be(true);
        gameBoard.GameBoardSquares[2, 2].HasGameShip.Should().Be(false);
    }

    [Fact]
    public void Build_WithSetParametersThatMakePlacingShipsRandomlyImpossible_ThrowsArgumentException()
    {
        const int maxTriesToPlaceShipRandomly = 5;

        var gameBoardBuilder = new RandomizedGameBoardBuilder(Mock.Of<IRandomGenerator>(), maxTriesToPlaceShipRandomly);
        SetDefaultValidTestParameters(gameBoardBuilder);

        gameBoardBuilder.Invoking(x => x.Build())
            .Should().Throw<ArgumentException>()
            .WithMessage("Ships cannot be placed randomly on the board with set parameters");
    }

    private IRandomizedGameBoardBuilder SetDefaultValidTestParameters(IRandomizedGameBoardBuilder randomGenerator)
    {
        return randomGenerator
            .SetGameBoardSize(10)
            .SetBattleshipHp(5)
            .SetDestroyerHp(4)
            .SetBattleshipsAmount(1)
            .SetDestroyersAmount(2);
    }
}
