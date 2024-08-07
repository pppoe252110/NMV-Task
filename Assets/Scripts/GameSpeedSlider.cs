using UnityEngine;

public class GameSpeedSlider : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _gameSpeedText;
    public void ChangeGameSpeed(float speed)
    {
        _gameSpeedText.text = ((int)speed).ToString() + "x";
        GameManager.SetGameSpeed(speed);
    }
}
