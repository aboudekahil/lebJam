shader_type canvas_item;

uniform float radius = 0.2;
uniform vec3 inColor: source_color = vec3(0.2, 0.5, 0.1);
uniform vec3 inColor2: source_color = vec3(0.2, 0.5, 0.1);

float sdCircle( vec2 p, float r ) {
    return length(p) - r;
}

void fragment() {
	vec2 uv = UV - vec2(0.5);
	float d = sdCircle(uv, radius); 
	if (d < -0.01) {
		COLOR.rgb = inColor;
		COLOR.a = 0.7;
	} else if (d < 0.0) {
		COLOR.rgb = inColor2;
		COLOR.a = 1.0;
	} else {
		COLOR.a = 0.0;
	}
}