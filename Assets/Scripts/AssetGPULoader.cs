using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AssetGPULoader : MonoBehaviour
{
    
    public Camera activeCamera;
    RenderTexture _rt;
    public GameObject flare;
    public GameObject flareSparks;

    void Awake()
    {
        _rt = new RenderTexture(32, 32, 24);
        _rt.Create();

        activeCamera.targetTexture = _rt;
        PreLoadObject(flare);
        PreLoadObject(flareSparks);

    }

 

    public void PreLoadObject(GameObject obj)
    {
        SnapshotObject(obj);
    }

    void SnapshotObject(GameObject obj)
    {
        //move camera into position 
        Vector3 pos = obj.transform.position;
        pos += new Vector3(0f, .5f, 0f);
        activeCamera.transform.position = pos;
        activeCamera.transform.LookAt(obj.transform.position);

        activeCamera.Render();

        RenderTexture.active = _rt;
        activeCamera.targetTexture = null;
        RenderTexture.active = null;
    }
}