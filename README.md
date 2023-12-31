![image](https://github.com/bstanasiuk/Battleships/assets/20246523/0094feac-3aeb-4016-b0f2-04d50ae14a31)
# Battleships

This project is an implementation of a recruitment task for Guestline. The challenge was to program a simple version of the game Battleships, in which a single human player can run a one-sided game of Battleships against ships placed by the computer.
The program creates a 10x10 grid and places several ships on the grid at random with the following sizes: 1x Battleship (5 squares), 2x Destroyers (4 squares).
The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses, or sinks. The game ends when all ships are sunk.
Read more details about the game requirements here: https://medium.com/guestline-labs/hints-for-our-interview-process-and-code-test-ae647325f400.

## How to run

### Run with Dotnet CLI
1. Install .NET 6 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
2. In a terminal go into the project's main location
3. Run command `dotnet publish --output ./Build`
4. Go into `./Build` location
5. Run `Battleships.exe`

### Run with Docker
1. Install Docker Desktop on your machine: https://www.docker.com/products/docker-desktop/
2. In a terminal go into the project's main location
3. Build an image: `docker build -t battleships .`
4. Run the image: `docker run -it battleships`

## Notes

- The app uses [Spectre.Console](https://github.com/spectreconsole/spectre.console) package for console UI formatting. The game uses a couple of colors to make the output more readable, for the best readability use a black background in your terminal.
- The app has a configurable json file where you can set the game parameters. The game board by default uses a 10x10 grid but that can be expanded to even 26x26, or you can change a ship size or amount. Make your changes here: [appsettings.json](./src/Battleships/appsettings.json).
The full list of configurable parameters:

  - `GameBoardSize` (the size of the game board grid)
  - `BattleshipHp` (the size of the battleship)
  - `DestroyerHp` (the size of the destroyer)
  - `BattleshipsAmount` (how many battleships will be placed on the board)
  - `DestroyersAmount` (how many destroyers will be placed on the board)
