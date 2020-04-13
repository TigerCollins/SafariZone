Thank you for purchasing the Object Fader Tool. 

General setup:

1. Drag and drop the FadeObjects script on the casting object (usually a camera)
2. Drag and drop a GameObject in the player slot. This is where the cast will be going towards.
3. Tag the (parent) object with the "CanFade" tag to make it fadeable.

FadeObjects options:

- Cast Type
	Sets the casting type to Raycast, Spherecast, or Capsulecast. If don't have many small objects that need to be faded at the same time, raycast
	is probably fine. Spherecast and capsulecast are more expensive, but it allows you to fade many obstructing objects simultaniously.
- Player
	This is the object that the cast will be sent towards.
- Radius
	Sets the radius of the sphere or capsule.
- Capsule height
	Sets the height of the capsule.
- Max transparency
	Sets to what value the transparency will be set.
- Fade speed
	Sets how fast objects will fade in/out.
- Depth tolerance
	Sets the minimum distance difference between player-camera and object-camera. When setting it to 2 for instance, only objects that are at least
	2 meters closer than the player will be faded.

FadeObjectOverride options:

Alpha Property Name
	This is the name of the color property that the FadeObjects script will look for in the shader. The alpha channel of that color property will be
	changed. The rgb channels remain untouched.
Fade Shader
	You can drag a fade shader in here if you like. When it's going to fade, it will create a material based on this shader. If you leave it empty
	it will create a material based on the shader it currently has and assumes that you have the _Mode, _SrcBlend, _DstBlend, and _ZWrite properties
	in the shader.

Here are some common scenerios and how to solve them:

1. Standard materials
For standard materials you don't have to do anything special. Simply drag the FadeObjects script onto the camera and you're good to go.

2. Multiple materials in one mesh
This is also handled automatically.

3. Triggers
Sometimes you want the trigger to be faded while sometimes you don't want this. Use the Include Triggers checkbox to pick your preference.

4. Groups of objects
If you have a group of objects, a single raycast might not be enough. Try it with the capsule or sphere casters and see if that helps.

5. Custom shaders
To make this work with a custom shaders you need a few things. First, the shaders needs to have a Color property that is multiplied with the final color.
Usually in the first pass. Type the name of this property in the Alpha Property Name of the Fade Object Override component.
You can prepare a fade version of your custom shader and drag that into the Fade Shader slot to have it work automatically.
You can also edit the existing shader by including the following lines/properties:

[HideInInspector] _Mode("__mode", Float) = 0.0
		
[HideInInspector] _SrcBlend("__src", Float) = 1.0
		
[HideInInspector] _DstBlend("__dst", Float) = 0.0
		
[HideInInspector] _ZWrite("__zw", Float) = 1.0

Blend[_SrcBlend][_DstBlend]
			
ZWrite[_ZWrite]