// /**
//  * This file is part of: Golf, yes?
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    private EventSystem _eventSystem;

    public GameObject FirstSelected
    {
        set => firstSelected = value;
    }

    public GameObject LastSelectedGameObject { get; private set; }

    private void Start()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        LastSelectedGameObject = firstSelected;
    }

    public void OnNavigate()
    {
        if (_eventSystem.currentSelectedGameObject != null) return;
        _eventSystem.SetSelectedGameObject(LastSelectedGameObject);
    }

    public void OnMouseMove()
    {
        if (_eventSystem.currentSelectedGameObject == null) return;
        LastSelectedGameObject = _eventSystem.currentSelectedGameObject;
    }
}
