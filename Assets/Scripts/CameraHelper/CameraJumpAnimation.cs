using UnityEngine;
using DG.Tweening;

public class CameraJumpAnimation : MonoBehaviour
{
    public Transform Source;

    private Vector3 _originPosition;
    private Vector3 _originRotation;

    private void Awake()
    {
        _originPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = _originPosition;

        transform.DOMove(Source.position, 0.3f).From();
    }
}
