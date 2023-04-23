using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子系统
/// </summary>
public class GridSystem
{
    /// <summary>
    /// 格子的宽
    /// </summary>
    private int width;
    /// <summary>
    /// 格子的高
    /// </summary>
    private int height;

    /// <summary>
    /// 单位格子的尺寸
    /// </summary>
    private float cellSize;

    /// <summary>
    /// 格子对象数组
    /// </summary>
    private GridObject[,] GridObjectArray;

    public GridSystem(int width, int height,float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        GridObjectArray = new GridObject[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                //创建并记录格子对象
                GridObjectArray[x, z] = new GridObject(this, gridPosition);

                //绘制格子线
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white,1000);
            }
        }
    }

    /// <summary>
    /// 获取格子的世界坐标
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    /// <summary>
    /// 根据世界坐标获取格子位置
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    /// <summary>
    /// 根据格子位置创建格子debug对象
    /// </summary>
    /// <param name="debugPrefab">debug预制体的位置</param>
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                //初始化格子位置
                GridPosition gridPosition = new GridPosition(x, z);
                //实例化格子debug对象并获取位置信息
                Transform debugTransform =  GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                //获取格子debug对象的脚本
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                //设置格子debug对象的位置
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    /// <summary>
    /// 根据格子位置得到格子对象
    /// </summary>
    /// <param name="gridPosition">格子位置</param>
    /// <returns></returns>
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return GridObjectArray[gridPosition.x, gridPosition.z];
    }
    
    /// <summary>
    /// 格子位置是否有效
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 && 
               gridPosition.z >= 0 && 
               gridPosition.x <= width && 
               gridPosition.z <= height; 
    }

    /// <summary>
    /// 获取格子系统的宽
    /// </summary>
    /// <returns></returns>
    public int GetWidth()
    {
        return width;
    }

    /// <summary>
    /// 获取格子系统的高
    /// </summary>
    /// <returns></returns>
    public int GetHeight()
    {
        return height;
    }
}
