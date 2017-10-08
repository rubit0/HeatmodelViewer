using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Heat Presentation/Drawer/Lines Drawer")]
public class LinesDrawer : MonoBehaviour
{
    public string Key;
    public Color Cold = Color.yellow;
    public Color Hot = Color.red;
    public Material LineMaterial;
    [Range(0f, 1f)]
    public float LineWidth = 0.05f;

    private LineRenderer _lineRenderer;

    public void Draw(HeatLinesNode data)
    {
        if (_lineRenderer != null)
            Destroy(_lineRenderer);

        _lineRenderer = gameObject.AddComponent<LineRenderer>();

        SetupLineRenderer(_lineRenderer);
        _lineRenderer.colorGradient = BuildGradient(data.Values);

        var childPositions = GetChildPositions();
        _lineRenderer.positionCount = childPositions.Length;
        _lineRenderer.SetPositions(GetChildPositions());
    }

    private void SetupLineRenderer(LineRenderer renderer)
    {
        renderer.material = LineMaterial;
        renderer.numCornerVertices = 4;
        renderer.numCapVertices = 2;
        renderer.useWorldSpace = false;
        renderer.receiveShadows = false;
        renderer.widthMultiplier = LineWidth;
    }

    private Vector3[] GetChildPositions()
    {
        var children = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            var childTransform = transform.GetChild(i);
            children[i] = childTransform.localPosition;
        }

        return children;
    }

    private Gradient GetSingleColorGradient(Color color)
    {
        return new Gradient
        {
            colorKeys = new[]
            {
                new GradientColorKey(color, 0),
                new GradientColorKey(color, 1)
            }
        };
    }

    private Gradient BuildGradient(float[] data)
    {
        if (data.Length > 8)
            Debug.LogWarning("Gradient can't accept more then 8 keys.\nHeat related color will be clamped to 8");

        var gradient = new Gradient();
        var saveLength = Mathf.Clamp(data.Length, 2, 8);
        var gradients = new List<GradientColorKey>(saveLength);

        for (int i = 0; i < saveLength; i++)
        {
            var normalizedData = Mathf.Clamp01(data[i]);
            var color = Color.Lerp(Cold, Hot, normalizedData);

            var colorPos = (i + 1f) / data.Length;
            var colorKey = new GradientColorKey(color, colorPos);
            gradients.Add(colorKey);
        }

        gradient.colorKeys = gradients.ToArray();
        return gradient;
    }
}
