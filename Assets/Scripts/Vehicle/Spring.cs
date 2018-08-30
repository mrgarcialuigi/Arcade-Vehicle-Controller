using System;
using UnityEngine;

namespace Vehicle
{
    /// <summary>
    /// Holds data of a spring of the vehicle.
    /// </summary>
    [Serializable]
    public struct Spring
    {
        /// <summary>
        /// The position of the screen related to the vehicle's chassi.
        /// Not mutable, never changes.
        /// </summary>
        public readonly Vector3 localPosition;

        /// <summary>
        /// Keeps the position of the spring related to the world.
        /// Note: same world coordinates of localPosition, which never changes.
        /// The spring never changes its position related to the chassi.
        /// </summary>
        public Vector3 position;

        /// <summary>
        /// Current length of the spring, between 0.0 and the restLength(max spring length) of the vehicle.
        /// </summary>
        public float currentLength;

        /// <summary>
        /// Derivative of the spring length, which formula is: deltaLength/deltaTime.
        /// </summary>
        public float velocity;

        public Spring(Vector3 localPosition)
        {
            this.localPosition = localPosition;
            currentLength = 0.0f;
            velocity = 0.0f;
            position = Vector3.zero;
        }
    }
}