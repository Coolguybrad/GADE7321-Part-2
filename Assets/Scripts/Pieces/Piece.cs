using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 location;
    [SerializeField] private bool canBushWalk;
    [SerializeField] private bool jumper;

    [SerializeField] private int team;
    [SerializeField] private int initialPower;
    [SerializeField] private int powerVal;

    [SerializeField] private TMP_Text powerValDisplay;

    [SerializeField] private Tile currentTile;

    void Start()
    {
        powerVal = initialPower;
        powerValDisplay.text = powerVal.ToString() + "/" + initialPower.ToString();

        currentTile = FindCurrentTile(location);
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
        this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        powerValDisplay.text = powerVal.ToString() + "/" + initialPower.ToString();
        currentTile = FindCurrentTile(location);
    }

    public bool getBushBool() 
    {
        return canBushWalk;
    }
    public bool getJumperBool()
    {
        return jumper;
    }

    public void setPowerVal(int newPower)
    {
        powerVal = newPower;
    }

    public int getInitialPower()
    {
        return initialPower;
    }

    public void MinorSpellingError()
    {
        Destroy(this.gameObject);
    }

    public Tile FindCurrentTile(Vector3 targetPos)
    {
        Tile tileToFind = null;

        int i = 0;

        foreach (Tile tile in BoardManager.Instance.tileArr)
        {
            if (BoardManager.Instance.tileArr[i].getLocation().x == targetPos.x && BoardManager.Instance.tileArr[i].getLocation().z == targetPos.z)
            {
                tileToFind = tile;
            }
            i++;
        }

        return tileToFind;
    }

}
