#version 430 core

uniform mat4 u_vp;
uniform mat4 u_model;
uniform vec3 u_viewPos;

out vec4 v_color;

const int VERTEX_COUNT = 18;

void main() {
    vec3 vertices[18] = vec3[](
        // --- X-Y 
        vec3(0, 0, 0), vec3(1, 0, 0), vec3(1, 1, 0),
        vec3(0, 0, 0), vec3(1, 1, 0), vec3(0, 1, 0),

        // --- X-Z 
        vec3(0, 0, 0), vec3(0, 0, 1), vec3(1, 0, 1),
        vec3(0, 0, 0), vec3(1, 0, 1), vec3(1, 0, 0),

        // --- Y-Z 
        vec3(0, 0, 0), vec3(0, 1, 0), vec3(0, 1, 1),
        vec3(0, 0, 0), vec3(0, 1, 1), vec3(0, 0, 1)
    );

    if (gl_VertexID < 6)       v_color = vec4(1, 0, 0, 0.4); // XY 
    else if (gl_VertexID < 12) v_color = vec4(0, 1, 0, 0.4); // XZ 
    else                       v_color = vec4(0, 0, 1, 0.4); // YZ 

    // 1. get gizmo center world position
    vec3 gizmoWorldPos = u_model[3].xyz;

    // 2. camear to gizmo center distance
    float distance = length(u_viewPos - gizmoWorldPos);

    // 3. set scale factor to 0.1
    float factor = 0.1; 
    float scale = distance * factor;

    // 4. modify in local space, then to world space 
    vec3 localPos = vertices[gl_VertexID] * scale;
    vec4 worldPos = u_model * vec4(localPos, 1.0);
    
    gl_Position = u_vp * worldPos;
}