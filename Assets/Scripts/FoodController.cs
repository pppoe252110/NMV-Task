using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private Dictionary<Vector2Int, UnitFood> _foods = new();

    private static FoodController _instance;

    private static FoodController instance
    {
        get
        {
            return _instance ?? (_instance = FindFirstObjectByType<FoodController>());
        }
    }

    public static void AddFood(Vector2Int cell, UnitFood food)
    {
        instance._foods.Add(cell, food);
    }

    public static void RemoveFood(Vector2Int cell)
    {
        instance._foods.Remove(cell);
    }

    public static Vector2Int FindFreeSpot()
    {
        var pos = new Vector2Int(Random.Range(0, GameManager.GridSize),Random.Range(0, GameManager.GridSize));
        while (instance._foods.ContainsKey(pos))
        {
            pos = new Vector2Int(Random.Range(0, GameManager.GridSize), Random.Range(0, GameManager.GridSize));
        }
        return pos;
    }

    public static Vector2Int FindFreePosInRange(Vector3 targetPos, float distance)
    {
        var pos = FindFreeSpot();
        while (Vector3.Distance(targetPos,PlaygroundGrid.GetGridPosition(pos.x,pos.y)) > distance)
        {
            pos = FindFreeSpot();
        }
        return pos;

    }
}
