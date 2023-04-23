using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个的视觉化格子
/// </summary>
public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
