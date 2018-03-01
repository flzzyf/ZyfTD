using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour {

    bool uiAbove = true;
	
	void Update ()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if(MouseAbove() == uiAbove)
        {
            Debug.Log("鼠标在上");
            uiAbove = !uiAbove;

            SwitchUIPos();

        }
    }

    //切换UI位置
    void SwitchUIPos()
    {
        Vector3 newPos = transform.position;
        newPos.z *= -1;
        transform.position = newPos;
    }

    //判断鼠标在屏幕上部
    bool MouseAbove()
    {
        return Input.mousePosition.y > Screen.height / 2;
    }
}
