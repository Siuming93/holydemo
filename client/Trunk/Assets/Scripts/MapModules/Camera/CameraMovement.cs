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
public class CameraMovement : MonoBehaviour
{
    public float Speed;

    /// <summary>
    /// 相机状态
    /// </summary>
    private CameraState cameraState = CameraState.Player;

    /// <summary>
    /// 相机可以移动的点
    /// </summary>
    private Vector3[] points;

    /// <summary>
    /// 玩家位置
    /// </summary>
    protected Transform player;

    /// <summary>
    /// 头顶的相对位置
    /// </summary>
    private Vector3 aboveVector;

    public CameraState CameraState
    {
        get { return cameraState; }
        set { cameraState = value; }
    }

    /// <summary>
    /// 找到玩家,并初始化相机的几个位置点
    /// </summary>
    private void Start()
    {
        player = GameObject.FindWithTag(Tags.Player).transform;
        points = new Vector3[5];
        aboveVector = player.transform.position + new Vector3(0f, transform.position.y, 0f);

        Vector3 v = transform.position - new Vector3(player.position.x, aboveVector.y, player.position.z);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = (i * 0.25f) * v;
        }
    }

    /// <summary>
    /// 实时更新相机位置
    /// </summary>
    private void Update()
    {
        switch (CameraState)
        {
            case CameraState.Player:
                SmoothMovement();
                SmoothLookAt();
                break;
        }
    }

    /// <summary>
    /// 让相机平滑的跟随主角
    /// </summary>
    private void SmoothMovement()
    {
        Vector3 targetPoint = points[4];
        for (int i = points.Length - 1; i >= 0; i--)
        {
            Vector3 point = new Vector3(player.position.x, aboveVector.y, player.position.z) + points[i];
            if (CanSeePlayer(point))
            {
                targetPoint = point;
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * Speed);
    }

    /// <summary>
    /// 让相机视角平滑的跟随主角
    /// </summary>
    private void SmoothLookAt()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * Speed);
    }

    /// <summary>
    /// 通过射线检测判断所给位置是否能够看到主角
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool CanSeePlayer(Vector3 position)
    {
        bool b = false;
        RaycastHit raycastHit = new RaycastHit();
        Vector3 direction = player.position - position;

        if (Physics.Raycast(position, direction, out raycastHit))
        {
            if (raycastHit.collider.gameObject.tag == Tags.Player)
                return true;
        }

        return b;
    }
}
