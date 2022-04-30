using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Btools.utils;
using Btools.DialougeSystem;
using Btools.TimedEvents;
using Btools.numerics;
using Btools.windowTransparency;
using Btools.Components;

namespace Btools.Testing
{
    /// <summary>
    /// Simple script for testing & development. This script is pointless in the final product
    /// </summary>
    public class Testing : MonoBehaviour
    {
        public UIContentScrollView scrollContent;
        public RectTransform Template;

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
                return;

            RectTransform newGM = Instantiate<RectTransform>(Template);
            newGM.GetComponent<Image>().color = Color.HSVToRGB(Random.Range(0, 1f), 1, 1);
            scrollContent.Add(newGM);
        }
    }
}
