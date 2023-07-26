// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieOrbiter : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private float spacing;
        [SerializeField] private float rowSpacing;
        [SerializeField] private GameObject orbitPrefab;
        [SerializeField] private IntVariable cookieOrbitAmount;

        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float moveSpeed = 1f;
        private float moveDistance;

        private void OnEnable()
        {
            LoadCookieOrbits();
        }

        public void LoadCookieOrbits()
        {
            for (var i = 0; i < cookieOrbitAmount.Value - transform.childCount; i++)
                Instantiate(orbitPrefab, transform);
            RedrawCookies();
        }

        private void Update()
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            moveDistance = Mathf.Abs(Mathf.Sin(Time.time));
            RedrawCookies();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void AddNewCookieOrbit()
        {
            cookieOrbitAmount.Value++;
            Instantiate(orbitPrefab, transform);
            RedrawCookies();
        }

        private void RedrawCookies()
        {
            var rows = 0;
            var childCount = transform.childCount;
            var cookies = 0;

            do
            {
                var rad = radius + rowSpacing * rows;
                var offset = moveDistance * moveSpeed * (rows + 1);
                cookies += PlaceCookies(rad, offset,childCount - cookies, cookies);
                rows++;
            } while (cookies <= childCount);
        }

        private int PlaceCookies(float rad, float offset, int amount, int index)
        {
            float cookiesPerCircumference = Mathf.FloorToInt(2 * Mathf.PI * rad / spacing);
            var angleStep = 360f / cookiesPerCircumference;

            rad += offset;

            for (var i = 0; i < Mathf.Min(cookiesPerCircumference, amount); i++)
            {
                var angle = i * angleStep * Mathf.Deg2Rad;
                var x = Mathf.Cos(angle) * rad;
                var y = Mathf.Sin(angle) * rad;

                var spawnPosition = new Vector3(x, y, 0f);
                transform.GetChild(index + i).localPosition = spawnPosition;
            }

            return Mathf.RoundToInt(cookiesPerCircumference);
        }
    }
}