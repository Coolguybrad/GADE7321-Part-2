using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{
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
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        powerVal = initialPower;
        powerValDisplay.text = powerVal.ToString() + "/" + initialPower.ToString();

        currentTile = FindCurrentTile(location);

        currentTile.setOccupancy(true);
        currentTile.setOccupiedBy(this);
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
        if(TurnHandler.Instance.teamTurn ==  team)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                BoardManager.Instance.wipePossibleMoves();
                Debug.Log(this.gameObject + "Selected");
                PieceManager.Instance.setSelectedPiece(this.gameObject.GetComponent<Piece>());
            }
        }           
    }

    public Vector3 getLocation()
    {
        return location;
    }

    public void movePiece(Vector3 pos) 
    {
        currentTile.setOccupancy(false);
        currentTile.setOccupiedBy(null);
        location = pos;
        this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        powerValDisplay.text = powerVal.ToString() + "/" + initialPower.ToString();
        currentTile = FindCurrentTile(location);
        currentTile.setOccupancy(true);
        currentTile.setOccupiedBy(this);
    }

    public bool getBushBool() 
    {
        return canBushWalk;
    }
    public bool getJumperBool()
    {
        return jumper;
    }

    public int getPowerVal()
    {
        return powerVal;
    }

    public void setPowerVal(int newPower)
    {
        powerVal = newPower;
    }

    public int getInitialPower()
    {
        return initialPower;
    }

    public int getTeam()
    {
        return team;
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
            //Debug.Log("test" + i);
            if (BoardManager.Instance.tileArr[i].getLocation().x == targetPos.x && BoardManager.Instance.tileArr[i].getLocation().z == targetPos.z)
            {
                tileToFind = tile;
            }
            i++;
        }

        return tileToFind;
    }

}
