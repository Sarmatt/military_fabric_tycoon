using System;
using UnityEngine;

namespace Event.Mouse
{
    public class MouseLeftAndRightEvent  :MonoBehaviour
    {
        public static event Action<int> onMouse;
        private void Update()
        {
            if (Input.touchCount != 1||Input.GetTouch(0).deltaPosition.x==0) return;
            var direction = Input.GetTouch(0).deltaPosition.x > 0 ? 1 : -1;
            onMouse?.Invoke(direction);
        }
    }
}