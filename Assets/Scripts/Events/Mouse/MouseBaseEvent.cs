using System;
using UnityEngine;

namespace Event.Mouse
{
    public abstract class MouseBaseEvent : MonoBehaviour
    {
        public event Action<float> onMouse;
        public float Value;
        private float _mouseCoordinate;
        protected void Start()
        {
            _mouseCoordinate = 0;
        }

        protected void HandlePosition(float mouseCoordinate)
        {
            if (_mouseCoordinate<mouseCoordinate)
            {
                onMouse?.Invoke(Value);
            }
            else if (_mouseCoordinate>mouseCoordinate)
            {
                onMouse?.Invoke(-Value);
            }
            _mouseCoordinate = mouseCoordinate;
        }
    }
}