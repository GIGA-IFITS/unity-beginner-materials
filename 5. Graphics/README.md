# Graphics Fundamentals
An introduction to the building blocks Unity uses to turn a scene into an image.<br>

## 1. The Rendering Pipeline
Every rendered frame follows the same high-level flow:

```text
Scene objects -> Camera visibility -> Materials and lighting -> Screen image
```

- **Mesh or Sprite** defines the visible shape.
- **Material** controls how the surface looks.
- **Shader** contains the rules used to calculate the surface color.
- **Light** provides illumination for lit shaders.
- **Camera** decides which parts of the scene are drawn.

Changing one part of this flow can affect both the appearance and performance of the final image.

## 2. Meshes, Sprites, and Renderers
A GameObject needs a renderer component before Unity can draw it.

| Content | Visual asset | Common renderer |
|---|---|---|
| 2D | Sprite | Sprite Renderer |
| 3D | Mesh | Mesh Renderer and Mesh Filter |
| UI | Sprite or texture | Image or Raw Image |

The asset contains the visual data, while the renderer controls how that data appears in the scene. Disabling a renderer hides the object without disabling its scripts or physics components.

## 3. Materials and Shaders
A **shader** is a program that tells the GPU how to draw a surface. A **material** stores a shader and the values supplied to it, such as color, texture, metallic strength, or transparency.

In a typical Universal Render Pipeline (URP) project:

- Use **Universal Render Pipeline/Lit** when the object should react to lights.
- Use **Universal Render Pipeline/Unlit** for a flat appearance that ignores lights.
- Create a material through **Assets -> Create -> Material**.
- Assign it by dragging the material onto a renderer or into the renderer's Materials list.

> Reusing one material across multiple objects reduces duplicated assets and keeps their appearance consistent.

## 4. Textures
A **texture** is an image read by a shader. The same texture file can be imported differently depending on its purpose.

| Import setting | Purpose |
|---|---|
| Texture Type | Identifies whether the image is a default texture, sprite, normal map, or another supported type |
| Max Size | Limits the largest imported resolution |
| Compression | Trades image quality for a smaller file and lower memory use |
| Filter Mode | Controls how the image is sampled when enlarged or reduced |
| Wrap Mode | Controls what happens when texture coordinates go outside the 0-1 range |

Use **Point** filtering for crisp pixel art. Use **Bilinear** filtering for smooth scaling in most other cases.

## 5. Lighting Basics
Lit materials need light sources. The most common 3D light types are:

| Light | Best used for |
|---|---|
| Directional | Sunlight affecting the whole scene |
| Point | Bulbs, lamps, and other lights shining in every direction |
| Spot | Flashlights and cone-shaped light sources |
| Area | Soft rectangular light in baked lighting workflows |

The **Intensity**, **Color**, and **Range** settings have the largest immediate effect. Start with one main light, then add supporting lights only when the scene needs them.

## 6. Cameras and Visibility
The Camera component converts the scene into the image shown to the player.

- **Perspective** makes distant objects appear smaller and is common in 3D games.
- **Orthographic** removes perspective distortion and is common in 2D and isometric games.
- **Clipping Planes** set the nearest and farthest distances the camera renders.
- **Culling Mask** selects which layers the camera can see.
- **Viewport Rect** chooses the part of the screen occupied by the camera.

Keep the far clipping plane no larger than necessary. Rendering a large visible range can make it harder to avoid drawing objects the player cannot meaningfully see.

## 7. Transparency and Draw Order
Transparent objects need extra care because Unity often has to blend them with objects already rendered behind them.

For 2D content, control overlap with:

1. **Sorting Layer** for broad groups such as Background, Gameplay, and Foreground.
2. **Order in Layer** for ordering objects inside the same Sorting Layer.

For 3D content, prefer opaque materials when possible. Transparency can create sorting artifacts and usually costs more to render than an opaque surface.

## 8. Graphics Performance Habits
Good graphics are not only visually appealing; they also fit the target hardware.

- Reuse materials instead of creating a unique material for every object.
- Import textures close to the resolution actually shown on screen.
- Avoid unnecessary real-time lights and shadows.
- Keep transparent layers and overdraw under control.
- Use the **Game View Stats**, **Frame Debugger**, and **Profiler** to measure before optimizing.

## Practice
Complete the [Material Comparison Exercise](PRACTICE.md) to apply the chapter concepts in a small URP scene.

## i. Additional References
- [Unity 6 Manual: Graphics](https://docs.unity3d.com/6000.0/Documentation/Manual/Graphics.html)
- [Unity 6 Manual: Materials](https://docs.unity3d.com/6000.0/Documentation/Manual/Materials.html)
- [Unity 6 Manual: Textures](https://docs.unity3d.com/6000.0/Documentation/Manual/Textures.html)
- [Unity 6 Manual: Cameras](https://docs.unity3d.com/6000.0/Documentation/Manual/CamerasOverview.html)
