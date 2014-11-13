#ifndef __shared_hlsli
#define __shared_hlsli

cbuffer cb0 : register(b0)
{
	float4  MousePosition;	  // xy : pos [0,w/h], 
	float4	RenderTargetSize; // xy : width,height
}


#endif // !__shared_hlsli

