#version 430 core

in vec2 v_uv;
out vec4 FragColor;

uniform sampler2D u_MainTex;
uniform sampler2D u_NoiseTex;

uniform vec3 u_lightDir; 
uniform vec3 u_lightColor;

uniform mat4 u_vp;
uniform mat4 u_model;
uniform vec3 u_viewPos;

uniform float u_time;

uniform vec4 u_cloud0_frame;
uniform vec4 u_cloud1_frame;

uniform vec3 cloud0_pos;
uniform vec3 cloud1_pos;

const vec3 cloudEdgeColor = vec3(0.1, 0.1, 0.4);
const vec3 cloudColorFar = vec3(0.8,0.8,0.8);
const vec3 cloudColorNear = vec3(1.0, 1.0, 1.0);
const vec3 cloudColorLow = vec3(0.8,0.8,0.8);

const float _Cloud_SDF_TSb = 0.0;

float cartoon(float c){
	return (1+c)*(1+c)*0.25;
}

float step3(float c){
    float s = 1.0/3.0;
    return step(0.0, c) * s + step( s, c)*s + step(2.0*s ,c ) * s;
}

vec3 step3(vec3 c){
    return vec3(step3(c.r), step3(c.g), step3(c.b));
}

float fresnel(vec3 normal, vec3 view)
{
	return 1.0 - clamp(dot(normal, view), 0.0, 1.0);
}

vec4 computeCloudColor(vec3 cloud_pos, vec4 cloud_frame, float cloud_radius, vec3 dir, vec3 sun){
    vec3 up = vec3(0,1,0);
    vec3 right = cross(cloud_pos, up);
    up = cross(right, cloud_pos);

    float dotCloud = dot(dir, cloud_pos);
    vec3 ptCloud = dir / dotCloud - cloud_pos;
    float x = dot(ptCloud, right);
    float y = dot(ptCloud, up);

    vec2 cloudSize = cloud_frame.zw - cloud_frame.xy;
    float x_radius =cloudSize.x;
    float y_radius = cloudSize.y;
    x = x / cloud_radius / x_radius;
    y = y / cloud_radius / y_radius;
    x = (x + 1.0)/2.0;
    y = (y + 1.0)/2.0;

    

    float sx = mix(cloud_frame.x, cloud_frame.z, x);
    float sy = mix(cloud_frame.y, cloud_frame.w, y);

    vec2 noiseUV = sin(vec2(sy, sx) + vec2(u_time, u_time) * 0.03);
    //noiseUV = fract(noiseUV);
    vec4 Noise = texture(u_NoiseTex, noiseUV);
                 
    float UVdisturbance =  Noise.b * 0.03;
    //vec2 disturbance = sin(u_time + dir.xy) * 0.01;
    float disturbance = sin(u_time + Noise.b * 6.2831) * 0.01;

    vec4 baseMap = texture(u_MainTex, vec2(sx,sy) + Noise.b * 0.01);
    float sdf = baseMap.b - 0.5;

    float baseMapSMstep = smoothstep(clamp((_Cloud_SDF_TSb-0.08),0.0,1.5),_Cloud_SDF_TSb, baseMap.b);

    float alpha = baseMap.a * step(0.0, y) * step(x, 1.0) * step(y, 1.0) * baseMapSMstep;
    
    float dis = length(dir - sun);
    //dis = dis/2.0;

    vec3  EdgeColor = cloudEdgeColor * baseMap.g * (1- smoothstep(0.0, 1.0, dis));

    vec4 cloudColor;
    cloudColor.xyz = mix(cloudColorNear,cloudColorFar,dis) ;
    //cloudColor.xyz = cloudColorFar.xyz;
    cloudColor.xyz = mix(cloudColorLow, cloudColor.xyz, smoothstep(0.0, 0.6, baseMap.r));
    cloudColor.a = alpha;

    cloudColor.xyz = cloudColor.xyz + EdgeColor;

    return cloudColor;
}


