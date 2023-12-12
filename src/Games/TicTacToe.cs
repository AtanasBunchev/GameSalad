namespace GameSalad.Games;

public class TicTacToe : IGame
{
    public string Type { get; } = "TicTacToe";

    public char[,] State = new char[3,3];

    public TicTacToe()
    {
        for(int y = 0; y < 3; y++)
            for(int x = 0; x < 3; x++)
                State[y, x] = ' ';
    }

    public string SaveState()
    {
        string state = "";
        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                state += State[y, x];
            }
        }
        return state;
    }

    public void LoadState(string state)
    {
        if(state.Length != 9)
            return;

        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                State[y, x] = state[y * 3 + x];
            }
        }
    }

    public void Action(string act) // place X at cell position (0-8)
    {
        if(!int.TryParse(act, out int pos))
            return;
        if(IsFinished())
            return;

        if(pos < 0)
            pos = 0;
        pos = pos % 9;

        int x = pos % 3;
        int y = pos / 3;
        if(State[y, x] == ' ')
            State[y, x] = 'x';
        else
            return;

        if(IsFinished())
            return;

        pos = ((37 + pos) * (x * 13) * (y + 1) / 4) % 9;
        while(State[y, x] != ' ')
        {
            pos = (pos + 1) % 9;
            x = pos % 3;
            y = pos / 3;
        }
        State[y, x] = 'o';
    }

    public List<string> GetValidActions()
    {
        var actions = new List<string>();
        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                if(State[y, x] == ' ')
                {
                    actions.Add((x + y * 3).ToString());
                }
            }
        }

        if(actions.Count() == 0)
            return actions;

        if(IsFinished())
            return new List<string>();

        return actions;
    }

    public bool IsFinished()
    {
        // Lines
        for(int i = 0; i < 3; i++)
        {
            if(State[i, 0] != ' ')
            if(State[i, 0] == State[i, 1])
            if(State[i, 1] == State[i, 2])
                return true;
            if(State[0, i] != ' ')
            if(State[0, i] == State[1, i])
            if(State[1, i] == State[2, i])
                return true;
        }

        // Columns
        if(State[0, 0] != ' ')
        if(State[0, 0] == State[1, 1])
        if(State[1, 1] == State[2, 2])
            return true;

        if(State[2, 0] != ' ')
        if(State[2, 0] == State[1, 1])
        if(State[1, 1] == State[0, 2])
            return true;

        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                if(State[y, x] == ' ')
                {
                    return false;
                }
            }
        }

        return true;
    }
}
