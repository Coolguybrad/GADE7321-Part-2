using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiniMaxClass : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private TurnHandler turnHandler;

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
        MinimaxAlg(maxDepth, int.MinValue, int.MaxValue, true);
        return bestMove;
    }

    public int MinimaxAlg(int depth, int alpha, int beta, bool isMax)
    {

        GetBoardState();

        if (depth == 0 || turnHandler.teamTurn == 2)
        {
            return Mathf.RoundToInt(Evaluate());
        }


        if (isMax)
        {
            int score = int.MinValue;
            List<MoveData> allMoves = GetMoves("red");

            //Debug.Log(allMoves.Count);

            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                //Debug.Log(move.destination);

                DoFakeMove(move.initial, move.destination);

                score = MinimaxAlg(depth - 1, alpha, beta, false);

                //Debug.Log(score);

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
                Debug.Log(move.score + "," + move.destination);
            }
            return alpha;
        }
        else
        {
            int score = int.MaxValue;
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
                Debug.Log(move.score + "," + move.destination);
            }
            //Debug.Log(score);
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
            pieces.RemoveAll(item => item == null);

            //Debug.Log("red");
        }
        else
        {
            pieces = bluePieces;
            pieces.RemoveAll(item => item == null);

            //Debug.Log("blue");
        }

        foreach (Piece piece in pieces)
        {
            List<Tile> possibleDestinations = boardManager.getPossibleMoves(piece).ToList();
            possibleDestinations.RemoveAll(item => item == null);

            foreach (Tile tile in possibleDestinations)
            {
                MoveData newMove = CreateMove(piece.FindCurrentTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z)), tile);;
                turnMove.Add(newMove);
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
        //Debug.Log("do");
        Debug.Log(initial + "," + destination);

        if (destination.getOccupiedBy() != null)
        {
            if (destination == boardManager.blueGoal)
            {
                fakeLose = true;
            }
            else
            {
                fakeLose = false;
            }
        }        

        destination.SwapFakes(initial.getOccupiedBy());
        initial.setOccupiedBy(null);
    }

    private void UndoFakeMove()
    {
        //Debug.Log("undo");

        MoveData temp = moveStack.Pop();
        Tile destination = temp.destination;
        Tile initial = temp.initial;
        Piece killed = temp.killed;
        Piece mover = temp.mover;

        initial.setOccupiedBy(mover);

        if (killed != null)
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

        foreach (Piece red in pieceManager.getRedArr())
        {
            if (red != null)
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

    public float Evaluate()
    {
        float pieceDiff = 0;
        float bluePower = 0;
        float redPower = 0;

        foreach (Piece p in bluePieces)
        {
            bluePower = bluePower + boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
        }
        foreach (Piece p in redPieces)
        {
            redPower = redPower + boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalRedValue();
        }

        pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

        //Debug.Log(redPower);
        //Debug.Log(bluePower);

        return pieceDiff*100;
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
