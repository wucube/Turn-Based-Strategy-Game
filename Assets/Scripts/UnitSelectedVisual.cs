 using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选中单位的可视化
/// </summary>
public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() 
    {
        //选择的单位改变事件订阅对应事件处理器
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UpdateVisual();
    }

    /// <summary>
    /// 选择单位已改变的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="empty"></param>
    private void UnitActionSystem_OnSelectedUnitChanged(object sender,EventArgs empty)
    {
        UpdateVisual();
    }
    /// <summary>
    /// 更新单位被选中的标记
    /// </summary>
    private void UpdateVisual()
    {
        if(UnitActionSystem.Instance.GetSelectedUnit()==unit)
        {
            meshRenderer.enabled = true;
        }else
        {
            meshRenderer.enabled = false;
        }
    }
}
