using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    #region Fields
    [Header("Target Settings")]
    public Transform target;
    public Vector3 offset;

    [Header("Camera Movement")]
    public float smoothSpeed = 0.125f;
    public float valorOffsetX = 2f;

    [Header("Follow Window")]
    public float ventanaX = 5f;
    public float ventanaY = 5f;

    private GameObject player;
   // private PlayerController pCon;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        InitializeCamera();
    }

    void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    private void OnDrawGizmos()
    {
        DrawFollowWindowGizmos();
    }
#endregion

    #region Private Methods
    private void InitializeCamera()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //pCon = player.GetComponent<PlayerController>();
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void UpdateCameraPosition()
    {
        //if (target == null || pCon == null) return;

        //offset = new Vector3(valorOffsetX * pCon.moveX, offset.y, offset.z);

        float limitR = target.position.x + ventanaX + offset.x;
        float limitL = target.position.x - ventanaX + offset.x;
        float limitU = target.position.y + ventanaY + offset.y;
        float limitD = target.position.y - ventanaY + offset.y;

        float posX = transform.position.x;
        float posY = transform.position.y;

        if (posX > limitR || posX < limitL || posY > limitU || posY < limitD)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void DrawFollowWindowGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector2(ventanaX, ventanaY) * 2);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position - new Vector3(offset.x, offset.y, offset.z), new Vector2(ventanaX, ventanaY) * 2);
    }
    #endregion
}
