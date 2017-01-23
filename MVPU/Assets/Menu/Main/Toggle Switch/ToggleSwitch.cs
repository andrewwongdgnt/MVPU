using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{


    public Text label;
    public float labelOnX;
    public float labelOffX;
    // Use this for initialization
    void Start()
    {
        Toggle targetToggle = GetComponent<Toggle>();
        SetLabelText(targetToggle.isOn);
    targetToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTargetToggleValueChanged(bool newValue)
    {
        SetLabelText(newValue);
       
    }

    private void SetLabelText(bool on)
    {

        label.text = on ? "On" : "Off";
        label.rectTransform.anchoredPosition3D = new Vector3(on ? labelOnX: labelOffX, label.rectTransform.anchoredPosition3D.y);
    }
}
