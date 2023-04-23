using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子系统测试
/// </summary>
public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidActionGridPotitionList());
            
        }
    }
    
}
