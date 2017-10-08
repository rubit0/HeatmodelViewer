using System;
using System.Collections.Generic;

[Serializable]
public class HeatModel
{
    public string Topic;
    public String Date;
    public List<string> Meta;
    public List<HeatNode> Nodes;
    public List<HeatLinesNode> LinesNodes;
}
