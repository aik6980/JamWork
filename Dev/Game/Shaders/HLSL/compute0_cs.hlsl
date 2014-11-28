RWTexture2D<float> Data_Texture : register(t0);

struct SortData
{
	float Distance;
	float PID;
};
//RWStructuredBuffer<float2> Data_Texture;

[numthreads(1, 1, 1)]
void main( uint3 DTid : SV_DispatchThreadID )
{
	float tmp = Data_Texture[DTid.xy];

	Data_Texture[DTid.xy] = tmp;
}