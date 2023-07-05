using Battleships.Models.Enums;

namespace Battleships.UI;

public interface IConsoleUserInterfaceOutput
{
    void DisplayWelcomeMessage();

    void DisplayGameSquareStatuses(GameSquareStatus[,] gameSquareStatuses);

    void DisplayShotResultMessage(ShotResult shotResult);
}
