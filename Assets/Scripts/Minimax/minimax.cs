using UnityEngine;
using System.Collections.Generic;
using System;

public class minimax : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;

    [SerializeField] private Piece pAI;
    [SerializeField] private Piece pP;
    [SerializeField] private Tile tile;
    public (float, Vector3) Minimax(Tile[] location, int depth, bool isMax, Piece pieceAI)
    {
        if (depth == 0)
        {
            Evaluate(pAI, pP, tile);
        }

        if (isMax)
        {
            float maximum = float.NegativeInfinity;
            Vector3 bestMove = new Vector3();
            for (int i = 0; i < boardManager.getPossibleMoves(pieceAI).Length; i++)
            {
                float eval = Minimax(boardManager.getPossibleMoves(pieceAI), depth-1, false, pieceAI).Item1;
                if (eval > maximum)
                {
                    maximum = eval;
                    bestMove = boardManager.getPossibleMoves(pieceAI)[i].getLocation();
                }
            }
            return (maximum, bestMove);
        }
        else
        {
            float minimum = float.PositiveInfinity;
            Vector3 bestMove = new Vector3();
            for (int i = 0; i < boardManager.getPossibleMoves(pieceAI).Length; i++)
            {
                float eval = Minimax(boardManager.getPossibleMoves(pieceAI), depth - 1, true, pieceAI).Item1;
                if (eval < minimum)
                {
                    minimum = eval;
                    bestMove = boardManager.getPossibleMoves(pieceAI)[i].getLocation();
                }
            }
            return (minimum, bestMove);
        }
    }

    public void setAIPiece(Piece p) 
    {
        pAI = p;
    }

    public void setPlayerPiece(Piece p) 
    {
        pP = p;
    }

    public void setTile(Tile t) 
    {
        tile = t;
    }

    private float Evaluate(Piece pieceAI, Piece piecePlayer, Tile tile)
    {
        return calcMaterial() + calcMobility(pieceAI) - calcDistance(pieceAI, tile) + calcRank(piecePlayer, pieceAI);
    }

    private int calcMaterial()
    {
        int score = 0;
        for (int i = 0; i < pieceManager.getBlueArr().Length; i++)
        {
            if (pieceManager.getRedArr()[i] == null && pieceManager.getBlueArr()[i] == null)
            {

            }
            else if (pieceManager.getBlueArr()[i] == null)
            {
                score += pieceManager.getRedArr()[i].getPowerVal();
            }
            else if (pieceManager.getRedArr()[i] == null)
            {
                score -= pieceManager.getBlueArr()[i].getPowerVal();
            }
            else
            {
                score += pieceManager.getRedArr()[i].getPowerVal() - pieceManager.getBlueArr()[i].getPowerVal();
            }

        }
        return score;
    }

    private int calcMobility(Piece piece)
    {
        int possibleMoves = 0;
        for (int i = 0; i < boardManager.getPossibleMoves(piece).Length; i++)
        {
            if (boardManager.getPossibleMoves(piece)[i] != null)
            {
                possibleMoves++;
            }
        }
        return possibleMoves;
    }

  

    private float calcDistance(Piece piece, Tile tile)
    {
        float pieceX = piece.getLocation().x;
        float pieceY = piece.getLocation().y;
        float tileX = tile.getLocation().x;
        float tileY = tile.getLocation().y;
        return Mathf.Sqrt(Mathf.Pow(tileX - pieceX, 2) - Mathf.Pow(tileY - pieceY, 2));

    }

    private int calcRank(Piece piecePlayer, Piece pieceAI)
    {
        return pieceAI.getPowerVal() - piecePlayer.getPowerVal();
    }

}
