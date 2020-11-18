#ifndef __NORMALCONVERT_HLSL__
#define __NORMALCONVERT_HLSL__

void NormalConvert_float(in float3 inNormal,  out float3 outNormal, out float roughness)
{
	float NormalY = 0;

	outNormal.xy = inNormal.rb ;
	outNormal.z = sqrt(1 - saturate(dot(outNormal.xy, outNormal.xy)));

	//outNormal.xy = inNormal.rb;
	//outNormal.z = 0;
	roughness = 1.0f - inNormal.g;
}

#endif//__NORMALCONVERT_HLSL__
