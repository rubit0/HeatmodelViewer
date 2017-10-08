using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas Main;
    public Canvas Detail;
    public MetaCanvas Meta;
    public Canvas Car;

    private void Start()
    {
        Main.enabled = true;
        Detail.enabled = false;
        Meta.gameObject.SetActive(false);
        Car.enabled = false;
    }

    public void UpdateMetaData(HeatModel model)
    {
        Meta.gameObject.SetActive(true);
        Meta.UpdateData(model);
        Car.enabled = true;
    }
}
