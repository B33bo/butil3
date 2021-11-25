using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using b33bo.components;
using b33bo.utils;

namespace b33bo.components
{
    /// <summary>A component that will show a tooltip when hovered over</summary>
    public class TooltipHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [TextArea]
        public string Text;

        /**Non-UI objects**/
        void OnMouseOver()
        {
            if (Tooltip.TooltipExists)
                Tooltip.SetInstanceText(Text);
        }

        void OnMouseExit()
        {
            if (Tooltip.TooltipExists)
                Tooltip.HideInstance();
        }

        /**UI objects**/
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Tooltip.TooltipExists)
                Tooltip.SetInstanceText(Text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Tooltip.TooltipExists)
                Tooltip.HideInstance();
        }
    }
}
