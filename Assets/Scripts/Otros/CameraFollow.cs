using System;
using UnityEngine;

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

    [Header("Look Down Settings")]
    public KeyCode lookDownKey = KeyCode.S;
    public float lookDownOffset = 8f;
    public float lookDownSpeed = 2f;

    [Header("Camera Limits")]
    public bool useCameraLimits = true;
    public float maxCameraY = 7f;
    public float minCameraY = -10f;

    private GameObject player;
    private PlayerMovement pMov;
    private bool isLookingDown = false;
    private float currentLookDownOffset = 0f;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        InitializeCamera();
    }
    void Update()
    {
        HandleLookDownInput();
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
            pMov = player.GetComponent<PlayerMovement>();
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void HandleLookDownInput()
    {
        if (Input.GetKey(lookDownKey))
        {
            if (!isLookingDown)
            {
                isLookingDown = true;
            }
        }
        else
        {
            if (isLookingDown)
            {
                isLookingDown = false;
            }
        }

        float targetLookDown = isLookingDown ? -lookDownOffset : 0f;
        currentLookDownOffset = Mathf.Lerp(currentLookDownOffset, targetLookDown, lookDownSpeed * Time.deltaTime);
    }

    private void UpdateCameraPosition()
    {
        if (target == null || pMov == null) return;
        Vector3 finalOffset = new Vector3(
            valorOffsetX * pMov.moveX,
            offset.y + currentLookDownOffset,
            offset.z
        );

        float limitR = target.position.x + ventanaX + finalOffset.x;
        float limitL = target.position.x - ventanaX + finalOffset.x;
        float limitU = target.position.y + ventanaY + finalOffset.y;
        float limitD = target.position.y - ventanaY + finalOffset.y;

        float posX = transform.position.x;
        float posY = transform.position.y;

        if (posX > limitR || posX < limitL || posY > limitU || posY < limitD || isLookingDown)
        {
            Vector3 desiredPosition = target.position + finalOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            if (useCameraLimits)
            {
                smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minCameraY, maxCameraY);
            }

            transform.position = smoothedPosition;
        }
    }

    private void DrawFollowWindowGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector2(ventanaX, ventanaY) * 2);
        Vector3 finalOffset = new Vector3(
            target != null && pMov != null ? valorOffsetX * pMov.moveX : offset.x,
            offset.y + currentLookDownOffset,
            offset.z
        );
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position - finalOffset, new Vector2(ventanaX, ventanaY) * 2);

        if (isLookingDown)
        {
            Gizmos.color = new Color(1, 1, 0, 0.8f);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * lookDownOffset);
        }

        if (useCameraLimits)
        {
            Gizmos.color = new Color(1, 0, 1, 0.3f);
            Gizmos.DrawLine(new Vector3(-100, maxCameraY, 0), new Vector3(100, maxCameraY, 0));
            Gizmos.DrawLine(new Vector3(-100, minCameraY, 0), new Vector3(100, minCameraY, 0));
        }
    }
    #endregion

    #region Public Methods
    public void SetLookDownKey(KeyCode newKey)
    {
        lookDownKey = newKey;
    }
    public void SetLookingDown(bool lookDown)
    {
        isLookingDown = lookDown;
    }
    public bool IsLookingDown()
    {
        return isLookingDown;
    }
    #endregion
}
