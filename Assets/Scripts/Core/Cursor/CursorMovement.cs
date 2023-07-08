// /**
//  * This file is part of: GMTK-2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace F4B1.Core.Cursor
{
    public class CursorMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float pathLength;
        [SerializeField] private int accuracy;
        
        [SerializeField] private Vector2 bottomLeft;
        [SerializeField] private Vector2 topRight;

        private LTBezierPath oldPath;
        private LTBezierPath path;
        private LTBezierPath newPath;

        private LTDescr activeAnimation;
        
        private void Awake()
        {
            var endPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            var position = transform.position;
            
            oldPath = CreateBezierPath(position, endPos);
            path = oldPath;
            newPath = path;
            StartCoroutine(nameof(MoveToRandomPosition));
        }

        private void Update()
        {
            UpdateLineRendererPath();
        }

        private IEnumerator MoveToRandomPosition()
        {
            var endPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            
            path = newPath;
            newPath = CreateBezierPath(path.pts[3], endPos);
            
            var distance = path.length;
            activeAnimation = LeanTween.move(gameObject, path, distance / speed);
            yield return new WaitForSeconds(distance / speed);
            oldPath = path;

            StartCoroutine(nameof(MoveToRandomPosition));
        }

        private LTBezierPath CreateBezierPath(Vector3 startPos, Vector3 endPos)
        {
            var offsetStart = RandomVector(3);
            var offsetEnd = RandomVector(3);

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

        private void UpdateLineRendererPath()
        {
            var currentTime = activeAnimation.passed / activeAnimation.time;
            var targetStart = (activeAnimation.passed - pathLength) / activeAnimation.time;
            var targetEnd = (activeAnimation.passed + pathLength) / activeAnimation.time;

            var positions = new List<Vector3>();

            if (targetStart < 0)
                positions = positions.Concat(CalculateBezierCurveLine(oldPath, 1 + targetStart, 1, accuracy)).ToList();            
            positions = positions.Concat(CalculateBezierCurveLine(path, 
                                    Mathf.Max(targetStart, 0), 
                                    Mathf.Min(targetEnd, 1), 
                                           accuracy)).ToList();
            if (targetEnd > 1)
                positions = positions.Concat(CalculateBezierCurveLine(newPath, 0, 1 - targetEnd, accuracy)).ToList();

            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
        
        private Vector3[] CalculateBezierCurveLine(LTBezierPath bezierPath, float startTime, float endTime, int numPoints)
        {
            var points = new List<Vector3>();
            float stepSize = bezierPath.length / numPoints;
            
            for (int i = Mathf.RoundToInt(startTime / stepSize); i <= numPoints; i++)
            {
                float t = i * stepSize;
                if (t > endTime) return points.ToArray();
                Vector3 point = bezierPath.point(t);
                points.Add(point);
            }

            return points.ToArray();
        }
    }
}