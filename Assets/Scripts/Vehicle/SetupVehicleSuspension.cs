using UnityEngine;

namespace Vehicle
{
    public class SetupVehicleSuspension : MonoBehaviour
    {
        [SerializeField]
        private Vector3 springFrontLeftPosition;

        [SerializeField]
        private Vector3 springFrontRightPosition;

        [SerializeField]
        private Vector3 springRearLeftPosition;

        [SerializeField]
        private Vector3 springRearRightPosition;

        [SerializeField]
        private VehicleSuspension suspension;

        private void Awake()
        {
            Debug.Assert(suspension != null, "Member \"suspension\" is required.", this);
        }

        private void Start()
        {
            suspension.InitializeSprings(springFrontLeftPosition, springFrontRightPosition, springRearLeftPosition, springRearRightPosition);
        }
    }
}