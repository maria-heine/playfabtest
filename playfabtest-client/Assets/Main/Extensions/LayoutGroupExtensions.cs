using UnityEngine;
using UnityEngine.UI;

public static class LayoutGroupExtensions
{
    public static void Clear(this GridLayoutGroup gridLayoutGroup)
    {
        DestoryChildren(gridLayoutGroup.transform);
    }

    public static void Clear(this VerticalLayoutGroup verticalLayoutGroup)
    {
        DestoryChildren(verticalLayoutGroup.transform);
    }

    private static void DestoryChildren(Transform t)
    {
        for (var i = t.childCount; i > 0; i--)
        {
            GameObject.Destroy(t.GetChild(i-1).gameObject);
        }
    }
}
