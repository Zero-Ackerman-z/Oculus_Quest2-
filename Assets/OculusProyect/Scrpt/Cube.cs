using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Cube : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private InputDevice leftController;
    private InputDevice rightController;
    private bool pressYRaw;


    void Update()
    {
        Vector2 leftThumbstick = Vector2.zero;
        Vector2 rightThumbstick = Vector2.zero;

        if (leftController.isValid)
            leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftThumbstick);

        transform.Translate(new Vector3(leftThumbstick.x, 0, leftThumbstick.y) * speed * Time.deltaTime);

        if (rightController.isValid)
            rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightThumbstick);

        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressA) && pressA)
            transform.Translate(Vector3.up * speed * Time.deltaTime); 

        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressB) && pressB)
            transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (leftController.isValid)
            leftController.TryGetFeatureValue(CommonUsages.primaryButton, out pressYRaw);

    }
}
