using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位脚本
/// </summary>
public class Unit : MonoBehaviour
{   
    [SerializeField] private Animator unitAnimator;

    /// <summary>
    /// 旋转速度
    /// </summary>
    [SerializeField] private float rotateSpeed;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField] private float moveSpeed;

    /// <summary>
    /// 移动到的目标位置
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// 单位的初始格子位置
    /// </summary>
    private GridPosition gridPosition;

    private void Awake() {
        
        targetPosition = transform.position;
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
        float stoppingDistance = .1f;

        //单位的位置与目标位置距离大于0.1，单位继承朝目标移动
        if(Vector3.Distance(transform.position,targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            //单位面朝向始终与移动方向一致
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

            unitAnimator.SetBool("IsWalking", true);

        }else{
            unitAnimator.SetBool("IsWalking", false);
        }

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
    /// 单位移动
    /// </summary>
    /// <param name="targetPosition"></param>
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
