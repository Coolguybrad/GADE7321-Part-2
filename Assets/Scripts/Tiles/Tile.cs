using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject possibleMove;
    private bool occupied = false;
    private GameObject occupiedBy;
    [SerializeField] private Vector2 location;


    private void Update()
    {
        
    }

    private void Awake()
    {
        location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
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

    public void setOccupancy(bool status) 
    {
        occupied = status;
    }

    public Vector2 getLocation() 
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



    
}
