# Texture Jinn

A node-based procedural texture editor to be built using the same framework I will build the game engine in.

Performant 'textures' above all else. This will be deeply integrated into my game engine.

## Reason

I've been wanting to make this ever since Unity removed their implementation of sbsar and replaced it with the terrible substance plugin. The substance plugin doesn't work in linux and doesn't let you edit sbsar data such as exposed variables at runtime.

The goal is for it to be a semi-viable alternative to substance designer and painter with a load of optimisations and extra features with a very good api.

I no longer want to optimise this directly for Unity. I would much rather have my own very open game engine and build it on the same Vulkan framework as that.

I suppose I'll create a Unity library when I've got it started.

## Features

### Compute Shaders

Packed full of them. I will build this thinking about how I can minimise interactions between CPU and RAM and the GPU. Perhaps that will involve compiling 'textures' into a single compute shader to be rendered at once. This would definitely make the 'texture' render as fast as possible but could have implications with stuttering.

### Animation

The whole idea is a 'texture' which is performantly rendered so with lighter 'textures' you could expect to render it at a high framerate.

### API

To integrate it into other projects. This is iffy at best.
