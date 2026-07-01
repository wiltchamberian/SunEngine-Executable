#version 430 core 
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec4 aColor;
layout (location = 3) in vec2 aTexCoord;

uniform mat4 u_vp;
uniform mat4 u_model;

out vec3 v_worldPos;
out vec3 v_normal;
out vec4 v_color;
out vec2 v_uv;

void main(){
    vec4 worldPos = u_model * vec4(aPos, 1.0);
    v_worldPos = worldPos.xyz;
    
    v_normal = mat3(transpose(inverse(u_model))) * aNormal;
    
    v_color = aColor;
    v_uv = aTexCoord.xy;
    
    gl_Position = u_vp * worldPos;
}