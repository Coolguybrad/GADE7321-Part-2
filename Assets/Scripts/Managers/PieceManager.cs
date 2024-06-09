using System.Collections;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private Piece selectedPiece;

    [SerializeField] private Piece[] blueArr;
    [SerializeField] private Piece[] redArr;

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TurnHandler turnHandler;

    [SerializeField] private MiniMaxClass minimax;

    public enum modeEnum
    {
        multiplayer,
        easy,
        medium,
        hard
    }

    public modeEnum mode;

    void Update()
    {
        if (selectedPiece != null)
        {
            boardManager.showPossibleMoves(selectedPiece);
        }
        else
        {
            boardManager.wipePossibleMoves();
        }
    }

    public Piece[] getBlueArr()
    {
        return blueArr;
    }

    public Piece[] getRedArr()
    {
        return redArr;
    }

    public void setSelectedPiece(Piece piece)                                                   //sets the selected piece
    {
        selectedPiece = piece;
    }
    public Piece getSelectedPiece()                                                             //returns the selected piece
    {
        return selectedPiece;
    }

    public void MoveSelectedPiece()                                                             //moves the piece
    {
        switch (mode)
        {
            case modeEnum.multiplayer:
                if (boardManager.getClickedTile().getPossibleMove().activeInHierarchy)
                {
                    //checks tiletype to see what behaviour should be carried out
                    switch (boardManager.getClickedTile().GetTileType())
                    {
                        case Tile.TileTypeEnum.normal:
                            NullChecker();
                            selectedPiece.setPowerVal(selectedPiece.getInitialPower());
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            boardManager.wipePossibleMoves();
                            break;
                        case Tile.TileTypeEnum.bush:
                            NullChecker();
                            selectedPiece.setPowerVal(100);
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            boardManager.wipePossibleMoves();
                            break;
                        case Tile.TileTypeEnum.trap:
                            NullChecker();
                            selectedPiece.setPowerVal(0);
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            boardManager.wipePossibleMoves();
                            break;
                        case Tile.TileTypeEnum.high:
                            NullChecker();
                            selectedPiece.setPowerVal(selectedPiece.getInitialPower() + 1);
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.54f, boardManager.getClickedTile().getLocation().z));
                            boardManager.wipePossibleMoves();
                            break;
                        case Tile.TileTypeEnum.rough:
                            NullChecker();
                            selectedPiece.setPowerVal(selectedPiece.getInitialPower() - 1);
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            boardManager.wipePossibleMoves();
                            break;
                        case Tile.TileTypeEnum.blueGoal:
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            break;
                        case Tile.TileTypeEnum.redGoal:
                            selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                            break;
                    }

                    if (turnHandler.teamTurn == 0)
                    {
                        if (boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.blueGoal)      //win con for blue team
                        {
                            BlueWin();
                        }
                        else if (turnHandler.teamTurn != 2)                                                  //return to red team turn
                        {
                            turnHandler.SetRedTurn();
                        }
                    }
                    else
                    {
                        if (boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.redGoal)        //win con for red team
                        {
                            RedWin();
                        }
                        else if (turnHandler.teamTurn != 2)                                                 //return to blue team turn
                        {
                            turnHandler.SetBlueTurn();
                        }
                    }

                    selectedPiece = null;                                                                   //resets selected piece
                }
                break;
            case modeEnum.easy:
                break;
            case modeEnum.medium:
                if (boardManager.getClickedTile().getPossibleMove().activeInHierarchy)
                {
                    if (turnHandler.teamTurn == 0)
                    {
                        switch (boardManager.getClickedTile().GetTileType())
                        {
                            case Tile.TileTypeEnum.normal:
                                NullChecker();
                                selectedPiece.setPowerVal(selectedPiece.getInitialPower());
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                boardManager.wipePossibleMoves();
                                break;
                            case Tile.TileTypeEnum.bush:
                                NullChecker();
                                selectedPiece.setPowerVal(100);
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                boardManager.wipePossibleMoves();
                                break;
                            case Tile.TileTypeEnum.trap:
                                NullChecker();
                                selectedPiece.setPowerVal(0);
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                boardManager.wipePossibleMoves();
                                break;
                            case Tile.TileTypeEnum.high:
                                NullChecker();
                                selectedPiece.setPowerVal(selectedPiece.getInitialPower() + 1);
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.54f, boardManager.getClickedTile().getLocation().z));
                                boardManager.wipePossibleMoves();
                                break;
                            case Tile.TileTypeEnum.rough:
                                NullChecker();
                                selectedPiece.setPowerVal(selectedPiece.getInitialPower() - 1);
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                boardManager.wipePossibleMoves();
                                break;
                            case Tile.TileTypeEnum.blueGoal:
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                break;
                            case Tile.TileTypeEnum.redGoal:
                                selectedPiece.movePiece(new Vector3(boardManager.getClickedTile().getLocation().x, 0.315f, boardManager.getClickedTile().getLocation().z));
                                break;
                        }
                        if (boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.blueGoal)      //win con for blue team
                        {
                            BlueWin();
                        }
                        else if (turnHandler.teamTurn != 2)                                                  //return to red team turn
                        {
                            turnHandler.SetRedTurn();
                        }

                        selectedPiece = null;                                                              //resets selected piece

                        if (turnHandler.teamTurn != 2)
                        {
                            StartCoroutine(MediumMoveCoroutine());
                        }

                    }

                }


                break;
            case modeEnum.hard:

                break;
        }
    }

    #region general
    public void SwapFakePiece(Piece piece)
    {

    }

    private void NullChecker()                                                                      //checks the piece array of each side to see if all pieces have been captured or not for win cons
    {
        if (boardManager.getClickedTile().getOccupancy())
        {
            boardManager.getClickedTile().getOccupiedBy().KillSelf();

            int i = 0;

            int nullCount = 1;

            if (turnHandler.teamTurn == 0)
            {
                foreach (Piece piece in redArr)
                {
                    if (boardManager.getClickedTile().getOccupiedBy() == piece)
                    {
                        redArr[i] = null;
                    }
                    if (piece == null)
                    {
                        nullCount++;
                    }

                    i++;
                }

                if (nullCount == redArr.Length)
                {
                    BlueWin();
                }
            }
            else
            {
                foreach (Piece piece in blueArr)
                {
                    if (boardManager.getClickedTile().getOccupiedBy() == piece)
                    {
                        blueArr[i] = null;
                    }
                    if (piece == null)
                    {
                        nullCount++;
                    }

                    i++;
                }

                if (nullCount == blueArr.Length)
                {
                    RedWin();
                }
            }
        }
    }

    public void BlueWin()
    {
        turnHandler.teamTurn = 2;
        turnHandler.turnText.text = "BLUE WINS!";
    }

    public void RedWin()
    {
        turnHandler.teamTurn = 2;
        turnHandler.turnText.text = "RED WINS!";
    }

    [SerializeField] private Piece[] inGoalAreaRed;
    [SerializeField] private Piece[] inGoalAreaBlue;

    IEnumerator MediumMoveCoroutine()
    {
        turnHandler.turnText.text = "RED THINKING...";
        turnHandler.turnText.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        Debug.Log(minimax.GetBestMove().score + " is the best score");
        MoveData move = minimax.GetBestMove();
        MoveMediumAI(move);
    }

    #endregion

    #region easyAI



    #endregion

    #region mediumAI

    public void MoveMediumAI(MoveData move)
    {
        Piece piece = move.mover;
        Tile moveTo = move.destination;


        switch (moveTo.GetTileType())
        {
            case Tile.TileTypeEnum.normal:
                //NullChecker();
                piece.setPowerVal(piece.getInitialPower());
                try
                {
                    moveTo.getOccupiedBy().KillSelf();
                }
                catch (System.Exception)
                {

                    Debug.Log("nothing to kill");
                }

                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.bush:
                //NullChecker();
                piece.setPowerVal(100);
                try
                {
                    moveTo.getOccupiedBy().KillSelf();
                }
                catch (System.Exception)
                {

                    Debug.Log("nothing to kill");
                }
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.trap:
                //NullChecker();
                piece.setPowerVal(0);
                try
                {
                    moveTo.getOccupiedBy().KillSelf();
                }
                catch (System.Exception)
                {

                    Debug.Log("nothing to kill");
                }
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.high:
                //NullChecker();
                piece.setPowerVal(piece.getInitialPower() + 1);
                try
                {
                    moveTo.getOccupiedBy().KillSelf();
                }
                catch (System.Exception)
                {

                    Debug.Log("nothing to kill");
                }
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.54f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.rough:
                //NullChecker();
                piece.setPowerVal(piece.getInitialPower() - 1);
                try
                {
                    moveTo.getOccupiedBy().KillSelf();
                }
                catch (System.Exception)
                {

                    Debug.Log("nothing to kill");
                }
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.blueGoal:
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
            case Tile.TileTypeEnum.redGoal:
                piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
                break;
        }




        if (moveTo.GetTileType() == Tile.TileTypeEnum.redGoal)      //win con for red team
        {
            RedWin();
        }
        else if (turnHandler.teamTurn != 2)                                                  //return to blue team turn
        {
            turnHandler.SetBlueTurn();
        }
    }

    #region graveyard of nonsense

    //Piece piece = chooseBestPiece();
    //Tile moveTo = chooseBestMove(chooseBestPiece());

    //switch (moveTo.GetTileType())
    //{
    //    case Tile.TileTypeEnum.normal:
    //        NullChecker();
    //        piece.setPowerVal(piece.getInitialPower());
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.bush:
    //        NullChecker();
    //        piece.setPowerVal(100);
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.trap:
    //        NullChecker();
    //        piece.setPowerVal(0);
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.high:
    //        NullChecker();
    //        piece.setPowerVal(piece.getInitialPower() + 1);
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.54f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.rough:
    //        NullChecker();
    //        piece.setPowerVal(piece.getInitialPower() - 1);
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.blueGoal:
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //    case Tile.TileTypeEnum.redGoal:
    //        piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //        break;
    //}
    //if (moveTo.GetTileType() == Tile.TileTypeEnum.redGoal)      //win con for red team
    //{
    //    RedWin();
    //}
    //else if (turnHandler.teamTurn != 2)                                                  //return to blue team turn
    //{
    //    turnHandler.SetBlueTurn();
    //}

    //public Piece chooseBestPiece()
    //{
    //    float bestPieceVal = float.NegativeInfinity;

    //    Piece bestPiece = null;

    //    for (int i = 0; i < redArr.Length; i++)
    //    {
    //        //try
    //        //{
    //        //    float thisValue = minimax.minimaxAlg(minimax.Evaluate(redArr[i], boardManager.redGoal), 2, float.NegativeInfinity, float.PositiveInfinity, true);

    //        //    Debug.Log(thisValue);

    //        //    if (thisValue > bestPieceVal)
    //        //    {
    //        //        bestPiece = redArr[i];
    //        //        bestPieceVal = thisValue;
    //        //    }
    //        //}
    //        //catch
    //        //{

    //        //}
    //    }

    //    return bestPiece;
    //}

    //public Tile chooseBestMove(Piece piece)
    //{
    //    Tile chosenMove = null;
    //    float distanceToGoal = float.PositiveInfinity;

    //    for (int i = 0; i < 4; i++)
    //    {
    //        try
    //        {
    //            if (minimax.calcDistance(boardManager.getPossibleMoves(piece)[i].getLocation(), boardManager.redGoal.getLocation()) < distanceToGoal)
    //            {
    //                chosenMove = boardManager.getPossibleMoves(piece)[i];
    //                distanceToGoal = minimax.calcDistance(boardManager.getPossibleMoves(piece)[i].getLocation(), boardManager.redGoal.getLocation());
    //            }
    //        }
    //        catch
    //        {

    //        }
    //    }
    //    return chosenMove;
    //}


    //    return (bestPiece, bestPos);
    //}

    //public void moveAI(Piece piece, Tile moveTo)
    //{
    //    switch (moveTo.GetTileType())
    //    {
    //        case Tile.TileTypeEnum.normal:
    //            NullChecker();
    //            piece.setPowerVal(piece.getInitialPower());
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.bush:
    //            NullChecker();
    //            piece.setPowerVal(100);
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.trap:
    //            NullChecker();
    //            piece.setPowerVal(0);
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.high:
    //            NullChecker();
    //            piece.setPowerVal(piece.getInitialPower() + 1);
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.54f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.rough:
    //            NullChecker();
    //            piece.setPowerVal(piece.getInitialPower() - 1);
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.blueGoal:
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //        case Tile.TileTypeEnum.redGoal:
    //            piece.movePiece(new Vector3(moveTo.getLocation().x, 0.315f, moveTo.getLocation().z));
    //            break;
    //    }
    //    if (moveTo.GetTileType() == Tile.TileTypeEnum.blueGoal)      //win con for red team
    //    {
    //        RedWin();
    //    }
    //    else if (turnHandler.teamTurn != 2)                                                  //return to blue team turn
    //    {
    //        turnHandler.SetBlueTurn();
    //    }

    //}
    #endregion

    #endregion




    #region hardAI

    #endregion
}
