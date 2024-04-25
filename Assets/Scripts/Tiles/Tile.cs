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

    private void Awake()
    {
        location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
    }
    private void OnMouseOver()
    {
        highlight.SetActive(true);
    }
    

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public void setOccupancy() 
    { 
    
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


    
}
