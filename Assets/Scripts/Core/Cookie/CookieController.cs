using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace F4B1.Core
{
    public class CookieController : MonoBehaviour
    {
        private Vector2 mousePos;
        [SerializeField] private float speed;
        [SerializeField] private Vector2 leftLowerBoundaryCorner;
        [SerializeField] private Vector2 rightUpperBoundaryCorner;

        public void OnPositionChange(InputValue value)
        {
            if (Camera.main == null) return;
            
            mousePos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            mousePos = new Vector2(
                Math.Clamp(mousePos.x, leftLowerBoundaryCorner.x, rightUpperBoundaryCorner.x),
                Math.Clamp(mousePos.y, leftLowerBoundaryCorner.y, rightUpperBoundaryCorner.y));
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(leftLowerBoundaryCorner, rightUpperBoundaryCorner);
        }
    }
}
