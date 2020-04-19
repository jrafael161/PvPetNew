using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrietLands : MonoBehaviour
{
    private void OnPreRender()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
}
