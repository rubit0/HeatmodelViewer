using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class HeatModelController : MonoBehaviour
{
    public CanvasController CanvasController;
    private Dictionary<string, Material> _gameObjects;

	private void Awake()
    {
        _gameObjects = new Dictionary<string, Material>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var mat = transform.GetChild(i).GetComponent<Renderer>().material;

            _gameObjects.Add(child.name, mat);
        }
	}

    public void LoadHeatMapFromFile(string path)
    {
        var json = File.ReadAllText(path);
        LoadHeatMapFromJson(json);
    }

    public void LoadHeatMapFromWeb(string address)
    {
        using (var client = new WebClient())
        {
            var json = client.DownloadString(address);
            LoadHeatMapFromJson(json);
        }
    }

    public void LoadHeatMapFromMock()
    {
        var dates = new List<HeatModel.HeatData> { 
            new HeatModel.HeatData { Key = "InjectionBlock", Value = 0.65f },
            new HeatModel.HeatData { Key = "FilterUnit", Value = 0.85f },
            new HeatModel.HeatData { Key = "Battery", Value = 0.25f },
            new HeatModel.HeatData { Key = "Trunk", Value = 0.85f }
        };

        var heatModel = new HeatModel
        {
            Topic = "Tests",
            Meta = new List<string> { "Weather: Cloudy" },
            Data = dates
        };
 
        SetHeatMap(heatModel);
    }

    private void SetHeatMap(HeatModel heatModel)
    {
        foreach (var item in heatModel.Data)
        {
            if (_gameObjects.ContainsKey(item.Key))
            {
                var color = Color.Lerp(Color.yellow, Color.red, item.Value);
                color.a = 0.65f;

                _gameObjects[item.Key].color = color;
            }
        }

        CanvasController.UpdateMetaData(heatModel);
    }

    private void LoadHeatMapFromJson(string json)
    {
        var heatModel = JsonUtility.FromJson<HeatModel>(json);
        SetHeatMap(heatModel);
    }
}
