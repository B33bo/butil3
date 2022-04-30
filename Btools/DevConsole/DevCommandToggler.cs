using UnityEngine;

namespace Btools.Components
{
    public class DevCommandToggler : MonoBehaviour
    {
        [SerializeField]
        private GameObject DevConsole;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
                DevConsole.SetActive(!DevConsole.activeSelf);
        }
    }
}
