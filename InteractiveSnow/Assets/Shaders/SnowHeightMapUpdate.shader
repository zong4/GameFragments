Shader "Custom/InteractiveSnow/SnowHeightMapUpdate"
{
    Properties
    {
        _DrawPosition ("DrawPos", Vector) = (-1,-1,0,0)
        _DrawHeight ("DrawHeight", Float) = 0.8
        _DrawMinDistance ("DrawMinDistance", Float) = 0.3
        _DrawMaxDistance ("DrawMaxDistance", Float) = 0.5
    }

    SubShader
    {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float4 _DrawPosition;
            float _DrawHeight;
            float _DrawMinDistance;
            float _DrawMaxDistance;
            float _DrawAngle;
            float _RestoreHeight;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 previous_color = tex2D(_SelfTexture2D, IN.localTexcoord.xy);
                if (_DrawPosition.x < 0)
                    return previous_color;
                
                // Rotation
                float2 offset = IN.localTexcoord.xy - _DrawPosition;
                float2x2 rot = float2x2(cos(_DrawAngle), -sin(_DrawAngle),
                                        sin(_DrawAngle), cos(_DrawAngle));
                offset = mul(rot, offset);

                // Distance
                float dist = length(offset);
                float height = smoothstep(_DrawMaxDistance, _DrawMinDistance, dist);
                
                return max(previous_color - height * _DrawHeight, 0.0f);
            }
            ENDCG
        }
    }
}