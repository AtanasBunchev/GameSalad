#nullable disable

namespace GameSalad.Games;

public interface IGame
{
    public abstract string Type { get; }

    public string SaveState();
    public void LoadState(string state);

    public void Action(string act);
    public List<string> GetValidActions();

    public bool IsFinished();
};
