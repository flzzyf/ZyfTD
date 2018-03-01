using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescribableObject : MonoBehaviour {

    GameObject infoUI;

    public string title;
    [TextArea(3, 30)]
    public string desc;

    private void Start()
    {
        infoUI = GameSetting.instance.infoUI;
    }

    //设置提示文本
    void SetText()
    {
        GameSetting.instance.infoUI_title.text = title;
        GameSetting.instance.infoUI_desc.text = desc;
    }

    private void OnMouseEnter()
    {
        SetText();
        //infoUI.SetActive(true);

        StartCoroutine(infoUIShow());
    }

    //刷新一下才能让其自适应长度正常
    IEnumerator infoUIShow()
    {
        infoUI.SetActive(true);

        yield return new WaitForSeconds(Time.deltaTime);
        infoUI.SetActive(false);
        infoUI.SetActive(true);

    }

    private void OnMouseExit()
    {
        infoUI.SetActive(false);
    }
}
