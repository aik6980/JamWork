#include "shared_cs.hlsli"

RWTexture2D<uint>	DiffuseTexture	: register(u0);

[numthreads(MAX_2D_GROUPTHREADS, MAX_2D_GROUPTHREADS, 1)]
void main( uint3 DTid : SV_DispatchThreadID )
{
	float tmp = DiffuseTexture[DTid.xy];

	DiffuseTexture[DTid.xy] = tmp;
}