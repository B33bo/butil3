using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using b33bo.timedEvents;

namespace b33bo.DialougeSystem
{
    /// <summary>Dialouge system, that uses delegates</summary>
    public class Dialouge : MonoBehaviour
    {
        public static float timeBetweenLetters = .05f;
        public static string currentText;

        private DialougeResponseComponent[] currentResponses;
        [SerializeField] DialougeResponseComponent responseTemplate;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] RectTransform bottomLeft;
        [SerializeField] RectTransform bottomRight;

        private static Dialouge instance;

        void Awake()
        {
            instance = this;
        }

        /// <summary>Make the dialouge system say something</summary>
        /// <param name="DisplayText">The thing to say</param>
        /// <param name="responses">The possible responses, and what they do</param>
        public static void Say(string DisplayText, params Response[] responses)
        {
            instance.SayInstance(DisplayText, responses);
        }

        /// <summary>Make the dialouge system say something</summary>
        /// <param name="DisplayText">The thing to say</param>
        /// <param name="responses">The possible responses, and what they do</param>
        public void SayInstance(string DisplayText, params Response[] responses)
        {
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

            //Prints it out
            TimedEvents.RepeatUntil(() =>
            {
                if (currentText != DisplayText)
                    return;

                str += DisplayText[str.Length];
                text.text = str;

            }, () => str == DisplayText, timeBetweenLetters);
        }
    }
}
