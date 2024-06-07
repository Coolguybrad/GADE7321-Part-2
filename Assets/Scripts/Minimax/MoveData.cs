using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData
{
    public Tile initial;
    public Tile destination;
    public Piece mover;
    public Piece killed;
    public float score = float.NegativeInfinity;
}
