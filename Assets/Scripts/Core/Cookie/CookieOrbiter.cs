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

        private void Awake()
        {
            RedrawCookies();
        }

        public void AddNewCookieOrbit()
        {
            cookieOrbitAmount.Value++;
            RedrawCookies();
        }

        private void RedrawCookies()
        {
            DestroyAllChildren();

            int rows = 0;
            int cookies = 0;

            do
            {
                cookies += SpawnCookies(radius + rowSpacing * rows, cookieOrbitAmount.Value - cookies);
                rows++;
            } while (cookies <= cookieOrbitAmount.Value);
        }

        private int SpawnCookies(float rad, int amount)
        {
            float cookiesPerCircumference = Mathf.FloorToInt(2 * Mathf.PI * rad / spacing);
            float angleStep = 360f / cookiesPerCircumference;
            
            for (int i = 0; i < Mathf.Min(cookiesPerCircumference, amount); i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * rad;
                float y = Mathf.Sin(angle) * rad;

                Vector3 spawnPosition = transform.position + new Vector3(x, y, 0f);

                GameObject spawnedObject = Instantiate(orbitPrefab, spawnPosition, Quaternion.identity);
                spawnedObject.transform.SetParent(transform);
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