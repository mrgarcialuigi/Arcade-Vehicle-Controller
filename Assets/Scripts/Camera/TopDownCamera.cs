using UnityEngine;

namespace Camera
{
    public class TopDownCamera : MonoBehaviour
    {
        [SerializeField]
        private int distance;

        [SerializeField]
        [Range(0, 90)]
        private int angle;

        [SerializeField]
        private float lerpSmooth;

        [SerializeField]
        private Transform target;

        private void LateUpdate()
        {
            if (target != null)
            {
                Vector3 directionFromTarget = Quaternion.AngleAxis(angle, Vector3.right) * Vector3.back;
                Vector3 newPosition = target.position + directionFromTarget * distance;
                transform.position = Vector3.Lerp(transform.position, newPosition, lerpSmooth * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-directionFromTarget), lerpSmooth * Time.deltaTime);
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}