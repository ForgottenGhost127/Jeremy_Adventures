using UnityEngine;
using System;

public class ParallaxBackground : MonoBehaviour
{
    #region Fields
    [Header("Parallax Settings")]
    [Range(0f, 1f)]
    public float parallaxSpeed = 0.5f;

    [Header("Background Repetition")]
    public bool repeatBackground = true;
    public float backgroundWidth = 20f;

    [Header("References")]
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private Transform[] backgroundLayers;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        InitializeParallax();
    }
    void Update()
    {
        UpdateParallaxPosition();

        if (repeatBackground)
        {
            CheckBackgroundRepetition();
        }
    }
    #endregion

    #region Private Methods
    private void InitializeParallax()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
        }
        else
        {
            Debug.LogError("No se encontró la cámara principal!");
        }

        backgroundLayers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgroundLayers[i] = transform.GetChild(i);
        }
    }

    private void UpdateParallaxPosition()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxSpeed, 0, 0);
        lastCameraPosition = cameraTransform.position;
    }

    private void CheckBackgroundRepetition()
    {
        if (cameraTransform == null) return;
        float distanceFromCamera = transform.position.x - cameraTransform.position.x;

        if (distanceFromCamera < -backgroundWidth)
        {
            transform.position = new Vector3(
                transform.position.x + backgroundWidth * 2f,
                transform.position.y,
                transform.position.z
            );
        }
        else if (distanceFromCamera > backgroundWidth)
        {
            transform.position = new Vector3(
                transform.position.x - backgroundWidth * 2f,
                transform.position.y,
                transform.position.z
            );
        }
    }
    #endregion

    #region Public Methods
    public void SetParallaxSpeed(float newSpeed)
    {
        parallaxSpeed = Mathf.Clamp01(newSpeed);
    }
    public void ResetPosition()
    {
        if (cameraTransform != null)
        {
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
            lastCameraPosition = cameraTransform.position;
        }
    }
    #endregion

}
