// Date   : 13.10.2018 09:56
// Project: Game2
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapGrid : MonoBehaviour
{

    private List<GridObject>[,] grid;

    private int width;
    private int height;

    [SerializeField]
    private bool debugMode = false;

    public void Initialize(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new List<GridObject>[width, height];
    }

    public void Clear()
    {
        grid = null;
    }

    private void Log(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }

    public void AddObject(GridObject gridMember, int x, int y)
    {
        if (gridMember == null)
        {

            Log(string.Format("Could not add null object: {0}!", gridMember));
            return;
        }
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            Log(string.Format("Could not add {0} to [x: {1}, y: {2}]: Out of bounds.", gridMember, x, y));
            return;
        }
        if (grid[x, y] == null)
        {
            grid[x, y] = new List<GridObject>();
        }
        grid[x, y].Add(gridMember);
    }

    public List<GridObject> Get(int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            Log(string.Format("<color=red><b>ERROR!</b></color> [x: {0}, y: {1}] out of bounds!", x, y));
            return null;
        }
        List<GridObject> gridObjects = grid[x, y];
        if (gridObjects == null)
        {
            Log(string.Format("Could not find any objects at [x: {0}, y: {1}]", x, y));
            return Enumerable.Empty<GridObject>().ToList<GridObject>();
        }
        return gridObjects;
    }

    public bool CanMoveIntoPosition(int x, int y)
    {
        List<GridObject> gridObjects = Get(x, y);
        if (gridObjects != null)
        {
            bool noWalls = true;
            foreach(GridObject gridObject in gridObjects)
            {
                Log(string.Format(
                    "Checking [x: {0}, y: {1}] for movement blocking objects: {2} found.",
                    x, y, gridObject.CollisionType
                ));
                if (gridObject.CollisionType == CollisionType.Wall)
                {
                    noWalls = false;
                    break;
                }
            }
            return noWalls;
        }
        return false;
    }

    public List<GridObject> GetAllWithActivationId(int activationId)
    {
        List<GridObject> gridObjects = new List<GridObject>();
        for (int i = 0; i < height; i += 1)
        {
            for (int j = 0; j < width; j += 1)
            {
                foreach(GridObject gridObject in Get(j, i))
                {
                    Log(string.Format("GridObject '{0}' activationId: {1}. Looking for: {2}",
                        gridObject, gridObject.ActivationId, activationId
                    ));
                    if (gridObject.ActivationId == activationId)
                    {
                        gridObjects.Add(gridObject);
                    }
                }
            }
        }
        return gridObjects;
    }

}
