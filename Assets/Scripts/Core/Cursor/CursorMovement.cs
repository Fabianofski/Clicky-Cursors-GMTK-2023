﻿// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 07.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace F4B1.Core.Cursor
{
    public class CursorMovement : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float speed;
        [SerializeField] private float idleChance;
        [SerializeField] private Vector2 idleTime;

        [Header("Boundaries")]
        [SerializeField] private Vector2 bottomLeft;
        [SerializeField] private Vector2 topRight;

        private LTBezierPath path;

        private void Awake()
        {
            var startPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            transform.position = startPos;
            
            MoveToRandomPosition();
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

        private void MoveToRandomPosition()
        {
            var endPos = new Vector2(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));

            path = CreateBezierPath(transform.position, endPos);

            var distance = path.length;
            LeanTween.move(gameObject, path, distance / speed).setOnComplete(() =>
            {
                Invoke(nameof(MoveToRandomPosition), GetIdleTime());
            });
        }

        private float GetIdleTime()
        {
            var random = Random.Range(0f, 1f);
            var idle = random < idleChance;
            return idle ? Random.Range(idleTime.x, idleTime.y) : 0;
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
            return new Vector3(Math.Clamp(vector.x, bottomLeft.x, topRight.x),
                Math.Clamp(vector.y, bottomLeft.y, topRight.y), 0);
        }

        private Vector3 RandomVector(float maxMagnitude)
        {
            var x = Random.Range(-maxMagnitude, maxMagnitude);
            var y = Random.Range(-maxMagnitude, maxMagnitude);
            var z = Random.Range(-maxMagnitude, maxMagnitude);
            return new Vector3(x, y, z);
        }
    }
}