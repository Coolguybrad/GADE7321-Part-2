using UnityEngine;

public class MiniMaxClass : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private TurnHandler turnHandler;

    [SerializeField] private Piece pAI;

    public float minimaxAlg(float boardEval, int depth, float alpha, float beta, bool isMax)
    {
        if(depth == 0 || turnHandler.teamTurn == 2)
        {
            return Evaluate(pAI, boardManager.redGoal);
        }

        if(isMax)
        {
            float maxEval = float.NegativeInfinity;

            foreach (Piece piece in pieceManager.getRedArr())
            {
                try
                {
                    float eval = minimaxAlg(Evaluate(piece, boardManager.redGoal), depth - 1, alpha, beta, false);
                    maxEval = Mathf.Max(maxEval, eval);
                    //alpha = Mathf.Max(alpha, eval);
                    //if (beta <= alpha)
                    //{
                    //    break;
                    //}
                }
                catch
                {

                }
            }
            return maxEval;
        }
        else
        {
            float minEval = float.PositiveInfinity;

            foreach (Piece piece in pieceManager.getRedArr())
            {
                try
                {
                    float eval = minimaxAlg(Evaluate(piece, boardManager.redGoal), depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    //beta = Mathf.Min(beta, eval);
                    //if (beta >= alpha)
                    //{
                    //    break;
                    //}
                }
                catch
                {

                }
            }

            return minEval;
        }
    }

    public float Evaluate(Piece pieceAI, Tile tile)
    {
        Debug.Log(calcDistance(pieceAI.getLocation(), tile.getLocation()));
        return calcMaterial() - calcDistance(pieceAI.getLocation(), tile.getLocation());
    }

    private int calcMaterial()
    {
        int score = 0;
        for (int i = 0; i < pieceManager.getBlueArr().Length; i++)
        {
            if (pieceManager.getRedArr()[i] != null)
            {
                score += pieceManager.getRedArr()[i].getPowerVal();
            }
            if (pieceManager.getBlueArr()[i] != null)
            {
                score -= pieceManager.getBlueArr()[i].getPowerVal();
            }
        }
        return score;
    }

    public float calcDistance(Vector3 startPos, Vector3 targetPos)
    {        
        return Mathf.Sqrt(Mathf.Pow(targetPos.x - startPos.x, 2) + Mathf.Pow(targetPos.z - startPos.z, 2));
    }
}

//======================================ver1==========================================

//using UnityEngine;
//using System.Collections.Generic;
//using System;

//public class minimax : MonoBehaviour
//{
//    [SerializeField] private BoardManager boardManager;
//    [SerializeField] private PieceManager pieceManager;
//    [SerializeField] private TurnHandler turnHandler;

//    [SerializeField] private Piece pAI;
//    [SerializeField] private Piece pP;
//    [SerializeField] private Tile tile;
//    public (float, Vector3) Minimax(Tile[] location, int depth, bool isMax, Piece pieceAI)
//    {
//        if (depth == 0 || turnHandler.teamTurn != 2)
//        {
//            return (Evaluate(pAI, pP, tile), Vector3.zero);
//        }

//        if (isMax)
//        {
//            float maximum = float.NegativeInfinity;
//            Vector3 bestMove = new Vector3();
//            for (int i = 0; i < boardManager.getPossibleMoves(pieceAI).Length; i++)
//            {
//                float eval = Minimax(boardManager.getPossibleMoves(pieceAI), depth-1, false, pieceAI).Item1;
//                if (eval > maximum)
//                {
//                    maximum = eval;
//                    bestMove = boardManager.getPossibleMoves(pieceAI)[i].getLocation();
//                }
//            }
//            return (maximum, bestMove);
//        }
//        else
//        {
//            float minimum = float.PositiveInfinity;
//            Vector3 bestMove = new Vector3();
//            for (int i = 0; i < boardManager.getPossibleMoves(pieceAI).Length; i++)
//            {
//                float eval = Minimax(boardManager.getPossibleMoves(pieceAI), depth - 1, true, pieceAI).Item1;
//                if (eval < minimum)
//                {
//                    minimum = eval;
//                    bestMove = boardManager.getPossibleMoves(pieceAI)[i].getLocation();
//                }
//            }
//            return (minimum, bestMove);
//        }
//    }

