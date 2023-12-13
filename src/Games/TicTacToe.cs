namespace GameSalad.Games;

public class TicTacToe : IGame
{
    public override string GetGameType() => "TicTacToe";



    /* State modifications */

    public override List<string> GetValidMoves()
    {
        return new();
    }

    public override void PlayMove(string move)
    {

    }


    /* State Checks */

    public override bool HasFinished()
    {
        return false;
    }

    public override bool DidPlayerWon()
    {
        return false;
    }


    /* Storage */
    public override string GetState()
    {
        return "";
    }

    public override void SetState(string data)
    {
    }

}
