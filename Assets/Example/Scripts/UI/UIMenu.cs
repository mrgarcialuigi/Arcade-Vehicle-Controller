using Camera;
using TMPro;
using UnityEngine;
using Vehicle;

namespace Example.UI
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
        private ThirdPersonCamera thirdPersonCamera;

        [SerializeField]
        private TMP_Dropdown vehicleDropdown;

        public void ButtonPlay_Click()
        {
            switch (vehicleDropdown.value)
            {
                case 0:
                    driver.SetVehicleController(car);
                    thirdPersonCamera.SetTarget(car.transform);
                    break;
                case 1:
                    driver.SetVehicleController(bike);
                    thirdPersonCamera.SetTarget(bike.transform);
                    break;
            }

            // Close ui
            gameObject.SetActive(false);
        }
    }
}