using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector2 location;
    
    void Start()
    {
        
    }
    private void Awake()
    {
        location = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log(this.gameObject + "Selected");
        PieceManager.Instance.setSelectedPiece(this.gameObject);
    }

    public Vector2 getLocation() 
    {
        return location;
    }

    
}
