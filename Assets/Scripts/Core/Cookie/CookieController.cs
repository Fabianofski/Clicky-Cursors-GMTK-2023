using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace F4B1.Core.Cookie
{
    public class CookieController : MonoBehaviour
    {
        [SerializeField] private Vector2 leftLowerBoundaryCorner;
        [SerializeField] private Vector2 rightUpperBoundaryCorner;

        [SerializeField] private IntVariable speedUpgrade;
        [SerializeField] private IntVariable sizeUpgrade;
        [SerializeField] private float originalSpeed = 2;
        [SerializeField] private float speedMultiplier = 1.1f;
        private bool lockCookie;
        private Vector2 targetPos;
        private float speed => originalSpeed * Mathf.Pow(speedMultiplier, speedUpgrade.Value) / (sizeUpgrade.Value + 1);

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(leftLowerBoundaryCorner, rightUpperBoundaryCorner);
        }

        public void OnPositionChange(InputValue value)
        {
            if (Camera.main == null) return;

            targetPos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
            targetPos = new Vector2(
                Math.Clamp(targetPos.x, leftLowerBoundaryCorner.x, rightUpperBoundaryCorner.x),
                Math.Clamp(targetPos.y, leftLowerBoundaryCorner.y, rightUpperBoundaryCorner.y));

            if (lockCookie)
                targetPos = transform.position;
        }

        public void OnLockCookie(InputValue value)
        {
            lockCookie = value.isPressed;
        }
    }
}