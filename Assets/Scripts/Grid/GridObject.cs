using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子对象
/// </summary>
public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    /// <summary>
    /// 返回单位所在的格子位置文本
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string unitString = string.Empty;
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        
        return gridPosition.ToString() + "\n" + unitString;
    }
    
    /// <summary>
    /// 添加单位
    /// </summary>
    /// <param name="unit"></param>
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    /// <summary>
    /// 移除单位
    /// </summary>
    /// <param name="unit"></param>
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
    /// <summary>
    /// 得到单位List
    /// </summary>
    /// <returns></returns>
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

}
