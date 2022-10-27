using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveCamGetGPUProjectionMatrix : MonoBehaviour
{
    //相机设置为 perspective , fov = 60 ,GameView宽高比为 2：1，近裁剪面1，远裁剪面100，最后的值等同于下面几个
    
    [SerializeField]
    private float fov = 60;
    [SerializeField]
    private float near = 1;
    [SerializeField]
    private float far = 100;
    [SerializeField]
    private float asp = 2;
    void Start()
    {
        //初始化相机
        Camera cam = this.GetComponentInChildren<Camera>();
        cam.orthographic = false;
        cam.fieldOfView = fov; //(half vertical H)
        cam.aspect = asp; //(width / height)
        cam.nearClipPlane = near;
        cam.farClipPlane = far;

        //------------------Matrix_I_V-------------------
        Debug.LogError(cam.cameraToWorldMatrix.ToString("0.000"));
        Vector3 xAxis = cam.transform.right;
        Vector3 yAxis = cam.transform.up;
        Vector3 zAxis = -cam.transform.forward;
        Vector3 camPos = cam.transform.position;
        Debug.Log("xAixs:" + xAxis.ToString("0.000") + "\n" +
                  "yAixs:" + yAxis.ToString("0.000") + "\n" +
                  "zAixs:" + zAxis.ToString("0.000") + "\n" +
                  "pos:" + camPos.ToString("0.000")
        );
        //cam.cameraToWorldMatrix = | ∧      ∧      ∧      ∧      |      
        //                          | xAxis  yAxis  zAxis  camPos |
        //                          | ∨      ∨      ∨      ∨      |
        //                          | 0      0      0      1      |
        //-------------------------------------------------


        //------------------Matrix_P in UnityEngine-------------------------------
        //下面是 Open GL 透视矩阵公式 ,θ = fov
        //               | cot(θ/2)/asp    0        0                0 |    | cot(30°)/2   0           0            0        |    |0.866   0     0     0     |
        //               | 0            cot(θ/2)    0                0 |    | 0           cot(30°)     0            0        |    |0     1.732   0     0     |    
        // perspective = | 0               0   -(f+n)/(f-n)  -2fn/(f-n)|  = | 0           0        -101/99     -2*100*1/99   | =  |0       0  -1.0202 -2.0202|    
        //               | 0               0        -1               0 |    | 0           0           -1            0        |    |0       0    -1     0     |
        //
        Debug.LogError("Camera.ProjectMatrix : \n" + cam.projectionMatrix.ToString("0.000"));
        Matrix4x4 perspective = Matrix4x4.Perspective(fov, asp, near, far);
        Debug.LogError("Matrix.Perspective : \n" + perspective.ToString("0.000"));
        Matrix4x4 myPespective = new Matrix4x4();
        var halfTheta = Mathf.Deg2Rad * fov / 2;
        myPespective.m00 = Mathf.Cos(halfTheta)/Mathf.Sin(halfTheta) / asp;    //cot θ = cos θ / sin θ
        myPespective.m11 = myPespective.m00 * asp;
        myPespective.m22 = -(far + near) / (far - near);
        myPespective.m23 = -2 * far * near / (far - near);
        myPespective.m32 = -1;
        Debug.Log("My Perspective\n" + myPespective.ToString("0.000"));

        //-------------------------------------------------

        //------------------Matrix_P in GPU (DX11 styled)-------------------------------
        //DX11图形API环境下上述Projection矩阵(unity中的openGL风格)转换成DX11风格,然后送入GPU
        //最后面是 Camera.ProjectionMatrix 到 shader 中 unity_Matrix_P矩阵的实现过程
        Matrix4x4 gpuProjectionMatrix = GL.GetGPUProjectionMatrix(perspective, true);
        Matrix4x4 unity_MatrixVP = gpuProjectionMatrix * cam.worldToCameraMatrix;
        Debug.LogError(
            "GL.GetGPUProjectionMatrix，去对比FrameDebug中的unity_MatrixVP :\n " + unity_MatrixVP.ToString("0.000"));
        //上述计算过程的实现,下述矩阵推到思路 ->  因为 gpu_VP = P * ? * V ; 所以 ？ = I_P * gpu_VP * I_V ; 除了问好，其他都是已知的。
        //                                      | 1    0    0          0      |
        // gpuProjectionMatrix =  perspective * | 0    1    0          0      |
        //                                      | 0    0    1          0      |
        //                                      | 0    0 -(2n+f)/2nf  -0.5    |
        Matrix4x4 perp2GPUProj = new Matrix4x4();
        perp2GPUProj.m00 = 1.0f;
        perp2GPUProj.m11 = -1.0f; //如果GetGPUProjectionMatrix 的第二个参数为 false，该值为1，表示不渲染到贴图上，不用翻转y轴
        perp2GPUProj.m22 = 1.0f;
        perp2GPUProj.m32 = -(2 * near + far) / (2 * near * far);
        perp2GPUProj.m33 = -0.5f;
        Matrix4x4 my_Unity_MatrixVP = perspective * perp2GPUProj * cam.worldToCameraMatrix;
        Debug.Log("my_Unity_MatrixVP : \n" + my_Unity_MatrixVP.ToString("0.000"));
        //------------------Matrix_P in GPU (DX11 styled)-------------------------------
        
        
        
        //拓展：
        //Pclip = perspective * Pview
        //Pclip = perspective  * |x| =  | x * cot(θ/2) / asp          | 
        //                       |y|    | y * cot(θ/2)                |
        //                       |z|    | -z(f+n)/(f-n) - 2fn/(f-n)   |
        //                       |1|    | -z                          |
        //裁剪空间中 w 是 相机空间中z值取反的结果
        
        //Pndc = float4( Pclip.x/Pclip.w , Pclip.y/Pclip.w , Pclip.z /Pclip.w, Pclip.w);
        //Zndc = Pclip.z / Pclip.w = 
    }
}
