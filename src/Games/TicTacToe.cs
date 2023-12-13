namespace GameSalad.Games;

public class TicTacToe : IGame
{
    public override string GetGameType() => "TicTacToe";

    public char[,] Grid = new char[3,3] {
        {' ', ' ', ' '},
        {' ', ' ', ' '},
        {' ', ' ', ' '}
    };
    public List<int> EmptyCells { get; set; } = new() {0, 1, 2, 3, 4, 5, 6, 7, 8};


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
