using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MetaCanvas : MonoBehaviour
{
    public Text Topic;
    public Text Date;
    public GameObject Meta;

    private Font _defaultFont;

    private void Awake()
    {
        _defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
    }

    public void UpdateData(HeatModel model)
    {
        SetTopic(model.Topic);
        SetDate(model.Date);
        SetMetaData(model.Meta);
    }

    public void SetTopic(string text)
    {
        Topic.text = text;
    }

    public void SetDate(System.DateTime date)
    {
        Date.text = date.ToShortDateString();
    }

    public void SetMetaData(List<string> data)
    {
        var children = GetAllChild();
        Meta.transform.DetachChildren();
        foreach (var child in children)
        {
            Destroy(child);
        }

        foreach (var item in data)
        {
            var child = new GameObject();
            var text = child.AddComponent<Text>();
            text.font = _defaultFont;
            text.text = item;

            child.transform.SetParent(Meta.transform);
        }
    }

    private List<GameObject> GetAllChild()
    {
        var children = new List<GameObject>();

        var currentChild = Meta.transform.childCount;
        for (int i = 0; i < currentChild; i++)
        {
            children.Add(Meta.transform.GetChild(i).gameObject);
        }

        return children;
    }
}
