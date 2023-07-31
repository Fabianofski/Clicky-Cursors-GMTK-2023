// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 31.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using UnityEngine;

namespace F4B1.Core.Pool
{
    public class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int amountToPool;
        private readonly List<T> pooledObjects = new ();

        private void Start()
        {
            for (var i = 0; i < amountToPool; i++)
            {
                var go = Instantiate(prefab, transform);
                go.SetActive(false);
                var component = go.GetComponentInChildren<T>();
                pooledObjects.Add(component);
            }
        }

        public T GetPooledGameObject()
        {
            foreach (var pooledObject in pooledObjects)
            {
                if (!pooledObject.gameObject.activeInHierarchy)
                    return pooledObject;
            }

            return null;
        }
    }
}