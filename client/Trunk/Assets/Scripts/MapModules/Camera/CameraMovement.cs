using UnityEngine;

public enum CameraState
{
    Player = 1,
    NPC = 2,
    SkillAnim = 3,
    Null = -1
}

/// <summary>
/// 相机移动
/// </summary>
public class CameraMovement
{
    public Transform cameraTransform;
    public float speed = 10f;

    /// <summary>
    /// 玩家位置
    /// </summary>
    public Transform player;

    /// <summary>
    /// 头顶的相对位置
    /// </summary>
    private Vector3 aboveVector = new Vector3(0f, 10f, -8f);

    /// <summary>
    /// 找到玩家,并初始化相机的几个位置点
    /// </summary>
    public CameraMovement()
    {
        UpdateProxy.Instance.UpdateEvent += Update;
    }

    public void Dispose()
    {
        UpdateProxy.Instance.UpdateEvent -= Update;
    }

    /// <summary>
    /// 实时更新相机位置
    /// </summary>
    private void Update()
    {
        SmoothMovement();
    }

    /// <summary>
    /// 让相机平滑的跟随主角
    /// </summary>
    private void SmoothMovement()
    {
        Vector3 targetPoint = player.transform.position + aboveVector;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPoint, Time.deltaTime * speed);
    }

    /// <summary>
    /// 让相机视角平滑的跟随主角
    /// </summary>
    private void SmoothLookAt()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.position - cameraTransform.position, Vector3.up);

        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * speed);
    }
}
