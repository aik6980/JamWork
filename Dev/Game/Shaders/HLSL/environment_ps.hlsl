#include "shared.hlsli"
#include "fullscreen_tri_shared.hlsli"

float hash( float n ) { return frac(sin(n)*43758.5453123); }

float noised( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);
    f = f*f*(3.0-2.0*f);
	
    float n = p.x + p.y*157.0 + 113.0*p.z;
    return lerp(lerp(lerp( hash(n+  0.0), hash(n+  1.0),f.x),
                   lerp( hash(n+157.0), hash(n+158.0),f.x),f.y),
               lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
                   lerp( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}

// value noise, and its analytical derivatives
float3 noised2( in float2 x )
{
    float2 p = floor(x);
    float2 f = frac(x);
    float2 u = f*f*(3.0-2.0*f);
	float a = noised(float3(p+float2(0.5,0.5)/256.0,-100.0)).x;
	float b = noised(float3(p+float2(1.5,0.5)/256.0,-100.0)).x;
	float c = noised(float3(p+float2(0.5,1.5)/256.0,-100.0)).x;
	float d = noised(float3(p+float2(1.5,1.5)/256.0,-100.0)).x;
	return float3(a+(b-a)*u.x+(c-a)*u.y+(a-b-c+d)*u.x*u.y,
				6.0*f*(1.0-f)*(float2(b-a,c-a)+(a-b-c+d)*u.yx));
}

static const float2x2 m2 = float2x2(0.8,-0.6,0.6,0.8);
float terrain( in float2 x )
{
	float2  p = x*0.003;
    float a = 0.0;
    float b = 1.0;
	float2 d = 0.0;
    for( int i=0; i<6; i++ )
    {
        float3 n = noised2(p);
        d += n.yz;
        a += b*n.x/(1.0+dot(d,d));
		b *= 0.5;
        p = mul(m2,p)*2.0;
    }

	return 140.0*a;
}

float4 main( VS_OUTPUT input ) : SV_TARGET
{
	float4 rtSize = RenderTargetSize;
	float2 p = input.Position.xy * rtSize.zw;

	float3 col = float3(1.0, 0.4, 0.0);

	//float2 center = MousePosition.xy * rtSize.zw;
	float2 center = float2(0.5, 0.5);
	float2 q = p - center;
	float r = 0.2;
	col *= smoothstep( r, r + 0.01, length(q));

	col = terrain(p.xy * 4096.0)/140.0;

	float4 output = float4(col,1.0);

	return output;
}