using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 跟随像机的Y偏移值最小值
    /// </summary>
    private const float Min_Follow_Y_Offset = 2f;
    /// <summary>
    /// 跟随像机的Y偏移值最大值
    /// </summary>
    private const float Max_Follow_Y_Offset = 12f;
     
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private CinemachineTransposer cinemachineTransposer;

    /// <summary>
    /// 摄像机的目标跟随偏移值
    /// </summary>
    private Vector3 targetFollowOffset;

    private void Start() 
    {
        //获取 CinemachineVirtualCamera的 Body-Transposer/Follow Offset 属性
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    /// <summary>
    /// 处理移动输入
    /// </summary>
    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        float moveSpeed = 10f;
        //摄像机移动方向向量，使用自身坐标系参与计算，使摄像机一直以自己的面朝向为移动基准。
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    
    /// <summary>
    /// 处理旋转输入
    /// </summary>
    private void HandleRotation()
    {
        //摄像机的旋转方向向量
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.Q))
        {
            rotationVector.y += +1f;
        }
        if(Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;  
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 处理变焦输入
    /// </summary>
    private void HandleZoom()
    {
        //变焦的幅度
        float zoomAmount = 1f;
        //变焦的速度
        float zoomSpeed = 5f;

        if(Input.mouseScrollDelta.y > 0) 
        {
            targetFollowOffset.y += zoomAmount;
        }
        if(Input.mouseScrollDelta.y < 0) 
        {
            targetFollowOffset.y -= zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, Min_Follow_Y_Offset, Max_Follow_Y_Offset);
        
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
