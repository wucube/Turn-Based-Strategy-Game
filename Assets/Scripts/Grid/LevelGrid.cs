using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关上格子对象
/// </summary>
public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance{ get; private set; }

    /// <summary>
    /// 格子debug对象预制体
    /// </summary>
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    private void Awake() 
    {
        //如果自己的实例已经存在，就不必再赋值给Instance，直接删除自己并返回。
        if(Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;


        gridSystem = new GridSystem(10,10,2f);

        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        
    }

    /// <summary>
    /// 根据格子位置添加对应单位
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="unit"></param>
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        //根据格子位置获取对应格子对象
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        //添加格子对象上的单位
        gridObject.AddUnit(unit);
    }

    /// <summary>
    /// 根据格子位置得到单位List
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) 
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);

        return gridObject.GetUnitList();
    }
    /// <summary>
    /// 根据格子位置移除对应单位
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="unit"></param>
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    /// <summary>
    /// 单位移动改变对应格子位置
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="fromGridPosition">单位离开的格子位置</param>
    /// <param name="toGridPosition">单位达到的格子位置</param>
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);

        AddUnitAtGridPosition(toGridPosition, unit);
    }

    /// <summary>
    /// 根据世界坐标获取对应的格子位置
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
}