//    public void setAIPiece(Piece p) 
//    {
//        pAI = p;
//    }

//    public void setPlayerPiece(Piece p) 
//    {
//        pP = p;
//    }

//    public void setTile(Tile t) 
//    {
//        tile = t;
//    }

//    private float Evaluate(Piece pieceAI, Piece piecePlayer, Tile tile)
//    {
//        return calcMaterial() + calcMobility(pieceAI) - calcDistance(pieceAI, tile) + calcRank(piecePlayer, pieceAI);
//    }

//    private int calcMaterial()
//    {
//        int score = 0;
//        for (int i = 0; i < pieceManager.getBlueArr().Length; i++)
//        {
//            if (pieceManager.getRedArr()[i] == null && pieceManager.getBlueArr()[i] == null)
//            {

//            }
//            else if (pieceManager.getBlueArr()[i] == null)
//            {
//                score += pieceManager.getRedArr()[i].getPowerVal();
//            }
//            else if (pieceManager.getRedArr()[i] == null)
//            {
//                score -= pieceManager.getBlueArr()[i].getPowerVal();
//            }
//            else
//            {
//                score += pieceManager.getRedArr()[i].getPowerVal() - pieceManager.getBlueArr()[i].getPowerVal();
//            }

//        }
//        return score;
//    }

//    private int calcMobility(Piece piece)
//    {
//        int possibleMoves = 0;
//        for (int i = 0; i < boardManager.getPossibleMoves(piece).Length; i++)
//        {
//            if (boardManager.getPossibleMoves(piece)[i] != null)
//            {
//                possibleMoves++;
//            }
//        }
//        return possibleMoves;
//    }



//    public static float calcDistance(Piece piece, Tile tile)
//    {
//        float pieceX = piece.getLocation().x;
//        float pieceY = piece.getLocation().y;
//        float tileX = tile.getLocation().x;
//        float tileY = tile.getLocation().y;
//        return Mathf.Sqrt(Mathf.Pow(tileX - pieceX, 2) + Mathf.Pow(tileY - pieceY, 2));

//    }

//    private int calcRank(Piece piecePlayer, Piece pieceAI)
//    {
//        return pieceAI.getPowerVal() - piecePlayer.getPowerVal();
//    }

//}


//==============================ver2==============================

//using UnityEngine;
//using System.Collections.Generic;
//using System;

//public class minimax : MonoBehaviour
//{
//    [SerializeField] private BoardManager boardManager;
//    [SerializeField] private PieceManager pieceManager;
//    [SerializeField] private TurnHandler turnHandler;

//    [SerializeField] private Piece pAI;
//    [SerializeField] private Piece pP;
//    [SerializeField] private Tile tile;

//    public (float, Vector3) Minimax(Tile[] location, int depth, bool isMax, Piece pieceAI)
//    {
//        if (depth == 0 || turnHandler.teamTurn != 2)
//        {
//            return (Evaluate(pAI, pP, tile), Vector3.zero);
//        }

//        Tile[] possibleMoves = boardManager.getPossibleMoves(pieceAI);
//        if (possibleMoves == null || possibleMoves.Length == 0)
//        {
//            return (Evaluate(pAI, pP, tile), Vector3.zero);
//        }

//        if (isMax)
//        {
//            float maximum = float.NegativeInfinity;
//            Vector3 bestMove = Vector3.zero;
//            foreach (Tile move in possibleMoves)
//            {
//                // Apply the move
//                Piece capturedPiece = MakeMove(pieceAI, move);

