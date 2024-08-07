using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlaygroundGrid : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private Material _gridMaterial;

    private static PlaygroundGrid _instance;

    private static PlaygroundGrid instance
    {
        get
        {
            return _instance ?? (_instance = FindFirstObjectByType<PlaygroundGrid>());
        }
    }

    private void Start()
    {
        GenerateGrid();
    }

    public static Vector3 GetGridPosition(int x, int y)
    {
        var size = GameManager.GridSize;
        return instance._grid.position + new Vector3(
            (float)x - size / 2f + 0.5f,
            0,
            (float)y - size / 2f + 0.5f);
    }
    
    public static Vector2Int GetCell(Vector3 pos)
    {
        var size = GameManager.GridSize;

        return new Vector2Int(Mathf.RoundToInt(pos.x + size / 2f - 0.5f), Mathf.RoundToInt(pos.z + size / 2f - 0.5f));
    }

    private void GenerateGrid()
    {
        var size = GameManager.GridSize;
        _grid.transform.localScale = new Vector3(size, size, 1);
        _gridMaterial.SetVector("_GridSize", new Vector4(size, size));
    }
}
