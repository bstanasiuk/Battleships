using System.Text.RegularExpressions;
using Battleships.Configuration;
using Battleships.Models;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace Battleships.UI;

public class ConsoleUserInterfaceInput : IConsoleUserInterfaceInput
{
    private const string ShotCoordinatesInputRegex = "^([A-Z])([0-9]{1,2})$";
    private const int AlphabetCharStartingPoint = 64;

    private readonly GameSettings _gameSettings;

    public ConsoleUserInterfaceInput(IOptions<GameSettings> gameSettings)
    {
        _gameSettings = gameSettings.Value;
    }

    public ShotCoordinates GetShotCoordinates()
    {
        var unparsedShotCoordinates = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter shot coordinates (in a format of “A5”):")
                .PromptStyle("green")
                .Validate(ValidateCoordinatesInput));

        var regexMatch = Regex.Match(unparsedShotCoordinates, ShotCoordinatesInputRegex, RegexOptions.IgnoreCase);
        var adjustedRow = GetRowNumberFromRegexMatch(regexMatch) - 1;
        var adjustedColumn = GetColumnNumberFromRegexMatch(regexMatch) - 1;

        return new ShotCoordinates(adjustedRow, adjustedColumn);
    }

    public bool GetRestartConfirmation()
    {
        return AnsiConsole.Confirm("All ships are sunk, congratulations! Do you want to restart?");
    }

    private ValidationResult ValidateCoordinatesInput(string coordinatesString)
    {
        var regexMatch = Regex.Match(coordinatesString, ShotCoordinatesInputRegex, RegexOptions.IgnoreCase);
        if (!regexMatch.Success)
        {
            return ValidationResult.Error("[red]You must enter coordinates in a format of “A5”[/]");
        }

        var column = GetColumnNumberFromRegexMatch(regexMatch);
        if (column < 1 || column > _gameSettings.GameBoardSize)
        {
            return ValidationResult.Error(
                $"[red]Column number must be between A and {(char) (_gameSettings.GameBoardSize + AlphabetCharStartingPoint)}[/]");
        }

        var row = GetRowNumberFromRegexMatch(regexMatch);
        if (row < 1 || row > _gameSettings.GameBoardSize)
        {
            return ValidationResult.Error($"[red]Row number must be between 1 and {_gameSettings.GameBoardSize}[/]");
        }

        return ValidationResult.Success();
    }

    private int GetColumnNumberFromRegexMatch(Match regexMatch)
    {
        return regexMatch.Groups[1].Value.ToUpper()[0] - AlphabetCharStartingPoint;
    }

    private int GetRowNumberFromRegexMatch(Match regexMatch)
    {
        return int.Parse(regexMatch.Groups[2].Value);
    }
}
