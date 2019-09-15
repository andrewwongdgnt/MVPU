using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPButton : MonoBehaviour
{
    public Text text;
    public Image checkMark;
    public Button btn;
    public Image imageBtn;

    public void SetAvailable(bool value)
    {
        btn.interactable = value;
        imageBtn.color = value ? Color.white : Color.green;
        checkMark.gameObject.SetActive(!value);
        text.gameObject.SetActive(value);
    }
}
