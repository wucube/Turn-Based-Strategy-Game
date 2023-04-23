
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位的行为系统
/// </summary>
public class UnitActionSystem : MonoBehaviour
{   
    public static UnitActionSystem Instance{get;private set;}

    /// <summary>
    /// 选中的单位已改变事件
    /// </summary>
    public event EventHandler OnSelectedUnitChanged;

    /// <summary>
    /// 已选中的单位
    /// </summary>
    [SerializeField] private Unit selectedUnit;

    /// <summary>
    /// 单位的层级
    /// </summary>
    /// <returns></returns>
    [SerializeField] private LayerMask unitLayerMask;

    /// <summary>
    /// 单位是否处于忙碌
    /// </summary>
    private bool isBusy;


    void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogError("There's more than one UnitActionSystem!" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    void Update()
    {
        if(isBusy) return;

        //鼠标左键按下
        if(Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;
            
            //得到鼠标点击的格子位置
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            
            //若鼠标点击的格子位置为有效位置
            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                //单位朝鼠标点击处移动
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
        }
        
        //鼠标右键按下
        if(Input.GetMouseButton(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }
 
    /// <summary>
    /// 设置忙碌状态
    /// </summary>
    private void SetBusy()
    {
        isBusy = true;
    }

    /// <summary>
    /// 清除忙碌状态
    /// </summary>
    private void ClearBusy()
    {
        isBusy = false;
    }
    /// <summary>
    /// 尝试获取鼠标点击的单位
    /// </summary>
    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //如果屏幕射线检测到对象
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            //尝试获取碰撞对象上的 Unit 脚本
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 设置要选择的单位
    /// </summary>
    /// <param name="unit"></param>
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 获取选择的单位
    /// </summary>
    /// <returns></returns>
    public Unit GetSelectedUnit() => selectedUnit;
}
