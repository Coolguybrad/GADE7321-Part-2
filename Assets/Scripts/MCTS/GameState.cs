using System.Collections.Generic;

public class GameState
{
    public enum Player { White, Black }
    public Player CurrentPlayer { get; set; }
    public Piece[,] Board { get; private set; }
    public bool WhiteCanCastleKingside { get; set; }
    public bool WhiteCanCastleQueenside { get; set; }
    public bool BlackCanCastleKingside { get; set; }
    public bool BlackCanCastleQueenside { get; set; }
    public (int, int)? EnPassantTarget { get; set; }
    public List<Move> MoveHistory { get; private set; }

    public GameState()
    {
        Board = new Piece[8, 8];
        MoveHistory = new List<Move>();
        // Initialize the board with the starting position
        InitializeBoard();
        CurrentPlayer = Player.White;
    }

    private void InitializeBoard()
    {
        // Initialize pieces on the board
        // Example: Board[0, 0] = new Piece(PieceType.Rook, Player.White);
    }

    public GameState Clone()
    {
        // Create a deep copy of the game state
        GameState clone = (GameState)this.MemberwiseClone();
        clone.Board = (Piece[,])this.Board.Clone();
        clone.MoveHistory = new List<Move>(this.MoveHistory);
        return clone;
    }

    public void ApplyMove(Move move)
    {
        // Apply the move to the board and update game state accordingly
        // Update piece positions, handle castling, en passant, pawn promotion, etc.
        MoveHistory.Add(move);
        SwitchPlayer();
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
    }

    public bool IsTerminal()
    {
        // Check if the game is over (checkmate, stalemate, draw)
    }

    public double Evaluate()
    {
        // Evaluate the board state from the perspective of the current player
    }
}

public class Piece
{
    public enum PieceType { Pawn, Knight, Bishop, Rook, Queen, King }
    public PieceType Type { get; private set; }
    public GameState.Player Owner { get; private set; }

    public Piece(PieceType type, GameState.Player owner)
    {
        Type = type;
        Owner = owner;
    }
}

public class Move
{
    public (int, int) From { get; private set; }
    public (int, int) To { get; private set; }
    public Piece Promotion { get; private set; } // Optional, for pawn promotion

    public Move((int, int) from, (int, int) to, Piece promotion = null)
    {
        From = from;
        To = to;
        Promotion = promotion;
    }
}
