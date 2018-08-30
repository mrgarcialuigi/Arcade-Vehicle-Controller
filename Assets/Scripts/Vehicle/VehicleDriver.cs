using PlayerInput;
using UnityEngine;

namespace Vehicle
{
    /// <summary>
    /// The vehicle driver represents an entity which reads player input,
    /// and drives the selected vehicle controller.
    /// </summary>
    public class VehicleDriver : MonoBehaviour
    {
        [SerializeField]
        private PcVehicleInput input;

        [SerializeField]
        private VehicleController controller;

        [SerializeField]
        private float steeringInputRate;

        private float steering;

        private void Update()
        {
            if (input != null && controller != null)
            {
                if (input.IsBreakOn)
                {
                    if (!Mathf.Approximately(controller.StraightVelocityMagnitude, 0.0f))
                    {
                        if (controller.IsMovingForward)
                        {
                            controller.Gas(0.0f);
                            controller.Brake(1.0f);
                        }
                        else
                        {
                            // Gas backwards
                            controller.Gas(-1.0f);
                            controller.Brake(0.0f);
                        }
                    }
                    else // carEngine.IsStationary, 0 straight(front back) velocity
                    {
                        // Gas backwards
                        controller.Gas(-1.0f);
                        controller.Brake(0.0f);
                    }
                }
                else if (input.IsGasOn)
                {
                    controller.Gas(1.0f);
                    controller.Brake(0.0f);
                }
                else
                {
                    controller.Gas(0.0f);
                    controller.Brake(0.0f);
                }       

                if (input.IsLeftOn)
                {
                    if (steering > -1.0f)
                    {
                        if (steering > 0.0f)
                        {
                            steering = 0.0f;
                        }

                        steering -= steeringInputRate * Time.deltaTime;
                        if (steering < -1.0f)
                        {
                            steering = -1.0f;
                        }
                    }
                }
                else if (input.IsRightOn)
                {
                    if (steering < 1.0f)
                    {
                        if (steering < 0.0f)
                        {
                            steering = 0.0f;
                        }

                        steering += steeringInputRate * Time.deltaTime;
                        if (steering > 1.0f)
                        {
                            steering = 1.0f;
                        }
                    }
                }
                else
                {
                    steering = 0.0f;
                }

                controller.Steer(steering);
            }
        }

        public void SetVehicleController(VehicleController controller)
        {
            this.controller = controller;
        }
    }
}