#include "shared.hlsli"
#include "fullscreen_tri_shared.hlsli"

Texture2D		DiffuseTexture	: register(t0);
SamplerState	LinearSampler	: register(s0);

float4 main( VS_OUTPUT input ) : SV_TARGET
{
	float3 col = DiffuseTexture.Sample( LinearSampler, input.Texcoord.xy ).rgb;

	return float4( col, 1.0 );
}