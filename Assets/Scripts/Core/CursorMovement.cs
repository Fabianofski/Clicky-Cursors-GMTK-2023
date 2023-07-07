// /**
//  * This file is part of: GMTK-2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace F4B1.Core
{
    public class CursorMovement : MonoBehaviour
    {
        [SerializeField] private LeanTweenType moveTweenType;
        [SerializeField] private float speed;

        [SerializeField] private Vector2 bottomLeft;
        [SerializeField] private Vector2 topRight;

        private LTBezierPath path;
        
        private void Awake()
        {
            StartCoroutine(nameof(MoveToRandomPosition));
        }
        
        private IEnumerator MoveToRandomPosition()
        {
            var endPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            var position = transform.position;
            path = CreateBezierPath(position, endPos);
            var distance = path.length;

            LeanTween.move(gameObject, path, distance / speed);
            yield return new WaitForSeconds(distance / speed);

            StartCoroutine(nameof(MoveToRandomPosition));
        }

        private LTBezierPath CreateBezierPath(Vector3 startPos, Vector3 endPos)
        {
            var offsetStart = RandomVector(20);
            var offsetEnd = RandomVector(20);

            var pts = new[]
            {
                startPos, ClampVector(endPos + offsetEnd), ClampVector(startPos + offsetStart),
                endPos
            };
            return new LTBezierPath(pts);
        }

        private Vector3 ClampVector(Vector3 vector)
        {
            return new Vector3(Math.Clamp(vector.x, bottomLeft.x, topRight.x), Math.Clamp(vector.y, bottomLeft.y, topRight.y), 0);
        }

        private Vector3 RandomVector(float maxMagnitude)
        {
            var x = Random.Range(-maxMagnitude, maxMagnitude);
            var y = Random.Range(-maxMagnitude, maxMagnitude);
            var z = Random.Range(-maxMagnitude, maxMagnitude);
            return new Vector3(x, y, z);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            ;
            Gizmos.DrawLine(bottomLeft, new Vector2(bottomLeft.x, topRight.y));
            Gizmos.DrawLine(bottomLeft, new Vector2(topRight.x, bottomLeft.y));
            Gizmos.DrawLine(topRight, new Vector2(topRight.x, bottomLeft.y));
            Gizmos.DrawLine(topRight, new Vector2(bottomLeft.x, topRight.y));
        }
    }
}