using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位的移动行为
/// </summary>
public class MoveAction : BaseAction
{

    [SerializeField] private Animator unitAnimator;
    /// <summary>
    /// 最大移动距离
    /// </summary>
    [SerializeField] private int maxMoveDistance = 4;

    /// <summary>
    /// 移动到的目标位置
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// 旋转速度
    /// </summary>
    [SerializeField] private float rotateSpeed = 4;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField] private float moveSpeed = 5;

    protected override void Awake() 
    {
        base.Awake();
        targetPosition = transform.position;
    }
    private void Update()
    {
        if(!isActive) return;

        float stoppingDistance = .1f;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        //单位的位置与目标位置距离大于0.1，单位继承朝目标移动
        if(Vector3.Distance(transform.position,targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            unitAnimator.SetBool("IsWalking", true);

        }else{ //否则停止移动
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();
        }

        //单位面朝向始终与移动方向一致
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 单位朝目标格子位置移动
    /// </summary>
    /// <param name="gridPosition"></param>
    public void Move(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
    }

    /// <summary>
    /// 是否为有效行为的格子位置
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPotitionList();
        return validGridPositionList.Contains(gridPosition);
    }

    /// <summary>
    /// 获取有效行为的格子位置列表
    /// </summary>
    /// <returns></returns>
    public List<GridPosition> GetValidActionGridPotitionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        
        //单位格子的位置
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                //格子的偏移位置
                GridPosition offsetGridPosition = new GridPosition(x, z);
                //格子的目标位置
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                //单位已在相同的位置
                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }

                //格子位置已被另一个单位占用
                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}

