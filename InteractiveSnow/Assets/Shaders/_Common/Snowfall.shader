Shader "Custom/Snowfall"
{
    Properties
    {
        [Header(Texture)]
        _MainTex ("Base Texture", 2D) = "white" {}

        [Header(Snow Settings)]
        _SnowIntensity ("Snow Intensity", Range(0,1)) = 0.5
        _SnowSpeed ("Snow Speed", Float) = 1.0
        _SnowSize ("Snow Size", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        ZTest Always
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _SnowIntensity;
            float _SnowSpeed;
            float _SnowSize;

            float rand(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv);

                float time = _Time.y * _SnowSpeed;
                float snow = 0;

                // 多层雪花叠加
                for (int layer = 1; layer <= 3; layer++)
                {
                    float scale = layer * 12.0;
                    float2 suv = uv * scale;

                    float2 cell = floor(suv);
                    float2 local = frac(suv);

                    // ===== 每个雪花随机方向 =====
                    float rnd = rand(cell);
                    float angle = rnd * UNITY_TWO_PI;

                    float2 dir = normalize(float2(
                        cos(angle),
                        sin(angle) * 0.7 - 1.0 // 偏向下落
                    ));

                    suv += dir * time * (0.15 + layer * 0.1);
                    local = frac(suv);

                    // ===== 雪花大小和形状 =====
                    float size = lerp(0.02, 0.08, rnd) / layer * _SnowSize;
                    float d = length(local - 0.5);
                    float flake = smoothstep(size, 0, d);

                    snow += flake * rnd;
                }

                // ===== 强度控制 =====
                snow = saturate(snow * _SnowIntensity);

                // ===== 混合到底色 =====
                col.rgb = lerp(col.rgb, float3(1, 1, 1), snow);

                return col;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}