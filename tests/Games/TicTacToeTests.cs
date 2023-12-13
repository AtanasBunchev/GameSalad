using GameSalad.Games;

namespace GameSaladTests.Games;

public class TicTacToeTests
{
    [Fact]
    public void HasFinishedOutOfEmptyMovesTest()
    {
        TicTacToe game = new ();
        game.Grid = new char[3,3]
        {
            {'x', 'o', 'x'},
            {'x', 'o', 'o'},
            {'o', 'x', 'x'}
        };
        game.EmptyCells.Clear();
        Assert.True(game.HasFinished());
        Assert.False(game.DidPlayerWon());
    }

    [Fact]
    public void HasFinishedPlayerWonTest()
    {
        // Perhaps run more exhaustive tests with all win conditions
        // Currently gonna leave that to the consistency test below

        TicTacToe game = new ();
        game.Grid = new char[3,3]
        {
            {'x', 'o', 'x'},
            {' ', 'x', 'o'},
            {'o', ' ', 'x'}
        };
        game.EmptyCells = new List<int>{3, 7};
        Assert.True(game.HasFinished());
        Assert.True(game.DidPlayerWon());
    }

    [Fact]
    public void HasFinishedPlayerLostTest()
    {
        TicTacToe game = new ();
        game.Grid = new char[3,3]
        {
            {'o', ' ', 'x'},
            {'o', 'x', 'x'},
            {'o', ' ', ' '}
        };
        game.EmptyCells = new List<int>{1, 7, 8};
        Assert.True(game.HasFinished());
        Assert.False(game.DidPlayerWon());
    }

    [Fact]
    public void PlayerMoveMarksCellTell()
    {
        for (int i = 0; i < 9; i++)
        {
            TicTacToe game = new ();
            game.PlayMove($"{i}");
            Assert.True(game.Grid[i % 3,i / 3] == 'x');
            Assert.DoesNotContain(i, game.EmptyCells);
        }
    }

    [Fact]
    public void PlayerMoveResultsEnemyMoveTest()
    {
        for (int i = 0; i < 9; i++)
        {
            TicTacToe game = new ();
            game.PlayMove($"{i}");
            Assert.True(game.EmptyCells.Count == 7,
                "Expected 2 cells to be taken on PlayMove");

            int playerMoves = 0, enemyMoves = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if(game.Grid[y,x] == 'x')
                        playerMoves++;
                    if(game.Grid[y,x] == 'y')
                        enemyMoves++;
                }
            }

            Assert.Equal(1, playerMoves);
            Assert.Equal(1, enemyMoves);
        }
    }

    [Fact]
    public void GetStateAndSetStateConsistencyTest()
    {
        // Predefined numbers to use the |N|-th valid move
        // The rest iterations use random numbers
        List<List<int>> moves = new()
        {
            new() {5, 1, 20, 15, 26},
            new() {1, 7, 2, 52, 1},
            new() {44, 61, 23, 3},
            new() {15, 4, 23, 44}
        };

        Random rnd = new ();

        for (int i = 0; i < 10; i++)
        {
            TicTacToe game = new ();
            for (int j = 0; !game.HasFinished() && j < 5; j++)
            {
                var available = game.GetValidMoves();
                Assert.NotEmpty(available);
                int move;
                if (moves.Count < i && moves[i].Count < j)
                {
                    move = moves[i][j] % available.Count;
                }
                else
                {
                    move = rnd.Next(0, available.Count);
                }
                game.PlayMove(available[move]);

                // Test
                TicTacToe game2 = new ();
                var state = game.GetState();
                Assert.NotEmpty(state);
                game2.SetState(state);

                Assert.Equal(game.Grid, game2.Grid);
                Assert.Equal(game.EmptyCells, game2.EmptyCells);

                foreach (int cell in game2.EmptyCells)
                {
                    Assert.Equal(' ', game2.Grid[cell / 3, cell % 3]);
                }
            }
        }
    }

    [Fact]
    public void EnemyDoNotMakeMoveIfDefeatedTest()
    {
        TicTacToe game = new ();
        game.Grid = new char[3,3]
        {
            {'x', ' ', 'o'},
            {' ', 'x', 'o'},
            {' ', ' ', ' '}
        };
        game.EmptyCells = new List<int>{1, 3, 6, 7, 8};
        game.PlayMove("8");
        Assert.True(game.HasFinished());
        Assert.True(game.DidPlayerWon());

        int playerMoves = 0, enemyMoves = 0;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if(game.Grid[y,x] == 'x')
                    playerMoves++;
                if(game.Grid[y,x] == 'y')
                    enemyMoves++;
            }
        }

        Assert.Equal(3, playerMoves);
        Assert.Equal(2, enemyMoves);
    }
}
