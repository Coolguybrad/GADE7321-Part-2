using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    [SerializeField] public Tile[] tileArr;
    [SerializeField] private Tile ClickedTile;

    public Tile blueGoal;
    public Tile redGoal;

    public void setClickedTile(Tile tile)                                                               //sets the tile clicked by the player
    {
        ClickedTile = tile;
    }

    public Tile getClickedTile()                                                                        //returns the tile clicked by the player
    {
        return ClickedTile;
    }

    public void showPossibleMoves(Piece piece)                                                          //check up down left right to see if the piece has legal moves
    {
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.bush)
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                    if (piece.getJumperBool())
                    {
                        if (!piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].getOccupancy())
                        {
                            piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].showPossibleMove();
                        }
                    }
                }
                else if(getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    if(piece.getTeam() == 0)
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }                    
                }
                else if (getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    if (piece.getTeam() == 1)
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                }
            }
            else
            {
                if (getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).getOccupiedBy().getTeam() != piece.getTeam())
                {
                    if (piece.getPowerVal() >= getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).getOccupiedBy().getPowerVal())
                    {
                        getTile(new Vector3(piece.getLocation().x - 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
            }
        }
        catch (Exception e)
        {

            //Debug.Log("Tile at " + (piece.getLocation().x - 1) + "," + piece.getLocation().z + " Does not exist");
        }
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.bush)
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                    if (piece.getJumperBool())
                    {
                        if (!piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[1].getOccupancy())
                        {
                            piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[1].showPossibleMove();
                        }
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    if (piece.getTeam() == 0)
                    {
                        getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    if (piece.getTeam() == 1)
                    {
                        getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                }
            }
            else
            {
                if (getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).getOccupiedBy().getTeam() != piece.getTeam())
                {
                    if (piece.getPowerVal() >= getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).getOccupiedBy().getPowerVal())
                    {
                        getTile(new Vector3(piece.getLocation().x + 1, 0, piece.getLocation().z)).showPossibleMove();
                    }
                }
            }

        }
        catch (Exception e)
        {

            //Debug.Log("Tile at " + (piece.getLocation().x + 1) + "," + piece.getLocation().z + " Does not exist");
        }
        try
        {
            if (!getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).GetTileType() == Tile.TileTypeEnum.bush)
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                    }
                    if (piece.getJumperBool())
                    {
                        if (!piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].getOccupancy())
                        {
                            piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].showPossibleMove();
                        }
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    if (piece.getTeam() == 0)
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    if (piece.getTeam() == 1)
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                    }
                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                }
            }
            else
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).getOccupiedBy().getTeam() != piece.getTeam())
                {
                    if (piece.getPowerVal() >= getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).getOccupiedBy().getPowerVal())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z - 1)).showPossibleMove();
                    }
                }
            }

        }
        catch (Exception e)
        {

            //Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().z - 1) + " Does not exist");
        }
        try
        {

            if (!getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).getOccupancy())
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).GetTileType() == Tile.TileTypeEnum.bush)
                {
                    if (piece.getBushBool())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                    }
                    if (piece.getJumperBool())
                    {
                        if(!piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].getOccupancy())
                        {
                            piece.FindCurrentTile(piece.getLocation()).getJumpTarget()[0].showPossibleMove();
                        }
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).GetTileType() == Tile.TileTypeEnum.blueGoal)
                {
                    if (piece.getTeam() == 0)
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                    }
                }
                else if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).GetTileType() == Tile.TileTypeEnum.redGoal)
                {
                    if (piece.getTeam() == 1)
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                    }
                }
                else
                {
                    getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                }
            }
            else
            {
                if (getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).getOccupiedBy().getTeam() != piece.getTeam())
                {
                    if (piece.getPowerVal() >= getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).getOccupiedBy().getPowerVal())
                    {
                        getTile(new Vector3(piece.getLocation().x, 0, piece.getLocation().z + 1)).showPossibleMove();
                    }
                }
            }
        }
        catch (Exception e)
        {

            //Debug.Log("Tile at " + piece.getLocation().x + "," + (piece.getLocation().z + 1) + " Does not exist");
        }

    }

    public void wipePossibleMoves()                                                                     //hides possible move objects
    {
        for (int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i].hidePossibleMove();
        }
    }

    public Tile getTile(Vector3 position)                                                               //method to get tile at target position
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

    public void Reload()                                                                                //method to reset the scene
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()                                                                                  //quit method
    {
        SceneManager.LoadScene(0);
    }
}
