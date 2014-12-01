struct SortData
{
	float Distance;
	float PID;
};
RWStructuredBuffer<float2> Data_Texture;

[numthreads(1, 1, 1)]
void main( uint3 DTid : SV_DispatchThreadID )
{
	float2 tmp = Data_Texture[DTid.x];

	Data_Texture[DTid.x] = tmp;
}