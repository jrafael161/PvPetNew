using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_sheet : MonoBehaviour
{

    public GameObject Pn_Character_sheet;

    public void OpenSheet()
    {
        Animator animator = Pn_Character_sheet.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Open_character_sheet", true);
    }

    public void CloseSheet()
    {
        Animator animator = Pn_Character_sheet.GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Open_character_sheet", false);
    }

}
