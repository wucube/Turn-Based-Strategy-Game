using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视觉化的格子系统
/// </summary>
public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance{get;private set;}

    /// <summary>
    /// 格子系统视觉化的单个预制体
    /// </summary>
    [SerializeField] private Transform gridSystemVisualSinglePrefab;

    /// <summary>
    /// 单个视觉化格子数组
    /// </summary>
    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

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
    // Start is called before the first frame update
    void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                //得到格子位置
                GridPosition gridPosition = new GridPosition(x, z);
                //根据格子位置实例化可视化格子预制体
                Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                //将实例化的可视化格子位置添加到列表中
                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    void Update()
    {
        UpdateGridVisual();
    }
    
    /// <summary>
    /// 隐藏所有视觉化的格子
    /// </summary>
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }
    
    /// <summary>
    /// 显示格子位置列表中的所有格子
    /// </summary>
    /// <param name="gridPositionList"></param>
    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    /// <summary>
    /// 更新显示可视化的格子
    /// </summary>
    private void UpdateGridVisual()
    {
        HideAllGridPosition();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPotitionList());
    }
}
