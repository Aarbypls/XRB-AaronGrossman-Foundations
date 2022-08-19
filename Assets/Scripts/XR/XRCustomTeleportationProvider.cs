using UnityEngine;

namespace XR
{
    public class XRCustomTeleportationProvider : MonoBehaviour
    {
        public bool isTeleporting;

        public void TeleportBegin()
        {
            isTeleporting = true;
        }

        public void TeleportEnd()
        {
            isTeleporting = false;
        }
    }
}
