using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistanceFromDepth : MonoBehaviour
{
    private void Update()
    {
        var cam = Camera.main;
        cam.depthTextureMode = DepthTextureMode.Depth;
        var inverseProj = cam.projectionMatrix.inverse;
        Shader.SetGlobalMatrix("_invProjectionMat",inverseProj);
        
    }
}
