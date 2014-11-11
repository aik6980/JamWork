#include "fullscreen_tri_shared.hlsli"

VS_OUTPUT main( uint id : SV_VERTEXID )
{
	VS_OUTPUT output;

	// generate clip space position
	output.Position.x	= (float)(id / 2) * 4.0 - 1.0;
	output.Position.y	= (float)(id % 2) * 4.0 - 1.0;
	output.Position.zw	= float2(0.0, 1.0);

	// texture coord
	output.Texcoord		= (float)(id / 2) * 2.0;
	output.Texcoord		= 1.0 - (float)(id % 2) * 2.0;

	return output;
}