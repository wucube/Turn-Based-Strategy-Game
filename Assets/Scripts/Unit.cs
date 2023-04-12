using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake() {
        
        targetPosition = transform.position;
        
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
