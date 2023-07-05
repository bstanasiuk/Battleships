using Battleships.Models.Builders;
using Battleships.Models.Enums;

namespace Battleships.UI;

public class ConsoleGameManager : IConsoleGameManager
{
    private readonly IConsoleUserInterfaceOutput _userInterfaceOutput;
    private readonly IConsoleUserInterfaceInput _userInterfaceInput;
    private readonly IRandomizedGameBoardDirector _randomizedGameBoardDirector;

    public ConsoleGameManager(
        IConsoleUserInterfaceOutput userInterfaceOutput,
        IConsoleUserInterfaceInput userInterfaceInput,
        IRandomizedGameBoardDirector randomizedGameBoardDirector)
    {
        _userInterfaceOutput = userInterfaceOutput;
        _userInterfaceInput = userInterfaceInput;
        _randomizedGameBoardDirector = randomizedGameBoardDirector;
    }

    public void StartGame()
    {
        _userInterfaceOutput.DisplayWelcomeMessage();

        var shouldLoopNewGame = true;
        while (shouldLoopNewGame)
        {
            RunNewGame();
            shouldLoopNewGame = _userInterfaceInput.GetRestartConfirmation();
        }
    }

    private void RunNewGame()
    {
        var gameBoard = _randomizedGameBoardDirector.BuildNew();

        var allShipsSunk = false;
        while (!allShipsSunk)
        {
            _userInterfaceOutput.DisplayGameSquareStatuses(gameBoard.GameSquareStatuses);

            var shotCoordinates = _userInterfaceInput.GetShotCoordinates();
            var shotResult = gameBoard.ShootAt(shotCoordinates);

            _userInterfaceOutput.DisplayShotResultMessage(shotResult);

            if (shotResult == ShotResult.AllShipsSank)
            {
                allShipsSunk = true;
            }
        }
    }
}
