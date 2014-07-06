Shader "Custom/VoxelWorldShader" {
	Properties {
		_TextureAtlas ("TextureAtlas (RGB/RGBA)", 2D) = "white" {}
		_Voxels ("Voxels (RGBA) (Dynamic)", 3D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Geometry" }
		//LOD 200
		//Blend SrcAlpha OneMinusSrcAlpha 
		Blend Off
		//Cull Off
		
		CGPROGRAM
		#pragma surface surf Lambert
		//#pragma surface surf Flat
		#pragma profileoption MaxTexIndirections=5
		#pragma glsl
		#pragma target 3.0
		  
        half4 LightingFlat (SurfaceOutput s, half3 lightDir, half atten) {
            return half4(s.Albedo.r, s.Albedo.g, s.Albedo.b, 1.0) * 0.8;
        }
         
		sampler2D _TextureAtlas;
		sampler3D _Voxels;

		struct Input {
			//float3 worldPos;
			float2 uv_TextureAtlas;
			float3 color : COLOR;
			float3 worldNormal;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			float WORLD_LENGTH = 256.0;
			float VOXEL_TEX_LENGTH = 2048.0;

			float GRID_SIZE = 16.0;
			
			float3 coords = floor(WORLD_LENGTH * IN.color.rgb);

			float4 vl2 = tex3D(_Voxels, (coords) / WORLD_LENGTH);
			float answer = WORLD_LENGTH * vl2.a;
			
			if (answer < 1.0) {
				discard;
				o.Albedo = half3(0.0, 0.0, 0.0);
				//o.Alpha = 0.0;
			} else {
			
				float2 uv0 = float2(floor(fmod(answer, GRID_SIZE)), floor(answer / GRID_SIZE)) / GRID_SIZE + 
			 		16.0 * fmod(IN.uv_TextureAtlas, 1.0/256.0);

				float2 uv_dx = ddx( IN.uv_TextureAtlas );
				float2 uv_dy = ddy( IN.uv_TextureAtlas );

				float4 rgbb = tex2D(_TextureAtlas, uv0, uv_dx, uv_dy);

				//float4 rgbb = tex2D(_TextureAtlas, uv0);

				o.Albedo = rgbb.rgb;
				//o.Alpha = rgbb.a;
			}
		}

		ENDCG
	} 
}
