using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _windows;
        [SerializeField] private TMP_Text _dominantHandText;

        private void Awake()
        {
            Handed handedness = (Handed)PlayerPrefs.GetInt("handedness");
            
            if (handedness == Handed.Left)
            {
                _dominantHandText.text = "Left";
            }
            else
            {
                _dominantHandText.text = "Right";
            }
        }

        public void OpenWindow(int index)
        {
            CloseAllWindows();
            _windows[index].SetActive(true);
        }
        
        public void CloseMenu()
        {
            CloseAllWindows();
            Invoke(nameof(DeactivateMenu), 0.05f);
        }

        private void DeactivateMenu()
        {
            gameObject.SetActive(false);
        }
        
        public void CloseAllWindows()
        {
            foreach (GameObject window in _windows)
            {
                window.SetActive(false);
            }
        }
    }
}
