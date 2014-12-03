//--------------------------------------------------------------------------------------
// Constant Buffers
//--------------------------------------------------------------------------------------
cbuffer CB : register( b8 )
{
    unsigned int g_iLevel;
    unsigned int g_iLevelMask;
    unsigned int g_iWidth;
    unsigned int g_iHeight;
};

//--------------------------------------------------------------------------------------
// Structured Buffers
//--------------------------------------------------------------------------------------
StructuredBuffer<int2>		SortedDataBuffer	: register( t0 );
RWStructuredBuffer<int2>	SortedDataRWBuffer	: register( u0 );