#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
// /*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:Toon Effects Master/TEM_Particle_Shader_Add_Blend,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:True,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33430,y:32397,varname:node_1,prsc:2|diff-149-OUT,spec-4921-OUT,normal-4935-OUT,emission-166-OUT,transm-133-OUT,lwrap-133-OUT,custl-149-OUT,voffset-140-OUT;n:type:ShaderForge.SFN_Subtract,id:18,x:31984,y:32133,varname:node_18,prsc:2|A-22-OUT,B-19-OUT;n:type:ShaderForge.SFN_Vector1,id:19,x:31757,y:32167,varname:node_19,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Abs,id:21,x:32211,y:32197,varname:node_21,prsc:2|IN-18-OUT;n:type:ShaderForge.SFN_Frac,id:22,x:31757,y:31992,varname:node_22,prsc:2|IN-24-OUT;n:type:ShaderForge.SFN_ComponentMask,id:24,x:31530,y:31923,varname:node_24,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-6017-UVOUT;n:type:ShaderForge.SFN_Multiply,id:25,x:32438,y:32333,cmnt:Triangle Wave,varname:node_25,prsc:2|A-21-OUT,B-26-OUT;n:type:ShaderForge.SFN_Vector1,id:26,x:32211,y:32365,varname:node_26,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:133,x:32665,y:32469,cmnt:Panning gradient,varname:node_133,prsc:2|VAL-25-OUT,EXP-8547-OUT;n:type:ShaderForge.SFN_NormalVector,id:139,x:32853,y:32916,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:140,x:33119,y:32787,varname:node_140,prsc:2|A-133-OUT,B-142-OUT,C-139-OUT,D-2439-OUT;n:type:ShaderForge.SFN_ValueProperty,id:142,x:32892,y:32789,ptovrint:False,ptlb:Bulge Scale,ptin:_BulgeScale,varname:_BulgeScale,prsc:2,glob:False,v1:0.2;n:type:ShaderForge.SFN_Lerp,id:149,x:33119,y:32115,varname:node_149,prsc:2|A-151-RGB,B-8608-OUT,T-133-OUT;n:type:ShaderForge.SFN_Lerp,id:150,x:32892,y:32285,varname:node_150,prsc:2|A-267-RGB,B-265-OUT,T-133-OUT;n:type:ShaderForge.SFN_Tex2d,id:151,x:32892,y:31949,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse,prsc:2,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:166,x:33119,y:32619,cmnt:Glow,varname:node_166,prsc:2|A-168-RGB,B-8677-OUT,C-133-OUT;n:type:ShaderForge.SFN_Color,id:168,x:32892,y:32453,ptovrint:False,ptlb:Glow Color,ptin:_GlowColor,varname:_GlowColor,prsc:2,glob:False,c1:1,c2:0.2391481,c3:0.1102941,c4:1;n:type:ShaderForge.SFN_Vector3,id:265,x:32665,y:32301,varname:node_265,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Tex2d,id:267,x:32665,y:32133,ptovrint:False,ptlb:Normals,ptin:_Normals,varname:_Normals,prsc:2,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector1,id:4921,x:33119,y:32283,varname:node_4921,prsc:2,v1:1;n:type:ShaderForge.SFN_Normalize,id:4935,x:33119,y:32451,varname:node_4935,prsc:2|IN-150-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8547,x:32438,y:32501,ptovrint:False,ptlb:Bulge Shape,ptin:_BulgeShape,varname:_BulgeShape,prsc:2,glob:False,v1:5;n:type:ShaderForge.SFN_Vector1,id:8608,x:32892,y:32117,varname:node_8608,prsc:2,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:8677,x:32892,y:32621,ptovrint:False,ptlb:Glow Intensity,ptin:_GlowIntensity,varname:_GlowIntensity,prsc:2,glob:False,v1:1.2;n:type:ShaderForge.SFN_Multiply,id:2439,x:33020,y:33027,varname:node_2439,prsc:2|A-139-OUT,B-5518-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5518,x:32870,y:33129,ptovrint:False,ptlb:Bulge Scale_copy,ptin:_BulgeScale_copy,varname:_BulgeScale_copy,prsc:2,glob:False,v1:1.2;n:type:ShaderForge.SFN_TexCoord,id:1235,x:31078,y:31933,varname:node_1235,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:6017,x:31292,y:31933,varname:node_6017,prsc:2,spu:0.2,spv:0.1|UVIN-1235-UVOUT,DIST-5027-T;n:type:ShaderForge.SFN_Time,id:5027,x:31068,y:32088,varname:node_5027,prsc:2;proporder:151-267-168-142-8547-8677-5518;pass:END;sub:END;*/

