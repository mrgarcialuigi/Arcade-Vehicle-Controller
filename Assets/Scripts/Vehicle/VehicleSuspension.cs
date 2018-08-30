using System;
using UnityEngine;

namespace Vehicle
{
    /// <summary>
    /// Logic for the vehicle suspension, spring damping, using 4 springs:
    /// FrontLeft, FrontRight, RearLeft, RearRight
    /// </summary>
    public class VehicleSuspension : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody myRigidbody;

        /// <summary>
        /// Positive value in metters representing the length of the springs in this suspension.
        /// </summary>
        [SerializeField]
        private float restLength;

        /// <summary>
        /// Is the constant force which the spring push or pulls towards its rest length. Can be seen
        /// basically as the force/power of the spring.
        /// An optimal setup value is usually 20 times the mass of the vehicle.
        /// </summary>
        [SerializeField]
        private float springConstant;

        /// <summary>
        /// Damping coefficient affects a force that acts according and countering the spring
        /// velocity, in order to avoid that the springs go up and down, but never sit the the
        /// equilibrium point(rest length). The higher the damping coefficient, the faster the spring
        /// is gonna achieve the rest length and sit at it.
        /// An optimal setup value is usually 10 times smaller than the spring constant.
        /// </summary>
        [Tooltip("Damping coefficient affects a force that acts according and countering the spring " +
                 "velocity, in order to avoid that the springs go up and down, but never sit the the " +
                 "equilibrium point(rest length). The higher the damping coefficient, the faster the spring " +
                 "is gonna achieve the rest length and sit at it. " +
                 "An optimal setup value is usually 10 times smaller than the spring constant.")]
        [SerializeField]
        private float dampingCoefficient;

        private bool springsInitialized;
        private Spring springFrontLeft;
        private Spring springFrontRight;
        private Spring springRearLeft;
        private Spring springRearRight;

        public float RestLength { get { return restLength; } }

        private void Awake()
        {
            Debug.Assert(myRigidbody != null, "Member \"myRigidbody\" is required.", this);
            Debug.Assert(restLength > 0.0f, "\"restLength\" has to be bigger than 0.0.", this);
            Debug.Assert(springConstant > 0.0f, "\"springConstant\" has to be bigger than 0.0.", this);
            Debug.Assert(dampingCoefficient > 0.0f, "\"dampingCoefficient\" has to be bigger than 0.0.", this);
        }

        private void FixedUpdate()
        {
            if (springsInitialized)
            {
                // Update first the spring positions based on the chassi and the spring localPosition setup
                UpdateSpringsPosition();

                // Update springs current length and velocity
                UpdateSpringsLengthAndVelocity();

                ////////////////////////////////////
                // Apply the spring DAMPED forces //
                ////////////////////////////////////
                myRigidbody.AddForceAtPosition(
                    CalculateDampedSpringForce(transform.up, springFrontLeft.currentLength, restLength, springConstant,
                        springFrontLeft.velocity, dampingCoefficient), springFrontLeft.position);
                myRigidbody.AddForceAtPosition(
                    CalculateDampedSpringForce(transform.up, springFrontRight.currentLength, restLength, springConstant,
                        springFrontRight.velocity, dampingCoefficient), springFrontRight.position);
                myRigidbody.AddForceAtPosition(
                    CalculateDampedSpringForce(transform.up, springRearLeft.currentLength, restLength, springConstant,
                        springRearLeft.velocity, dampingCoefficient), springRearLeft.position);
                myRigidbody.AddForceAtPosition(
                    CalculateDampedSpringForce(transform.up, springRearRight.currentLength, restLength, springConstant,
                        springRearRight.velocity, dampingCoefficient), springRearRight.position);
            }
        }

