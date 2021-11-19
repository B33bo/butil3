using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace b33bo.components
{
    /// <summary>Scales an image to the right dimenstions in the UI</summary>
    [ExecuteInEditMode]
    [AddComponentMenu("UI/Image Scaler")]
    public class UIscaler : MonoBehaviour
    {
        public bool UseXAxis;

        [SerializeField]
        float scale;

        /// <summary>The scale of the UI item</summary>
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                ResetSprite();
            }
        }

        private RectTransform rectTransform;
        private Image sp;
        private Sprite oldSprite;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            sp = GetComponent<Image>();
        }

        /// <summary>Forces it to retry</summary>
        public void ResetSprite()
        {
            if (!rectTransform)
                rectTransform = GetComponent<RectTransform>();

            if (!sp)
                sp = GetComponent<Image>();

            if (!sp.sprite)
                return;

            float width = sp.sprite.rect.width;
            float height = sp.sprite.rect.height;

            float aspectRatio;

            if (UseXAxis)
                aspectRatio = height / width;
            else
                aspectRatio = width / height;

            if (UseXAxis)
                rectTransform.sizeDelta = new Vector2(scale, scale * aspectRatio);
            else
                rectTransform.sizeDelta = new Vector2(scale * aspectRatio, scale);
        }

        void Update()
        {
            if (sp.sprite == oldSprite)
                return;

            oldSprite = sp.sprite;
            ResetSprite();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIscaler))]
    public class UIscalerEditor : Editor
    {
        UIscaler script;

        private void OnEnable()
        {
            script = (UIscaler)target;
        }

        public override void OnInspectorGUI()
        {
            float scale = script.Scale;
            bool UseXAxis = script.UseXAxis;

            script.Scale = EditorGUILayout.FloatField("Scale", scale);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Axis selected:");

            if (GUILayout.Button(UseXAxis ? "X axis" : "Y axis"))
                script.UseXAxis = !script.UseXAxis;

            EditorGUILayout.EndHorizontal();

            if (scale != script.Scale)
                script.ResetSprite();

            if (UseXAxis != script.UseXAxis)
                script.ResetSprite();
        }
    }
#endif
}
