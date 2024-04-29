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

    public enum TileTypeEnum
    {
        normal,
        high,
        bush,
        rough,
        trap,
        goal
    }

    [SerializeField] private TileTypeEnum thisTileType;

    [SerializeField] private Tile[] jumpTarget;


    private void Update()
    {
        
    }

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
        BoardManager.Instance.setClickedTile(this);
        PieceManager.Instance.MoveSelectedPiece();

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
}
