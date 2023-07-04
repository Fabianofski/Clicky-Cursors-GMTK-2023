// /**
//  * This file is part of: Golf, yes?
//  * Copyright (C) 2022 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BoolVariable pauseToggled;
    [SerializeField] private InputAction pauseInputAction;
    [SerializeField] private List<AtomBaseVariable> resetAtoms;

    public BoolVariable PauseToggled
    {
        set => pauseToggled = value;
    }

    private void Awake()
    {
        pauseInputAction.performed += OnPause;
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
        pauseToggled.Value = !pauseToggled.Value;
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
