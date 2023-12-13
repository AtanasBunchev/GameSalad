namespace GameSalad.Games;

public class TicTacToe : IGame
{
    public override string GetGameType() => "TicTacToe";


    public override string GetState()
    {
        return "";
    }

    public override void SetState(string data)
    {
    }


    public override List<string> GetValidMoves()
    {
        return new();
    }
    public override void PlayMove(string move)
    {
    }

    public override bool HasFinished()
    {
        return false;
    }

    public override bool DidPlayerWon()
    {
        return false;
    }
}
