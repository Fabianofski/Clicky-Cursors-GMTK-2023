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
            // var oldPathPoints = CalculateBezierCurveLine(oldPath, Mathf.RoundToInt(oldPath.length) * accuracy);
            var pathPoints = CalculateBezierCurveLine(path, Mathf.RoundToInt(path.length) * accuracy);
            // var newPathPoints = CalculateBezierCurveLine(newPath, Mathf.RoundToInt(newPath.length) * accuracy);

            // var concat = oldPathPoints.Concat(pathPoints).Concat(newPathPoints).ToList();
            //
            // var index = Mathf.RoundToInt((activeAnimation.passed / activeAnimation.time) * pathPoints.Length) + oldPathPoints.Length;

            // var truncated = concat.GetRange(index, concat.Count - index);
            // var positions = GetLineWithDesiredLength(truncated, 3).ToArray();
            // Debug.Log(truncated.Count + " " + concat.Count);
            // concat.Reverse();
            // var endLinePositions = GetLineWithDesiredLength(concat.GetRange(index, concat.Count - index), 3).ToArray();
            // var positions = startLinePositions.Concat(endLinePositions).ToArray();

            lineRenderer.positionCount = pathPoints.Length;
            lineRenderer.SetPositions(pathPoints);
            //
            // oldLineRenderer.positionCount = oldPathPoints.Length;
            // oldLineRenderer.SetPositions(oldPathPoints);
            //
            // curLineRenderer.positionCount = pathPoints.Length;
            // curLineRenderer.SetPositions(pathPoints);
            //
            // newLineRenderer.positionCount = newPathPoints.Length;
            // newLineRenderer.SetPositions(newPathPoints);
        }
        
        private List<Vector3> GetLineWithDesiredLength(List<Vector3> points, float desiredLength)
        {
            var result = new List<Vector3>();

            float totalLength = 0f;
            for (int i = 1; i < points.Count; i++)
            {
                totalLength += Vector3.Distance(points[i - 1], points[i]);
            }

            float currentLength = 0f;
            for (int i = 1; i < points.Count; i++)
            {
                float segmentLength = Vector3.Distance(points[i - 1], points[i]);
                float remainingLength = desiredLength - currentLength;

                if (remainingLength <= 0f)
                    break;

                if (currentLength + segmentLength <= desiredLength)
                {
                    result.Add(points[i]);
                    if(segmentLength >= 0.1f)
                        currentLength += segmentLength;
                }
                else
                {
                    float t = remainingLength / segmentLength;
                    Vector3 interpolatedPoint = Vector3.Lerp(points[i - 1], points[i], t);
                    result.Add(interpolatedPoint);
                    currentLength += remainingLength;
                }
            }

            return result;
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