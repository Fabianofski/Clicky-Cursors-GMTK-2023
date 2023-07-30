// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace F4B1.UI.Navigation
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private BoolVariable pauseToggled;
        [SerializeField] private InputAction pauseInputAction;
        [SerializeField] private List<AtomBaseVariable> resetAtoms;
        private EventSystem eventSystem;

        private void Awake()
        {
            Time.timeScale = 0;
            pauseInputAction.performed += OnPause;
            eventSystem = FindObjectOfType<EventSystem>();
        }

        private void OnEnable()
        {
            pauseInputAction.Enable();
        }

        private void OnDisable()
        {
            pauseInputAction.Disable();
        }

        private void OnPause(InputAction.CallbackContext ctx)
        {
            var selected = eventSystem.currentSelectedGameObject;
            if (selected != null && selected.GetComponent<TMP_InputField>()) return;
            
            pauseToggled.Value = !pauseToggled.Value;
        }

        public void PauseToggled()
        {
            Time.timeScale = pauseToggled.Value ? 0 : 1;
        }

        public void LoadNextScene()
        {
            ResetAtoms();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadMenuScene()
        {
            ResetAtoms();
            SceneManager.LoadScene(0);
        }

        public void ReloadCurrentScene()
        {
            ResetAtoms();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        private void ResetAtoms()
        {
            foreach (var atom in resetAtoms) atom.Reset();
        }
    }
}