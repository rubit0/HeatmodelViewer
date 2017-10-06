using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DetailCamerasController : MonoBehaviour
{
    public PostProcessingProfile PostProcessingProfile;
    private List<Camera> _detailCameras;
    private int _currentDetailCameraIndex;
    private Camera _currentCamera;

    private void Awake()
    {
        var childCount = transform.childCount;
        _detailCameras = new List<Camera>(childCount);

        for (int i = 0; i < childCount; i++)
        {
            var childGameObject = transform.GetChild(i);
            var postProcessor = childGameObject.GetComponent<PostProcessingBehaviour>() 
                ?? childGameObject.gameObject.AddComponent<PostProcessingBehaviour>();
            
            postProcessor.profile = PostProcessingProfile;
            var childCamera = childGameObject.GetComponent<Camera>();
            if (childCamera != null)
            {
                _detailCameras.Add(childCamera);
                var state = childCount == 0;
                childGameObject.gameObject.SetActive(state);
            }
        }
    }

    public void DisableAllCameras()
    {
        var childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
