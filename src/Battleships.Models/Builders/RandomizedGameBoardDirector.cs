using Battleships.Configuration;
using Microsoft.Extensions.Options;

namespace Battleships.Models.Builders;

public class RandomizedGameBoardDirector : IRandomizedGameBoardDirector
{
    private readonly GameSettings _gameSettings;
    private readonly IRandomizedGameBoardBuilder _builder;

    public RandomizedGameBoardDirector(IOptions<GameSettings> gameSettings, IRandomizedGameBoardBuilder builder)
    {
        _gameSettings = gameSettings.Value;
        _builder = builder;
    }

    public IGameBoard BuildNew()
    {
        return _builder
            .SetGameBoardSize(_gameSettings.GameBoardSize)
            .SetBattleshipHp(_gameSettings.BattleshipHp)
            .SetDestroyerHp(_gameSettings.DestroyerHp)
            .SetBattleshipsAmount(_gameSettings.BattleshipsAmount)
            .SetDestroyersAmount(_gameSettings.DestroyersAmount)
            .Build();
    }
}
