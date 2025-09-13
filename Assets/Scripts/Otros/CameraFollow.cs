using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Fields
    [Header("Seguimiento")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 2f;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -10);

    [Header("Límites (Opcional)")]
    [SerializeField] private bool useLimits = false;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        InitializeCamera();
    }
    void LateUpdate()
    {
        FollowTarget();
    }
    #endregion

    #region Public Methods
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    #endregion

    #region Private Methods
    private void InitializeCamera()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }

        if (target != null)
        {
            Vector3 initialPosition = target.position + offset;
            if (useLimits)
            {
                initialPosition.x = Mathf.Clamp(initialPosition.x, minX, maxX);
                initialPosition.y = Mathf.Clamp(initialPosition.y, minY, maxY);
            }
            transform.position = initialPosition;
            Debug.Log("Camera positioned at: " + transform.position + " - Target: " + target.name);
        }
    }

    private void FollowTarget()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;

        if (useLimits)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
    #endregion
}
