// /**
//  * This file is part of: GMTK-2023
//  * Created: 08.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cursor
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

        [Header("Cursor Count")] 
        [SerializeField] private IntVariable brokenCursorCount;
        [SerializeField] private IntVariable grandpaCursorCount;
        [SerializeField] private IntVariable normalCursorCount;
        [SerializeField] private IntVariable greenCursorCount;
        [SerializeField] private IntVariable proCursorCount;
        [SerializeField] private IntVariable roboCursorCount;

        private void Start()
        {
            for (var i = 0; i < brokenCursorCount.Value; i++) BuyBrokenCursor();
            for (var i = 0; i < grandpaCursorCount.Value; i++) BuyGrandpaCursor();
            for (var i = 0; i < normalCursorCount.Value; i++) BuyNormalCursor();
            for (var i = 0; i < greenCursorCount.Value; i++) BuyGreenCursor();
            for (var i = 0; i < proCursorCount.Value; i++) BuyProCursor();
            for (var i = 0; i < roboCursorCount.Value; i++) BuyRoboCursor();
        }

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