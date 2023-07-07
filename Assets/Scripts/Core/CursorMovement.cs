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

namespace F4B1.Core
{
    public class CursorMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private int renderPoints;
        
        [SerializeField] private Vector2 bottomLeft;
        [SerializeField] private Vector2 topRight;

        private LTBezierPath oldPath;
        private LTBezierPath path;
        private LTBezierPath newPath;
        
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
            LeanTween.move(gameObject, path, distance / speed);
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
            var oldPathPoints = CalculateBezierCurveLine(oldPath, 200);
            var pathPoints = CalculateBezierCurveLine(path, 200);
            var newPathPoints = CalculateBezierCurveLine(newPath, 200);

            var concat = oldPathPoints.Concat(pathPoints).Concat(newPathPoints).ToList();
            var closestIndex = FindClosestPointIndex(concat.ToArray(), transform.position);
            
            var startIndex = Mathf.Max(0, closestIndex - renderPoints / 2);
            var endIndex = Mathf.Min(concat.Count - 1, closestIndex + renderPoints / 2);
            var positions = concat.GetRange(startIndex, endIndex - startIndex + 1).ToArray();           

            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }
        
        private int FindClosestPointIndex(Vector3[] points, Vector3 targetPoint)
        {
            float closestDistance = Mathf.Infinity;
            int closestIndex = -1;

            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector3.Distance(points[i], targetPoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
        
        private Vector3[] CalculateBezierCurveLine(LTBezierPath bezierPath, int numPoints)
        {
            var points = new List<Vector3>();
            float stepSize = bezierPath.length / numPoints;
            
            for (int i = 0; i <= numPoints; i++)
            {
                float t = i * stepSize;
                Vector3 point = bezierPath.point(t);
                points.Add(point);
            }

            return points.ToArray();
        }
    }
}