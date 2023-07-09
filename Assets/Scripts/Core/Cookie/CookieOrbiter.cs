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

        private void Start()
        {
            for (int i = 0; i < cookieOrbitAmount.Value; i++)
                Instantiate(orbitPrefab, transform);
            RedrawCookies();
        }

        void Update()
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            moveDistance = Mathf.Abs(Mathf.Sin(Time.time));
            RedrawCookies();
        }

        public void AddNewCookieOrbit()
        {
            cookieOrbitAmount.Value++;
            Instantiate(orbitPrefab, transform);
            RedrawCookies();
        }

        private void RedrawCookies()
        {
            int rows = 0;
            int childCount = transform.childCount;
            int cookies = 0;

            do
            {
                cookies += PlaceCookies(radius + rowSpacing * rows, moveDistance * moveSpeed * (rows + 1), childCount - cookies, cookies);
                rows++;
            } while (cookies <= childCount);
        }

        private int PlaceCookies(float rad, float offset, int amount, int index)
        {
            float cookiesPerCircumference = Mathf.FloorToInt(2 * Mathf.PI * rad / spacing);
            float angleStep = 360f / cookiesPerCircumference;

            rad += offset;
            
            for (int i = 0; i < Mathf.Min(cookiesPerCircumference, amount); i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * rad;
                float y = Mathf.Sin(angle) * rad;

                Vector3 spawnPosition = new Vector3(x, y, 0f);
                transform.GetChild(index + i).localPosition = spawnPosition;
            }

            return Mathf.RoundToInt(cookiesPerCircumference);
        }

        private void DestroyAllChildren()
        {
            int childCount = transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}