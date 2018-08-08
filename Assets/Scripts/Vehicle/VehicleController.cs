using UnityEngine;

namespace Vehicle
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody myRigidbody;

        [SerializeField]
        private BoxCollider boxCollider;

        [SerializeField]
        private VehicleSuspension suspension;

        [SerializeField]
        private Transform centerOfMassTransform;

        [SerializeField]
        private Transform enginePowerTransform;

        [SerializeField]
        private Transform steeringTransform;

        [SerializeField]
        private float maximumVelocity;

        [SerializeField]
        private float accelerationFactor;

        [SerializeField]
        private float brakeFactor;

        [SerializeField]
        private float steerFactor;

        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float sidewaysTraction;

        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float angularTraction;

        /// <summary>
        /// Range between 0.0f and 1.0f
        /// </summary>
        private float gasInput;

        /// <summary>
        /// Range between 0.0f and 1.0f
        /// </summary>
        private float brakeInput;

        /// <summary>
        /// Range between -1.0f and 1.0f
        /// </summary>
        private float steerInput;
        private bool isMovingForward;
        private Vector3 straightVelocity;
        private float straightVelocityMagnitude;

        public float SteerInput { get { return steerInput; } }
        public bool IsMovingForward { get { return isMovingForward; } }
        public float StraightVelocityMagnitude { get { return straightVelocityMagnitude; } }

        private void Awake()
        {
            Debug.Assert(myRigidbody != null, "Member \"myRigidbody\" is required.", this);
            Debug.Assert(boxCollider != null, "Member \"boxCollider\" is required.", this);
            Debug.Assert(suspension != null, "Member \"suspension\" is required.", this);
            Debug.Assert(centerOfMassTransform != null, "Member \"centerOfMassTransform\" is required.", this);
            Debug.Assert(enginePowerTransform != null, "Member \"enginePowerTransform\" is required.", this);
            Debug.Assert(steeringTransform != null, "Member \"steeringTransform\" is required.", this);

            myRigidbody.centerOfMass = centerOfMassTransform.localPosition;
        }

        private void FixedUpdate()
        {
            if (suspension.AreAllSpringsGrounded())
            {
                Vector3 projectedForward = suspension.GetSuspensionProjectedForwardDirection();
                Vector3 projectedRight = Vector3.Cross(Vector3.up, projectedForward);
                straightVelocity = projectedForward * Vector3.Dot(myRigidbody.velocity, projectedForward);
                straightVelocityMagnitude = straightVelocity.magnitude;
                isMovingForward = Vector3.Dot(projectedForward, straightVelocity.normalized) > 0.0f;

                if (gasInput > 0.0f)
                {
                    // Positive Gas Input
                    // Top forward speed if
                    if (!isMovingForward || straightVelocityMagnitude < maximumVelocity)
                    {
                        myRigidbody.AddForceAtPosition(
                            projectedForward * accelerationFactor * gasInput,
                            enginePowerTransform.position,
                            ForceMode.Acceleration);
                    }
                }
                else if (gasInput < 0.0f)
                {
                    // Negative Gas Input
                    // Top backward speed if
                    if (isMovingForward || straightVelocityMagnitude < maximumVelocity * 0.4f)
                    {
                        myRigidbody.AddForceAtPosition(
                            projectedForward * accelerationFactor * 0.7f * gasInput,
                            enginePowerTransform.position,
                            ForceMode.Acceleration);
                    }
                }

                // Steering
                if (!Mathf.Approximately(0.0f, steerInput) && straightVelocityMagnitude > 0.0f)
                {
                    if (isMovingForward)
                    {
                        myRigidbody.AddForceAtPosition(steerInput * projectedRight * steerFactor, steeringTransform.position, ForceMode.Acceleration);
                    }
                    else
                    {
                        // Moving backwards (reverse the steer) To provide a better feeling, we do an
                        // extra test for steering when the Vehicle is moving back, so that if the
                        // vehicle is still moving backwards because of physics, but the player is
                        // already doing input for moving forward(gas positive), then we don't turn
                        // at all.
                        if (gasInput < 0.0f)
                        {
                            // Case is moving backwards and player still giving input for keep moving backwards, that is when we rear (reverse) steer
                            myRigidbody.AddForceAtPosition(steerInput * -projectedRight * steerFactor, steeringTransform.position, ForceMode.Acceleration);
                        }
                    }
                }

                if (brakeInput > 0.0f)
                {
                    if (straightVelocityMagnitude > 0.0f && isMovingForward)
                    {
                        myRigidbody.AddForceAtPosition(
                            -projectedForward * brakeInput * brakeFactor,
                            enginePowerTransform.position,
                            ForceMode.Acceleration);
                    }
                }

                // Angular traction
                myRigidbody.AddTorque(-new Vector3(0.0f, myRigidbody.angularVelocity.y, 0.0f) * angularTraction, ForceMode.Acceleration);

                // Sideways traction
                Vector3 contrarySidewaysVelocity = -Vector3.Project(myRigidbody.velocity, transform.right);
                if (contrarySidewaysVelocity.sqrMagnitude > 0.0f)
                {
                    myRigidbody.AddForce(contrarySidewaysVelocity * sidewaysTraction, ForceMode.Acceleration);
                }

                // Wheels Rolling Resistance, turn front and back traction (so car doesnt tend to move front/back forever)
                const float WHEELS_ROLLING_RESISTANCE_COEFFICIENT = 0.2f;
                Vector3 wheelsRollingResistanceForce = -(straightVelocity * WHEELS_ROLLING_RESISTANCE_COEFFICIENT);
                myRigidbody.AddForce(wheelsRollingResistanceForce, ForceMode.Acceleration);
            }
            else
            {
                isMovingForward = false;
            }

            // Air Resistance / Aerodynamic drag
            const float AERODYNAMIC_DRAG_MODIFIER = 0.005f;
            myRigidbody.AddForce(
                -myRigidbody.velocity.normalized * boxCollider.size.magnitude * AERODYNAMIC_DRAG_MODIFIER * myRigidbody.velocity.sqrMagnitude,
                ForceMode.Acceleration);
        }

        public void Gas(float gasInput)
        {
            this.gasInput = Mathf.Clamp(gasInput, -1.0f, 1.0f);
        }

        public void Brake(float brakeInput)
        {
            this.brakeInput = Mathf.Clamp01(brakeInput);
        }

        public void Steer(float steerInput)
        {
            this.steerInput = Mathf.Clamp(steerInput, -1.0f, 1.0f);
        }
    }
}