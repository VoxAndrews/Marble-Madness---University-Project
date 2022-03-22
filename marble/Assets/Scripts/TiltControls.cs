using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltControls : MonoBehaviour
{
    Rigidbody rb; //The rigidbody attached to the object

    [SerializeField]
    GameObject map; //The Map GameObject (The stage the player moves on)

    static Vector3 initialAcceleratorRotation; //The initial rotation of the Accelerometer at startup
    Vector3 initialSceneRotation; //The initial rotation of the Map object at Startup

    [SerializeField, Range(0.0f, 40.0f)]
    float angleContraint = 40.0f; //The Maximum angle that the Map can be rotated to (Degrees)
    float smooth = 0.4f;
    float sensitivity = 20.0f;

    Quaternion currentRotation; //The current rotation of the phone
    Vector3 newRotation;

    static bool initRotGathered = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Gets the Rigidbody component attached to the object

        if(map == null)
        {
            map = GameObject.FindWithTag("Map");
        }
    }

    void FixedUpdate()
    {
        if (initRotGathered == false)
        {
            initialAcceleratorRotation = Input.acceleration;

            if (initialAcceleratorRotation.magnitude > 0.2f)
            {
                initialSceneRotation = map.transform.rotation.eulerAngles;
                currentRotation = Quaternion.identity;

                Debug.Log("Initial Accelorometer Rotation: " + initialAcceleratorRotation);
                Debug.Log("Initial Scene Rotation: " + initialSceneRotation);
                initRotGathered = true;
            }
        }
        
        //forward / back tilt of phone is Y
        //twist of phone is X
        Vector3 relRot = Input.acceleration - initialAcceleratorRotation;
        currentRotation = Quaternion.Euler( Mathf.Clamp(relRot.y *  sensitivity, -angleContraint, angleContraint),
                                           0,
                                           Mathf.Clamp(-relRot.x * sensitivity, -angleContraint, angleContraint) );
        map.transform.rotation = Quaternion.Lerp(map.transform.rotation, currentRotation, Time.deltaTime / smooth);

        /*
        currentRotation = Vector3.Lerp(currentRotation, Input.acceleration - initialAcceleratorRotation, Time.deltaTime / smooth);

        newRotation = Vector3.Scale(currentRotation * angleContraint, Input.acceleration);
        */

        /*
        Vector3 currentRotation;

        currentRotation.x = initialSceneRotation.x * rotationalDifference.x;
        currentRotation.y = initialSceneRotation.y * rotationalDifference.y;
        currentRotation.z = initialSceneRotation.z * rotationalDifference.z;
        */

        /*
        Debug.Log("Accelorometer: " + Input.acceleration);
        //Debug.Log("Rotational Difference: " + rotationalDifference);
        Debug.Log("Current Rotation: " + currentRotation);
        Debug.Log("New Rotation: " + newRotation);
        */

        /*
        if ((newRotation.x < angleContraint) && (newRotation.y < angleContraint))
        {
            map.transform.rotation = Quaternion.Euler(-newRotation.y, 0.0f, -newRotation.x);
        }
        */

        //rb.rotation = currentRotation; //Rotates the Rigidbody with interpolation from currentRotation

        //rb.rotation = Quaternion.Euler(currentRotation); //Rotates the Rigidbody with interpolation from currentRotation
    }
}

/*
 * References:
 *      - https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html
*/
