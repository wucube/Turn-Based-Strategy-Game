using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自旋转行为
/// </summary>
public class SpinAction : BaseAction
{
    /// <summary>
    /// 旋转总量
    /// </summary> 
    private float totalSpinAmount;

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;

        //每帧旋转增加的值
        float spinAddAmount = 360f * Time.deltaTime;  
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;

        if(totalSpinAmount >= 360f) 
        {
            isActive = false;
            onActionComplete();
        }
            

    } 
    /// <summary>
    /// 自身旋转
    /// </summary>
    public void Spin(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;       
        isActive = true;
        totalSpinAmount  = 0f;
    }
}
