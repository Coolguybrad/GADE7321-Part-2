using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private Piece selectedPiece;

    [SerializeField] private Piece[] blueArr;
    [SerializeField] private Piece[] redArr;

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TurnHandler turnHandler;

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
                else if(turnHandler.teamTurn != 2)                                                  //return to red team turn
                {
                    turnHandler.SetRedTurn();
                }
            }
            else
            {
                if(boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.redGoal)        //win con for red team
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

}
