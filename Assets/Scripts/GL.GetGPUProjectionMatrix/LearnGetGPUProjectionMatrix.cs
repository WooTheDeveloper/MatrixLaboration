//参考博文
//https://blog.csdn.net/wodownload2/article/details/104793139/

using UnityEngine;

public class LearnGetGPUProjectionMatrix : MonoBehaviour
{
    //相机设置为 ortho , size = 2(视口高度的一半),GameView宽高比为 2：1，近裁剪面1，远裁剪面100，最后的值等同于下面几个
    [SerializeField]
    private float w = 4;
    [SerializeField]
    private float h = 2;
    [SerializeField]
    private float near = 1;
    [SerializeField]
    private float far = 100;
    void Start()
    {
        //初始化相机
        Camera cam = this.GetComponentInChildren<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = h;    //(half vertical H)
        cam.aspect = 2;              //(width / height)
        cam.nearClipPlane = near;
        cam.farClipPlane = far;
        
        //------------------Matrix_I_V-------------------
        Debug.LogError(cam.cameraToWorldMatrix.ToString("0.000"));
        Vector3 xAxis =  cam.transform.right;
        Vector3 yAxis =  cam.transform.up;
        Vector3 zAxis = -cam.transform.forward;
        Vector3 camPos = cam.transform.position;
        Debug.Log("xAixs:" +  xAxis.ToString("0.000") + "\n"+
                        "yAixs:" +  yAxis.ToString("0.000") + "\n" +
                        "zAixs:" +  zAxis.ToString("0.000") + "\n"+
                        "pos:" +  camPos.ToString("0.000")
        );
        //cam.cameraToWorldMatrix = | ∧      ∧      ∧      ∧      |      
        //                          | xAxis  yAxis  zAxis  camPos |
        //                          | ∨      ∨      ∨      ∨      |
        //                          | 0      0      0      1      |
        //-------------------------------------------------

        
        //------------------Matrix_P in UnityEngine-------------------------------
        //下面是 Open GL 正交矩阵公式
        //         | 2/(R-L)    0        0        -(R+L)/(R-L) |    | 2/(4-(-4))  0            0            0        |    |0.25    0     0     0     |
        //         | 0        2/(t-b)    0        -(t+b)/(t-b) |    | 0           2/(2-(-2))   0            0        |    |0     0.5     0     0     |    
        // Ortho = | 0          0     -2/(f-n)    -(f+n)/(f-n) |  = | 0           0      -2/(100-1) -(100+1)/(100-1))| =  |0       0  -0.0202 -1.0202|    
        //         | 0          0        0            1        |    | 0           0            0            1        |    |0       0     0     1     |
        //
        Debug.LogError("Camera.ProjectMatrix : \n" + cam.projectionMatrix.ToString("0.000") );
        float L = -w;
        float R = w;
        float T = h;
        float B = -h;
        Matrix4x4 ortho = Matrix4x4.Ortho(L, R, B, T, near, far);
        Debug.LogError("Matrix.Ortho : \n" + ortho.ToString("0.000"));
        Matrix4x4 myOrtho = new Matrix4x4();
        myOrtho.m00 = 2 / (R - L);
        myOrtho.m03 = -(R + L) / (R - L);
        myOrtho.m11 = 2 / (T - B);
        myOrtho.m13 = -(T + B) / (T - B);
        myOrtho.m22 = -2 / (far - near);
        myOrtho.m23 = -(far + near) / (far - near);
        myOrtho.m33 = 1;
        Debug.Log("My Ortho\n" + myOrtho.ToString("0.000"));
        
        //-------------------------------------------------

        //------------------Matrix_P in GPU (DX11 styled)-------------------------------
        //DX11图形API环境下上述Projection矩阵(unity中的openGL风格)转换成DX11风格,然后送入GPU
        //最后面是 Camera.ProjectionMatrix 到 shader 中 unity_Matrix_P矩阵的实现过程
        Matrix4x4 gpuProjectionMatrix = GL.GetGPUProjectionMatrix(ortho, true);
        Matrix4x4 unity_MatrixVP = gpuProjectionMatrix * cam.worldToCameraMatrix;
        Debug.LogError("GL.GetGPUProjectionMatrix，去对比FrameDebug中的unity_MatrixVP :\n " + unity_MatrixVP.ToString("0.000"));
        //上述计算过程的实现
        //                                | 1    0    0     0      |
        // gpuProjectionMatrix =  ortho * | 0   -1    0     0      |
        //                                | 0    0   -0.5  -f-0.5n |
        //                                | 0    0    0     1      |
        Matrix4x4 ortho2GPUProjMatrix = new Matrix4x4();
        ortho2GPUProjMatrix.m00 = 1.0f;
        ortho2GPUProjMatrix.m11 = -1.0f;    //如果GetGPUProjectionMatrix 的第二个参数为 false，该值为1，表示不渲染到贴图上，不用翻转y轴
        ortho2GPUProjMatrix.m22 = -0.5f;
        ortho2GPUProjMatrix.m23 = -far - 0.5f * near;
        ortho2GPUProjMatrix.m33 = 1.0f;
        Matrix4x4 my_Unity_MatrixVP = ortho * ortho2GPUProjMatrix * cam.worldToCameraMatrix;
        Debug.Log("my_Unity_MatrixVP : \n" + my_Unity_MatrixVP.ToString("0.000"));
        //------------------Matrix_P in GPU (DX11 styled)-------------------------------


    }
    
    
    
}
