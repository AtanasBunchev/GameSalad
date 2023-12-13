namespace GameSalad.Games;

public abstract class IGame
{
    public abstract string GetGameType();

    public abstract string GetState();
    public abstract void SetState(string data);

    public abstract void PlayMove(string move);
    public abstract List<string> GetValidMoves();

    public abstract bool HasFinished();
    public abstract bool DidPlayerWon();
}
