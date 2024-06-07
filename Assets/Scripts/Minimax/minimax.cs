using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniMaxClass : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private TurnHandler turnHandler;

    private List<Tile> occupiedTiles = new List<Tile>();
    private List<Piece> redPieces = new List<Piece>();
    private List<Piece> bluePieces = new List<Piece>();

    private Stack<MoveData> moveStack = new Stack<MoveData>();
    private MoveData bestMove;

    private int redScore = 0;
    private int blueScore = 0;
    [SerializeField] private int maxDepth = 3;
    private bool fakeLose = false; 

    public MoveData GetBestMove()
    {
        bestMove = CreateMove(boardManager.getTile(new Vector3(0, 0, 0)), boardManager.getTile(new Vector3(0, 0, 0)));
        MinimaxAlg(maxDepth, float.NegativeInfinity, float.PositiveInfinity, true);
        return bestMove;
    }

    public float MinimaxAlg(int depth, float alpha, float beta, bool isMax)
    {

        GetBoardState();

        if (depth == 0 || turnHandler.teamTurn == 2)
        {
            return Evaluate();
        }

        if (isMax)
        {
            float score = int.MinValue;
            List<MoveData> allMoves = GetMoves("red");

            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                DoFakeMove(move.initial, move.destination);

                score = MinimaxAlg(depth - 1, alpha, beta, false);

                UndoFakeMove();

                if (score > alpha)
                {
                    move.score = score;
                    if (move.score > bestMove.score && depth >= maxDepth)
                    {
                        bestMove = move;
                    }
                    alpha = score;
                }
                if (score >= beta)
                {
                    break;
                }
            }
            return alpha;
        }
        else
        {
            float score = int.MaxValue;
            List<MoveData> allMoves = GetMoves("blue");
            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                DoFakeMove(move.initial, move.destination);

                score = MinimaxAlg(depth - 1, alpha, beta, true);

                UndoFakeMove();

                if (score < beta)
                {
                    move.score = score;
                    beta = score;
                }
                if (score <= alpha)
                {
                    break;
                }
            }
            return beta;
        }
                
    }

    public List<MoveData> GetMoves(string team) 
    {
        List<Piece> pieces = new List<Piece>();
        List<MoveData> turnMove = new List<MoveData>();

        if (team.Equals("red"))
        {
            pieces = redPieces;
        }
        else
        {
            pieces = bluePieces;
        }

        foreach (Piece piece in pieces) 
        { 
       
        List<Tile> tiles = new List<Tile>();
            tiles = boardManager.getPossibleMoves(piece).ToList();
            foreach (Tile tile in tiles)
            {
                MoveData move = CreateMove(boardManager.getTile(new Vector3(piece.getLocation().x, 0 , piece.getLocation().z)), tile);
                turnMove.Add(move);

            }
        }

        return turnMove;
    }

    private MoveData CreateMove(Tile initial, Tile destination)
    {
        MoveData temp = new MoveData
        {
            initial = initial,
            mover = initial.getOccupiedBy(),
            destination = destination
        };

        if (destination.getOccupiedBy() != null)
        {
            temp.killed = destination.getOccupiedBy();
        }

        return temp;
    }

    private void DoFakeMove(Tile initial, Tile destination)
    {
        if (destination == boardManager.blueGoal)
        {
            fakeLose = true;
        }
        else
        {
            fakeLose = false;
        }

        destination.SwapFakes(initial.getOccupiedBy());
        initial.setOccupiedBy(null);
    }

    private void UndoFakeMove()
    {
        MoveData temp = moveStack.Pop();
        Tile destination = temp.destination;
        Tile initial = temp.initial;
        Piece killed = temp.killed;
        Piece mover = temp.mover;

        initial.setOccupiedBy(mover);

        if(killed != null)
        {
            destination.setOccupiedBy(killed);
        }
        else
        {
            destination.setOccupiedBy(null);
        }
    }

    private void GetBoardState()
    {
        redPieces.Clear();
        bluePieces.Clear();
        blueScore = 0;
        redScore = 0;

        occupiedTiles.Clear();

        foreach(Piece red in pieceManager.getRedArr())
        {
            if(red != null)
            {
                redPieces.Add(red);
                redScore += red.getPowerVal();
            }
        }

        foreach (Piece blue in pieceManager.getBlueArr())
        {
            if (blue != null)
            {
                bluePieces.Add(blue);
                blueScore += blue.getPowerVal();
            }
        }
    }    

    public int Evaluate()
    {
        float pieceDiff = 0;
        float bluePower = 0;
        float redPower = 0;

        foreach (Piece p in bluePieces)
        {
            bluePower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
        }
        foreach (Piece p in redPieces) 
        {
            redPower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalRedValue();
        }

        pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));
        return Mathf.RoundToInt(pieceDiff * 100);
    } 


}

// nonsense

//private int calcMaterial()
//{
//    int score = 0;
//    for (int i = 0; i < pieceManager.getBlueArr().Length; i++)
//    {
//        if (pieceManager.getRedArr()[i] != null)
//        {
//            score += pieceManager.getRedArr()[i].getPowerVal();
//        }
//        if (pieceManager.getBlueArr()[i] != null)
//        {
//            score -= pieceManager.getBlueArr()[i].getPowerVal();
//        }
//    }
//    return score;
//}

//public float calcDistance(Vector3 startPos, Vector3 targetPos)
//{
//    return Mathf.Sqrt(Mathf.Pow(targetPos.x - startPos.x, 2) + Mathf.Pow(targetPos.z - startPos.z, 2));
//}

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