Shader "Toon Effects Master/Toon Water Waves Ripple Splash G ICE" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Normals ("Normals", 2D) = "bump" {}
        _GlowColor ("Glow Color", Color) = (1,0.2391481,0.1102941,1)
        _BulgeScale ("Bulge Scale", Float ) = 0.2
        _BulgeShape ("Bulge Shape", Float ) = 5
        _GlowIntensity ("Glow Intensity", Float ) = 1.2
        _BulgeScale_copy ("Bulge Scale_copy", Float ) = 1.2
               
        _Params1("Parameters 1", Vector) = (1, 1, 0.8, 0)
        _Params2("Parameters 2", Vector) = (1, 1, 0.8, 0)
                
        _WaveControl1("Waves", Vector) = (1, 0.01, 0.001, 0)
        _TimeControl1("Time", Vector) = (1, 10, 0.02, 100)
        _OceanCenter("Ocean Center", Vector) = (0, 0, 0, 0)
        
        _FlowSpeed ("Flow Speed", Float ) = 0.05
                
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="PrepassFinal"
            }         
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH_PROBE ( defined (LIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
           // #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _BulgeScale;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _GlowColor;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _BulgeShape;
            uniform float _GlowIntensity;
            uniform float _BulgeScale_copy;
            
            uniform float _FlowSpeed;
            
            //RIPLE PARAMS
              
    float4 _Params1;    
    float4 _Params2;

   //uniform float4 _LightColor0;
    
    float3 _WaveControl1;
    float4 _TimeControl1;
    float3 _OceanCenter;
    
    
    uniform float T;
	const float pi = 3.14159265;        
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 binormalDir : TEXCOORD3;
                float3 shLight : TEXCOORD4;
                float4 vPos : TEXCOORD5;
                float2 PassParam : TEXCOORD6;
                //LIGHTING_COORDS(7,8)
            };
            
            
	VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                #if SHOULD_SAMPLE_SH_PROBE
                    o.shLight = ShadeSH9(float4(mul(unity_ObjectToWorld, float4(v.normal,0)).xyz * 1.0,1)) * 0.5;
                #endif
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                
                float dist = distance(_OceanCenter, float3(_WaveControl1.x*mul(unity_ObjectToWorld, v.vertex).y,_WaveControl1.y*mul(unity_ObjectToWorld, v.vertex).x,_WaveControl1.z*mul(unity_ObjectToWorld, v.vertex).z) );
                 
                float dist2 = distance(_OceanCenter, float3(mul(unity_ObjectToWorld, v.vertex).y,mul(unity_ObjectToWorld, v.vertex).x*0.10,0.1*mul(unity_ObjectToWorld, v.vertex).z) );
                                
                o.PassParam.x = dist;
                o.PassParam.y = dist2;
                
                float node_5027 = (_Time.y*_TimeControl1.x + _TimeEditor)+dist2*_TimeControl1.y;//*sin(dist + 1.5*dist*pi);
                float node_133 = pow((abs((frac((o.uv0+node_5027*float2(0.2,0.1)).r)-0.5))*2.0),_BulgeShape); // Panning gradient, 0.2 is speed or wave !!!
               
                o.vPos = v.vertex;              
                
                v.vertex.xyz += (node_133*(_BulgeScale*sin(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params1.xy), v.vertex.xz)))*v.normal*(v.normal*_BulgeScale_copy));
				v.vertex.xyz += (node_133*(_Params2.z*_BulgeScale*cos(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params2.xy), v.vertex.xz)))*v.normal*(v.normal*_BulgeScale_copy));

				//v.vertex.xyz += (node_133*(_BulgeScale*abs(sin(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params1.xy), v.vertex.xz))))*v.normal*(v.normal*_BulgeScale_copy));
				//v.vertex.xyz += (node_133*(_Params2.z*_BulgeScale*abs(cos(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params2.xy), v.vertex.xz))))*v.normal*(v.normal*_BulgeScale_copy));

                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
    }
            
            

	fixed4 frag(VertexOutput i) : COLOR {
            
        i.normalDir = normalize(i.normalDir);
        float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
///////  Vectors:
       // float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                      
        float dist = i.PassParam.x;
        float dist2 =i.PassParam.y;
        
        float4 node_5027 = (_Time*_TimeControl1.x + _TimeEditor)+dist2*_TimeControl1.y;
        float4 node_50271 = (_Time + _TimeEditor);
                        
        float node_133 = pow((abs((frac((i.uv0+node_5027.g*float2(0.2,0.1)).r)-0.5))*2.0),_BulgeShape); // Panning gradient               
        
        //RIPPLE CODE                
       
        float2 p = i.uv0 * _Params1.xy ;
       
    	float4 t=node_50271;
		float d=length(tex2D(_Diffuse,t.xy).xyz);	
    
//     			float3 normalLocal = normalize(lerp(_Normals_var.rgb,float3(0,0,1),node_133));
//                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
//                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
//        		float NdotL = dot( normalDirection, lightDirection );
//         		float attenuation = LIGHT_ATTENUATION(i);
//                float3 attenColor = attenuation * _LightColor0.xyz;
//                float3 w = float3(node_133,node_133,node_133)*0.5; // Light wrapping
//                float3 NdotLWrap = NdotL * ( 1.0 - w );
//                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
//                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_133,node_133,node_133);
//                float3 indirectDiffuse = float3(0,0,0);
//                float3 directDiffuse = (forwardLight+backLight) * attenColor;
//                #if SHOULD_SAMPLE_SH_PROBE
//                    indirectDiffuse += i.shLight; // Per-Vertex Light Probes / Spherical harmonics
//                #endif
    
////// Emissive:
                float3 emissive = ((_GlowColor.rgb)*_GlowIntensity*node_133);
               // float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0*(3-sin(node_50271 + 1.5*i.uv0*pi)), _Diffuse)+ t.xy*0.05+d*p.xy*0.00000003 ); //t.xy defines speed, 1.000000003 defines tiling
               float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse)+ t.xy*_FlowSpeed ); //t.xy defines speed, 1.000000003 defines tiling
                
                
                  float node_8608 = 0.00005;
                //float3 node_149 = (directDiffuse + indirectDiffuse) *lerp((_Diffuse_var.rgb),float3(node_8608,node_8608,node_8608),node_133);
                float3 node_149 = lerp((_Diffuse_var.rgb),float3(node_8608,node_8608,node_8608),node_133);
                                
                float3 finalColor = emissive + node_149*_GlowColor.a;                
               
                return fixed4(finalColor,1);
                
  }
  ENDCG
}

        Pass {
            Name "ForwardBase"
            Tags {
              //  "LightMode"="ForwardBase"
            }         
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH_PROBE ( defined (LIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
           // #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _BulgeScale;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _GlowColor;
            uniform sampler2D _Normals; uniform float4 _Normals_ST;
            uniform float _BulgeShape;
            uniform float _GlowIntensity;
            uniform float _BulgeScale_copy;
            
            uniform float _FlowSpeed;
            
            //RIPLE PARAMS
              
    float4 _Params1;    
    float4 _Params2;

   //uniform float4 _LightColor0;
    
    float3 _WaveControl1;
    float4 _TimeControl1;
    float3 _OceanCenter;
    
    
    uniform float T;
	const float pi = 3.14159265;        
            
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 binormalDir : TEXCOORD3;
                float3 shLight : TEXCOORD4;
                float4 vPos : TEXCOORD5;
                float2 PassParam : TEXCOORD6;
                //LIGHTING_COORDS(7,8)
            };
            
            
	VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                #if SHOULD_SAMPLE_SH_PROBE
                    o.shLight = ShadeSH9(float4(mul(unity_ObjectToWorld, float4(v.normal,0)).xyz * 1.0,1)) * 0.5;
                #endif
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                
                float dist = distance(_OceanCenter, float3(_WaveControl1.x*mul(unity_ObjectToWorld, v.vertex).y,_WaveControl1.y*mul(unity_ObjectToWorld, v.vertex).x,_WaveControl1.z*mul(unity_ObjectToWorld, v.vertex).z) );
                 
                float dist2 = distance(_OceanCenter, float3(mul(unity_ObjectToWorld, v.vertex).y,mul(unity_ObjectToWorld, v.vertex).x*0.10,0.1*mul(unity_ObjectToWorld, v.vertex).z) );
                                
                o.PassParam.x = dist;
                o.PassParam.y = dist2;
                
                float node_5027 = (_Time.y*_TimeControl1.x + _TimeEditor)+dist2*_TimeControl1.y;//*sin(dist + 1.5*dist*pi);
                float node_133 = pow((abs((frac((o.uv0+node_5027*float2(0.2,0.1)).r)-0.5))*2.0),_BulgeShape); // Panning gradient, 0.2 is speed or wave !!!
               
                o.vPos = v.vertex;              
                
                v.vertex.xyz += (node_133*(_BulgeScale*sin(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params1.xy), v.vertex.xz)))*v.normal*(v.normal*_BulgeScale_copy));
				v.vertex.xyz += (node_133*(_Params2.z*_BulgeScale*cos(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params2.xy), v.vertex.xz)))*v.normal*(v.normal*_BulgeScale_copy));

				//v.vertex.xyz += (node_133*(_BulgeScale*abs(sin(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params1.xy), v.vertex.xz))))*v.normal*(v.normal*_BulgeScale_copy));
				//v.vertex.xyz += (node_133*(_Params2.z*_BulgeScale*abs(cos(_TimeControl1.w*dist +_TimeControl1.z*dot(normalize(_Params2.xy), v.vertex.xz))))*v.normal*(v.normal*_BulgeScale_copy));

                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
    }
            
            

	fixed4 frag(VertexOutput i) : COLOR {
            
        i.normalDir = normalize(i.normalDir);
        float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
///////  Vectors:
       // float3 _Normals_var = UnpackNormal(tex2D(_Normals,TRANSFORM_TEX(i.uv0, _Normals)));
                      
        float dist = i.PassParam.x;
        float dist2 =i.PassParam.y;
        
        float4 node_5027 = (_Time*_TimeControl1.x + _TimeEditor)+dist2*_TimeControl1.y;
        float4 node_50271 = (_Time + _TimeEditor);
                        
        float node_133 = pow((abs((frac((i.uv0+node_5027.g*float2(0.2,0.1)).r)-0.5))*2.0),_BulgeShape); // Panning gradient               
        
        //RIPPLE CODE                
       
        float2 p = i.uv0 * _Params1.xy ;
       
    	float4 t=node_50271;
		float d=length(tex2D(_Diffuse,t.xy).xyz);	
    
//     			float3 normalLocal = normalize(lerp(_Normals_var.rgb,float3(0,0,1),node_133));
//                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
//                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
//        		float NdotL = dot( normalDirection, lightDirection );
//         		float attenuation = LIGHT_ATTENUATION(i);
//                float3 attenColor = attenuation * _LightColor0.xyz;
//                float3 w = float3(node_133,node_133,node_133)*0.5; // Light wrapping
//                float3 NdotLWrap = NdotL * ( 1.0 - w );
//                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
//                float3 backLight = max(float3(0.0,0.0,0.0), -NdotLWrap + w ) * float3(node_133,node_133,node_133);
//                float3 indirectDiffuse = float3(0,0,0);
//                float3 directDiffuse = (forwardLight+backLight) * attenColor;
//                #if SHOULD_SAMPLE_SH_PROBE
//                    indirectDiffuse += i.shLight; // Per-Vertex Light Probes / Spherical harmonics
//                #endif
    
////// Emissive:
                float3 emissive = ((_GlowColor.rgb)*_GlowIntensity*node_133);
               // float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0*(3-sin(node_50271 + 1.5*i.uv0*pi)), _Diffuse)+ t.xy*0.05+d*p.xy*0.00000003 ); //t.xy defines speed, 1.000000003 defines tiling
               float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse)+ t.xy*_FlowSpeed ); //t.xy defines speed, 1.000000003 defines tiling
                
                
                  float node_8608 = 0.00005;
                //float3 node_149 = (directDiffuse + indirectDiffuse) *lerp((_Diffuse_var.rgb),float3(node_8608,node_8608,node_8608),node_133);
                float3 node_149 = lerp((_Diffuse_var.rgb),float3(node_8608,node_8608,node_8608),node_133);
                                
                float3 finalColor = emissive + node_149*_GlowColor.a;                
               
                return fixed4(finalColor,1);
                
  }
  ENDCG
}
        
    }
    FallBack "Toon Effects Master/TEM_Particle_Shader_Add_Blend"
    CustomEditor "ShaderForgeMaterialInspector"
}
