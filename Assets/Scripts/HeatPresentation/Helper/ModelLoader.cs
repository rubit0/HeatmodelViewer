using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public static class ModelLoader
{
    private static HeatModel LoadHeatMapFromJson(string json)
    {
        return JsonUtility.FromJson<HeatModel>(json);
    }

    public static HeatModel LoadHeatMapFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return LoadHeatMapFromJson(json);
    }

    public static HeatModel LoadHeatMapFromWeb(string address)
    {
        using (var client = new WebClient())
        {
            var json = client.DownloadString(address);
            return LoadHeatMapFromJson(json);
        }
    }

    public static HeatModel GetHeatMapMock()
    {
        var nodes = new List<HeatNode> {
            new HeatNode { Key = "InjectionBlock", Value = 0.65f },
            new HeatNode { Key = "FilterUnit", Value = 0.85f },
            new HeatNode { Key = "Battery", Value = 0.25f },
            new HeatNode { Key = "Trunk", Value = 0.85f },
            new HeatNode { Key = "Temp0", Value = 0.25f },
            new HeatNode { Key = "Temp1", Value = 0.85f },
            new HeatNode { Key = "Temp2", Value = 0.40f },
            new HeatNode { Key = "Radio", Value = 0.85f }
        };

        var linesNode = new HeatLinesNode
        {
            Key = "Trunk",
            Values = new []{ 0.1f, 0.1f, 0.2f, 0.4f, 0.1f, 0.9f, 0.9f, 0.1f }
        };

        var heatModel = new HeatModel
        {
            Topic = "Car stress tests",
            Meta = new List<string> { "Conditions\nTemp: 65C°\tHumidity: 35%\tWeather: Cloudy", "Tester(s): Mr. Gates" },
            Date = DateTime.Now.ToShortDateString(),
            Nodes = nodes,
            LinesNodes = new List<HeatLinesNode> { linesNode }
        };

        return heatModel;
    }
}
