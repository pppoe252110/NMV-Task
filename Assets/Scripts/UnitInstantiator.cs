using System.Collections.Generic;
using UnityEngine;
using static RandomExtension;

public class UnitInstantiator : MonoBehaviour
{
    [SerializeField] private UnitRandom[] randomsUnits;

    void Start()
    {
        List<Vector2Int> claimed = new();
        var c = GameManager.AnimalsCount;

        for (int i = 0; i < c; i++)
        {
            var pos = FindFreeSpot(claimed);
            while (claimed.Contains(pos))
            {
                pos = FindFreeSpot(claimed);
            }
            var unit = randomsUnits[ProceedValue(Random.value, randomsUnits)];
            Instantiate(unit.unit, PlaygroundGrid.GetGridPosition(pos.x, pos.y), Quaternion.identity);
            claimed.Add(pos);
        }
    }

    public Vector2Int FindFreeSpot(List<Vector2Int> claimed)
    {
        var pos = new Vector2Int(Random.Range(0, GameManager.GridSize), Random.Range(0, GameManager.GridSize));
        while (claimed.Contains(pos))
        {
            pos = new Vector2Int(Random.Range(0, GameManager.GridSize), Random.Range(0, GameManager.GridSize));
        }
        return pos;
    }
}
[System.Serializable]
public class UnitRandom : RandomData
{
    public GameUnit unit;
}