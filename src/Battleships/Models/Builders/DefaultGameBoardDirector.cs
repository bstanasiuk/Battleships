namespace Battleships.Models.Builders;

public class DefaultGameBoardDirector : IDefaultGameBoardDirector
{
    private const int DefaultGameBoardSize = 10;
    private const int DefaultBattleshipHp = 5;
    private const int DefaultDestroyerHp = 4;
    private const int DefaultBattleshipsAmount = 1;
    private const int DefaultDestroyerHpAmount = 2;

    private readonly IRandomizedGameBoardBuilder _builder;

    public DefaultGameBoardDirector(IRandomizedGameBoardBuilder builder)
    {
        _builder = builder;
    }

    public IGameBoard BuildGameBoard()
    {
        return _builder
            .SetGameBoardSize(DefaultGameBoardSize)
            .SetBattleshipHp(DefaultBattleshipHp)
            .SetDestroyerHp(DefaultDestroyerHp)
            .SetBattleshipsAmount(DefaultBattleshipsAmount)
            .SetDestroyersAmount(DefaultDestroyerHpAmount)
            .Build();
    }
}
