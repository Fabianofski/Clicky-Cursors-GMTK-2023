// /**
//  * This file is part of: ClickyCursors-GMTK2023
//  * Created: 14.07.2023
//  * Copyright (C) 2023 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
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
        
        [Header("Input Checks")]
        private bool userFilledAndExists;
        private bool passwordFilled;
        [SerializeField] private StringVariable usernameVariable;
        
        public void UsernameChanged(string username)
        {
            APIManager.username = username;
            signUpBtn.interactable = false;
            loginBtn.interactable = false;

            if (username.Length <= 3)
            {
                usernameTooltip.text = "";
                userFilledAndExists = false;
                return;
            }

            usernameTooltip.text = "Loading...";
            StartCoroutine(APIManager.UserExists(UserExistsCallback));
        }

        public void PasswordChanged()
        {
            passwordTooltip.text = "";
            passwordFilled = passwordInput.text.Length >= 5;
            ActivateButtons();
        }

        private void UserExistsCallback(bool userExists)
        {
            userFilledAndExists = userExists;
            usernameTooltip.text = userFilledAndExists ? "Found existing user with this username." : "Username is free.";
            ActivateButtons();
        }

        private void ActivateButtons()
        {
            signUpBtn.interactable = passwordFilled && !userFilledAndExists;
            loginBtn.interactable = passwordFilled && userFilledAndExists;
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
                saveGameEvent.Raise();
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void SaveCredentials()
        {
            APIManager.username = usernameInput.text;
            usernameVariable.SetValue(usernameInput.text);
            APIManager.password = passwordInput.text;
            PlayerPrefs.SetString("username", usernameInput.text);
            PlayerPrefs.SetString("password", passwordInput.text);
            PlayerPrefs.Save();
            LeanTween.cancelAll();
        }
    }
}