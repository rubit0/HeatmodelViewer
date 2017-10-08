using System.Collections.Generic;
using UnityEngine;

public class SceneCamerasController : MonoBehaviour
{
    public Transform OrbitCamera;
    public Transform DetailCameras;
    public Canvas UiCanvas;
    public Canvas CarCanvas;

    private List<Camera> _cameras;

    private void Awake()
    {
        var orbitCams = OrbitCamera.GetComponentsInChildren<Camera>(true);
        var detailCams = DetailCameras.GetComponentsInChildren<Camera>(true);
        _cameras = new List<Camera>(orbitCams.Length + detailCams.Length);
        _cameras.AddRange(orbitCams);
        _cameras.AddRange(detailCams);
    }

    public void SwitchToOrbit()
    {
        ToggleHeatModelVisibilty(false);

        DetailCameras.gameObject.SetActive(false);
        OrbitCamera.gameObject.SetActive(true);
        CarCanvas.enabled = true;
        UiCanvas.enabled = false;
    }

    public void SwitchToDetail()
    {
        ToggleHeatModelVisibilty(true);

        DetailCameras.gameObject.SetActive(true);
        OrbitCamera.gameObject.SetActive(false);
        CarCanvas.enabled = false;
        UiCanvas.enabled = true;
    }

    private void ToggleHeatModelVisibilty(bool state)
    {
        foreach (var camera in _cameras)
        {
            if(!state)
                camera.cullingMask &= ~(1 << 8);
            else
                camera.cullingMask = -1;
        }
    }
}
