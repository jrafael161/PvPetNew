﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AuthManager : MonoBehaviour
{
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseUser user;
    private string displayName;
    private bool signedIn;
    public GameObject Panellogin;
    public GameObject Panelinicio;

    [SerializeField]
    private InputField newinputFieldEmail = null;
    [SerializeField]
    private InputField newinputFieldPassword = null;

    [SerializeField]
    private InputField inputFieldEmail = null;
    [SerializeField]
    private InputField inputFieldPassword = null;

  
    public void Inicio()
    {
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            if (signedIn)
            {
                displayName = user.DisplayName ?? "";
                Debug.Log("Signed in " + user.UserId);
                saveuserid(user.UserId);
                SceneManager.LoadScene("01-Main");
            }
            else
            {
                OpenPanel();
            }
        }
        else
        {
            OpenPanel();
        }

    }

    public void CreateUser()
    {
        string email = newinputFieldEmail.text;
        string password = newinputFieldPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
            saveuserid(newUser.UserId);

        });
    }

    public void LoginUser()
    {

        string email = inputFieldEmail.text;
        string password = inputFieldPassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.Log("Userid:" + newUser.UserId);
            saveuserid(newUser.UserId);
            SceneManager.LoadScene("01-Main");

        });
    }   

    void saveuserid(string userid)
    {
        GameController.userid = userid;
    }

    public void OpenPanel()
    {
        Panellogin.SetActive(true);
        Panelinicio.SetActive(false);
    }

}