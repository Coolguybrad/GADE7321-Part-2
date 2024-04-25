using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    
    private void OnMouseOver()
    {
        highlight.SetActive(true);
    }
    

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }


    
}
