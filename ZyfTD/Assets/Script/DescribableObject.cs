using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescribableObject : MonoBehaviour {

    GameObject infoUI;

    private void Start()
    {
        infoUI = GameSetting.instance.infoUI;
    }

    private void OnMouseEnter()
    {
        infoUI.SetActive(true);
    }

    private void OnMouseExit()
    {
        infoUI.SetActive(false);
    }
}
