using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace b33bo.DialougeSystem
{
    /// <summary>A response for the Dialouge</summary>
    public class Response
    {
        public string text;
        public Action action;

        public Response()
        {

        }

        public Response(string text)
        {
            this.text = text;
            this.action = () =>
            {

            };
        }

        public Response(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }
    }
}
