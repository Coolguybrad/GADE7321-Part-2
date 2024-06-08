using System.Collections.Generic;

public static class GameLogic
{
    public static List<Move> GetLegalMoves(GameState state)
    {
        // Implement logic to generate all legal moves from the current game state
    }

    public static GameState ApplyMove(GameState state, Move move)
    {
        // Implement logic to apply a move and return the new game state
    }

    public static bool IsTerminal(GameState state)
    {
        // Implement logic to check if the game state is terminal (win/loss/draw)
    }

    public static double EvaluateTerminalState(GameState state)
    {
        // Implement logic to evaluate the terminal state (e.g., win = 1, loss = -1, draw = 0)
    }
}
