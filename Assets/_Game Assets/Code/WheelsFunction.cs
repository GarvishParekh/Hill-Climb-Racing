using UnityEngine;
using System.Collections.Generic;

public class WheelsFunction : MonoBehaviour
{
    [SerializeField] public InputManager inputManager;
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public Transform follower;
    Vector3 rotationVector;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void FixedUpdate()
    {
        CarFollower();

        float motor = maxMotorTorque * inputManager.GetGasDirection();
        float steering = maxSteeringAngle * inputManager.LerpedSterring();

        //float steering = maxSteeringAngle * inputManager.GetGasDirection();

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }

        ApplyLocalPositionToVisuals(axleInfos[0].leftWheel, axleInfos[0].leftTransform);
        ApplyLocalPositionToVisuals(axleInfos[0].rightWheel, axleInfos[0].rightTransform);

        ApplyLocalPositionToVisuals(axleInfos[1].leftWheel, axleInfos[1].leftTransform);
        ApplyLocalPositionToVisuals(axleInfos[1].rightWheel, axleInfos[1].rightTransform);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform wheelTranform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheelTranform.transform.position = position;
        wheelTranform.transform.rotation = rotation;
    }

    private void CarFollower()
    {
        follower.position = transform.position;
        rotationVector.x = follower.rotation.eulerAngles.x;
        rotationVector.z = follower.rotation.eulerAngles.z;
        rotationVector.y = transform.rotation.eulerAngles.y;
        follower.rotation = Quaternion.Euler(rotationVector);
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    [Space]
    public Transform leftTransform;
    public Transform rightTransform;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
