using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private Vector3 dragStartPosition;
    public float dragSpeed = 1f;

    [SerializeField]
    private Transform _objectToFollow;

    public Transform ObjectToFollow
    {
        get { return _objectToFollow; }
        set { _objectToFollow = value; }
    }

    [SerializeField]
    private Vector3 _offset;

    public Vector3 Offset
    {
        get { return _offset; }
        set { _offset = value; }
    }

    void LateUpdate()
    {
        if (_objectToFollow)
        {
            transform.position = _objectToFollow.position + _offset;
        }

        // If left mouse button is pressed down, start dragging
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = Input.mousePosition;
        }

        // If left mouse button is held down and moving, rotate the camera around the target
        if (Input.GetMouseButton(0))
        {
            float rotationX = (dragStartPosition.x - Input.mousePosition.x) * rotationSpeed;
            float rotationY = (dragStartPosition.y - Input.mousePosition.y) * rotationSpeed;
            transform.localEulerAngles += new Vector3(rotationY, rotationX, 0f);
            dragStartPosition = Input.mousePosition;
        }
        // If right mouse button is held down and moving, adjust the _offset
        if (Input.GetMouseButton(1))
        {
            float _offsetX = (dragStartPosition.x - Input.mousePosition.x) * dragSpeed * Time.deltaTime;
            float _offsetY = (dragStartPosition.y - Input.mousePosition.y) * dragSpeed * Time.deltaTime;
            _offset += new Vector3(_offsetX, _offsetY, 0f);
            dragStartPosition = Input.mousePosition;
        }
    }
    
}
