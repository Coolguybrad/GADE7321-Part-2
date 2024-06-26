using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject possibleMove;
    [SerializeField] private bool occupied = false;

    [SerializeField] private Piece occupiedBy;

    [SerializeField] private Vector3 location;

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private PieceManager pieceManager;

    [SerializeField] private int internalRedValue;
    [SerializeField] private int internalBlueValue;
    public enum TileTypeEnum
    {
        normal,
        high,
        bush,
        rough,
        trap,
        blueGoal,
        redGoal
    }

    [SerializeField] private TileTypeEnum thisTileType;

    [SerializeField] private Tile[] jumpTarget;

    private void Awake()
    {
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnMouseOver()
    {
        highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(pieceManager.getSelectedPiece() != null)
        {
            boardManager.setClickedTile(this);
            pieceManager.MoveSelectedPiece();
        }  

    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public bool getOccupancy() 
    {
        return occupied;
    }
    public void setOccupancy(bool status) 
    {
        occupied = status;
    }

    public Vector3 getLocation() 
    {
        return location;
    }

    public void showPossibleMove() 
    {
        possibleMove.SetActive(true);
    }
    public void hidePossibleMove() 
    {
        possibleMove.SetActive(false);
    }

    public void SwapFakes(Piece piece)
    {
        this.setOccupiedBy(piece);
        this.setOccupancy(true);
    }

    public GameObject getPossibleMove() 
    {
        return possibleMove;
    }

    public void setOccupiedBy(Piece theNicePiece)
    {
        occupiedBy = theNicePiece;
    }

    public Piece getOccupiedBy()
    {
        return occupiedBy;
    }
        
    public TileTypeEnum GetTileType()
    {
        return thisTileType;
    }

    public Tile[] getJumpTarget()
    {
        return jumpTarget;
    }

    public int getInternalBlueValue()
    {
        return internalBlueValue;
    }
    public int getInternalRedValue()
    {
        return internalRedValue;
    }
}
