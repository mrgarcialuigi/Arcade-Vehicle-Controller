using UnityEngine;

namespace Camera
{
    /// <summary>
    /// Simple third person camera behaviour, acts upon performing transformation
    /// on the attached gameObject, to follow a given target transform.
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        /// <summary>
        /// The target camera is following.
        /// </summary>
        [SerializeField]
        private Transform target;

        [SerializeField]
        private float distance = 10.0f;

        [SerializeField]
        private float height = 5.0f;

        [SerializeField]
        private float heightDamping = 2.0f;

        [SerializeField]
        private float rotationDamping = 3.0f;

        public void LateUpdate()
        {
            if (target != null)
            {
                // Calculate the current rotation angles
                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + height;
                float currentRotationAngle = transform.eulerAngles.y;
                float currentHeight = transform.position.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

                // Damp the height
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                // Convert the angle into a rotation
                Quaternion currentRotation = Quaternion.Euler(0.0f, currentRotationAngle, 0.0f);

                // Set the localPosition of the camera on the x-z plane to:
                // distance meters behind the target
                transform.position = target.position;
                transform.position -= currentRotation * Vector3.forward * distance;

                // Set the height of the camera
                transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

                // Always look at the target
                transform.LookAt(target);
            }
        }

        /// <summary>
        /// Sets the target transform the camera is following
        /// If passed as null, camera will stop following
        /// </summary>
        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}