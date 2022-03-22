using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; //The Transform of the 'Player'

    public float smoothSpeed = 0.125f; //The higher the value, the faster the camera will lock onto target
    public Vector3 cameraOffset; //The Offset which will be applied to the Camera Position 
    public bool smoothOn;
    public bool followCam = true;

    Quaternion initialRot;

    private void Start() {
        initialRot = transform.rotation;
    }

    private void LateUpdate() //LateUpdate is run after Update
    {
        Vector3 desiredPosition = player.position + cameraOffset; //The position of the Camera with the Offset applied (No Smoothing)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); //The position of the Camera with the Offset applied (Smoothed)

        if(followCam == true)
        {
            if (smoothOn == false)
            {
                transform.position = desiredPosition;
            }
            else
            {
                transform.position = smoothedPosition;
            }
        }
        else
        {
            CameraLookAt();
        }
    }

    void CameraLookAt()
    {
        transform.position = transform.position;
        
        transform.LookAt(player);
    }

    public void ResetRotation()
    {
        transform.rotation = initialRot;
    }
}

/*
 * References:
 *      - https://youtu.be/MFQhpwc6cKE
*/
