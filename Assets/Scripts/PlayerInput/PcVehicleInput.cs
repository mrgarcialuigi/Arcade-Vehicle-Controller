using UnityEngine;

namespace PlayerInput
{
    /// <summary>
    /// Encapsulates player input logic to control the vehicles,
    /// exposes readonly flags informing the current input state.
    /// Note: Right and Left are never true at the same time,
    /// same for Break and Gas.
    /// </summary>
    public class PcVehicleInput : MonoBehaviour
    {
        [SerializeField]
        private KeyCode leftKeyCode;

        [SerializeField]
        private KeyCode rightKeyCode;

        [SerializeField]
        private KeyCode gasKeyCode;

        [SerializeField]
        private KeyCode breakKeyCode;

        private bool isLeftOn;
        private bool isRightOn;
        private bool isGasOn;
        private bool isBreakOn;

        public bool IsLeftOn { get { return isLeftOn; } }
        public bool IsRightOn { get { return isRightOn; } }
        public bool IsGasOn { get { return isGasOn; } }
        public bool IsBreakOn { get { return isBreakOn; } }
        
        private void Update()
        {
            if (!(Input.GetKey(leftKeyCode) && Input.GetKey(rightKeyCode)))
            {
                if (Input.GetKey(leftKeyCode))
                {
                    isLeftOn = true;
                    isRightOn = false;
                }
                else if (Input.GetKey(rightKeyCode))
                {
                    isLeftOn = false;
                    isRightOn = true;
                }
                else
                {
                    isLeftOn = false;
                    isRightOn = false;
                }
            }
            else
            {
                isLeftOn = false;
                isRightOn = false;
            }

            if (Input.GetKey(gasKeyCode))
            {
                isGasOn = true;
                isBreakOn = false;
            }
            else if (Input.GetKey(breakKeyCode))
            {
                isGasOn = false;
                isBreakOn = true;
            }
            else
            {
                isGasOn = false;
                isBreakOn = false;
            }
        }
    }
}