Shader "Custom/GlitchCube"
{
    Properties
    {
        _MainTex("Base", 2D) = "white" {}
        _Color("Color", Color) = (0.5, 0.5, 0.5, 1)
        _CubeSpec("Geometric", Float) = 40
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 texcoord : TEXCOORD;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _CubeSpec;

            v2f vert(appdata_base v)
            {
                v2f o;

                float4 wp = mul(UNITY_MATRIX_MV, v.vertex);
                wp.x = floor(wp.x * _CubeSpec + wp.xy) / _CubeSpec;
				wp.y = floor(wp.y * _CubeSpec - wp.xz) / _CubeSpec;
				wp.z = floor(wp.z * _CubeSpec - wp.yz) / _CubeSpec;
				
			
				

                float4 sp = mul(UNITY_MATRIX_P, wp);
                o.position = sp;

                float2 uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.texcoord = float3(uv * sp.w, sp.w);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.texcoord.x * i.texcoord.y * i.texcoord.zx;
                return tex2D(_MainTex, uv) / _Color * 1.5;
            }

            ENDCG
        }
    }
}
