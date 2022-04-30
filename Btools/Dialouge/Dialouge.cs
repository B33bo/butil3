using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Btools.TimedEvents;

namespace Btools.DialougeSystem
{
    /// <summary>Dialouge system, that uses delegates</summary>
    public class Dialouge : MonoBehaviour
    {
        public static float timeBetweenLetters = .05f;
        public static string currentText;

        private DialougeResponseComponent[] currentResponses;
        [SerializeField] private DialougeResponseComponent responseTemplate;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform bottomLeft;
        [SerializeField] private RectTransform bottomRight;

        [SerializeField] private AudioClip BlipSound;

        public static Dialouge Instance { get; private set; }

        private static bool _Skip = false;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                Skip();
        }

        public void Skip() =>
            _Skip = true;

        /// <summary>Make the dialouge system say something</summary>
        /// <param name="DisplayText">The thing to say</param>
        /// <param name="responses">The possible responses, and what they do</param>
        public static void Say(string DisplayText, params Response[] responses)
        {
            Instance.SayInstance(DisplayText, responses);
        }

        /// <summary>Make the dialouge system say something</summary>
        /// <param name="DisplayText">The thing to say</param>
        /// <param name="responses">The possible responses, and what they do</param>
        public void SayInstance(string DisplayText, params Response[] responses)
        {
            gameObject.SetActive(true);
            /**Clearing old responses**/
            float start = bottomLeft.localPosition.x;
            float end = bottomRight.localPosition.x;

            if (currentResponses != null)
            {
                for (int i = 0; i < currentResponses.Length; i++)
                    Destroy(currentResponses[i].gameObject); //clear current questions
            }

            currentResponses = new DialougeResponseComponent[responses.Length];

            /**Makes new responses**/
            for (int i = 0; i < responses.Length; i++)
            {
                var DialougeResponse = Instantiate(responseTemplate.gameObject, transform)
                                .GetComponent<DialougeResponseComponent>();

                //calculate position
                float newX = Mathf.Lerp(start, end, (i + .5f) / responses.Length);

                DialougeResponse.rectTransform.localPosition = new Vector3(newX, bottomLeft.localPosition.y);

                DialougeResponse.text.text = responses[i].text;
                DialougeResponse.button.onClick.AddListener(new UnityEngine.Events.UnityAction(responses[i].action));

                //set size
                DialougeResponse.rectTransform.sizeDelta = new Vector2(DialougeResponse.rectTransform.sizeDelta.x / (responses.Length) * 2,
                    DialougeResponse.rectTransform.sizeDelta.y);

                currentResponses[i] = DialougeResponse;
            }

            text.text = "";
            string str = "";

            currentText = DisplayText;

            bool Halt = false;
            //Prints it out
            Timed.RepeatUntil(() =>
            {
                if (_Skip)
                {
                    text.text = currentText;
                    Halt = true;
                    _Skip = false;
                    return;
                }

                if (currentText != DisplayText)
                {
                    Halt = true;
                    return;
                }

                if (DisplayText[str.Length] == '<')
                {
                    string newString = GetStringFromAngleBrackets(currentText, str.Length);
                    str += newString;
                    return;
                }

                str += DisplayText[str.Length];
                text.text = str;

                if (BlipSound)
                    AudioSource.PlayClipAtPoint(BlipSound, Camera.main.transform.position, .2f);

            }, () => str == DisplayText || Halt, timeBetweenLetters);
        }

        private string GetStringFromAngleBrackets(string s, int start)
        {
            string newString = "";
            for (int i = start; i < s.Length; i++)
            {
                newString += s[i];
                if (s[i] == '>')
                    return newString;
            }
            return "<";
        }
    }
}
