using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 location;
    [SerializeField] private bool canBushWalk;
    [SerializeField] private bool jumper;

    [SerializeField] private int team;
    [SerializeField] private int powerVal;

    void Start()
    {

    }

    private void Awake()
    {
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
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

    public Vector3 getLocation()
    {
        return location;
    }

    public void movePiece(Vector3 pos) 
    {
        location = pos;
        this.gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.z);
    }

    public bool getBushBool() 
    {
        return canBushWalk;
    }

    public void setPowerVal(int newPower)
    {
        powerVal = newPower;
    }

    public int getPowerVal()
    {
        return powerVal;
    }

    public void MinorSpellingError()
    {
        Destroy(this.gameObject);
    }

}
