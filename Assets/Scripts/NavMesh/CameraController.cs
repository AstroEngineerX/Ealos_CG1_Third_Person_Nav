using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _target; // The target for the camera to follow
    public float _smoothSpeed = 5f; // The speed of the Interpolation
    public Vector3 _offset; // Camera offset from the target position



    // Update is called once per frame
    void Update()
    {
        if (_target == null) return;//If there is no target, exit the method
        Vector3 _desiredPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, _target.position.z + _offset.z);
        Vector3 _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed * Time.deltaTime);//Lerp does a linear interpolation between two points. It takes three parameters: the start point, the end point, and a value between 0 and 1 that represents how far to interpolate between the two points.
        //                                      transform.position Sets the camera position to the smoothed position
        transform.position = _smoothedPosition;
    }
}
