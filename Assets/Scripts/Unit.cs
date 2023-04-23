using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位
/// </summary>
public class Unit : MonoBehaviour
{   
    /// <summary>
    /// 单位对应的格子位置
    /// </summary>
    private GridPosition gridPosition;
    /// <summary>
    /// 单位的移动行为
    /// </summary>
    private MoveAction moveAction;

    /// <summary>
    /// 单位的自旋转行为
    /// </summary>
    private SpinAction spinAction;
    
    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
    }
    private void Start() 
    {
        //根据单位的位置得到对应格子位置
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        //根据单位的格子位置添加对应格子
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition,this);
    }

    // Update is called once per frame
    void Update()
    {
        //根据单位位置得到新的格子位置
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        //如果新的格子位置不等于初始的格子位置，表明单位的格子位置已经改变
        if(newGridPosition != gridPosition)
        {
            //单位移动，对应格子位置也改变
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    /// <summary>
    /// 得到 移动行为 类
    /// </summary>
    /// <returns></returns>
    public MoveAction GetMoveAction()
    {
        return moveAction;
    }
    
    /// <summary>
    /// 获取 自旋转行为 类
    /// </summary>
    /// <returns></returns>
    public SpinAction GetSpinAction()
    {
        return spinAction;
    }
    
    /// <summary>
    /// 得到单位的格子位置
    /// </summary>
    /// <returns></returns>
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    
}
