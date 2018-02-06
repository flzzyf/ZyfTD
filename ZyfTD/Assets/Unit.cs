using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    //移动速度
    public float speed = 3;

    public int maxHp = 1;
    int currentHp = 1;

    public GameObject gfx;

    bool isDead = false;
}
