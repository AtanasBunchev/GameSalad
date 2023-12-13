using GameSalad.Games;

namespace GameSaladTests.Games;

public class MockGame : IGame
{
    /* Interface */

    public override string GetGameType() => "MockGame";

    public override string GetState() => State;
    public override void SetState(string data)
    {
        this.State = data;
    }

    public override void PlayMove(string move)
    {
        this.PlayedMoves.Add(move);
    }
    public override List<string> GetValidMoves() => ValidMoves;

    public override bool HasFinished() => Finished;
    public override bool DidPlayerWon() => PlayerWon;


    /* Mock Data */

    public string State = "";
    public List<string> PlayedMoves = new();
    public List<string> ValidMoves = new();
    public bool Finished = false;
    public bool PlayerWon = false;

}
