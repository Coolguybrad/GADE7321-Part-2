using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance;
    // Start is called before the first frame update
    [SerializeField] private Piece selectedPiece;

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
    void Start()
    {
        
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
            if(BoardManager.Instance.getClickedTile().GetTileType()==Tile.TileTypeEnum.normal)
            {
                selectedPiece.setPowerVal(selectedPiece.getInitialPower());
                selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                BoardManager.Instance.wipePossibleMoves();
            }
            else if (BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.high)
            {
                selectedPiece.setPowerVal(selectedPiece.getInitialPower() + 1);
                selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.54f, BoardManager.Instance.getClickedTile().getLocation().z));
                BoardManager.Instance.wipePossibleMoves();
            }
            else if (BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.rough)
            {
                selectedPiece.setPowerVal(selectedPiece.getInitialPower()-1);
                selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                BoardManager.Instance.wipePossibleMoves();
            }
            else if (BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.bush)
            {
                selectedPiece.setPowerVal(selectedPiece.getInitialPower());
                selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                BoardManager.Instance.wipePossibleMoves();
            }
            else if (BoardManager.Instance.getClickedTile().GetTileType() == Tile.TileTypeEnum.trap)
            {
                selectedPiece.setPowerVal(0);
                selectedPiece.movePiece(new Vector3(BoardManager.Instance.getClickedTile().getLocation().x, 0.315f, BoardManager.Instance.getClickedTile().getLocation().z));
                BoardManager.Instance.wipePossibleMoves();
            }
        }
    }


    

    
}
