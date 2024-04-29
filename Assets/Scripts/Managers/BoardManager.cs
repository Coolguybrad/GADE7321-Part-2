using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] public Tile[] tileArr;
    [SerializeField] private Piece[] pieceArr;
    [SerializeField] private Tile ClickedTile;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        //for (int i = 0; i < tileArr.Length; i++)
        //{
        //    Debug.Log(tileArr[i] + " " + tileArr[i].getLocation());
        //}
        //for (int i = 0; i < pieceArr.Length; i++)
        //{
        //    Debug.Log(pieceArr[i] + " " + pieceArr[i].getLocation());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        occupy();
    }

    public void occupy()
    {
        for (int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i].setOccupancy(false);
            for (int j = 0; j < pieceArr.Length; j++)
            {
                if (tileArr[i].getLocation() == pieceArr[j].getLocation())
                {
                    tileArr[i].setOccupancy(true);
                    break;
                }
            }
        }
    }

    public void setClickedTile(Tile tile)
    {
        ClickedTile = tile;
    }

    public Tile getClickedTile()
    {
        return ClickedTile;
    }

    public void showPossibleMoves(Piece piece)
    {
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).gameObject.tag == "Bush")
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                    if (piece.getJumperBool())
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                }
            }

        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + (piece.getLocation().x - 1) + "," + piece.getLocation().z + " Does not exist");
        }
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).gameObject.tag == "Bush")
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                    }

                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                }
            }

        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + (piece.getLocation().x + 1) + "," + piece.getLocation().z + " Does not exist");
        }
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).gameObject.tag == "Bush")
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                    }

                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                }
            }

        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().z - 1) + " Does not exist");
        }
        try
        {

            if (!getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).gameObject.tag == "Bush")
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                    }

                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                }

            }
        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().z + 1) + " Does not exist");
        }

    }

    public void wipePossibleMoves()
    {
        for (int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i].hidePossibleMove();
        }
    }



    public Piece getPiece(Vector3 position)
    {
        Piece piece = null;
        for (int i = 0; i < pieceArr.Length; i++)
        {
            if (pieceArr[i].getLocation().Equals(position))
            {
                piece = pieceArr[i];
                break;
            }
        }
        return piece;
    }

    public int getPieceArrIndex(Vector3 position)
    {
        int index = -1;
        for (int i = 0; i < pieceArr.Length; i++)
        {
            if (pieceArr[i].getLocation().Equals(position))
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public Tile getTile(Vector3 position)
    {
        Tile tile = null;
        for (int i = 0; i < tileArr.Length; i++)
        {
            if (tileArr[i].getLocation().Equals(position))
            {
                tile = tileArr[i];
                break;
            }
        }
        return tile;
    }
    public int getTileArrIndex(Vector3 position)
    {
        int index = -1;
        for (int i = 0; i < tileArr.Length; i++)
        {
            if (tileArr[i].getLocation().Equals(position))
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
