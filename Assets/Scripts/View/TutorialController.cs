using System.Collections;
using UnityEngine;

namespace ArcadeVehicleController
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private GameObject m_ControlsPanel;

        private static bool s_DisplayedControls;

        private void Start()
        {
            if (!s_DisplayedControls)
            {
                StartCoroutine(DisplayControlsCoroutine());

                s_DisplayedControls = true;
            }
        }

        private IEnumerator DisplayControlsCoroutine()
        {
            m_ControlsPanel.SetActive(true);
            yield return new WaitForSeconds(5.0f);
            m_ControlsPanel.SetActive(false);
        }
    }
}