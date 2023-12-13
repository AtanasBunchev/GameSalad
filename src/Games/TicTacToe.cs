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

    private char winner = ' '; // set to winner if game finishes

    /* State modifications */

    public override List<string> GetValidMoves()
    {
        if (HasFinished())
            return new();

        return EmptyCells.Select(i => $"{i}").ToList();
    }

    public override void PlayMove(string move)
    {
        // player move
        int pos = int.Parse(move); // Controller only sends valid moves
        Grid[pos / 3, pos % 3] = 'x';
        EmptyCells.Remove(pos);

        if (HasFinished())
            return;

        // enemy move
        Random rng = new ();
        int pick = rng.Next(0, EmptyCells.Count);
        pos = EmptyCells[pick];
        Grid[pos / 3, pos % 3] = 'o';
        EmptyCells.Remove(pos);
    }


    /* State Checks */

    public override bool HasFinished()
    {
        char winnerSymbol = ' ';
        for (int i = 0; i < 3; i++)
        {
            // Check Rows
            if (Grid[0, i] != ' ')
                if (Grid[0, i] == Grid[1, i])
                    if (Grid[1, i] == Grid[2, i])
                        winnerSymbol = Grid[0, i];

            // Check Cols
            if (Grid[i, 0] != ' ')
                if (Grid[i, 0] == Grid[i, 1])
                    if (Grid[i, 1] == Grid[i, 2])
                        winnerSymbol = Grid[i, 0];
        }

        // Check main diagonal
        if (Grid[0, 0] != ' ')
            if (Grid[0, 0] == Grid[1, 1])
                if (Grid[1, 1] == Grid[2, 2])
                    winnerSymbol = Grid[0, 0];

        // Check secondary diagonal
        if (Grid[2, 0] != ' ')
            if (Grid[2, 0] == Grid[1, 1])
                if (Grid[1, 1] == Grid[0, 2])
                    winnerSymbol = Grid[2, 0];

        if (winnerSymbol == ' ')
        {
            return EmptyCells.Count == 0;
        }
        else
        {
            winner = winnerSymbol;
            return true;
        }
    }

    public override bool DidPlayerWon()
    {
        if (winner == ' ')
            if (!HasFinished())
                return false;

        return winner == 'x';
    }


    /* Storage */
    public override string GetState()
    {
        var output = "";
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                output += Grid[y,x];
            }
        }
        return output;
    }

    public override void SetState(string data)
    {
        Grid = new char[3,3] {
            {' ', ' ', ' '},
            {' ', ' ', ' '},
            {' ', ' ', ' '}
        };
        EmptyCells = new List<int>();

        this.winner = ' ';

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Grid[y,x] = data[y * 3 + x];
                if(Grid[y,x] == ' ')
                    EmptyCells.Add(y * 3 + x);
            }
        }
    }

}
