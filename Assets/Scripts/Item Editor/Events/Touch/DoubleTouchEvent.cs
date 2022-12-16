using System;
using UnityEngine;

namespace Event.Mouse
{
    public class DoubleTouchEvent : MonoBehaviour
    {
        public static Action<Touch,Touch> Handle;
        private void Update()
        {
            if (Input.touchCount != 2) return;

            Handle?.Invoke(Input.GetTouch(0),Input.GetTouch(1));
        }
    }
}