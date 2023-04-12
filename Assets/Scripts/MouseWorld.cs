using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld instance;

    /// <summary>
    /// 鼠标射线检测层
    /// </summary>
    /// <returns></returns>
    [SerializeField] private LayerMask mousePlaneLayerMask;
    

    void Awake() 
    {
        instance = this;
    }
    void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    /// <summary>
    /// 获取鼠标射线碰撞点的坐标 
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit raycastHit,float.MaxValue,instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
