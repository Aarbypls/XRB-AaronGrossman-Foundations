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
        [SerializeField] private GameObject _lightsObject;
        [SerializeField] private List<GameObject> _cubes;
        [SerializeField] private Material _mat1;
        [SerializeField] private Material _mat2;
        [SerializeField] private Material _mat3;

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

        public void ToggleLight()
        {
            if (_lightsObject.activeSelf)
            {
                _lightsObject.SetActive(false);
            }
            else
            {
                _lightsObject.SetActive(true);
            }
        }
        
        public void ToggleLightColor(string color)
        {
            switch (color)
            {
                case "red":
                    _lightsObject.GetComponentInChildren<Light>().color = Color.red;
                    break;
                case "yellow":
                    _lightsObject.GetComponentInChildren<Light>().color = Color.yellow;
                    break;
                case "blue":
                    _lightsObject.GetComponentInChildren<Light>().color = Color.blue;
                    break;
                default:
                    break;
            }
        }

        public void ToggleMaterial(int materialNumber)
        {
            switch (materialNumber)
            {
                case 1:
                    foreach (GameObject cube in _cubes)
                    {
                        cube.GetComponent<Renderer>().material = _mat1;
                    }
                    break;
                case 2:
                    foreach (GameObject cube in _cubes)
                    {
                        cube.GetComponent<Renderer>().material = _mat2;
                    }                    
                    break;
                case 3:
                    foreach (GameObject cube in _cubes)
                    {
                        cube.GetComponent<Renderer>().material = _mat3;
                    }
                    break;
            }
        }

        public void ToggleGravity()
        {
            foreach (GameObject cube in _cubes)
            {
                var rb = cube.GetComponent<Rigidbody>();
                if (rb.useGravity)
                {
                    cube.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    cube.GetComponent<Rigidbody>().useGravity = true;
                }
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
