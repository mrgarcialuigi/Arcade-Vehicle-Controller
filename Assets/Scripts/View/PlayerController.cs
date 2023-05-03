using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeVehicleController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vehicle m_Vehicle;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }

            if (m_Vehicle == null) return;

            m_Vehicle.SetSteerInput(Input.GetAxis("Horizontal"));

            m_Vehicle.SetAccelerateInput(Input.GetAxis("Vertical"));
        }
    }
}