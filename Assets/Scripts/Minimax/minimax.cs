using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniMaxClass : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private TurnHandler turnHandler;

    private List<Piece> redPieces = new List<Piece>();
    private List<Piece> bluePieces = new List<Piece>();
    private List<Tile> tilesWithPieces = new List<Tile>();

    private Stack<MoveData> moveStack = new Stack<MoveData>();
    private MoveData bestMove;

    private int redScore = 0;
    private int blueScore = 0;
    [SerializeField] private int maxDepth = 3;

    float distanceDiff = 0;

    #region general

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
        //Debug.Log(initial + "," + destination);

        destination.SwapFakes(initial.getOccupiedBy());
        initial.setOccupiedBy(null);
        initial.setOccupancy(false);
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
        initial.setOccupancy(true);

        if (killed != null)
        {
            destination.setOccupiedBy(killed);
            destination.setOccupancy(true);
        }
        else
        {
            destination.setOccupiedBy(null);
            destination.setOccupancy(false);
        }
    }

    public float calcDistance(Vector3 startPos, Vector3 targetPos)
    {
        return Mathf.Sqrt(Mathf.Pow(targetPos.x - startPos.x, 2) + Mathf.Pow(targetPos.z - startPos.z, 2));
    }

    #endregion

    #region easy

    public MoveData GetBestEasyMove()
    {
        bestMove = CreateMove(boardManager.getTile(new Vector3(0, 0, 0)), boardManager.getTile(new Vector3(0, 0, 0)));
        EasyMiniMax(maxDepth, float.NegativeInfinity, float.PositiveInfinity, true);

        return bestMove;
    }

    public float EasyMiniMax(int depth, float alpha, float beta, bool isMax)
    {
        GetEasyState();

        if (depth == 0 || turnHandler.teamTurn == 2)
        {
            return EasyEvaluate();
        }

        if (isMax)
        {
            float score = int.MinValue;
            List<MoveData> allMoves = GetEasyMoves("red");

            //Debug.Log(" there are this many moves maximizer : " + allMoves.Count);

            //Debug.Log(allMoves.Count);

            foreach (MoveData move in allMoves)
            { 
                moveStack.Push(move);

                if (move.destination.GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    bestMove = move;
                    break;
                }

                DoFakeMove(move.initial, move.destination);

                score = MinimaxAlg(depth - 1, alpha, beta, false) + (boardManager.getPossibleMoves(move.mover).Length)/100;

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
                //Debug.Log(move.score + "," + move.destination);
            }
            return alpha;
        }
        else
        {
            float score = int.MaxValue;
            List<MoveData> allMoves = GetEasyMoves("blue");

            foreach (MoveData move in allMoves)
            {
                //Debug.Log(" there are this many moves minimizer : " + allMoves.Count);
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
                //Debug.Log(move.score + "," + move.destination);
            }
            //Debug.Log(score);
            return beta;
        }
    }

    public List<MoveData> GetEasyMoves(string team)
    {
        List<Piece> pieces = new List<Piece>();
        List<MoveData> turnMove = new List<MoveData>();

        if (team.Equals("red"))
        {
            pieces = redPieces;
            pieces.RemoveAll(item => item == null);
        }
        else
        {
            pieces = bluePieces;
            pieces.RemoveAll(item => item == null);
        }

        System.Random random = new System.Random(DateTime.Now.Millisecond);
        Piece randomPiece = pieces[random.Next(0, pieces.Count)];

        List<Tile> possibleDestinations = boardManager.getPossibleMoves(randomPiece).ToList();
        possibleDestinations.RemoveAll(item => item == null);

        foreach (Tile tile in possibleDestinations)
        {
            if (randomPiece.getTeam() == 0)
            {
                blueScore++;
            }
            else
            {
                redScore++;
            }
            MoveData newMove = CreateMove(randomPiece.FindCurrentTile(new Vector3(randomPiece.getLocation().x, 0, randomPiece.getLocation().z)), tile); ;
            turnMove.Add(newMove);
        }

        return turnMove;
    }

    private void GetEasyState()
    {
        redPieces.Clear();
        bluePieces.Clear();
        blueScore = 0;
        redScore = 0;

        tilesWithPieces.Clear();

        Piece closestBlue;
        float blueDistance = float.PositiveInfinity;

        List<Tile> allTiles = boardManager.getTileArr().ToList();

        foreach (Tile tile in allTiles)
        {
            if (tile.getOccupiedBy() != null)
            {
                tile.setOccupancy(true);
            }
            else
            {
                tile.setOccupancy(false);
            }
            if (tile.getOccupancy())
            {
                tilesWithPieces.Add(tile);
            }
        }

        tilesWithPieces.RemoveAll(item => item == null);

        foreach (Tile tile in tilesWithPieces)
        {
            if (tile.getOccupiedBy().getTeam() == 0)
            {
                blueScore += tile.getInternalBlueValue();

                bluePieces.Add(tile.getOccupiedBy());

                if (calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation()) < blueDistance)
                {
                    closestBlue = tile.getOccupiedBy();
                    blueDistance = calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation());
                }
            }
            else
            {
                redScore += tile.getInternalRedValue();

                redPieces.Add(tile.getOccupiedBy());

            }
        }
        distanceDiff = blueDistance;
    }

    public float EasyEvaluate()
    {
        float bluePower = 0;
        float redPower = 0;

        if (distanceDiff > 7)
        {
            foreach (Piece p in bluePieces)
            {
                bluePower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }
            foreach (Piece p in redPieces)
            {
                redPower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalRedValue();
            }

            float pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

            return pieceDiff * 100;
        }
        else
        {
            Debug.Log("defensive");

            foreach (Piece p in bluePieces)
            {
                bluePower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }
            foreach (Piece p in redPieces)
            {
                redPower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }

            float pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

            return pieceDiff * 100;
        }
    }

    #endregion easy

    #region medium/hard

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

            //Debug.Log(" there are this many moves maximizer : " + allMoves.Count);

            //Debug.Log(allMoves.Count);

            foreach (MoveData move in allMoves)
            {
                moveStack.Push(move);

                if (move.destination.GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    bestMove = move;
                    break;
                }

                DoFakeMove(move.initial, move.destination);

                score = MinimaxAlg(depth - 1, alpha, beta, false) + (boardManager.getPossibleMoves(move.mover).Length) / 100;

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
                //Debug.Log(move.score + "," + move.destination);
            }
            return alpha;
        }
        else
        {
            float score = int.MaxValue;
            List<MoveData> allMoves = GetMoves("blue");
            //movesToString();
            foreach (MoveData move in allMoves)
            {
                //Debug.Log(" there are this many moves minimizer : " + allMoves.Count);
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
                //Debug.Log(move.score + "," + move.destination);
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
                if (piece.getTeam() == 0)
                {
                    blueScore++;
                }
                else
                {
                    redScore++;
                }
                MoveData newMove = CreateMove(piece.FindCurrentTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z)), tile); ;
                turnMove.Add(newMove);
            }
        }

        return turnMove;
    }

    private void GetBoardState()
    {
        redPieces.Clear();
        bluePieces.Clear();
        blueScore = 0;
        redScore = 0;

        tilesWithPieces.Clear();

        //Piece closestRed;
        //float redDistance = float.PositiveInfinity;

        Piece closestBlue;
        float blueDistance = float.PositiveInfinity;

        List<Tile> allTiles = boardManager.getTileArr().ToList();

        foreach (Tile tile in allTiles)
        {
            if (tile.getOccupiedBy() != null)
            {
                tile.setOccupancy(true);
            }
            else
            {
                tile.setOccupancy(false);
            }
            if (tile.getOccupancy())
            {
                tilesWithPieces.Add(tile);
            }
        }

        tilesWithPieces.RemoveAll(item => item == null);

        foreach (Tile tile in tilesWithPieces)
        {
            if (tile.getOccupiedBy().getTeam() == 0)
            {
                blueScore += tile.getInternalBlueValue();

                //blueScore += tile.getOccupiedBy().getPowerVal();
                bluePieces.Add(tile.getOccupiedBy());

                if (calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation()) < blueDistance)
                {
                    closestBlue = tile.getOccupiedBy();
                    blueDistance = calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation());
                }
            }
            else
            {
                redScore += tile.getInternalRedValue();

                //redScore += tile.getOccupiedBy().getPowerVal();
                redPieces.Add(tile.getOccupiedBy());

                //if (calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation()) < blueDistance)
                //{
                //    closestRed = tile.getOccupiedBy();
                //    redDistance = calcDistance(tile.getOccupiedBy().getLocation(), boardManager.blueGoal.getLocation());
                //}
            }
        }

        distanceDiff = blueDistance;

    }

    public float Evaluate()
    {
        float bluePower = 0;
        float redPower = 0;

        if (distanceDiff > 7)
        {
            foreach (Piece p in bluePieces)
            {
                bluePower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }
            foreach (Piece p in redPieces)
            {
                redPower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalRedValue();
            }

            float pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

            return pieceDiff * 100;
        }
        else
        {
            Debug.Log("defensive");

            foreach (Piece p in bluePieces)
            {
                bluePower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }
            foreach (Piece p in redPieces)
            {
                redPower += boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
            }

            float pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

            return pieceDiff * 100;
        }
    }    
}

#endregion medium/hard


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
