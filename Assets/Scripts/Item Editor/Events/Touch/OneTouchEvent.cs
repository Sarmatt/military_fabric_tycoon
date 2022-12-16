using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event.Mouse
{
    public class OneTouchEvent : MonoBehaviour
    {
        public static Action<Touch> Handle;
        private void Update()
        {
            if (Input.touchCount != 1) return;

            Handle?.Invoke(Input.GetTouch(0));
        }
    }
}