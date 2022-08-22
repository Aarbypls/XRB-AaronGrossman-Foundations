using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MenuInput : MonoBehaviour
    {
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private DebugSandboxManager _debugSandboxManager;
        public InputActionReference menuButton;
        public InputActionReference DebugSandboxButton;
        
        // Start is called before the first frame update
        private void Start()
        {
            menuButton.action.started += MenuButtonPressed;
            DebugSandboxButton.action.started += DebugSandboxMenuPressed;
        }

        private void MenuButtonPressed(InputAction.CallbackContext obj)
        {
            if (_menuManager.gameObject.activeSelf)
            {
                _menuManager.CloseMenu();
            }
            else
            {
                _menuManager.gameObject.SetActive(true);
            }
        }

        private void DebugSandboxMenuPressed(InputAction.CallbackContext obj)
        {
            if (_debugSandboxManager.gameObject.activeSelf)
            {
                _debugSandboxManager.CloseMenu();
            }
            else
            {
                _debugSandboxManager.gameObject.SetActive(true);
            }
        }
    }
}
