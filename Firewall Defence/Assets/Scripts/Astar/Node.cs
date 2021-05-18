using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Node
{

    public Point gridPosition { get; set; }

    public TileScript TileRef { get; private set; }


    public Vector2 WorldPosition { get; set; }

    public Node Parent { get; private set; }

    public int G { get; private set; }
    public int H { get; private set; }
    public int F { get; private set; }

    public Node(TileScript tileRef)
    {
        this.TileRef = tileRef;
        this.gridPosition = tileRef.GridPosition;
        this.WorldPosition = tileRef.WorldPosition;
    }
    public void CalcValues(Node parent, Node goal, int gCost)
    {
        this.Parent = parent;
        this.G = parent.G + gCost;
        this.H = ((Math.Abs(gridPosition.X - goal.gridPosition.X)) + (Math.Abs(goal.gridPosition.Y - gridPosition.Y))) * 10;
        this.F = G + H;
      
    }

   

}
