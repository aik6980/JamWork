//--------------------------------------------------------------------------------------
// File: ComputeShaderSort11.hlsl
//
// This file contains the compute shaders to perform GPU sorting using DirectX 11.
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
//--------------------------------------------------------------------------------------
#include "bitonicsort_shared.hlsli"

#define BITONIC_BLOCK_SIZE 512

//--------------------------------------------------------------------------------------
// Bitonic Sort Compute Shader
//--------------------------------------------------------------------------------------
groupshared float2 shared_data[BITONIC_BLOCK_SIZE];

[numthreads(BITONIC_BLOCK_SIZE, 1, 1)]
void main( uint3 Gid : SV_GroupID, 
                  uint3 DTid : SV_DispatchThreadID, 
                  uint3 GTid : SV_GroupThreadID, 
                  uint GI : SV_GroupIndex )
{
    // Load shared data
    shared_data[GI] = SortedDataRWBuffer[DTid.x];
    GroupMemoryBarrierWithGroupSync();
    
    // Sort the shared data
    for (unsigned int j = g_iLevel >> 1 ; j > 0 ; j >>= 1)
    {
        float2 result = ((shared_data[GI & ~j].x <= shared_data[GI | j].x) == (bool)(g_iLevelMask & DTid.x))? shared_data[GI ^ j] : shared_data[GI];
        GroupMemoryBarrierWithGroupSync();
        shared_data[GI] = result;
        GroupMemoryBarrierWithGroupSync();
    }
    
    // Store shared data
    SortedDataRWBuffer[DTid.x] = shared_data[GI];
}
