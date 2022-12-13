using UnityEngine;

namespace ObjectAction
{
    public class Scale : MonoBehaviour
    {
        public Camera Camera;
        private void Awake()
        {
            if (Camera == null)
                Camera = Camera.main;
            Camera.transform.position=new Vector3(transform.position.x,transform.position.y,-3);
        }

        private void Update()
        {
            //Pinch
            if (Input.touchCount >= 2)
            {
                var pos1 = PlanePosition(Input.GetTouch(0).position);
                var pos2 = PlanePosition(Input.GetTouch(1).position);
                var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
                var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

                //calc zoom
                var zoom = Vector3.Distance(pos1,pos2) /
                           Vector3.Distance(pos1b,pos2b);

                //edge case
                if (zoom == 0 || zoom > 10)
                    return;

                //Move cam amount the mid ray
                Camera.transform.position = new Vector3(Camera.transform.position.x,Camera.transform.position.y,Camera.transform.position.z*zoom);
            }

        }

        protected Vector3 PlanePosition(Vector2 screenPos)
        {
            //position
            var rayNow = Camera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(rayNow,out var enterNow))
                return rayNow.GetPoint(enterNow.distance);

            return Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position,transform.position + transform.up);
        }
    }
}