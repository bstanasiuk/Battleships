using Battleships.Models;

namespace Battleships.UI;

public interface IConsoleUserInterfaceInput
{
    ShotCoordinates GetShotCoordinates();

    bool GetRestartConfirmation();
}
