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
        private FirstPersonCamera firstPersonCamera;

        [SerializeField]
        private TMP_Dropdown vehicleDropdown;

        public void ButtonPlay_Click()
        {
            switch (vehicleDropdown.value)
            {
                case 0:
                    driver.SetVehicleController(car);
                    firstPersonCamera.SetTarget(car.transform);
                    break;
                case 1:
                    driver.SetVehicleController(bike);
                    firstPersonCamera.SetTarget(bike.transform);
                    break;
            }

            // Close ui
            gameObject.SetActive(false);
        }
    }
}