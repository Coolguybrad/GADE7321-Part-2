using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance;
    [SerializeField] private Piece selectedPiece;

    [SerializeField] private Piece[] blueArr;
    [SerializeField] private Piece[] redArr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedPiece != null)
        {
            BoardManager.Instance.showPossibleMoves(selectedPiece);
        }
        else
        {
            BoardManager.Instance.wipePossibleMoves();
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
        if (BoardManager.Instance.getClickedTile().getPossibleMove().activeInHierarchy)
        {
            if (BoardManager.Instance.getClickedTile().getOccupancy())
            {
                int i = 0;

                int nullCount = 1;

                if(TurnHandler.Instance.teamTurn == 0)
                {
                    foreach(Piece piece in redArr)
                    {
                        if (BoardManager.Instance.getClickedTile().getOccupiedBy() == piece)
                        {
                            redArr[i] = null;
                        }
                        if(piece == null)
                        {
                            nullCount++;
                        }

                        i++;
                    }

                    if(nullCount == redArr.Length)
                    {
                        BlueWin();
                    }

                    Debug.Log(nullCount);
                }
                else
                {
                    foreach (Piece piece in blueArr)
                    {
                        if (BoardManager.Instance.getClickedTile().getOccupiedBy() == piece)
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

                    Debug.Log(nullCount);

                }

                BoardManager.Instance.getClickedTile().getOccupiedBy().MinorSpellingError();
            }

            switch (BoardManager.Instance.getClickedTile().GetTileType())
            {
                case Tile.TileTypeEnum.normal:
                    selectedPiece.setPowerVal(selectedPiece.getInitialPower());
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    BoardManager.Instance.wipePossibleMoves();
                    break;
                case Tile.TileTypeEnum.bush:
                    selectedPiece.setPowerVal(100);
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    BoardManager.Instance.wipePossibleMoves();
                    break;
                case Tile.TileTypeEnum.trap:
                    selectedPiece.setPowerVal(0);
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    BoardManager.Instance.wipePossibleMoves();
                    break;
                case Tile.TileTypeEnum.high:
                    selectedPiece.setPowerVal(selectedPiece.getInitialPower() + 1);
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.54f, BoardManager.Instance.getClickedTile().getLocation().z));
                    BoardManager.Instance.wipePossibleMoves();
                    break;
                case Tile.TileTypeEnum.rough:
                    selectedPiece.setPowerVal(selectedPiece.getInitialPower() - 1);
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    BoardManager.Instance.wipePossibleMoves();
                    break;
                case Tile.TileTypeEnum.blueGoal:
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    break;
                case Tile.TileTypeEnum.redGoal:
                    selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                    break;
            }

            if(TurnHandler.Instance.teamTurn == 0)
            {
                if (BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    BlueWin();
                }
                else
                {
                    TurnHandler.Instance.SetRedTurn();
                }
            }
            else
            {
                if(BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    RedWin();
                }
                else
                {
                    TurnHandler.Instance.SetBlueTurn();
                }
            }

            selectedPiece = null;
        }      


    }

    public void BlueWin()
    {
        TurnHandler.Instance.teamTurn = 2;
        TurnHandler.Instance.turnText.text = "BLUE WINS!";
    }

    public void RedWin()
    {
        TurnHandler.Instance.teamTurn = 2;
        TurnHandler.Instance.turnText.text = "RED WINS!";
    }

}
