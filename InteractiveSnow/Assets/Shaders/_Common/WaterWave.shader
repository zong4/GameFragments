Shader "Custom/WaterWave"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (0.2,0.5,0.7,0.5)
        _Glossiness ("Smoothness", Range(0,1)) = 0.8
        _Metallic ("Metallic", Range(0,1)) = 0.0

        // Wave parameters
        _WaveHeight ("Wave Height", Float) = 0.2
        _WaveFrequency ("Wave Frequency", Float) = 1.5
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _FlowSpeed ("Flow Speed", Vector) = (0.1,0.05,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        half _Glossiness;
        half _Metallic;

        // Wave parameters
        float _WaveHeight;
        float _WaveFrequency;
        float _WaveSpeed;
        float4 _FlowSpeed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            float3 world_pos = mul(unity_ObjectToWorld, v.vertex).xyz;
            float wave =
                sin(world_pos.x * _WaveFrequency + _Time.y * _WaveSpeed) +
                cos(world_pos.z * _WaveFrequency + _Time.y * _WaveSpeed);
            v.vertex.y += wave * 0.5 * _WaveHeight;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Flowing UV coordinates
            float2 flow_uv = IN.uv_MainTex + _FlowSpeed.xy * _Time.y;
            fixed4 c = tex2D(_MainTex, flow_uv) * _Color;

            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }

    FallBack "Transparent/Diffuse"
}