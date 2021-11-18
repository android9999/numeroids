// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
/*
Shader "ddShaders/dd_Invert" {
Properties 
	{
		_Color ("Tint Color", Color) = (1,1,1,1)
	}
	
	SubShader 
	{
		Tags { "Queue"="Transparent" }

		Pass
		{
		   ZWrite On
		   ColorMask 0
		}
        Blend OneMinusDstColor OneMinusSrcAlpha //invert blending, so long as FG color is 1,1,1,1
        BlendOp Add
        
        Pass
		{ 
		
CGPROGRAM
#pragma vertex vert
#pragma fragment frag 
uniform float4 _Color;

struct vertexInput
{
	float4 vertex: POSITION;
    float4 color : COLOR;	
};

struct fragmentInput
{
	float4 pos : SV_POSITION;
	float4 color : COLOR0; 
};

fragmentInput vert( vertexInput i )
{
	fragmentInput o;
	o.pos = UnityObjectToClipPos(i.vertex);
	o.color = _Color;
	return o;
}

half4 frag( fragmentInput i ) : COLOR
{
	return i.color;
}

ENDCG
}
}
}

*/

Shader "GUI/ReverseFont" {
    Properties{
       _MainTex("Font Texture", 2D) = "white" {}
       _Color("Text Color", Color) = (1,1,1,1)
    }

        SubShader{
           Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
           Lighting Off Cull Off ZWrite Off Fog { Mode Off }

           Pass {
              Color[_Color]
              AlphaTest Greater 0.5
              Blend SrcColor DstColor
              BlendOp Sub
              SetTexture[_MainTex] {
                 combine previous, texture * primary
              }
           }
       }
}