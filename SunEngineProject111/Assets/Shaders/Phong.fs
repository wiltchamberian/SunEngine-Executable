#version 430 core 

out vec4 FragColor;

in vec3 v_worldPos;
in vec3 v_normal;
in vec4 v_color;
in vec2 v_uv;

uniform sampler2D u_MainTex;
uniform vec3 u_viewPos;

uniform vec3 u_lightDir; 
uniform vec3 u_lightColor;

//[Property: Range(0.0,1.0)]
uniform float u_ambientStrength = 1.0;
uniform float u_specularStrength = 0.5;
uniform float u_shininess = 32.0;

float cartoon(float c){
	return (1+c)*(1+c)*0.5;
}

float step4(float c){
    float s = 1.0/4.0;
    return step(0.0, c) * s + step( s, c)*s + step(2.0*s ,c ) * s + step(3.0*s ,c ) * s;
}

vec3 step4(vec3 c){
    return vec3(step4(c.r), step4(c.g), step4(c.b));
}

float fresnel(vec3 normal, vec3 view)
{
	return 1.0 - clamp(dot(normal, view), 0.0, 1.0);
}

void main(){
    vec3 texColor = texture(u_MainTex, v_uv).rgb;
    vec3 norm = normalize(v_normal);

    // 1. ambient
    vec3 ambient = u_ambientStrength * u_lightColor;

    // 2. diffuse
    vec3 lightDir = normalize(u_lightDir); 

    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * u_lightColor;

    // 3.(Blinn-Phong )
    vec3 viewDir = normalize(u_viewPos - v_worldPos);
    vec3 halfwayDir = normalize(lightDir + viewDir); 
    float spec = pow(max(dot(norm, halfwayDir), 0.0), u_shininess);
    vec3 specular = u_specularStrength * spec * u_lightColor;


    // 4. composite
    vec3 result = (ambient + diffuse + specular) * texColor;


    FragColor = vec4(result, 1.0);
}