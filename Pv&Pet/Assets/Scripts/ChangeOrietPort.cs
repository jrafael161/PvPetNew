using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrietPort : MonoBehaviour
{
    private void OnPreRender()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
