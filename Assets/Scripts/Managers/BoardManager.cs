using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Tile[] tileArr;
    [SerializeField] private Piece[] pieceArr;


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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showPossibleMoves(Piece piece)
    {
        try
        {
            getTile(new Vector2(piece.getLocation().x - 1, piece.getLocation().y)).showPossibleMove();
        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + (piece.getLocation().x - 1) + "," + piece.getLocation().y + " Does not exist");
        }
        try
        {
            getTile(new Vector2(piece.getLocation().x + 1, piece.getLocation().y)).showPossibleMove();
        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + (piece.getLocation().x + 1) + "," + piece.getLocation().y + " Does not exist");
        }
        try
        {
            getTile(new Vector2(piece.getLocation().x, piece.getLocation().y - 1)).showPossibleMove();
        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().y - 1) + " Does not exist");
        }
        try
        {
            getTile(new Vector2(piece.getLocation().x, piece.getLocation().y + 1)).showPossibleMove();
        }
        catch (Exception e)
        {

            Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().y + 1) + " Does not exist");
        }

    }

    public void wipePossibleMoves()
    {
        for (int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i].hidePossibleMove();
        }
    }

    //public void hidePossibleMoves(Piece piece)
    //{
    //    try
    //    {
    //        getTile(new Vector2(piece.getLocation().x - 1, piece.getLocation().y)).hidePossibleMove();
    //    }
    //    catch (Exception e)
    //    {

    //        Debug.Log("Tile at " + (piece.getLocation().x - 1) + "," + piece.getLocation().y + " Does not exist");
    //    }
    //    try
    //    {
    //        getTile(new Vector2(piece.getLocation().x + 1, piece.getLocation().y)).hidePossibleMove();
    //    }
    //    catch (Exception e)
    //    {

    //        Debug.Log("Tile at " + (piece.getLocation().x + 1) + "," + piece.getLocation().y + " Does not exist");
    //    }
    //    try
    //    {
    //        getTile(new Vector2(piece.getLocation().x, piece.getLocation().y - 1)).hidePossibleMove();
    //    }
    //    catch (Exception e)
    //    {

    //        Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().y - 1) + " Does not exist");
    //    }
    //    try
    //    {
    //        getTile(new Vector2(piece.getLocation().x, piece.getLocation().y + 1)).hidePossibleMove();
    //    }
    //    catch (Exception e)
    //    {

    //        Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().y + 1) + " Does not exist");
    //    }
    //}

    public Piece getPiece(Vector2 position)
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

    public int getPieceArrIndex(Vector2 position)
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

    public Tile getTile(Vector2 position)
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
    public int getTileArrIndex(Vector2 position)
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
