using Battleships.Models.Builders;

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

        do
        {
            RunNewGame();
        }
        while (_userInterfaceInput.GetRestartConfirmation());
    }

    private void RunNewGame()
    {
        var gameBoard = _randomizedGameBoardDirector.BuildNew();
        _userInterfaceOutput.DisplayGameBoard(gameBoard);

        while (!gameBoard.AreAllShipsSunk)
        {
            var shotCoordinates = _userInterfaceInput.GetShotCoordinates();
            var shotResult = gameBoard.ShootAt(shotCoordinates);

            _userInterfaceOutput.DisplayShotResultMessage(shotResult);
            _userInterfaceOutput.DisplayGameBoard(gameBoard);
        }
    }
}
