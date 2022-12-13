﻿using System;
using UnityEngine;

namespace Event.Mouse
{
    public class MouseUpAndDownEvent  :MonoBehaviour
    {
        public static event Action<int> onMouse;
        private void Update()
        {
            if (Input.touchCount != 1||Input.GetTouch(0).deltaPosition.y==0) return;
            var direction = Input.GetTouch(0).deltaPosition.y > 0 ? -1 : 1;
            onMouse?.Invoke(direction);
        }
    }
}