using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本行为抽象类
/// </summary>
public abstract class BaseAction : MonoBehaviour
{
    /// <summary>
    /// 单位
    /// </summary>
    protected Unit unit;
    /// <summary>
    /// 单位是否处于活动状态中
    /// </summary>
    protected bool isActive;

    /// <summary>
    /// 行为完成时的委托
    /// </summary>
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
