#include "bitonicsort_shared.hlsli"

#define TRANSPOSE_BLOCK_SIZE 16

//--------------------------------------------------------------------------------------
// Matrix Transpose Compute Shader
//--------------------------------------------------------------------------------------
groupshared int2 transpose_shared_data[TRANSPOSE_BLOCK_SIZE * TRANSPOSE_BLOCK_SIZE];

[numthreads(TRANSPOSE_BLOCK_SIZE, TRANSPOSE_BLOCK_SIZE, 1)]
void main( uint3 Gid : SV_GroupID, 
                      uint3 DTid : SV_DispatchThreadID, 
                      uint3 GTid : SV_GroupThreadID, 
                      uint GI : SV_GroupIndex )
{
    transpose_shared_data[GI] = SortedDataBuffer[DTid.y * g_iWidth + DTid.x];
    GroupMemoryBarrierWithGroupSync();
    uint2 XY = DTid.yx - GTid.yx + GTid.xy;
    SortedDataRWBuffer[XY.y * g_iHeight + XY.x] = transpose_shared_data[GTid.x * TRANSPOSE_BLOCK_SIZE + GTid.y];
}