using UnityEngine;

namespace ArcadeVehicleController
{
    public class VisualsStateUpdater : MonoBehaviour
    {
        [SerializeField] private Vehicle m_Vehicle;
        [SerializeField] private JeepVisual m_JeepVisual;
        [SerializeField] private ThirdPersonCameraController m_CameraController;

        private void Start()
        {
            m_JeepVisual.SpringsRestLength = m_Vehicle.Settings.SpringRestLength;
            m_JeepVisual.SteerAngle = m_Vehicle.Settings.SteerAngle;
            m_CameraController.FollowTarget = m_Vehicle.transform;
        }

        private void Update()
        {
            m_JeepVisual.SteerInput = Input.GetAxis("Horizontal");

            float forwardSpeed = Vector3.Dot(m_Vehicle.Forward, m_Vehicle.Velocity);
            m_JeepVisual.ForwardSpeed = forwardSpeed;
            m_JeepVisual.IsMovingForward = forwardSpeed > 0.0f;

            m_JeepVisual.SpringsCurrentLength[Wheel.FrontLeft] = m_Vehicle.GetSpringCurrentLength(Wheel.FrontLeft);
            m_JeepVisual.SpringsCurrentLength[Wheel.FrontRight] = m_Vehicle.GetSpringCurrentLength(Wheel.FrontRight);
            m_JeepVisual.SpringsCurrentLength[Wheel.BackLeft] = m_Vehicle.GetSpringCurrentLength(Wheel.BackLeft);
            m_JeepVisual.SpringsCurrentLength[Wheel.BackRight] = m_Vehicle.GetSpringCurrentLength(Wheel.BackRight);

            m_CameraController.SpeedRatio = m_Vehicle.Velocity.magnitude / m_Vehicle.Settings.MaxSpeed;
        }
    }
}