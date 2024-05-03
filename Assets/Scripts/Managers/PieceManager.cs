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

    // Update is called once per frame
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

    public void setSelectedPiece(Piece piece) 
    {
        selectedPiece = piece;
    }
    public Piece getSelectedPiece() 
    {
        return selectedPiece;
    }

    public void MoveSelectedPiece() 
    {
        if (boardManager.getClickedTile().getPossibleMove().activeInHierarchy)
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

            if(turnHandler.teamTurn == 0)
            {
                if (boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    BlueWin();
                }
                else if(turnHandler.teamTurn != 2)
                {
                    turnHandler.SetRedTurn();
                }
            }
            else
            {
                if(boardManager.getClickedTile().GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    RedWin();
                }
                else if (turnHandler.teamTurn != 2)
                {
                    turnHandler.SetBlueTurn();
                }
            }           

            selectedPiece = null;
        }        
    }


    private void NullChecker()
    {
        if (boardManager.getClickedTile().getOccupancy())
        {
            boardManager.getClickedTile().getOccupiedBy().MinorSpellingError();

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

                Debug.Log(redArr.Length);

                Debug.Log(nullCount);
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

                Debug.Log(blueArr.Length);

                Debug.Log(nullCount);
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
