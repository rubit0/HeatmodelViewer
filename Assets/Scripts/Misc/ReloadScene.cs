using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        var current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name, LoadSceneMode.Single);
    }
}
