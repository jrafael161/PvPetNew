using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShowPassword : MonoBehaviour
{
    public InputField password;

    private void OnMouseDown()
    {
        password.contentType = InputField.ContentType.Standard;
    }

}
