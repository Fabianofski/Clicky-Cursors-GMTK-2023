// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 30.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class ObjectPool : MonoBehaviour
    {

        [SerializeField] private GameObject prefab;
        [SerializeField] private int amountToPool;
        private readonly List<GameObject> pooledObjects = new();


        private void Start()
        {
            for (var i = 0; i < amountToPool; i++)
            {
                var go = Instantiate(prefab, transform);
                go.SetActive(false);
                pooledObjects.Add(go);
            }
        }

        public GameObject GetPooledGameObject()
        {
            foreach (var pooledObject in pooledObjects)
            {
                if (!pooledObject.activeInHierarchy)
                    return pooledObject;
            }

            return null;
        }
    }
}