#include "fullscreen_tri_shared.hlsli"

float4 main( VS_OUTPUT input ) : SV_TARGET
{
	float2 rtSize = float2(800.0,600.0);
	float2 p = input.Position.xy/rtSize;

	float3 col = float3(1.0, 0.4, 0.0);

	float2 q = p - float2(0.5, 0.5);
	float r = 0.2;
	col *= smoothstep( r, r + 0.01, length(q));

	float4 output = float4(col,1.0);

	return output;
}