using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMouseScroll : MonoBehaviour
{
    Vector3 _startDragPos;
    Vector3 _startDragCameraPos;
    bool _move;

    void LateUpdate()
    {
        var plane = new Plane(Vector3.zero, Vector3.forward, Vector3.right);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            _move = true;
            var ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
            plane.Raycast(ray, out float d);

            var point = ray.GetPoint(d);

            _startDragCameraPos = transform.position;
            _startDragPos = point;
        }
        
        if (_move)
            if (Input.GetKey(KeyCode.Mouse0))
            {
                transform.position = _startDragCameraPos;
            
                var ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
                plane.Raycast(ray, out float d);

                var point = ray.GetPoint(d);


                transform.position = _startDragCameraPos + (_startDragPos - point);
            }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _move = false;
        }

        transform.position += Vector3.down * Input.GetAxis("Mouse ScrollWheel") * transform.position.y;
        _startDragCameraPos += Vector3.down * Input.GetAxis("Mouse ScrollWheel") * transform.position.y;
    }
}