        /// <summary>
        /// Creates and intializes the springs based on the given positions.
        /// The suspension logic is unfuncional until this method is called.
        /// Note: can be called only once.
        /// </summary>
        public void InitializeSprings(Vector3 frontLeftPosition, Vector3 frontRightPosition, Vector3 rearLeftPosition, Vector3 rearRightPosition)
        {
            if (springsInitialized)
            {
                throw new InvalidOperationException("Springs already initialized");
            }

            springFrontLeft = new Spring(frontLeftPosition);
            springFrontRight = new Spring(frontRightPosition);
            springRearLeft = new Spring(rearLeftPosition);
            springRearRight = new Spring(rearRightPosition);
            springsInitialized = true;
        }

        private void UpdateSpringsPosition()
        {
            springFrontLeft.position = CalculateSpringPosition(transform.up, transform.right, transform.forward, transform.position,
                springFrontLeft.localPosition);
            springFrontRight.position = CalculateSpringPosition(transform.up, transform.right, transform.forward, transform.position,
                springFrontRight.localPosition);
            springRearLeft.position = CalculateSpringPosition(transform.up, transform.right, transform.forward, transform.position,
                springRearLeft.localPosition);
            springRearRight.position = CalculateSpringPosition(transform.up, transform.right, transform.forward, transform.position,
                springRearRight.localPosition);
        }

        private void UpdateSpringsLengthAndVelocity()
        {
            // Velocity is the derivative of springLength, which formula is: springLength/deltaTime
            // This will give you the delta value according to time, second basis
            // If the frame took exactly 1second, then the division wont do a thing

            // RL
            float previousSpringLength = springRearLeft.currentLength;
            springRearLeft.currentLength =
                RaycastGetCurrentLengthOfSpring(springRearLeft.position, transform.up, restLength);
            springRearLeft.velocity = (springRearLeft.currentLength - previousSpringLength) / Time.fixedDeltaTime;

            // RR
            previousSpringLength = springRearRight.currentLength;
            springRearRight.currentLength =
                RaycastGetCurrentLengthOfSpring(springRearRight.position, transform.up, restLength);
            springRearRight.velocity = (springRearRight.currentLength - previousSpringLength) / Time.fixedDeltaTime;

            // FL
            previousSpringLength = springFrontLeft.currentLength;
            springFrontLeft.currentLength =
                RaycastGetCurrentLengthOfSpring(springFrontLeft.position, transform.up, restLength);
            springFrontLeft.velocity = (springFrontLeft.currentLength - previousSpringLength) / Time.fixedDeltaTime;

            // FR
            previousSpringLength = springFrontRight.currentLength;
            springFrontRight.currentLength =
                RaycastGetCurrentLengthOfSpring(springFrontRight.position, transform.up, restLength);
            springFrontRight.velocity = (springFrontRight.currentLength - previousSpringLength) / Time.fixedDeltaTime;
        }


        public Spring GetSpring(WheelName wheelName)
        {
            if (wheelName == WheelName.None)
            {
                throw new ArgumentException("wheelName");
            }

            if (!springsInitialized)
            {
                throw new InvalidOperationException("Springs not initialized");
            }

            Spring spring = default(Spring);
            switch (wheelName)
            {
                case WheelName.FrontLeft:
                    spring = springFrontLeft;
                    break;
                case WheelName.FrontRight:
                    spring = springFrontRight;
                    break;
                case WheelName.RearLeft:
                    spring = springRearLeft;
                    break;
                case WheelName.RearRight:
                    spring = springRearRight;
                    break;
            }

            return spring;
        }

        /// <summary>
        /// Returns true when all the springs are "compressed",
        /// which means all the springs are touching the ground.
        /// </summary>
        /// <returns></returns>
        public bool AreAllSpringsGrounded()
        {
            return springFrontLeft.currentLength < restLength &&
                   springFrontRight.currentLength < restLength &&
                   springRearLeft.currentLength < restLength &&
                   springRearRight.currentLength < restLength;
        }

