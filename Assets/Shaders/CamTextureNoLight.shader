Shader "MyShaders/CamTextureNoLight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass { 
      	//Cull Back 
      
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         struct vertexInput {
            float4 vertex : POSITION; 
            float2 texcoord : TEXCOORD0; 
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION; 
            float2 uv : TEXCOORD0; 
         };
 			
 		 sampler2D _MainTex;
 
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 			output.uv = input.texcoord;
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
         	float4 textureColor = tex2D(_MainTex, input.uv);  
            return textureColor * 0.35; 
         }
 
         ENDCG  
      }
	} 
}
