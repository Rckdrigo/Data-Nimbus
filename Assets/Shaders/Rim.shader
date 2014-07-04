Shader "Example/Rim" 
{
    Properties {
	  _MainColor ("Main Color", Color) = (1,1,1,1)
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 viewDir;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
	  float4 _MainColor;
      float4 _RimColor;
      float _RimPower;

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _MainColor.rgb;
          half rim = 1.0 - saturate(normalize(IN.viewDir));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower * 0.5);
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }