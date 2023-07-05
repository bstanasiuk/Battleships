using Battleships.Configuration;
using Battleships.Models.Enums;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace Battleships.UI;

public class ConsoleUserInterfaceOutput : IConsoleUserInterfaceOutput
{
    private readonly GameSettings _gameSettings;

    public ConsoleUserInterfaceOutput(IOptions<GameSettings> gameSettings)
    {
        _gameSettings = gameSettings.Value;
    }

    public void DisplayWelcomeMessage()
    {
        AnsiConsole.Write(
            new FigletText("Battleships")
                .LeftJustified()
                .Color(Color.Blue));

        AnsiConsole.WriteLine();

        AnsiConsole.Write(
            new Markup(
                "Welcome to Battleships! A simple version of the game " +
                "where you play a one-sided game against ships placed randomly by the computer. " +
                "You will enter coordinates in a form of “A5”, " +
                "where “A” is the column and “5” is the row, to specify a square to target. " +
                "Shots result in hits, misses or sinks. The game ends when all ships are sunk. Let's start!"));

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
    }

    public void DisplayGameSquareStatuses(GameSquareStatus[,] gameSquareStatuses)
    {
        AnsiConsole.Write(new Markup(
            "Current state of the game ([white]“M”[/] means Miss, [blue]“H”[/] means Hit, [red]“S”[/] means Sunk):"));
        AnsiConsole.WriteLine();

        var gameBoardTable = new Table();
        AddHeaderColumnsToDisplayTable(gameBoardTable);
        AddGameStatusRowsToDisplayTable(gameBoardTable, gameSquareStatuses);

        AnsiConsole.Write(gameBoardTable);
    }

    public void DisplayShotResultMessage(ShotResult shotResult)
    {
        var shotMessage = shotResult switch
        {
            ShotResult.SquareAlreadyShotAt => "[red]Cannot shoot at the same square twice![/]",
            ShotResult.ShotMissed => "It's a [white]Miss[/]!",
            ShotResult.ShipHit => "It's a [blue]Hit[/]!",
            ShotResult.ShipSank => "It's a hit, the ship is [red]Sunk[/]!",
            ShotResult.AllShipsSank => "It's a hit, all ships are [red]Sunk[/]!",
            _ => ""
        };

        AnsiConsole.Write(new Markup(shotMessage));
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
    }

    private void AddHeaderColumnsToDisplayTable(Table table)
    {
        table.AddColumn(new TableColumn("").Centered());
        for (var i = 0; i < _gameSettings.GameBoardSize; i++)
        {
            table.AddColumn(new TableColumn(new Panel($"[bold]{(char) (i + 65)}[/]")).Centered());
        }
    }

    private void AddGameStatusRowsToDisplayTable(Table table, GameSquareStatus[,] gameSquareStatuses)
    {
        for (var i = 0; i < gameSquareStatuses.GetLength(0); i++)
        {
            var rowIdentifierPanel = new Panel($"[bold]{i + 1}[/]");
            var renderableRow = new List<Panel> { rowIdentifierPanel };

            for (var j = 0; j < gameSquareStatuses.GetLength(1); j++)
            {
                var squareMarkupValue = gameSquareStatuses[i, j] switch
                {
                    GameSquareStatus.NotShotAtYet => " ",
                    GameSquareStatus.ShotAtAndMissed => "[white]M[/]",
                    GameSquareStatus.ShotAtAndHit => "[blue]H[/]",
                    GameSquareStatus.ShotAtAndSunk => "[red]S[/]",
                    _ => " "
                };

                renderableRow.Add(new Panel(squareMarkupValue));
            }

            table.AddRow(renderableRow);
        }
    }
}
