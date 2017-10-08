using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Heat Presentation/Controller/Heat Model Controller")]
public class HeatModelController : MonoBehaviour
{
    public Color Cold = Color.yellow;
    public Color Hot = Color.red;
    public CanvasController CanvasController;

    private Dictionary<string, NodeDrawer> valueDrawer;
    private Dictionary<string, LinesDrawer> heatLineDrawer;

	private void Awake()
    {
        valueDrawer = transform.GetComponentsInChildren<NodeDrawer>(true).ToDictionary(c => c.Key);
        heatLineDrawer = transform.GetComponentsInChildren<LinesDrawer>(true).ToDictionary(c => c.Key);
    }

    public void LoadHeatMapFromFile(string path)
    {
        var model = ModelLoader.LoadHeatMapFromFile(path);
        SetHeatMap(model);
    }

    public void LoadHeatMapFromWeb(string address)
    {
        var model = ModelLoader.LoadHeatMapFromWeb(address);
        SetHeatMap(model);
    }

    public void LoadHeatMapFromMock()
    {
        var model = ModelLoader.GetHeatMapMock();
        SetHeatMap(model);
    }

    private void SetHeatMap(HeatModel heatModel)
    {
        foreach (var value in heatModel.Nodes)
        {
            if (!valueDrawer.ContainsKey(value.Key))
                continue;

            valueDrawer[value.Key].Cold = Cold;
            valueDrawer[value.Key].Hot = Hot;
            valueDrawer[value.Key].Draw(value);
        }

        foreach (var line in heatModel.LinesNodes)
        {
            if (!heatLineDrawer.ContainsKey(line.Key))
                continue;

            heatLineDrawer[line.Key].Cold = Cold;
            heatLineDrawer[line.Key].Hot = Hot;
            heatLineDrawer[line.Key].Draw(line);
        }

        CanvasController.UpdateMetaData(heatModel);
    }
}
