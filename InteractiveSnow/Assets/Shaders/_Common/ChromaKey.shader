Shader "Custom/ChromaKey"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _ChromaColor ("Chroma Color", Color) = (0,1,0,1)
        _Tolerance ("Tolerance", Range(0,1)) = 0.3
        _Feather ("Feather", Range(0,1)) = 0.1

        // Color adjustment properties
        _MulColor ("Multiply Color", Color) = (1,1,1,1)
        _AddColor ("Add Color", Color) = (0,0,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _ChromaColor;
            float _Tolerance;
            float _Feather;

            // Color adjustment properties
            fixed4 _MulColor;
            fixed4 _AddColor;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                // Color distance from chroma key
                float diff = distance(color.rgb, _ChromaColor.rgb);

                // Smooth alpha transition
                float alpha = smoothstep(
                    _Tolerance - _Feather,
                    _Tolerance + _Feather,
                    diff
                );

                // Apply color adjustment
                color.rgb = color.rgb * _MulColor.rgb + _AddColor.rgb;
                color.a = alpha;
                return color;
            }
            ENDCG
        }
    }

    FallBack Off
}