using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector2 location;
    [SerializeField] private bool canBushWalk;

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
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log(this.gameObject + "Deselected");
            PieceManager.Instance.setSelectedPiece(null);
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            BoardManager.Instance.wipePossibleMoves();
            Debug.Log(this.gameObject + "Selected");
            PieceManager.Instance.setSelectedPiece(this.gameObject.GetComponent<Piece>());
        }
        
    }

    public Vector2 getLocation()
    {
        return location;
    }

    public void movePiece(Vector2 pos) 
    {
        location = pos;
        this.gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.y);
    }

    public bool getBushBool() 
    {
        return canBushWalk;
    }


}
