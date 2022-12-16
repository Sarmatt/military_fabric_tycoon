using System;
using UnityEngine;

namespace Item_Editor.Mechanics
{
    public class Droppable : MonoBehaviour
    {
        public float MaxAngle=110f;
        public float MinAngle=80f;
        public string Tag;
        public Action<GameObject> DropAction;
        public void OnMouseUp()
        {
            var otherObject= GameObject.FindGameObjectWithTag(Tag);
            if (otherObject == null) return;

            var angle = Vector3.Angle(otherObject.transform.position,transform.position);
            if (MaxAngle>angle&&angle>MinAngle)
            {
                DropAction?.Invoke(otherObject);
            }
        }
    }
}