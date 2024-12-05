Shader "Unlit/CrossSectionShader"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0, 0)
        _Section("Section Plane", Vector) = (0, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            CULL Back
            Blend Zero One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float sectionDepth : TEXCOORD0;
            };

            float4 _Section;

            v2f vert (float4 vertex : POSITION)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);

                float3 worldPosition = mul(unity_ObjectToWorld, vertex).xyz;
                float cameraSide = sign(_Section.w - dot(_WorldSpaceCameraPos, _Section.xyz));
                o.sectionDepth = cameraSide * (dot(worldPosition, _Section.xyz) - _Section.w);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                clip(i.sectionDepth);
                return 0;
            }
            ENDCG
        }

        Pass
        {
            CULL Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float sectionDepth : TEXCOORD0;
            };

            fixed4 _Color;
            float4 _Section;

            v2f vert(float4 vertex : POSITION)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);

                float3 worldPosition = mul(unity_ObjectToWorld, vertex).xyz;
                float cameraSide = sign(_Section.w - dot(_WorldSpaceCameraPos, _Section.xyz));
                o.sectionDepth = cameraSide * (dot(worldPosition, _Section.xyz) - _Section.w);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                clip(i.sectionDepth);
                return _Color;
            }
            ENDCG
        }
    }
}