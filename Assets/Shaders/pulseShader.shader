//Crazy Dan Pulse glow shader
Shader "Custom/pulseShader"{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GlowColor ("Glow Color", Color ) = ( 1.0, 1.0, 1.0, 1.0 )
		_Frequency( "Glow Frequency", Float ) = 50.0
		_MinPulseVal( "Minimum Glow Multiplier", Range( 0, 2 ) ) = 0.5
		
		_RimColor("Rim Colour", Color) = (1,1,1,1)
		_RimPower("Rim Power",Range(0.0,6.0)) = 1.0	
		
	}
	
//In c# code try something like this
//this.renderer.material.SetFloat("_RimPower", someFloatValue);
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D	_MainTex;
		fixed4		_GlowColor;
		half		_Frequency;
		half		_MinPulseVal;
		
		float4 _RimColor;
		float _RimPower;

		struct Input {
			float2 uv_MainTex;
			
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half posSin = 0.5 * sin( _Frequency * _Time.x ) + 0.5;
			half pulseMultiplier = posSin * ( 1 - _MinPulseVal ) + _MinPulseVal;
			o.Albedo = c.rgb * _GlowColor.rgb;
			o.Alpha = c.a;
			
			
			half rim =    1.0 - saturate(dot(normalize(IN.viewDir),o.Normal));
			o.Emission = _RimColor.rgb * pow(rim,_RimPower)/ pulseMultiplier;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}