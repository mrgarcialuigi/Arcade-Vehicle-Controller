using UnityEngine;

namespace ArcadeVehicleController
{
    [CreateAssetMenu]
    public class VehicleSettings : ScriptableObject
    {
        [Header("Shape")]
        [SerializeField] private float m_Width;
        [SerializeField] private float m_Height;
        [SerializeField] private float m_Length;

        [Header("Wheels")]
        [SerializeField][Range(0.0f, 0.5f)] private float m_WheelsPaddingX;
        [SerializeField][Range(0.0f, 0.5f)] private float m_WheelsPaddingZ;

        [Header("Body")]
        [SerializeField] private float m_ChassiMass;
        [SerializeField] private float m_TireMass;

        [Header("Susupension")]
        [SerializeField] private float m_SpringRestLength;
        [SerializeField] private float m_SpringStrength;
        [SerializeField] private float m_SpringDamper;

        [Header("Power")]
        [SerializeField] private float m_AcceleratePower;
        [SerializeField] private float m_BrakesPower;
        [SerializeField] private float m_MaxSpeed;
        [SerializeField] private float m_MaxReverseSpeed;

        [Header("Handling")]
        [SerializeField][Range(0.0f, 60.0f)] private float m_SteerAngle = 45.0f;
        [SerializeField][Range(0.0f, 1.0f)] private float m_FrontWheelsGripFactor = 0.5f;
        [SerializeField][Range(0.0f, 1.0f)] private float m_RearWheelsGripFactor = 0.5f;

        [Header("Other")]
        [SerializeField] private float m_AirResistance;


        public float Width => m_Width;
        public float Height => m_Height;
        public float Length => m_Length;

        public float WheelsPaddingX => m_WheelsPaddingX;
        public float WheelsPaddingZ => m_WheelsPaddingZ;

        public float ChassiMass => m_ChassiMass;
        public float TireMass => m_TireMass;

        public float SpringRestLength => m_SpringRestLength;
        public float SpringStrength => m_SpringStrength;
        public float SpringDamper => m_SpringDamper;

        public float AcceleratePower => m_AcceleratePower;
        public float BrakesPower => m_BrakesPower;
        public float MaxSpeed => m_MaxSpeed;
        public float MaxReverseSpeed => m_MaxReverseSpeed;

        public float SteerAngle => m_SteerAngle;
        public float FrontWheelsGripFactor => m_FrontWheelsGripFactor;
        public float RearWheelsGripFactor => m_RearWheelsGripFactor;
        
        public float AirResistance => m_AirResistance;
    }
}