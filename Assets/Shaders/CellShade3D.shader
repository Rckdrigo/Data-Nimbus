Shader "MyShaders/CellShade3D" {
	Properties{
		_MainTex("Main Texture",2D) = "white" {}
		_LineColor("Line Color",Color) = (0,0,0,0)
		_LineWidth("Line Width",Range(0,100)) = 0
		
	}

   SubShader { 
      Pass { 
      	 Cull Front 
      
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         struct vertexInput {
            float4 vertex : POSITION; 
            float3 normal : NORMAL;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION; 
            float4 v : FLOAT4;
         };
 			
 		float _LineWidth;
 		float4 _LineColor;
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex 
            				+ float4(input.normal,1)*_LineWidth / 1000);
 			output.v = input.vertex;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {  
            return _LineColor; 
         }
 
         ENDCG  
      }
      
      Pass {
         Tags { "LightMode" = "ForwardBase" } 
      	//Cull Back 
      
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
         #include "UnityCG.cginc"
 
 		uniform float4 _LightColor0;
 
         struct vertexInput {
            float4 vertex : POSITION; 
            float4 texcoord : TEXCOORD0; 
            float3 normal : NORMAL;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION; 
            float2 uv : TEXCOORD0; 
            float diffuseIntensity : FLOAT;
         };
 			
 		 sampler2D _MainTex;
 		 half4 _MainColor;
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 			output.uv = input.texcoord;
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            
            float3 normalDirection = mul(_Object2World,input.normal);
            float3 lightDirection = normalize(_WorldSpaceLightPos0 );
            output.diffuseIntensity = dot(normalDirection, lightDirection);
 			
 			
 			
            return output;
         }
 
         half4 frag(vertexOutput input) : COLOR 
         {

         	half4 textureColor = tex2D(_MainTex, input.uv) * input.diffuseIntensity;  
            return textureColor; 
         }
 
         ENDCG  
      }
   }

   Fallback "Diffuse"
}