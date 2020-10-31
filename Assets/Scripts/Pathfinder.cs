using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using UnityEngine;

public class PathNode
{
    public Vector2 pos;
    public float fCost;
    public float gCost;
    public float hCost;
    public PathNode parent;

    public PathNode(Vector2 pos)
    {
        this.pos = pos;
        gCost = 0;
        hCost = 0;
        fCost = 0;
    }

    public PathNode(Vector2 pos, PathNode parent)
    {
        this.pos = pos;
        gCost = 0;
        hCost = 0;
        this.parent = parent;
    }
}

public class Pathfinder
{
    GameObject colliderInst;
    Collider2D colliderChecker;
    public Pathfinder()
    {
        Object colliderObject = Resources.Load("CircleChecker");
        colliderInst = GameObject.Instantiate(colliderObject) as GameObject;
        colliderChecker = colliderInst.GetComponent<Collider2D>();
    }

    bool IsNodeWalkable(PathNode node, PathNode node2)
    {
        RaycastHit2D[] result = Physics2D.LinecastAll(node.pos, node2.pos);
        return !result.Any(x => x.transform.tag == "Walls");
    }

    HashSet<PathNode> GetNeighbourNodes(PathNode node)
    {
        HashSet<PathNode> result = new HashSet<PathNode>();
        result.Add(new PathNode(new Vector2(node.pos.x + 1, node.pos.y), node));
        result.Add(new PathNode(new Vector2(node.pos.x, node.pos.y + 1), node));
        result.Add(new PathNode(new Vector2(node.pos.x, node.pos.y - 1), node));
        result.Add(new PathNode(new Vector2(node.pos.x - 1, node.pos.y), node));
        return result;
    }

    float heuristic_cost_estimate(PathNode nodeA, PathNode nodeB)
    {
        float deltaX = Mathf.Abs(nodeA.pos.x - nodeB.pos.x);
        float deltaY = Mathf.Abs(nodeA.pos.y - nodeB.pos.y);

        return deltaX + deltaY;
    }
   
    public LinkedList<PathNode> findPath(PathNode startNode, PathNode endNode)
    {
        Debug.Log("Search started");
        LinkedList<PathNode> path = new LinkedList<PathNode>();
        List<PathNode> openSet = new List<PathNode>();
        List<Vector2> closedCoords = new List<Vector2>();
        List<PathNode> closedSet = new List<PathNode>();
        openSet.Add(startNode);
        while (openSet.Count != 0)
        {
            float minf = float.MaxValue;
            PathNode currentNode = null;
            foreach (PathNode p in openSet)
            {
                if (p.fCost < minf)
                {
                    minf = p.fCost;
                    currentNode = p;
                }
            }
            //Debug.Log(currentNode);
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            closedCoords.Add(new Vector2(currentNode.pos.x, currentNode.pos.y));
            if ((currentNode.pos.x == endNode.pos.x) && (currentNode.pos.y == endNode.pos.y))
            {
                while (currentNode != startNode)
                {
                    path.AddFirst(currentNode);
                    currentNode = currentNode.parent;
                }
                Debug.Log("FOUND!");
                return path;
            }

            foreach (PathNode neighbour in GetNeighbourNodes(currentNode))
            {
                if (!IsNodeWalkable(neighbour, currentNode) || (closedCoords.Any(x => (x.x == neighbour.pos.x) && (x.y == neighbour.pos.y))))
                {
                    
                    continue;
                }
                neighbour.gCost = currentNode.gCost + heuristic_cost_estimate(currentNode, neighbour);
                neighbour.hCost = heuristic_cost_estimate(neighbour, endNode);
                neighbour.fCost = neighbour.hCost + neighbour.gCost;

                bool breaker = false;
                foreach (PathNode openNode in openSet)
                {
                    if ((openNode.pos.x == neighbour.pos.x) && (openNode.pos.y == neighbour.pos.y) && (neighbour.gCost > openNode.gCost))
                    {
                        breaker = true;
                        break;
                    }
                }
                if (breaker)
                {
                    continue;
                }
                

                openSet.Add(neighbour);
            }
        }
        return path;
    }
}
