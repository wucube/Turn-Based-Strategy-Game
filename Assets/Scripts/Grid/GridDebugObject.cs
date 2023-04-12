using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 格子Debug对象脚本
/// </summary>
public class GridDebugObject : MonoBehaviour
{
    /// <summary>
    /// 格子位置显示文本
    /// </summary>
    [SerializeField] private TextMeshPro textMeshPro;

    private GridObject gridObject;

    /// <summary>
    /// 设置格子对象
    /// </summary>
    /// <param name="girdObject"></param>
    public void SetGridObject(GridObject girdObject)
    {
        this.gridObject = girdObject;
    }

    private void Update() 
    {
        textMeshPro.text = gridObject.ToString();
    }
}
