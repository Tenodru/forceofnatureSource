using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FirstPersonPlayerController))]
public class CameraController : MonoBehaviour
{
    FirstPersonPlayerController playerController;

    #region Variables
    private bool camSmoothing;
    private float accumulatorX;
    private float accumulatorY;
    private float responsiveness;
    private float lookSpeed;
    private Vector2 rotation = Vector2.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<FirstPersonPlayerController>();
        camSmoothing = playerController.GetCamSmoothingEnabled();
        lookSpeed = playerController.GetLookSpeed();
        responsiveness = playerController.GetCamResponsiveness();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //If camera smoothing is enabled, lerp camera movement.
        if (camSmoothing)
        {
            accumulatorY = Mathf.Lerp(accumulatorY, Input.GetAxis("Mouse Y"), responsiveness * Time.deltaTime);
            accumulatorX = Mathf.Lerp(accumulatorX, Input.GetAxis("Mouse X"), responsiveness * Time.deltaTime);

            //Sets camera rotation axes to accelerated mouse movement axes.
            rotation.y += accumulatorY * -1.0f;
            rotation.y = Mathf.Clamp(rotation.y, -30f, 30f); //Limits how far player can look up or down.
            rotation.x += accumulatorX;
        }
        else
        {
            //Sets camera rotation axes to mouse movement axes.
            rotation.y += Input.GetAxis("Mouse Y") * -1.0f;
            rotation.y = Mathf.Clamp(rotation.y, -30f, 30f); //Limits how far player can look up or down.
            rotation.x += Input.GetAxis("Mouse X");
        }

        //Rotates the camera.
        transform.eulerAngles = new Vector2(0, rotation.x) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.y * lookSpeed, 0, 0);
    }
}
