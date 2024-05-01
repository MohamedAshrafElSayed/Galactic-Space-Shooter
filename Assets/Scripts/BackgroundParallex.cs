using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallex : MonoBehaviour
{
    private Material _backgroundMat;
    private float _distance;
    private float _speed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        _backgroundMat = GetComponent<Renderer>().material;        
    }

    // Update is called once per frame
    void Update()
    {
        _distance += Time.deltaTime * _speed;
        _backgroundMat.SetTextureOffset("_MainTex", Vector2.right * _distance);
    }
}
