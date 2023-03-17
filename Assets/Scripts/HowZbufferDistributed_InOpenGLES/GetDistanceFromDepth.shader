Shader "Custom/GetDistanceFromDepth"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite On

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 screenUV : TEXCOORD0;
                float4 clipPos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4x4 _invProjectionMat;
            SamplerState sampler_CameraDepthTexture;
            Texture2D _CameraDepthTexture;
            
            float GetDistanceFromDepth(float2 uv, out float3 rayDir)
            {
                // Bring UV coordinates to correct space, for matrix math below
                float2 p = uv * 2.0f - 1.0f; // from -1 to 1
            
                // Figure out the factor, to convert depth into distance.
                // This is the distance, from the cameras origin to the corresponding UV
                // coordinate on the near plane. 
                float3 rd = mul(_invProjectionMat, float4(p, -1.0, 1.0)).xyz;
            
                // Let's create some variables here. _ProjectionParams y and z are Near and Far plane distances.
                float a = _ProjectionParams.z / (_ProjectionParams.z - _ProjectionParams.y);
                float b = _ProjectionParams.z * _ProjectionParams.y / (_ProjectionParams.y - _ProjectionParams.z);
                float z_buffer_value =  _CameraDepthTexture.Sample(sampler_CameraDepthTexture,uv).r;
            
                // Z buffer valeus are distributed as follows:
                // z_buffer_value =  a + b / z 
                // So, below is the inverse, to calculate the linearEyeDepth. 
                float d = b / (z_buffer_value-a);
            
                // This function also returns the ray direction, used later (very important)
                rayDir = normalize(rd);
            
                return d;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.clipPos = UnityObjectToClipPos(v.vertex);
                o.screenUV = (o.clipPos.xy * float2(1,_ProjectionParams.x) / o.clipPos.w) * 0.5 + 0.5;  //screenUV -> (0,1)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //return tex2D(_CameraDepthTexture, i.screenUV.xy).r;
                float3 rayDir;
                float depth = GetDistanceFromDepth(i.screenUV,rayDir);
                return depth / 10;
            }
            ENDCG
        }
        
        UsePass "Standard/ShadowCaster"
    }
    //FallBack "VertexLit"
}
