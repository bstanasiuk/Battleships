namespace Battleships.Models.Builders
{
    public interface IRandomizedGameBoardBuilder
    {
        IRandomizedGameBoardBuilder SetGameBoardSize(int gameBoardSize);

        IRandomizedGameBoardBuilder SetBattleshipHp(int battleshipHp);

        IRandomizedGameBoardBuilder SetDestroyerHp(int destroyerHp);

        IRandomizedGameBoardBuilder SetBattleshipsAmount(int battleshipsAmount);

        IRandomizedGameBoardBuilder SetDestroyersAmount(int destroyersAmount);

        IGameBoard Build();
    }
}
