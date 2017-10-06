using UnityEngine;

public class MouseOrbit : MonoBehaviour
{
    public Transform Anchor;
    public Transform Camera;

    //Speed of orbital motion
    public float HorizontalRotationSpeed = 80.0f;
    public float VerticalRotationSpeed = 80.0f;

    //Rotation limit
    public float VerticalRotationLimitMax = 60f;
    public float VerticalRotationLimitMin = -15f;
    public bool InvertRotation = true;

    //Scroll Speed
    public float ZoomSpeed = 2f;

    //Distance Clamp
    public float ZoomNearClipMax = 1.5f;
    public float ZoomDistanceMax = 8f;
    public bool InvertZoom = false;

    private float _anchorRotationX;
    private float _anchorRotationY;

    private void Start()
    {
        _anchorRotationX = Anchor.rotation.x;
        _anchorRotationY = Anchor.rotation.x;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _anchorRotationX += Input.GetAxis("Mouse X") * HorizontalRotationSpeed * 0.1f;
            var yMouse = (InvertRotation) ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");
            _anchorRotationY += yMouse * HorizontalRotationSpeed * 0.1f;
            _anchorRotationY = Mathf.Clamp(_anchorRotationY, VerticalRotationLimitMin, VerticalRotationLimitMax);

            Anchor.rotation = Quaternion.Euler(0, _anchorRotationX, _anchorRotationY);
        }

        var scrollInput = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        if(scrollInput == 0)
            return;

        if (InvertZoom)
            scrollInput *= -1;

        var delta = Vector3.MoveTowards(Camera.localPosition, Anchor.localPosition, scrollInput);
        if (Vector3.Distance(delta, Anchor.localPosition) > ZoomDistanceMax
            || Vector3.Distance(delta, Anchor.localPosition) < ZoomNearClipMax)
            return;

        Camera.localPosition = delta;
    }
}
