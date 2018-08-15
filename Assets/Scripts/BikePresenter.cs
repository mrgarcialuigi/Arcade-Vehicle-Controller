using UnityEngine;
using Vehicle;

namespace DefaultNamespace
{
    public class BikePresenter : MonoBehaviour
    {
        [SerializeField]
        private VehicleController vehicleController;

        //[SerializeField]
        //private VehicleSuspension vehicleSuspension;

        [SerializeField]
        private GameObject wheelFront;

        [SerializeField]
        private GameObject wheelRear;

        [SerializeField]
        private float wheelsSpinSpeed;

        [SerializeField]
        private float wheelsMaxSteerAngle;

        [SerializeField]
        private Light lightBrake;

        //[SerializeField]
        //private float wheelYWhenSpringMin;
        //
        //[SerializeField]
        //private float wheelYWhenSpringMax;

        //private float springFrontLeftLengthPercent;
        //private float springFrontRightLengthPercent;
        //private float springRearLeftLengthPercent;
        //private float springRearRightLengthPercent;

        private void Update()
        {
            Vector3 rollingRotateAxis = vehicleController.IsMovingForward ? Vector3.right : Vector3.left;
            float rollingRotateAngle = vehicleController.StraightVelocityMagnitude * wheelsSpinSpeed * Time.deltaTime;
            wheelFront.transform.Rotate(rollingRotateAxis, rollingRotateAngle);
            wheelRear.transform.Rotate(rollingRotateAxis, rollingRotateAngle);

            // TODO: Fix conflict of rotations, front wheel not rolling properly
            wheelFront.transform.localRotation =
                Quaternion.AngleAxis(wheelsMaxSteerAngle * vehicleController.SteerInput, Vector3.up);

            // Update light
            bool isBrakeOn = vehicleController.BrakeInput > 0.0f;
            lightBrake.enabled = isBrakeOn;

            // TODO: Add suspension presentation code

            //springFrontLeftLengthPercent = vehicleSuspension.GetSpring(WheelName.FrontLeft).currentLength /
            //                               vehicleSuspension.RestLength;
            //
            //springFrontRightLengthPercent = vehicleSuspension.GetSpring(WheelName.FrontRight).currentLength /
            //                               vehicleSuspension.RestLength;
            //
            //springRearLeftLengthPercent = vehicleSuspension.GetSpring(WheelName.RearLeft).currentLength /
            //                               vehicleSuspension.RestLength;
            //
            //springRearRightLengthPercent = vehicleSuspension.GetSpring(WheelName.RearRight).currentLength /
            //                               vehicleSuspension.RestLength;

            //wheelFrontLeft.transform.localPosition = new Vector3(wheelFrontLeft.transform.localPosition.x,
            //    wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springFrontLeftLengthPercent,
            //    wheelFrontLeft.transform.localPosition.z);
            //
            //wheelFrontRight.transform.localPosition = new Vector3(wheelFrontRight.transform.localPosition.x,
            //    wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springFrontRightLengthPercent,
            //    wheelFrontRight.transform.localPosition.z);
            //
            //wheelRearRight.transform.localPosition = new Vector3(wheelRearRight.transform.localPosition.x,
            //    wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springRearRightLengthPercent,
            //    wheelRearRight.transform.localPosition.z);
            //
            //wheelRearLeft.transform.localPosition = new Vector3(wheelRearLeft.transform.localPosition.x,
            //    wheelYWhenSpringMin + (wheelYWhenSpringMax - wheelYWhenSpringMin) * springRearLeftLengthPercent,
            //    wheelRearLeft.transform.localPosition.z);
        }
    }
}