//                float eval = Minimax(possibleMoves, depth - 1, false, pieceAI).Item1;

//                // Undo the move
//                UndoMove(pieceAI, move, capturedPiece);

//                if (eval > maximum)
//                {
//                    maximum = eval;
//                    bestMove = move.getLocation();
//                }
//            }
//            return (maximum, bestMove);
//        }
//        else
//        {
//            float minimum = float.PositiveInfinity;
//            Vector3 bestMove = Vector3.zero;
//            foreach (Tile move in possibleMoves)
//            {
//                // Apply the move
//                Piece capturedPiece = MakeMove(pieceAI, move);

//                float eval = Minimax(possibleMoves, depth - 1, true, pieceAI).Item1;

//                // Undo the move
//                UndoMove(pieceAI, move, capturedPiece);

//                if (eval < minimum)
//                {
//                    minimum = eval;
//                    bestMove = move.getLocation();
//                }
//            }
//            return (minimum, bestMove);
//        }
//    }

//    public void setAIPiece(Piece p)
//    {
//        pAI = p;
//    }

//    public void setPlayerPiece(Piece p)
//    {
//        pP = p;
//    }

//    public void setTile(Tile t)
//    {
//        tile = t;
//    }

//    private float Evaluate(Piece pieceAI, Piece piecePlayer, Tile tile)
//    {
//        return calcMaterial() + calcMobility(pieceAI) - calcDistance(pieceAI, tile) + calcRank(piecePlayer, pieceAI);
//    }

//    private int calcMaterial()
//    {
//        int score = 0;
//        for (int i = 0; i < pieceManager.getBlueArr().Length; i++)
//        {
//            if (pieceManager.getRedArr()[i] == null && pieceManager.getBlueArr()[i] == null)
//            {
//                continue;
//            }
//            else if (pieceManager.getBlueArr()[i] == null)
//            {
//                score += pieceManager.getRedArr()[i].getPowerVal();
//            }
//            else if (pieceManager.getRedArr()[i] == null)
//            {
//                score -= pieceManager.getBlueArr()[i].getPowerVal();
//            }
//            else
//            {
//                score += pieceManager.getRedArr()[i].getPowerVal() - pieceManager.getBlueArr()[i].getPowerVal();
//            }
//        }
//        return score;
//    }

//    private int calcMobility(Piece piece)
//    {
//        int possibleMoves = boardManager.getPossibleMoves(piece).Length;
//        return possibleMoves;
//    }

//    public static float calcDistance(Piece piece, Tile tile)
//    {
//        float pieceX = piece.getLocation().x;
//        float pieceY = piece.getLocation().y;
//        float tileX = tile.getLocation().x;
//        float tileY = tile.getLocation().y;
//        return Mathf.Sqrt(Mathf.Pow(tileX - pieceX, 2) + Mathf.Pow(tileY - pieceY, 2));
//    }

//    private int calcRank(Piece piecePlayer, Piece pieceAI)
//    {
//        return pieceAI.getPowerVal() - piecePlayer.getPowerVal();
//    }

//    // Method to make a move
//    public Piece MakeMove(Piece piece, Tile destinationTile)
//    {
//        Vector3 startPos = piece.getLocation();
//        Tile startTile = boardManager.getTile(startPos);
//        Piece capturedPiece = destinationTile.getOccupiedBy();

//        // Move piece to the new tile
//        piece.movePiece(destinationTile.getLocation());

//        return capturedPiece;
//    }

//    // Method to undo a move
//    public void UndoMove(Piece piece, Tile destinationTile, Piece capturedPiece)
//    {
//        Vector3 startPos = piece.getLocation();
//        Tile startTile = boardManager.getTile(startPos);

//        // Move piece back to the original tile
//        destinationTile.setOccupiedBy(capturedPiece);
//        piece.movePiece(startTile.getLocation());
//    }
//}
