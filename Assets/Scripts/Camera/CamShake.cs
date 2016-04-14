using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{

    public Vector3 originalCameraPosition;
    float shakeAmt = 0;
    public Camera mainCamera;

    void Awake()
    {
        this.mainCamera = GetComponent<Camera>();
    }

    public void ShakeCamera(float magnitude)
    {
        shakeAmt = magnitude * .0025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2f - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
    }

}