using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour {

    IEnumerator OnMouseDown()
    {
        //获取物体在屏幕坐标
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //获取物体与鼠标的位移
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        //Debug.Log("qwe");
        //while还按着鼠标
        while (Input.GetMouseButton(0))
        {
            //获取鼠标屏幕位置
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            //获取鼠标三维位置
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //将物体移动到鼠标三维位置
            transform.position = CurPosition;

            yield return new WaitForFixedUpdate();
        }
    }

}
