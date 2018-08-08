using System;
using UnityEngine;

namespace Vehicle
{
    [Serializable]
    public struct Spring
    {
        public readonly Vector3 localPosition;

        public Vector3 position;

        public float currentLength;

        // Velocity is the derivative of localPosition, which formula is: deltaPosition/deltaTime
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