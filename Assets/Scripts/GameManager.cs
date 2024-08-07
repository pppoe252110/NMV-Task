using System;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _gridSize = 16;
    [SerializeField] private float _unitSpeed = 1f;
    [SerializeField] private int _animalsCount = 2;
    [SerializeField] private float _gameSpeed = 1;

    public static int GridSize => Instance._gridSize; 
    public static int FoodCount => Instance._animalsCount;
    public static float UnitSpeed => Instance._unitSpeed * (Instance._gameSpeed / 10f);
    public static int AnimalsCount => Instance._animalsCount; 

    private static GameManager Instance
    {
        get
        {
            return _instance ?? (_instance = FindFirstObjectByType<GameManager>());
        }
    }

    private static GameManager _instance;

    internal void SetUnitSpeed(float speed)
    {
        _unitSpeed = Mathf.Clamp(speed, 0, 100);
    }

    internal void SetGridSize(float n)
    {
        _gridSize = (int)n;
    }
    
    internal void SetAnimalsCount(float m)
    {
        _animalsCount = (int)m;
    }

    internal static void SetGameSpeed(float v)
    {
        Instance._gameSpeed = v;
        Time.timeScale = v * v > 0 ? math.remap(1f, 1000f, 1f, 100f, v) : 0;
    }
}
