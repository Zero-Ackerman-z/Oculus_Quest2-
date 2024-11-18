using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Proyect : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float punchThreshold = 1.5f;
    [SerializeField] private float forceMultiplier = 2f;
    [SerializeField] private float cooldown = 0.5f;
    private float lastPunchTime;
    private InputDevice rightController;
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    void Update()
    {
        if (Time.time < lastPunchTime + cooldown) return;
        Vector3 controllerVelocity = Vector3.zero;
        if (rightController.isValid)
        {
            rightController.TryGetFeatureValue(CommonUsages.rightEyeVelocity, out controllerVelocity);
        }
        Quaternion controllerRotation = Quaternion.identity;
        if (rightController.isValid)
        {
            rightController.TryGetFeatureValue(CommonUsages.rightEyeRotation, out controllerRotation);
        }
        float speed = controllerVelocity.magnitude;
        bool isPunchingForward = false;
        Vector3 controllerDirection = controllerRotation * Vector3.forward; 
        if (rightController.isValid)
        {
            rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition);
            isPunchingForward = Vector3.Dot(controllerVelocity.normalized, controllerDirection) > 0;
        }
        if (speed > punchThreshold && isPunchingForward)
        {
            Vector3 controllerPosition = Vector3.zero;
            if (rightController.isValid)
            {
                rightController.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition);
            }

            FireProjectile(controllerPosition, controllerRotation, controllerVelocity);
            lastPunchTime = Time.time;
        }
    }
    void FireProjectile(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(rotation * Vector3.forward * velocity.magnitude * forceMultiplier, ForceMode.VelocityChange);
        Destroy(projectile, 3f);
    }


}
