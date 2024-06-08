//using System.Collections.Generic;
//using UnityEngine;
//public class GameState
//{
//    public Piece[,] Board { get; private set; }
//    public List<MoveData> MoveHistory { get; private set; }

//    [SerializeField]  private PieceManager pieceManager;

//    [SerializeField]  private BoardManager boardManager;

//    private float redScore;
//    private float blueScore;

//    private float gameStateValue;

//    public GameState()
//    {
//        MoveHistory = new List<MoveData>();
//        gameStateValue = Evaluate();
//        // Initialize the board with the starting position
//    }

//    private void InitializeBoard()
//    {
//        // Initialize pieces on the board
//        // Example: Board[0, 0] = new Piece(PieceType.Rook, Player.White);
//    }

//    public GameState Clone()
//    {
//        // Create a deep copy of the game state
//        GameState clone = (GameState)this.MemberwiseClone();
//        clone.Board = (Piece[,])this.Board.Clone();
//        clone.MoveHistory = new List<MoveData>(this.MoveHistory);
//        return clone;
//    }

//    public void ApplyMove(MoveData move)
//    {
//        // Apply the move to the board and update game state accordingly
//        // Update piece positions, handle castling, en passant, pawn promotion, etc.
//        MoveHistory.Add(move);
//        //SwitchPlayer();
//    }


//    //private void SwitchPlayer()
//    //{
//    //    CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
//    //}

//    //public bool IsTerminal()
//    //{
//    //    // Check if the game is over (checkmate, stalemate, draw)
//    //}

//    public double Evaluate()
//    {
//        float pieceDiff = 0;
//        float bluePower = 0;
//        float redPower = 0;

//        foreach (Piece p in pieceManager.getBlueArr())
//        {
//            bluePower = bluePower + boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalBlueValue();
//        }
//        foreach (Piece p in pieceManager.getRedArr())
//        {
//            redPower = redPower + boardManager.getTile(new Vector3(p.getLocation().x, 0, p.getLocation().z)).getInternalRedValue();
//        }

//        pieceDiff = (redScore + (redPower / 100)) - (blueScore + (bluePower / 100));

//        //Debug.Log(redPower);
//        //Debug.Log(bluePower);

//        return pieceDiff * 100;
//    }
//}

////have piece class already
////public class Piece
////{
////    public enum PieceType { Pawn, Knight, Bishop, Rook, Queen, King }
////    public PieceType Type { get; private set; }
////    public GameState.Player Owner { get; private set; }

////    public Piece(PieceType type, GameState.Player owner)
////    {
////        Type = type;
////        Owner = owner;
////    }
////}


////use MoveData
////public class Move
////{
////    public (int, int) From { get; private set; }
////    public (int, int) To { get; private set; }
////    public Piece Promotion { get; private set; } // Optional, for pawn promotion

////    public Move((int, int) from, (int, int) to, Piece promotion = null)
////    {
////        From = from;
////        To = to;
////        Promotion = promotion;
////    }
////}
