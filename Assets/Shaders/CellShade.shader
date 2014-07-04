Shader "MyShaders/CellShade" {
	Properties{
		
		_MainTex("Main Texture",2D) = "white" {}
		_LineColor("Line Color",Color) = (0,0,0,0)
		_LineWidth("Line Width",Range(0,40)) = 0
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
         };
 			
 		float _LineWidth;
 		float4 _LineColor;
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex 
            				+ float4(input.normal,1)*_LineWidth / 1000);
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {  
            return _LineColor; 
         }
 
         ENDCG  
      }
      
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
            return textureColor; 
         }
 
         ENDCG  
      }
   }
}