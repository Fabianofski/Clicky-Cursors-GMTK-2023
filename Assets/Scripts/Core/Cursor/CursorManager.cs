// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class CursorManager : MonoBehaviour
    {
        [Header("Cursors")]
        [SerializeField] private GameObject brokenCursor;
        [SerializeField] private GameObject grandpaCursor;
        [SerializeField] private GameObject normalCursor;
        [SerializeField] private GameObject greenCursor;
        [SerializeField] private GameObject proCursor;
        [SerializeField] private GameObject roboCursor;
        
        public void BuyBrokenCursor()
        {
            InstantiateCursor(brokenCursor);
        }
        
        public void BuyGrandpaCursor()
        {
            InstantiateCursor(grandpaCursor);
        }
        
        public void BuyNormalCursor()
        {
            InstantiateCursor(normalCursor);
        }
        
        public void BuyGreenCursor()
        {
            InstantiateCursor(greenCursor);
        }
        
        public void BuyProCursor()
        {
            InstantiateCursor(proCursor);
        }
        
        public void BuyRoboCursor()
        {
            InstantiateCursor(roboCursor);
        }

        private void InstantiateCursor(GameObject cursor)
        {
            Instantiate(cursor.gameObject, transform);
        }
    }
}