using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using b33bo.utils;
using b33bo.components;

namespace b33bo.components
{
    /// <summary>Allows you to chose any colour</summary>
    public class ColourPicker : MonoBehaviour
    {
        [SerializeField] Color color;
        [SerializeField] bool m_HSV;

        [Space]
        [SerializeField] Slider hueSlider;
        [SerializeField] Slider saturationSlider;
        [SerializeField] Slider valueSlider;
        [SerializeField] Slider alphaSlider;

        [Space]

        [SerializeField] Image hueSquareImage;
        private Image saturationSliderImage;
        Image valueSliderImage;
        Image alphaSliderImage;

        Image red1;
        Image green1;
        Image blue1;

        [Space]
        [SerializeField] Image redBackground;
        [SerializeField] Image greenBackground;
        [SerializeField] Image blueBackground;

        [Space]
        [SerializeField] Slider red;
        [SerializeField] Slider green;
        [SerializeField] Slider blue;

        [Space]
        [SerializeField] Slider2D colourPicker2DComponent;
        Image colourPreview;
        [SerializeField] TMP_InputField hexCode;
        [Space]

        [SerializeField] GameObject hsv;
        [SerializeField] GameObject rgb;
        [SerializeField] TextMeshProUGUI hsvRgbText;

        public UnityEvent<Color> OnColourChanged = new UnityEvent<Color>();

        /// <summary>The current value of the colour picker</summary>
        public Color Colour
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                RefreshColour();
                OnColourChanged.Invoke(color);
            }
        }

        /// <summary>Is the colour picker in HSV mode or RGB mode</summary>
        public bool HSV
        {
            get
            {
                return m_HSV;
            }
            set
            {
                m_HSV = value;
                hsv.SetActive(value);
                rgb.SetActive(!value);
            }
        }


        private void OnValidate()
        {
            LoadPrivateValues();
            Colour = color;
            HSV = m_HSV;
        }

        void Awake()
        {
            LoadPrivateValues();
        }

        void RefreshColour()
        {
            try
            {
                float alpha = color.a;
                Color.RGBToHSV(color, out float H, out float S, out float V);

                hueSquareImage.color = Color.HSVToRGB(H, 1, 1);

                hueSlider.value = H;

                saturationSliderImage.color = Color.HSVToRGB(H, 1, V);
                saturationSlider.value = S;

                valueSliderImage.color = Color.HSVToRGB(H, S, 1);
                valueSlider.value = V;

                alphaSliderImage.color = Color.HSVToRGB(H, S, V);
                alphaSlider.value = alpha;

                //last
                red1.color = new Color(0, color.g, color.b);
                redBackground.color = new Color(1, color.g, color.b);

                green1.color = new Color(color.r, 0, color.b);
                greenBackground.color = new Color(color.r, 1, color.b);

                blue1.color = new Color(color.r, color.g, 0);
                blueBackground.color = new Color(color.r, color.g, 1);

                red.value = color.r;
                green.value = color.g;
                blue.value = color.b;
                colourPreview.color = color;

                string HexCode = ColorUtility.ToHtmlStringRGBA(color);
                hexCode.text = "#" + HexCode;

                hexCode.textComponent.color = color.Invert();

                colourPicker2DComponent.Value = new Vector2(S, V);
            }
            catch (System.NullReferenceException)
            {

            }
        }

        void LoadPrivateValues()
        {
            //AAAA so slow. Luckily it's only called on awake

            saturationSliderImage = saturationSlider.GetComponent<Image>();
            red1 = red.GetComponent<Image>();
            green1 = green.GetComponent<Image>();
            blue1 = blue.GetComponent<Image>();
            valueSliderImage = valueSlider.GetComponent<Image>();
            alphaSliderImage = alphaSlider.GetComponent<Image>();
            colourPreview = hexCode.GetComponent<Image>();
        }

        /**Only here because you can't really pass arguments in the editor :/**/

        public void SetColourSatVal(Vector2 satHue)
        {
            Color.RGBToHSV(color, out float H, out _, out _);
            Color newCol = Color.HSVToRGB(H, satHue.x, satHue.y);
            Colour = newCol;
        }

        public void ToggleHSV()
        {
            HSV = !HSV;

            hsvRgbText.text = HSV ? "HSV" : "RGB";
        }

        public void Randomize()
        {
            Colour = new Color(Random.value, Random.value, Random.value, Random.value);
        }

        public void Invert()
        {
            Colour = color.Invert();
        }

        public void SetHue(float H)
        {
            Color.RGBToHSV(color, out float _, out float S, out float V);
            Color newCol = Color.HSVToRGB(H, S, V);
            Colour = newCol;
        }

        public void SetSat(float S)
        {
            Color.RGBToHSV(color, out float H, out float _, out float V);
            Color newCol = Color.HSVToRGB(H, S, V);
            Colour = newCol;
        }

        public void SetVal(float V)
        {
            Color.RGBToHSV(color, out float H, out float S, out _);
            Color newCol = Color.HSVToRGB(H, S, V);
            Colour = newCol;
        }

        public void SetRed(float R)
        {
            Color newCol = color;
            newCol.r = R;
            Colour = newCol;
        }

        public void SetGreen(float G)
        {
            Color newCol = color;
            newCol.g = G;
            newCol.a = color.a;
            Colour = newCol;
        }

        public void SetBlue(float B)
        {
            Color newCol = color;
            newCol.b = B;
            Colour = newCol;
        }

        public void SetAlpha(float A)
        {
            Color newCol = color;
            newCol.a = A;
            Colour = newCol;
        }

        public void SetHex(string Hex)
        {
            if (ColorUtility.TryParseHtmlString(Hex, out Color newCol))
            {
                Colour = newCol;
            }
        }
    }
}
