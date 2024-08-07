using UnityEngine;

public class InheritRotation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _rotationOffset = new Vector3(90, 0, 0);

    private void Start()
    {
        if(_target == null)
            _target = Camera.allCameras[0].transform;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0 + _rotationOffset.x, _target.eulerAngles.y + _rotationOffset.y, 0 + _rotationOffset.z);
    }
}
