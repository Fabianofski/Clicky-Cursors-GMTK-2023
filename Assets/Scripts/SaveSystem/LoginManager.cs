// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 14.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace F4B1.SaveSystem
{
    public class LoginManager : MonoBehaviour
    {
        
        [Header("Components")]
        [SerializeField] private Button signUpBtn;
        [SerializeField] private Button loginBtn;
        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TextMeshProUGUI usernameTooltip;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private TextMeshProUGUI passwordTooltip;

        [Header("Events")] 
        [SerializeField] private VoidEvent saveGameEvent;
        [SerializeField] private UnityEvent signedUp;
        
        [Header("Input Checks")]
        private bool userFilled;
        private bool userExists;
        private bool serverError;
        private bool passwordFilled;
        [SerializeField] private StringVariable usernameVariable;
        
        public void UsernameChanged(string username)
        {
            signUpBtn.interactable = false;
            loginBtn.interactable = false;

            userFilled = username.Length >= 3;
            if (!userFilled)
            {
                usernameTooltip.text = "";
                return;
            }
            
            usernameTooltip.text = "Loading...";
            StartCoroutine(APIManager.UserExists(username ,UserExistsCallback, exception =>
            {
                serverError = true;
                usernameTooltip.text = "Couldn't connect to server.";
            }));
        }

        public void PasswordChanged()
        {
            passwordTooltip.text = "";
            passwordFilled = passwordInput.text.Length >= 5;
            ActivateButtons();
        }

        private void UserExistsCallback(bool userExists)
        {
            serverError = false;
            this.userExists = userExists;
            usernameTooltip.text = userExists && userFilled ? "Found existing user with this username." : "Username is free.";
            ActivateButtons();
        }

        private void ActivateButtons()
        {
            signUpBtn.interactable = passwordFilled && !userExists && userFilled && !serverError;
            loginBtn.interactable = passwordFilled && userExists && userFilled && !serverError;
        }
        
        public void Login()
        {
            passwordTooltip.text = "Loading...";
            StartCoroutine(APIManager.AttemptLogin(usernameInput.text, passwordInput.text, LoginCallback));
        }

        private void LoginCallback(bool success)
        {
            if (success)
            {
                SaveCredentials();
                LeanTween.cancelAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
                passwordTooltip.text = "Password or username invalid.";
        }
        
        public void SignUp()
        {
            var isLoggedIn = usernameVariable.Value != "";
            SaveCredentials();
            if (!isLoggedIn) 
                SaveAndClosePanel();
            else
            {
                LeanTween.cancelAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void SaveAndClosePanel()
        {
            saveGameEvent.Raise();
            loginBtn.interactable = false;
            signUpBtn.interactable = false;
            usernameInput.text = "";
            passwordInput.text = "";
            signedUp.Invoke();
        }

        private void SaveCredentials()
        {
            APIManager.username = usernameInput.text;
            usernameVariable.SetValue(usernameInput.text);
            APIManager.password = passwordInput.text;
            PlayerPrefs.SetString("username", usernameInput.text);
            PlayerPrefs.Save();
        }
    }
}