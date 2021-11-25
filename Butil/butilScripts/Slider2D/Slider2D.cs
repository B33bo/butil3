using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using b33bo.numerics;

namespace b33bo.components
{
    ///<summary>A 2D version of the slider</summary>
    public class Slider2D : MonoBehaviour, IDragHandler
    {
        private RectTransform rectTransform;
        public RectTransform handle;

        public Vector2 min = new Vector2(0, 0);
        public Vector2 max = new Vector2(1, 1);

        public bool WholeNumbers;

        [Space]
        public UnityEvent<Vector2> OnValueChanged = new UnityEvent<Vector2>();

        /// <summary>A normalised version of Value, goes from 0-1</summary>
        public Vector2 NormalisedValue
        {
            get
            {
                //Adds .5 because normally, it goes from -0.5 to +0.5 and I want it to go from 0 to 1
                float x = (handle.localPosition.x / rectTransform.rect.width) + .5f;
                float y = (handle.localPosition.y / rectTransform.rect.height) + .5f;

                return new Vector2(x, y);
            }
            set
            {
                if (rectTransform == null)
                    return;

                Rect rect = rectTransform.rect;
                float x = Mathf.Lerp(rect.xMin, rect.xMax, value.x);
                float y = Mathf.Lerp(rect.yMin, rect.yMax, value.y);

                handle.localPosition = new Vector2(x, y);
            }
        }

        /// <summary>The current value of the slider</summary>
        public Vector2 Value
        {
            get
            {
                float x = Mathf.Lerp(min.x, max.x, NormalisedValue.x);
                float y = Mathf.Lerp(min.y, max.y, NormalisedValue.y);

                return new Vector3(x, y);
            }
            set
            {
                //get % of each
                //removes the minimum value, so we don't have to worry about it
                float x = (value.x - min.x) / (max.x - min.x);
                float y = (value.y - min.y) / (max.y - min.y);

                NormalisedValue = new Vector2(x, y);
            }
        }

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Value = new Vector2(3.5f, 4f);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            handle.position = Input.mousePosition;
            handle.localPosition = Clamp.Rect(handle.localPosition, rectTransform.rect);

            if (WholeNumbers)
                Value = Round.Vector3(Value, 1);

            OnValueChanged.Invoke(Value);
        }
    }
}
