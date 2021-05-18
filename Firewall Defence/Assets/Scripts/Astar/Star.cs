using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Star
{
    private static Dictionary<Point, Node> nodes;


    private static void CreateNodes()
    {

        nodes = new Dictionary<Point, Node>();

        foreach(TileScript tile in LevelManagement.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }

    }

    public static Stack<Node> GetPath(Point start, Point goal)
    {



        if(nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();



        //no hashset for the pathy boi.  stack means I can backtrack 
       Stack<Node> finalPath = new Stack<Node>();


        Node currentNode = nodes[start];

        openList.Add(currentNode);

        while(openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {

                    Point neigPos = new Point(currentNode.gridPosition.X - x, currentNode.gridPosition.Y - y);

                    if (LevelManagement.Instance.Inbounds(neigPos) && LevelManagement.Instance.Tiles[neigPos].Walkable && neigPos != currentNode.gridPosition)
                    {
                        int gCost = 0;

                        if (Math.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if(!ConnectedDiagonally(currentNode, nodes[neigPos]))
                            {
                                //Jumps to the next loop and doesnt excute 
                                continue;
                            }

                            gCost = 14;
                        }


                        Node neighbour = nodes[neigPos];
                        //  neighbour.TileRef.SpriteRenderer.color = Color.black;


                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        //this if ignores nodes that are undiscovered and adds them to the list
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
                        }


                      //Code was deleted due to it causing infinite loops


                    }


                    //Debuggo stuff

                }
            }


            //Removes the node from the list and then adds it to the closed list.  Closed list is for tiles that are inaccesible
            openList.Remove(currentNode);
            closedList.Add(currentNode);



            if (openList.Count > 0)
            {
                //Not using a loop, this method prevents the game having to loop.  This finds the tile surrounding the game with the lowest F score
                currentNode = openList.OrderBy(n => n.F).First();
            }

            //node has reached final goal node, therefore while looped can be broken
            if(currentNode == nodes[goal])
            {
                //I needed a while loop in here because the start position does not have a parent therefore I get null pointer references
                while (currentNode.gridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                  
                }

                break;
            }

        }





        return finalPath;

        //Debuggo mode
        //GameObject.Find("Debugger").GetComponent<PathDebug>().DebugPath(openList, closedList, finalPath);

    }

    //refresh check to see if the diagonal is free while the baddy is on its path. if not send it back or look for other route
    private static bool ConnectedDiagonally(Node currentNode, Node neigbour)
    {
        Point direction = neigbour.gridPosition - currentNode.gridPosition;

        Point first = new Point(currentNode.gridPosition.X + direction.X, currentNode.gridPosition.Y);
        Point second = new Point(currentNode.gridPosition.X, currentNode.gridPosition.Y + direction.Y);

        //Checks to see if tiles are within the map and if there is a tower on that tile
        if(LevelManagement.Instance.Inbounds(first) && !LevelManagement.Instance.Tiles[first].Walkable)
        {
            return false;
        }
        if(LevelManagement.Instance.Inbounds(second) && !LevelManagement.Instance.Tiles[second].Walkable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }




}
 

