using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using b33bo.numerics;

namespace b33bo.components
{
    ///<summary>Shows a cool tooltip</summary>
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textField;

        [SerializeField]
        private RectTransform border;

        [SerializeField]
        private float padding;

        private Canvas canvas;

        private static Tooltip Instance { get; set; }

        public string Text { get => textField.text; set => SetText(value); }

        public static string TextInstance { get => Instance.Text; set => Instance.SetText(value); }

        public float Padding { get => padding; set { padding = value; ForceUpdate(); } }

        public static float PaddingInstance { get => Instance.padding; set => Instance.Padding = value; }

        public static bool TooltipExists { get => Instance; }

        void Awake()
        {
            Instance = this;
            //Gets the rect transform of the canvas
            canvas = transform.GetComponentInParent<Canvas>();
            Hide();
        }

        void Update()
        {
            if (!canvas)
                return;

            Vector3 width = new Vector2(border.rect.width, border.rect.height);
            Vector2 newPosition = Input.mousePosition + (width / 2);

            border.position = Clamp.Canvas(newPosition, border.rect, canvas);
        }

        /// <summary>Same as Text = ...</summary>
        /// <param name="NewText">What should the new text be?</param>
        public void SetText(string NewText)
        {
            textField.text = NewText;
            if (NewText == string.Empty && Application.isPlaying)
            {
                Hide();
                return;
            }

            gameObject.SetActive(true);
            Update(); //So the object doesn't flicker into place when it's enabled
            ForceUpdate();
        }

        /// <summary>Same as TextInstance = ...</summary>
        /// <param name="NewText">What should the new text be?</param>
        public static void SetInstanceText(string NewText)
        {
            Instance.SetText(NewText);
        }

        ///<summary>Forces the tooltip to update</summary>
        public void ForceUpdate()
        {
            textField.ForceMeshUpdate();

            Vector2 widthOfText = textField.GetRenderedValues(true);

            //If the widthOfText is negative, it can be annoying.
            //Probably because it WENT INTO -4294967295 when there's no text to render

            if (widthOfText.x < 0)
                widthOfText.x = 0;

            if (widthOfText.y < 0)
                widthOfText.y = 0;

            border.sizeDelta = widthOfText + (Vector2.one * Padding);
        }

        ///<summary>Forces the tooltip to update</summary>
        public static void ForceInstanceUpdate()
        {
            Instance.ForceUpdate();
        }

        /// <summary>Hides the tooltip</summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>Hides the tooltip</summary>
        public static void HideInstance()
        {
            Instance.Hide();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Tooltip))]
    public class TooltipEditor : Editor
    {
        SerializedProperty textField;
        SerializedProperty border;

        void OnEnable()
        {
            textField = serializedObject.FindProperty("textField");
            border = serializedObject.FindProperty("border");
        }

        public override void OnInspectorGUI()
        {
            Tooltip script = target as Tooltip;

            EditorGUILayout.PropertyField(textField, new GUIContent("Text Field"));
            EditorGUILayout.PropertyField(border, new GUIContent("Border"));

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.LabelField("Text");
            script.Text = EditorGUILayout.TextArea(script.Text);

            script.Padding = EditorGUILayout.FloatField("Padding", script.Padding);
        }
    }
#endif
}
