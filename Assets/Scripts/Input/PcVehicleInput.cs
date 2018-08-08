using UnityEngine;

namespace Input
{
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
            if (!(UnityEngine.Input.GetKey(leftKeyCode) && UnityEngine.Input.GetKey(rightKeyCode)))
            {
                if (UnityEngine.Input.GetKey(leftKeyCode))
                {
                    isLeftOn = true;
                    isRightOn = false;
                }
                else if (UnityEngine.Input.GetKey(rightKeyCode))
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

            if (UnityEngine.Input.GetKey(gasKeyCode))
            {
                isGasOn = true;
                isBreakOn = false;
            }
            else if (UnityEngine.Input.GetKey(breakKeyCode))
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