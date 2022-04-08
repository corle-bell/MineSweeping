/*************************************************
----Author:       Cyy 

----CreateDate:   2022-04-08 15:22:33

----Desc:         Create By BM
**************************************************/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public class ScreenColliderFit : MonoBehaviour
{
    public Camera mainCamera;

    public Transform left;
    public Transform right;
    public Transform bottom;

    [Range(-0.5f, 0.5f)]
    public float boarderOffset=0.45f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        //зѓжа
        Vector3 leftMid = mainCamera.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector3 rightMid = mainCamera.ViewportToWorldPoint(new Vector2(1, 0.5f));
        Vector3 bottomMid = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0f));


        left.position = leftMid.Muti(new Vector3(1, 1, 0)) - new Vector3(left.localScale.x * boarderOffset, 0, 0);
        right.position = rightMid.Muti(new Vector3(1, 1, 0)) + new Vector3(right.localScale.x * boarderOffset, 0, 0);
        bottom.position = bottomMid.Muti(new Vector3(1, 1, 0)) - new Vector3(0, bottom.localScale.y * boarderOffset, 0);

        bottom.SetLocalScaleX(Mathf.Abs(right.position.x - left.position.x));
    }

}
