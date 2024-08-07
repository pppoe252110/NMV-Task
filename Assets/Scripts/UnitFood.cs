using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnitFood : MonoBehaviour
{
    [SerializeField] private GameObject _foodObject;
    [SerializeField] private ParticleSystem _destroyParticles;

    private bool _isAvailible;
    private Vector2Int _currentCell;

    public UnityAction onFoodAvailible;
    public bool Available => _isAvailible;

    private void Start()
    {
        ProceedRespawn();
    }

    public void ProceedRespawn(bool delayed = false)
    {
        _isAvailible = false;
        StartCoroutine(Respawn(delayed));
    }

    private IEnumerator Respawn(bool delayed = false)
    {
        if (delayed)
        {
            _foodObject.SetActive(false);
            _destroyParticles.Play();
            yield return new WaitForSeconds(1f);
            _destroyParticles.Stop();
            _foodObject.SetActive(true);
        }

        var cell = FoodController.FindFreePosInRange(transform.position, Mathf.Max(GameManager.UnitSpeed * 5f, 5f));

        FoodController.RemoveFood(_currentCell);
        FoodController.AddFood(cell, this);

        _currentCell = cell;


        transform.position = PlaygroundGrid.GetGridPosition(cell.x, cell.y);
        _isAvailible = true;
        onFoodAvailible?.Invoke();
    }
}
