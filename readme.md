# Texture Jinn

## About

This is a project aiming to provide an intuitive node-based editor for texturing and vertex shaders.

*yawn*

### Reason

I've been wanting to make this ever since Unity removed their implementation of sbsar and replaced it with the terrible substance plugin. The substance plugin doesn't work in linux and doesn't let you edit sbsar data such as exposed variables at runtime.

The goal is for it to be a semi-viable alternative to substance designer and painter with a load of optimizations and extra features with a very good api.

### Features

#### Compute Shaders

The rendering will be done on the gpu with compute shaders where possible. The compute shaders will be compiled uniquely by the application to have the least interaction with cpu/ram as possible for better performance.

#### Animation

It will be possible to create performant animated textures capable of being used in runtime applications. I isn't a good idea to create animated textures that rely on constantly rasterising data.

Anything beyond parallel mathematical functions (e.g. Perlin noise, math, Voronoi noise) will have a noticeable impact on performance.

Nodes will be automatically marked as baked but it is possible to manually mark nodes as baked, but manually assigning a node as baked will result in a loss of animation fidelity.

#### Vertex shaders and 3D model manipulation

I plan on the program having built-in functionality with vertex shaders, tesselation and a marching cube algorithm, both with the intention making it very easy to create procedural 3D terrain in TextureJinn.

#### API

There will be a general C# api and one built optimised for use in unity 3d.

## Future use

### SRP

My plan is to build a voxelated unity srp with realtime gi and dynamically rendered LOD with TextureJinn at its heart (for rendering textures at different resolutions).

The goal of the srp is to be able to create a scene of never before seen geometry and textures at runtime but it look nearly as good as if it was baked in the unity editor whilst being as performant as if it was.

The srp will split all meshes in the scene down into constant size chunks which are then split `n` number of times, keeping the number of vertices in every level of detail the same (ish, math is hard).

This will create a 3D model split into chunks which can be swapped out for lower or higher resolution chunks (chunks that are bigger, they will still have the same (ish) number of vertices) as the camera gets further or closer to the object.

The srp will also perform ray traced gi in a dynamic resolution voxel-based system.