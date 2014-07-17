Shader "MyShaders/Multicolor" {
	SubShader {

		Pass { 
         Tags { "LightMode" = "ForwardBase" } 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
         #include "UnityCG.cginc"
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION; 
            float4 worldPosition : TEXCOORD;
            float4 diffuseIntensity : TEXCOORD1;
         };
 			
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 			output.worldPosition = mul(_Object2World, input.vertex);
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
   	
   			float3 normalDirection = normalize(input.normal);
            float3 lightDirection = normalize(_WorldSpaceLightPos0);
            output.diffuseIntensity.x = dot(normalDirection, lightDirection);
         
            return output;
         }
 
         half4 frag(vertexOutput input) : COLOR 
         {
         	half4 textureColor;
         	half y =input.worldPosition.y;
         	if(y <= 0)
         		textureColor = half4(0.92,0.619,0.274,1);
 			else if(y > 0 && y <= 5)
 				textureColor = half4(1,0.8,0.333,1);
 			else if(y > 5 && y <= 10)
 				textureColor = half4(0.568,0.745,0.682,1);
 			else if(y > 10 && y <= 15)
 				textureColor = half4(0.411,0.705,0.788,1);
 			else
         		textureColor = half4(0,0.266,0.368,1);
            return textureColor * input.diffuseIntensity.x; 
         }
 
         ENDCG  
      }
	} 
}
