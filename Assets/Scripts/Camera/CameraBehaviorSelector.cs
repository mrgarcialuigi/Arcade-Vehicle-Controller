using System;
using UnityEngine;

namespace Camera
{
    public class CameraBehaviorSelector : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour[] behaviors;

        private int indexSelected = -1;

        public void Select(int index)
        {
            if (index < 0 || index > behaviors.Length - 1)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index != indexSelected)
            {
                for (int i = 0; i < behaviors.Length; i++)
                {
                    behaviors[i].enabled = i == index;
                }

                indexSelected = index;
            }
        }
    }
}