using Camera;
using TMPro;
using UnityEngine;
using Vehicle;

namespace UI
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField]
        private VehicleDriver driver;

        [SerializeField]
        private VehicleController car;

        [SerializeField]
        private VehicleController bike;

        [SerializeField]
        private VehicleController tank;

        [SerializeField]
        private CameraBehaviorSelector cameraBehaviorSelector;

        [SerializeField]
        private FirstPersonCamera firstPersonCamera;

        [SerializeField]
        private TopDownCamera topDownCamera;

        [SerializeField]
        private TMP_Dropdown vehicleDropdown;

        [SerializeField]
        private TMP_Dropdown cameraDropdown;

        public void ButtonPlay_Click()
        {
            switch (vehicleDropdown.value)
            {
                case 0:
                    driver.SetVehicleController(car);
                    topDownCamera.SetTarget(car.transform);
                    firstPersonCamera.SetTarget(car.transform);
                    break;
                case 1:
                    driver.SetVehicleController(bike);
                    topDownCamera.SetTarget(bike.transform);
                    firstPersonCamera.SetTarget(bike.transform);
                    break;
                case 2:
                    driver.SetVehicleController(tank);
                    topDownCamera.SetTarget(tank.transform);
                    firstPersonCamera.SetTarget(tank.transform);
                    break;
            }

            cameraBehaviorSelector.Select(cameraDropdown.value);

            // Close ui
            gameObject.SetActive(false);
        }
    }
}