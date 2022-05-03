using UnityEngine;
using Btools.DialougeSystem;
using Btools.utils;
using System.Collections;

namespace Btools.testing
{
    public class DialougeSystemExample : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(DialougeTST());
        }

        private IEnumerator DialougeTST()
        {
            Dialouge.Show();
            yield return Dialouge.Say("Hello, asdjasd ,as das asd asd as");
            Debug.Log("End of txt");
        }
    }
}
