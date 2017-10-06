using System;
using System.Collections.Generic;

[Serializable]
public class HeatModel
{
    public string Topic;
    public DateTime Date;
    public List<string> Meta;
    public List<HeatData> Data;

    [Serializable]
    public class HeatData
    {
        public string Key;
        public float Value;
    }
}
