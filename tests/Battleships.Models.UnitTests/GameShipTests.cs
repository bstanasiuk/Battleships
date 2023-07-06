namespace Battleships.Models.UnitTests;

public class GameShipTests
{
    [Fact]
    public void ShootAt_WithShipHavingHealthPointsAboveZero_ReducesHealthPointsByOne()
    {
        const int healthPoints = 5;
        var gameShip = new GameShip(healthPoints);

        gameShip.ShootAt();

        gameShip.HealthPoints.Should().Be(healthPoints - 1);
    }

    [Fact]
    public void ShootAt_WithShipBeingShotMultipleTimesEqualToItsHp_ReducesHealthPointsToZero()
    {
        const int healthPoints = 3;
        var gameShip = new GameShip(healthPoints);

        for (var i = 0; i < healthPoints; i++)
        {
            gameShip.ShootAt();
        }

        gameShip.HealthPoints.Should().Be(0);
    }

    [Fact]
    public void ShootAt_WithShipHavingZeroHealthPoints_DoesNotReducesHealthPointsFurther()
    {
        const int healthPoints = 0;
        var gameShip = new GameShip(healthPoints);

        gameShip.ShootAt();

        gameShip.HealthPoints.Should().Be(0);
    }
}