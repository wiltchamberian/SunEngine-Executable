#version 430 core

uniform mat4 u_vp;
uniform mat4 u_model;
uniform vec3 u_viewPos;

out vec2 v_uv;

void main()
{
    //a quad that covers the ndc screen, with vertex id 0,1,2
    vec2 pos[3] = vec2[](
        vec2(-1.0, -1.0),
        vec2( 3.0, -1.0),
        vec2(-1.0,  3.0)
    );

    v_uv = pos[gl_VertexID] * 0.5 + 0.5;
    gl_Position = vec4(pos[gl_VertexID], 0, 1.0);
}