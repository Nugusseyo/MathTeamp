using System;
using UnityEngine;
using UnityEngine.UI;

namespace AH.Code
{
    public class QuitBtn : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Quit()
        {
            if (_button != null)
            {
                Application.Quit();
            }
        }
    }
}