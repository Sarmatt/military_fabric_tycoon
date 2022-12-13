using System;
using UnityEngine;

namespace Event.Mouse
{
    public class MouseUpAndDownEvent : MouseBaseEvent
    {
        private void Update()
        {
            HandlePosition(Input.mousePosition.y);
        }
    }
}