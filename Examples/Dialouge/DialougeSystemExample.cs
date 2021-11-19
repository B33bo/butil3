using UnityEngine;
using b33bo.DialougeSystem;
using b33bo.utils;

namespace b33bo.testing
{
    public class DialougeSystemExample : MonoBehaviour
    {
        void Start()
        {
            //it can get a bit messy though :/
            //it's fiiiinneee
            //maybe pass in the name of a method or something, but dialouge is complicated!

            Dialouge.Say("HELLO, this is a dialouge system!!",
                new Response("I bet you can't put whatever you want in there!", () =>
                {
                    Application.OpenURL("https://www.youtube.com");
                }), new Response("Tell me more!", () =>
                {
                    Dialouge.Say("This was made from c# delegates, which allow you to pass code as variables\n" +
                        "the syntax can be a little wonky at some points, but it's not too complicated!", new Response("How do I use the syntax??", () =>
                        {
                            Dialouge.Say("to use the syntax, you type '() => ' and then just put your code in the curly brackets!", new Response("Open the example", () =>
                            {
                                Application.OpenURL("./Assets/Butil/Dialouge/DialougeSystemExample.cs");
                            }));
                        }), new Response("That's pretty cool!", () =>
                        {
                            Dialouge.Say("I KNOW RIGHT!!!");
                        }), new Response("Woah you can do three options???", () =>
                        {
                            Dialouge.Say("You can have as many as you want!", new Response("woah").NewArrayOfType(100));
                        }));
                }));
        }
    }
}
