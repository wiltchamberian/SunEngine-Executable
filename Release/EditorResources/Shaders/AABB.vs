#version 430 core 
layout (location = 0) in vec3 aPos;

uniform mat4 u_vp;
//uniform mat4 u_model;
//uniform vec3 u_viewPos;


void main(){
    gl_Position = u_vp  * vec4(aPos, 1.0);
}
