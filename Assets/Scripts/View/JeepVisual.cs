using UnityEngine;
using System.Collections.Generic;

namespace ArcadeVehicleController
{
    public class JeepVisual : MonoBehaviour
    {
        [SerializeField] private Transform m_WheelFrontLeft;
        [SerializeField] private Transform m_WheelFrontRight;
        [SerializeField] private Transform m_WheelBackLeft;
        [SerializeField] private Transform m_WheelBackRight;
        [SerializeField] private float m_WheelsSpinSpeed;
        [SerializeField] private float m_WheelYWhenSpringMin;
        [SerializeField] private float m_WheelYWhenSpringMax;

        private Quaternion m_WheelFrontLeftRoll;
        private Quaternion m_WheelFrontRightRoll;

        public bool IsMovingForward { get; set; }

        public float ForwardSpeed { get; set; }

        public float SteerInput { get; set; }

        public float SteerAngle { get; set; }

        public float SpringsRestLength { get; set; }

        public Dictionary<Wheel, float> SpringsCurrentLength { get; set; } = new()
        {
            { Wheel.FrontLeft, 0.0f },
            { Wheel.FrontRight, 0.0f },
            { Wheel.BackLeft, 0.0f },
            { Wheel.BackRight, 0.0f }
        };

        private void Start()
        {
            m_WheelFrontLeftRoll = m_WheelFrontLeft.localRotation;
            m_WheelFrontRightRoll = m_WheelFrontRight.localRotation;
        }

        private void Update()
        {
            if (SpringsCurrentLength[Wheel.FrontLeft] < SpringsRestLength)
            {
                m_WheelFrontLeftRoll *= Quaternion.AngleAxis(ForwardSpeed * m_WheelsSpinSpeed * Time.deltaTime, Vector3.right);
            }

            if (SpringsCurrentLength[Wheel.FrontRight] < SpringsRestLength)
            {
                m_WheelFrontRightRoll *= Quaternion.AngleAxis(ForwardSpeed * m_WheelsSpinSpeed * Time.deltaTime, Vector3.right);
            }

            if (SpringsCurrentLength[Wheel.BackLeft] < SpringsRestLength)
            {
                m_WheelBackLeft.localRotation *= Quaternion.AngleAxis(ForwardSpeed * m_WheelsSpinSpeed * Time.deltaTime, Vector3.right);
            }

            if (SpringsCurrentLength[Wheel.BackRight] < SpringsRestLength)
            {
                m_WheelBackRight.localRotation *= Quaternion.AngleAxis(ForwardSpeed * m_WheelsSpinSpeed * Time.deltaTime, Vector3.right);
            }

            m_WheelFrontLeft.localRotation = Quaternion.AngleAxis(SteerInput * SteerAngle, Vector3.up) * m_WheelFrontLeftRoll;
            m_WheelFrontRight.localRotation = Quaternion.AngleAxis(SteerInput * SteerAngle, Vector3.up) * m_WheelFrontRightRoll;

            float springFrontLeftRatio = SpringsCurrentLength[Wheel.FrontLeft] / SpringsRestLength;
            float springFrontRightRatio = SpringsCurrentLength[Wheel.FrontRight] / SpringsRestLength;
            float springBackLeftRatio = SpringsCurrentLength[Wheel.BackLeft] / SpringsRestLength;
            float springBackRightRatio = SpringsCurrentLength[Wheel.BackRight] / SpringsRestLength;

            m_WheelFrontLeft.localPosition = new Vector3(m_WheelFrontLeft.localPosition.x,
                m_WheelYWhenSpringMin + (m_WheelYWhenSpringMax - m_WheelYWhenSpringMin) * springFrontLeftRatio,
                m_WheelFrontLeft.localPosition.z);

            m_WheelFrontRight.localPosition = new Vector3(m_WheelFrontRight.localPosition.x,
                m_WheelYWhenSpringMin + (m_WheelYWhenSpringMax - m_WheelYWhenSpringMin) * springFrontRightRatio,
                m_WheelFrontRight.localPosition.z);

            m_WheelBackRight.localPosition = new Vector3(m_WheelBackRight.localPosition.x,
                m_WheelYWhenSpringMin + (m_WheelYWhenSpringMax - m_WheelYWhenSpringMin) * springBackRightRatio,
                m_WheelBackRight.localPosition.z);

            m_WheelBackLeft.localPosition = new Vector3(m_WheelBackLeft.localPosition.x,
                m_WheelYWhenSpringMin + (m_WheelYWhenSpringMax - m_WheelYWhenSpringMin) * springBackLeftRatio,
                m_WheelBackLeft.localPosition.z);
        }
    }
}