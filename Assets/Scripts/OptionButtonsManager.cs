using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonsManager : MonoBehaviour
{
    public void SelectFirstActiveOptionButton()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                gameObject.transform.GetChild(i).GetComponent<Button>().Select();
                return;
            }
        }
    }
}