        public Vector3 GetSuspensionProjectedForwardDirection()
        {
            Vector3 springFrontLeftEndPosition = springFrontLeft.position - transform.up * springFrontLeft.currentLength;
            Vector3 springFrontRightEndPosition = springFrontRight.position - transform.up * springFrontRight.currentLength;

            Vector3 springBackLeftEndPosition = springRearLeft.position - transform.up * springRearLeft.currentLength;
            Vector3 springBackRightEndPosition = springRearRight.position - transform.up * springRearRight.currentLength;

            Vector3 backEndCenter = springBackLeftEndPosition + (springBackRightEndPosition - springBackLeftEndPosition) * 0.5f;
            Vector3 frontEndCenter = springFrontLeftEndPosition + (springFrontRightEndPosition - springFrontLeftEndPosition) * 0.5f;

            return (frontEndCenter - backEndCenter).normalized;
        }

        public static Vector3 CalculateSpringPosition(Vector3 chassiUpVector, Vector3 chassiRightVector,
            Vector3 chassiForwardVector, Vector3 chassiPosition, Vector3 springLocalPosition)
        {
            return chassiPosition + (chassiRightVector * springLocalPosition.x) +
                   (chassiUpVector * springLocalPosition.y) + (chassiForwardVector * springLocalPosition.z);
        }

        private static float RaycastGetCurrentLengthOfSpring(Vector3 springPosition, Vector3 chassiUpVector, float restLength)
        {
            RaycastHit raycastHit;
            return Physics.Raycast(springPosition, -chassiUpVector, out raycastHit, restLength) ? raycastHit.distance : restLength;
        }

        /// <summary>
        /// Calculates the spring force for the case. Not damped, that means it will tend to bounce forever.
        /// The spring applies contrary force relative(in direction to) its rest length
        /// The formula would be -constant * (currentLength - restLength)
        /// This force its the behavior of the spring to fight compression or extension, and try to stay in its rest length
        /// </summary>
        public static Vector3 CalculateSpringForce(Vector3 chassiUpVector, float currentLength, float restLength, float springConstant)
        {
            return chassiUpVector * -springConstant * (currentLength - restLength);
        }

        /// <summary>
        /// A damped spring is just like our simple spring with an additional force. In the simple spring case, 
        /// we had a force proportional to our distance from the equilibrium localPosition. 
        /// This caused us to always accelerate towards equilibrium, but never come to rest at it 
        /// (except for the trivial case where we started there with no motion). In order to settle at equilibrium, 
        /// we add a force proportional to our velocity in the case of damped springs. 
        /// F =−βv−kx
        /// https://gafferongames.com/post/spring_physics/
        /// </summary>
        public static Vector3 CalculateDampedSpringForce(Vector3 chassiUpVector,
            float currentLength,
            float restLength,
            float springConstant,
            float springVelocity,
            float dampingCoefficient)
        {
            return chassiUpVector * (-dampingCoefficient * springVelocity - springConstant * (currentLength - restLength));
        }

        #region Editor

#if UNITY_EDITOR
        [Header("UNITY_EDITOR")]
        [SerializeField]
        private bool drawGizmos;

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(springFrontRight.position, springFrontRight.position - transform.up * restLength);
                Gizmos.DrawLine(springFrontLeft.position, springFrontLeft.position - transform.up * restLength);
                Gizmos.DrawLine(springRearRight.position, springRearRight.position - transform.up * restLength);
                Gizmos.DrawLine(springRearLeft.position, springRearLeft.position - transform.up * restLength);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(springFrontRight.position - transform.up * springFrontRight.currentLength, Vector3.one * 0.08f);
                Gizmos.DrawCube(springFrontLeft.position - transform.up * springFrontLeft.currentLength, Vector3.one * 0.08f);
                Gizmos.DrawCube(springRearRight.position - transform.up * springRearRight.currentLength, Vector3.one * 0.08f);
                Gizmos.DrawCube(springRearLeft.position - transform.up * springRearLeft.currentLength, Vector3.one * 0.08f);
            }
        }

#endif

        #endregion
    }
}