void main()
{

    vec2 ndc = v_uv * 2.0 - 1.0;

    vec4 clip = vec4(ndc, -1.0, 1.0);

    mat4 u_invVP = inverse(u_vp);
    vec4 world = u_invVP * clip;
    world.xyz /= world.w;


    vec3 dir = normalize(world.xyz - u_viewPos);
    float yaw = atan(dir.z, dir.x);
    float hillY = sin(6 * yaw) * 0.05 + 0.025 + sin(11 * yaw + 4.7) *0.025 + sin(23 * yaw + 18.7)*0.0125 + sin(37 * yaw + 7.3)*0.00626 + sin(103 * yaw + 12.3)*0.003125 + sin(199 * yaw + 3.7)*0.0015625;

    vec3 cloud = normalize(vec3(2.5,1.0,0.1));
    //vec3 cloud1 = normalize(vec3(0.6,0.3,-1.0));
    //vec3 cloud  = normalize(cloud0_pos);
    vec3 cloud1 = normalize(cloud1_pos);

    //vec3 sun = normalize(vec3(0.4, 0.8, -1.0));
    vec3 sun = normalize(u_lightDir);

    float sun_radius = 0.1;

    float dis = length(dir - sun);

    vec4 cloudColor = computeCloudColor(cloud, u_cloud0_frame, 0.5, dir, sun);
    vec4 cloudColor1 = computeCloudColor(cloud1, u_cloud1_frame, 0.5, dir, sun);
    // float rim = smoothstep(-0.1,-0.05, sdf) * (1-smoothstep(-0.05, 0.0, sdf));
    // vec3 cloudEdgeColor = vec3(0.1,0.1,0.1);
    // vec3 edgeCloud =  cloudEdgeColor * rim;
    
    //cloudColor = cloudColor + edgeCloud;
    //cloudColor  =vec3(1.0,0.0,0.0);


    float lightDirtY = 1;
    vec3 lDir = vec3(0,0,0);
    lDir.y = dir.y;
    lDir.z = 0.1;

    vec3 _DayC1 = vec3(0.4, 0.7, 1.0); // orange / warm
    vec3 _DayC2 = vec3(0.4, 0.7, 1.0); // skyblue
    vec3 _DayC3 = vec3(0.4, 0.7, 1.0);
    vec3 _NightC1 = vec3(0, 0, 0.2);

    vec3 _HorizonColor[6];
    _HorizonColor[0] = vec3(0.4, 0.7, 1.0); // horizon orange
    _HorizonColor[1] = vec3(0.9, 0.5, 0.6); // purple, night
    _HorizonColor[2] = vec3(0.4, 0.7, 1.0); // light sky blue
    _HorizonColor[3] = vec3(0.6, 0.8, 1.0); // white sky blue
    _HorizonColor[4] = vec3(0.1, 0.3, 0.6); // deep blue
    _HorizonColor[5] = vec3(0.0, 0.0, 0.2); // night

    vec3 daySkyColor;
    vec3 dayHorizonColor;
    vec3 dayMixedColor;
    if(dir.y > 0 && dir.y < 1){
        vec3 colLowMid = mix(_DayC1, _DayC2, smoothstep(0.0, 0.5, lDir.z));
        daySkyColor = mix(colLowMid, _DayC3, smoothstep(0.5, 1.0, lDir.z));

        dayHorizonColor = mix(_HorizonColor[0], _HorizonColor[1], smoothstep(0.0, 0.2, lDir.z));
        dayHorizonColor = mix(dayHorizonColor, _HorizonColor[2], smoothstep(0.2, 0.5, lDir.z));
        dayHorizonColor = mix(dayHorizonColor, _HorizonColor[3], smoothstep(0.8, 0.9, lDir.z));
        dayHorizonColor = mix(dayHorizonColor, _HorizonColor[4], smoothstep(0.9, 1.0, lDir.z));
        dayMixedColor = mix(daySkyColor, daySkyColor, smoothstep(0.0, 0.5 +0.4 * smoothstep(0.9,1.0,lDir.z), lDir.y));
    }

    vec3 nightSkyColor = mix(_DayC1, mix(_NightC1, _DayC3, smoothstep(0.5, 1.0, lDir.z)), smoothstep(0.0, 0.5, lDir.z));

    vec3 nightHorizonColor =  mix(_HorizonColor[5], _HorizonColor[4], smoothstep(0.0, 0.1, smoothstep(0.7, 1.0, lDir.z) * 0.1));
    nightHorizonColor = mix(_HorizonColor[1], nightHorizonColor, smoothstep(0.1, 0.7, lDir.z));
    nightHorizonColor = mix(_HorizonColor[2], nightHorizonColor, smoothstep(0.0, 0.1, lDir.z));
    
    float posWSY = 0.5 + 0.4 * smoothstep(0.9, 1.0, lDir.z);
    float decreaCurve = (cos(9.9 * 3.1415926 * lDir.z) - 1) * 0.2;
    if(lDir.z < 0){
        decreaCurve = 0;
    }else if(lDir.z > 0.202){
        decreaCurve =  0;
    }
    posWSY += decreaCurve;
    vec3 nightMixedColor = mix(nightHorizonColor, nightSkyColor, smoothstep(0, posWSY, dir.y));

    float t  = smoothstep(0.0, 1.0, 1.0-dis);
    float t4 = t * t * t * t;
    t4 = t4 * t4;

    vec3 sunColor = vec3(1.0, 0.956, 0.7); 
    dayMixedColor  = mix(dayMixedColor, sunColor,t4);
    //nightMixedColor = nightMixedColor * (1.0 - dis);

    vec3 skyColor;
    // if(lDir.y > 0){
    //     skyColor = dayMixedColor;
    // }else{
    //     skyColor = nightMixedColor;
    // }
    skyColor = mix(nightMixedColor, dayMixedColor, smoothstep(-0.1, 0.1, lDir.y));
    //skyColor = step3(skyColor);

    // float lowAlpha = 0.4;
    // float alpha = lowAlpha + smoothstep(0.0, 0.2, -sdf) * (1 - lowAlpha);


    vec3 finalCloud = cloudColor.xyz * cloudColor.a + skyColor * (1 - cloudColor.a);
    finalCloud = cloudColor1.xyz * cloudColor1.a + finalCloud * (1-cloudColor1.a); 
    FragColor = vec4(finalCloud * 1.1, 1.0);
    //FragColor = vec4(1,1,1,1);

    if(dir.y > -0.5 && dir.y < hillY){
        //FragColor = FragColor * 0.8;
    }

    //float ds = dot(dir,normalize(vec3(-0.8,0.8,0.8)));
    //FragColor = mix(FragColor, vec4(0.9,0.2,0.2,1.0), smoothstep(0., 0.99, ds));

    //FragColor = vec4(1.0,1.0,1.0,1.0);
}