using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class AuthManager : MonoBehaviour
{
    protected Firebase.Auth.FirebaseAuth auth;
    protected Firebase.Auth.FirebaseUser user;
    
    private string displayName;
    private bool signedIn;

    public GameObject Panellogin;
    public GameObject Panelinicio;
    public GameObject Panelreg;

    [SerializeField]
    private InputField newinputFieldEmail = null;
    [SerializeField]
    private InputField newinputFieldPassword = null;

    [SerializeField]
    private InputField inputFieldEmail = null;
    [SerializeField]
    private InputField inputFieldPassword = null;

    public void Start()
    {
        string HtmlText = GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            Debug.Log("No connection");
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            Debug.Log("Connection succes");
        }
    }

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
            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
            saveuserid(newUser.UserId);
            Debug.Log("Cerrando");
            Panelreg.SetActive(false);
        });
    }

    public void LoginUser()
    {
        string email = inputFieldEmail.text;
        string password = inputFieldPassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
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

    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

}