using UnityEngine;

[AddComponentMenu("Heat Presentation/Drawer/Node Drawer")]
public class NodeDrawer : MonoBehaviour
{
    public string Key;
    private Material material;

    public Color Cold = Color.yellow;
    public Color Hot = Color.red;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    
    public void Draw(HeatNode data)
    {
        var color = Color.Lerp(Cold, Hot, data.Value);
        color.a = 0.65f;

        material.color = color;
    }
}
