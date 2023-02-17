using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{

public bool movelinear = true;

[SerializeField]
Vector3 cameraMove= new Vector3(1,0,0) ;



    void Start()
    {
        transform.DOMove(cameraMove,5,movelinear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
