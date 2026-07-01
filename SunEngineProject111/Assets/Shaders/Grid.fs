#version 430 core

uniform mat4 u_vp;
uniform vec3 u_viewPos;

uniform int axis = 0; // 0: yz plane, 1: xz plane, 2: xy plane
out vec4 FragColor;

in vec4 v_color;
in vec3 v_worldPos;
in vec2 v_uv;

float computeLineAlpha(int axis, vec3 dir){

    float t = -u_viewPos[axis] / dir[axis];
    if( t< 0){
        return 0.0;
    }
    vec3 pos = u_viewPos + dir * t;
    vec2 coord;
    //expect compiler optimization
    if (axis == 0) {
        coord = pos.yz;
    } else if (axis == 1) {
        coord = pos.xz;
    } else {
        coord = pos.xy;
    }
    vec2 grid = abs(fract(coord - 0.5) - 0.5) / fwidth(coord);
    float line = min(grid.x, grid.y);

    float lineAlpha = 1 - min(line, 1.0);
    float dist =abs(t);
    float fade = exp(-dist * 0.03);
    float alpha = lineAlpha * fade;
    return alpha;
}


void main()
{
    vec4 ndc = vec4(v_uv * 2 - 1, -1 , 1);
    mat4 inv = inverse(u_vp);
    ndc = inv * ndc;
    ndc.xyz /= ndc.w;

    vec3 dir = normalize(ndc.xyz - u_viewPos);

    // if(abs(dir.y) < 0.0001){
    //     discard;
    // }
    // y=0 plane
    float t = -u_viewPos.y / dir.y;
    // if(t < 0.0){
    //     float t2 = -u_viewPos.z / dir.z;
    //     vec3 pos2 = u_viewPos + dir * t2;
    //     vec2 coord2 = pos2.xy;
    //     vec2 grid3 = abs(coord2)/fwidth(coord2);
    //     vec3 green= grid3.x < 1.0? vec3(0,1,0):vec3(1);
    //     float alpha = grid3.x < 1.0 ? 1.0 : 0.0;
    //     FragColor = vec4(vec3(1.0) * green, alpha);
    //     return;
    // }
    float t2 = -u_viewPos.z / dir.z;
    vec3 pos2 = u_viewPos + dir * t2;
    vec2 coord2 = pos2.xy;
    vec2 grid3 = abs(coord2)/fwidth(coord2);

    vec3 pos = u_viewPos + dir * t;
    //y=0 plane
    vec2 coord = pos.xz ;

    //vec2 grid = abs(fract(coord - 0.5) - 0.5) / fwidth(coord);
    //float line = min(grid.x, grid.y);

    //float lineAlpha = 1 - min(line, 1.0);
    // float dist =abs(t);
    // float fade = exp(-dist * 0.03);
    // float alpha = lineAlpha * fade;
    float alpha = computeLineAlpha(axis ,dir);


    vec2 grid2 = abs(coord)/fwidth(coord);

    

    

    vec3 red = grid2.y < 1.0 ? vec3(1, 0, 0) : vec3(1);
    vec3 blue = grid2.x < 1.0 ? vec3(0,0,1):vec3(1);
    vec3 mulColor = red * blue;
    vec3 green= grid3.x < 1.0? vec3(0,1,0):vec3(1);
    mulColor *= green;
    alpha = grid3.x < 1.0 ? 1.0 : alpha;
    FragColor = vec4(vec3(1.0) * mulColor, alpha);
    


}