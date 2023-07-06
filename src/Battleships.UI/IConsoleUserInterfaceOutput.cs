using Battleships.Models;
using Battleships.Models.Enums;

namespace Battleships.UI;

public interface IConsoleUserInterfaceOutput
{
    void DisplayWelcomeMessage();

    void DisplayGameBoard(IGameBoard gameBoard);

    void DisplayShotResultMessage(ShotResult shotResult);
}
