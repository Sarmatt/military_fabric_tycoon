using System;
using UnityEngine;

namespace Event.Mouse
{
    public class MouseLeftAndRightEvent : MouseBaseEvent
    {
        private void Update()
        {
            HandlePosition(Input.mousePosition.x);
        }
    }
}