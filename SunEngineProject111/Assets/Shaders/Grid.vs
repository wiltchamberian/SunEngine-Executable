#version 430 core

uniform mat4 u_vp;
uniform mat4 u_model;
uniform vec3 u_viewPos;

uniform float u_near;

out vec4 v_color;
out vec3 v_worldPos;
out vec2 v_uv;

const int VERTEX_COUNT = 6;

void main() {
    vec2 vertices[6] = vec2[](
        vec2(-1.0, -1.0), vec2(1.0, -1.0), vec2(1.0, 1.0),
        vec2(-1.0, -1.0), vec2(1.0, 1.0), vec2(-1.0, 1.0)
    );

    v_uv = vertices[gl_VertexID] * 0.5 + 0.5;

    v_color = vec4(1, 0, 0, 0.4); // XY 
    
    gl_Position = vec4(vertices[gl_VertexID], -1.0, 1.0);
}