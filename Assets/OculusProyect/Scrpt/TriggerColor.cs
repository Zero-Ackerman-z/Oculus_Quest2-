using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class TriggerColor : MonoBehaviour
{
    [SerializeField] private Renderer sphereRenderer;
    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftTriggerPressure = 0f;
        if (leftController.isValid)
        {
            leftController.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerPressure);
        }

        float rightTriggerPressure = 0f;
        if (rightController.isValid)
        {
            rightController.TryGetFeatureValue(CommonUsages.trigger, out rightTriggerPressure);
        }

        float triggerPressure = Mathf.Max(leftTriggerPressure, rightTriggerPressure); 
        sphereRenderer.material.color = Color.Lerp(Color.white, Color.red, triggerPressure);
    }
}

 
