Shader "URP/SpriteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        _AmbientIntensity ("Ambient Intensity", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _AMBIENT_LIGHT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 color : COLOR; 
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
                float _Smoothness;
                float _Metallic;
                float _AmbientIntensity;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.color = IN.color;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Muestra la textura y color base
                half4 texColor = tex2D(_MainTex, IN.uv) * _Color * IN.color;
                
                // Calcular iluminación ambiental
                float3 ambient = SampleSH(IN.normalWS) * _AmbientIntensity;
                float3 finalColor = texColor.rgb * ambient;

                // Iluminación de la luz principal
                #ifdef _MAIN_LIGHT_SHADOWS
                    Light mainLight = GetMainLight(TransformWorldToShadowCoord(IN.positionWS));
                    float3 lightDir = normalize(mainLight.direction);
                    float NdotL = max(0, dot(normalize(IN.normalWS), lightDir));
                    finalColor += texColor.rgb * mainLight.color * NdotL * mainLight.distanceAttenuation;
                #endif

                // Luces adicionales (puntuales, focos, etc.)
                #ifdef _ADDITIONAL_LIGHTS
                    uint numAdditionalLights = GetAdditionalLightsCount();
                    for (uint i = 0; i < numAdditionalLights; i++)
                    {
                        Light light = GetAdditionalLight(i, IN.positionWS);
                        float3 lightDir = normalize(light.direction);
                        float NdotL = max(0, dot(normalize(IN.normalWS), lightDir));
                        finalColor += texColor.rgb * light.color * NdotL * light.distanceAttenuation;
                    }
                #endif

                return half4(finalColor, texColor.a);
            }
            ENDHLSL
        }
    }
}