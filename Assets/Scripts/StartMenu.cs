using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _grid;
    [SerializeField] private TextMeshProUGUI _n;
    [SerializeField] private TextMeshProUGUI _m;
    [SerializeField] private Slider _mSlider;
    [SerializeField] private TextMeshProUGUI _v;

    public void SetN(float n)
    {
        _gameManager.SetGridSize(n);
        _mSlider.maxValue = n * n / 2f;
        _n.text = "N: " + ((int)n).ToString();
    }

    public void SetM(float m)
    {
        _gameManager.SetAnimalsCount(m);
        _m.text = "M: " + ((int)m).ToString();
    }

    public void SetV(float v)
    {
        _gameManager.SetUnitSpeed(v);
        _v.text = "V: " + ((int)v).ToString();
    }

    public void Done()
    {
        _gameManager.gameObject.SetActive(true);
        _grid.SetActive(true);
        gameObject.SetActive(false);
    }
}
