using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    [SerializeField] private UnitFood _targetFoodPrefab;
    private Rigidbody _rb;
    private float _move = 0f;
    private UnitFood _targetFood;

    List<Vector3> _path = new List<Vector3>();

    private void OnDisable()
    {
        _targetFood.onFoodAvailible -= CalculatePath;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _targetFood = Instantiate(_targetFoodPrefab, transform.position, Quaternion.identity);
        _targetFood.onFoodAvailible += CalculatePath;
    }

    private void FixedUpdate()
    {
        if (GameManager.UnitSpeed <= 0)
            return;
        UpdateMovement();
        HandleTargetFoodAvailability();
    }

    private void UpdateMovement()
    {
        if (_path.Count > 1)
        {
            _move += GameManager.UnitSpeed * Time.deltaTime / Mathf.Max(_path.Count, 1);
            ApplyForceToReachNextPosition();
        }
    }

    private void HandleTargetFoodAvailability()
    {
        if (!_targetFood.Available && _path.Count > 0)
        {
            _rb.AddForce(_path.Last() - _rb.position, ForceMode.VelocityChange);
        }
        else if (_targetFood.Available && _path.Count > 0)
        {
            if (_path.Count == 1)
            {
                _path.Add(transform.position);
            }
            var nextPos = Multilerp.MultilerpFunction(_path.ToArray(), _move);
            _rb.AddForce(nextPos - _rb.position, ForceMode.VelocityChange);
        }

        if (_move >= 1f || _path.Count <= 0)
        {
            if (_targetFood.Available && _path.Count > 0)
            {
                _targetFood.ProceedRespawn(true);
            }
        }
    }

    private void CalculatePath()
    {
        _path.Clear();
        var foodCell = PlaygroundGrid.GetCell(_targetFood.transform.position);

        var currentCell = PlaygroundGrid.GetCell(transform.position);
        var currentPos = PlaygroundGrid.GetGridPosition(currentCell.x, currentCell.y);

        _path.Add(currentPos);
        int iteration = 0;
        while ((currentCell != foodCell) && iteration < 100)
        {
            iteration++;
            var direction = (_targetFood.transform.position - currentPos).normalized;

            currentCell = PickNextCell(currentPos, direction);
            currentPos = PlaygroundGrid.GetGridPosition(currentCell.x, currentCell.y);
            _path.Add(currentPos);
        }
    }

    private Vector2Int PickNextCell(Vector3 position, Vector3 direction)
    {
        _move = 0;
        direction.Normalize();

        int directionCount = 8;

        float angle = Mathf.Atan2(direction.z, direction.x);
        float normalizedAngle = Mathf.Repeat(angle / (Mathf.PI * 2f), 1f);

        angle = Mathf.Round(normalizedAngle * directionCount) * Mathf.PI * 2f / directionCount;
        direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

        return PlaygroundGrid.GetCell(position + direction);
    }

    private void ApplyForceToReachNextPosition()
    {
        if (_path.Count > 0)
        {
            var nextPos = Multilerp.MultilerpFunction(_path.ToArray(), _move);
            _rb.AddForce((nextPos - _rb.position)*GameManager.UnitSpeed * Time.deltaTime / Mathf.Max(_path.Count, 1), ForceMode.VelocityChange);
        }
    }
}
