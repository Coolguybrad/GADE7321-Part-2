using UnityEngine;

public class AIController : MonoBehaviour
{
    public float maxThinkTime = 1.0f;
    public GameState currentGameState;

    public void AIPlay()
    {
        Move bestMove = MCTS.FindBestMove(currentGameState, maxThinkTime);
        currentGameState = GameLogic.ApplyMove(currentGameState, bestMove);
        // Update the game UI and state accordingly
    }
}
