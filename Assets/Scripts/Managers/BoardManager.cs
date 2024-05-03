using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] public Tile[] tileArr;
    [SerializeField] private Tile ClickedTile;

    public Tile blueGoal;
    public Tile redGoal;

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

    public void wipePossibleMoves()
    {
        for (int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i].hidePossibleMove();
        }
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
