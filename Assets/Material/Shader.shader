float4x4 WorldViewProjection;    // World * View * Projection matrix
float4x4 WorldView;              // World * View
float4x4 Proj;                   // Projection matrix
float4   Ambience;                // Ambience light color
float4   ShadowColor;            // Shadow volume color (for visualization)
float4   LightColor;             // Color of the light
float4   MatColor;               // Color of the material
float4   LightView;              // Position of light in view space
float    FarClip;                // Z of far clip plane
texture  Texture;                // Texture for scene rendering
float LIGHT_FALLOFF = 6.0f;

sampler textureSampler = sampler_state
{
	Texture = <Texture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
};

//////////////////////////////////////////////////////////////
//Structures
//////////////////////////////////////////////////////////////

struct AmbienceA2V
{
	float4 Position : POSITION;
	float2 TexCoord0 : TEXCOORD0;
};

struct AmbienceV2P
{
	float4 Position : POSITION;
	float2 TexCoord0 : TEXCOORD0;
};

struct PointLightA2V
{
	float4 Position : POSITION;
	float3 Normal : NORMAL;
	float2 TexCoord0 : TEXCOORD0;
};

struct PointLightV2P
{
	float4 Position : POSITION;
	float3 LightDir : TEXCOORD0;
	float3 ViewNormal : TEXCOORD1;
	float2 TexCoord0 : TEXCOORD2;
	float4 Diffuse : TEXCOORD3;
};

struct ShadowVolumeA2V
{
	float4 Position : POSITION;
	float3 Normal : NORMAL;
};

struct ShadowVolumeV2P
{
	float4 Position : POSITION;
};

//////////////////////////////////////////////////////////////
//Vertex Shaders
//////////////////////////////////////////////////////////////

void AmbientVS(in AmbienceA2V IN, out AmbienceV2P OUT)
{
	//Transform the position from object space to homogeneous projection space
	OUT.Position = mul(IN.Position, WorldViewProjection);

	//Copy the texture coordinate through
	OUT.TexCoord0 = IN.TexCoord0;
}

void PointLightVS(in PointLightA2V IN, out PointLightV2P OUT)
{
	// Transform the position from view space to homogeneous projection space
	OUT.Position = mul(IN.Position, WorldViewProjection);

	// Compute view space position
	OUT.LightDir = LightView - mul(IN.Position, WorldView);

	// Compute world space normal
	OUT.ViewNormal = normalize(mul(IN.Normal, (float3x3)WorldView));

	// Modulate material with light to obtain diffuse
	OUT.Diffuse = MatColor * LightColor;

	// Copy the texture coordinate through
	OUT.TexCoord0 = IN.TexCoord0;
}

void ShadowVolumeVS(in ShadowVolumeA2V IN, out ShadowVolumeV2P OUT)
{
	//Compute view space normal
	IN.Normal = mul(IN.Normal, (float3x3)WorldView);

	//Obtain view space position
	float4 PosView = mul(IN.Position, WorldView);

	//Light-to-vertex vector in view space
	float3 LightVecView = PosView - LightView;

	//Extrude the vertex away from light if it's facing away from the light.
	if (dot(IN.Normal, -LightVecView) < 0.0f)
	{
		if (PosView.z > LightView.z)
			PosView.xyz += LightVecView * (FarClip - PosView.z) / LightVecView.z;
		else
			PosView = float4(LightVecView, 0.0f);

		//Transform the position from view space to homogeneous projection space
		OUT.Position = mul(PosView, Proj);
	}
	else
		OUT.Position = mul(IN.Position, WorldViewProjection);
}

//////////////////////////////////////////////////////////////
//Pixel Shaders
//////////////////////////////////////////////////////////////

float4 AmbientPS(in AmbienceV2P IN) : COLOR0
{
	// Lookup mesh texture and modulate it with material and Ambience amount
	return Ambience * tex2D(textureSampler, IN.TexCoord0);
}

float4 PointLightPS(in PointLightV2P IN) : COLOR0
{
	// Pixel to light vector
	float LenSq = dot(IN.LightDir, IN.LightDir);
IN.LightDir = normalize(IN.LightDir);

float Attn = min((LIGHT_FALLOFF * LIGHT_FALLOFF) / LenSq, 1.0f);

// Compute lighting amount
float4 I = saturate(dot(normalize(IN.ViewNormal), IN.LightDir)) * IN.Diffuse * Attn;

// Lookup mesh texture and modulate it with diffuse
return float4(tex2D(textureSampler, IN.TexCoord0).xyz, 1.0f) * I;
}

float4 ShadowVolumePS() : COLOR0
{
	return ShadowColor;
}

//////////////////////////////////////////////////////////////
//Techniques
//////////////////////////////////////////////////////////////

technique Ambient
{
	pass P0
	{
		VertexShader = compile vs_3_0 AmbientVS();
		PixelShader = compile ps_3_0 AmbientPS();
		StencilEnable = false;
		ZFunc = LessEqual;
	}
}

technique ShadowVolume2Sided
{
	pass P0
	{
		VertexShader = compile vs_3_0 ShadowVolumeVS();
		PixelShader = compile ps_3_0 ShadowVolumePS();
		CullMode = None;

		// Disable writing to the frame buffer
		AlphaBlendEnable = true;
		SrcBlend = Zero;//Set to one to show shadow volumes
		DestBlend = One;

		// Disable writing to depth buffer
		ZWriteEnable = false;
		ZFunc = Less;

		// Setup stencil states
		TwoSidedStencilMode = true;
		StencilEnable = true;
		StencilRef = 1;
		StencilMask = 0x0000FFFF;
		StencilWriteMask = 0xFFFFFFFF;
		Ccw_StencilFunc = Always;
		Ccw_StencilZFail = Incr;
		Ccw_StencilPass = Keep;
		StencilFunc = Always;
		StencilZFail = Decr;
		StencilPass = Keep;
	}
}

technique PointLight
{
	pass P0
	{
		VertexShader = compile vs_3_0 PointLightVS();
		PixelShader = compile ps_3_0 PointLightPS();
		ZEnable = true;
		ZFunc = LessEqual;
		AlphaBlendEnable = true;
		StencilEnable = true;
		BlendOp = Add;
		SrcBlend = One;
		DestBlend = One;
		StencilRef = 1;
		StencilFunc = Greater;
		StencilPass = Keep;
	}
}
