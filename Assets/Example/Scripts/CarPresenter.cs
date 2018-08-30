using UnityEngine;
using Vehicle;

namespace Example
{
    public class CarPresenter : MonoBehaviour
    {
        [SerializeField]
        private VehicleController vehicleController;

        [SerializeField]
        private VehicleSuspension vehicleSuspension;

        [SerializeField]
        private GameObject wheelFrontLeft;

        [SerializeField]
        private GameObject wheelFrontRight;

        [SerializeField]
        private GameObject wheelRearLeft;

        [SerializeField]
        private GameObject wheelRearRight;

        [SerializeField]
        private float wheelsSpinSpeed;

        [SerializeField]
        private float wheelsMaxSteer;

        [SerializeField]
        private float wheelYWhenSpringMin;

        [SerializeField]
        private float wheelYWhenSpringMax;

        [SerializeField]
        private Light lightBrakeLeft;

        [SerializeField]
        private Light lightBrakeRight;

        private float springFrontLeftLengthPercent;
        private float springFrontRightLengthPercent;
        private float springRearLeftLengthPercent;
        private float springRearRightLengthPercent;

        private void Update()
        {
            Vector3 rollingRotateAxis = vehicleController.IsMovingForward ? Vector3.right : Vector3.left;
            float rollingRotateAngle = vehicleController.StraightVelocityMagnitude * wheelsSpinSpeed * Time.deltaTime;
            wheelFrontLeft.transform.Rotate(rollingRotateAxis, rollingRotateAngle);
            wheelFrontRight.transform.Rotate(rollingRotateAxis, rollingRotateAngle);
            wheelRearLeft.transform.Rotate(rollingRotateAxis, rollingRotateAngle);
            wheelRearLeft.transform.Rotate(rollingRotateAxis, rollingRotateAngle);

            // TODO: Fix conflict of rotations, front wheels not rolling properly
            wheelFrontLeft.transform.localEulerAngles = new Vector3(wheelFrontLeft.transform.localEulerAngles.x,
                wheelsMaxSteer * vehicleController.SteerInput);
            wheelFrontRight.transform.localEulerAngles = new Vector3(wheelFrontLeft.transform.localEulerAngles.x,
                wheelsMaxSteer * vehicleController.SteerInput);

            springFrontLeftLengthPercent = vehicleSuspension.GetSpring(WheelName.FrontLeft).currentLength /
                                           vehicleSuspension.RestLength;

            springFrontRightLengthPercent = vehicleSuspension.GetSpring(WheelName.FrontRight).currentLength /
                                           vehicleSuspension.RestLength;

            springRearLeftLengthPercent = vehicleSuspension.GetSpring(WheelName.RearLeft).currentLength /
                                           vehicleSuspension.RestLength;

            springRearRightLengthPercent = vehicleSuspension.GetSpring(WheelName.RearRight).currentLength /
                                           vehicleSuspension.RestLength;

            wheelFrontLeft.transform.localPosition = new Vector3(wheelFrontLeft.transform.localPosition.x,
                wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springFrontLeftLengthPercent,
                wheelFrontLeft.transform.localPosition.z);

            wheelFrontRight.transform.localPosition = new Vector3(wheelFrontRight.transform.localPosition.x,
                wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springFrontRightLengthPercent,
                wheelFrontRight.transform.localPosition.z);

            wheelRearRight.transform.localPosition = new Vector3(wheelRearRight.transform.localPosition.x,
                wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springRearRightLengthPercent,
                wheelRearRight.transform.localPosition.z);

            wheelRearLeft.transform.localPosition = new Vector3(wheelRearLeft.transform.localPosition.x,
                wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springRearLeftLengthPercent,
                wheelRearLeft.transform.localPosition.z);

            // Update lights
            bool isBrakeOn = vehicleController.BrakeInput > 0.0f;
            lightBrakeLeft.enabled = isBrakeOn;
            lightBrakeRight.enabled = isBrakeOn;
        }
    }
}