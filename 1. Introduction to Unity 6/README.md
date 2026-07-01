# Introduction to Unity 6
A quick tour of the Unity Editor and its core concepts.<br>

## 1. The Unity Editor Layout
When you open a project, you'll see the Unity Editor made up of several panels.

![Unity Editor Layout](resources/Editor%20layout.png)

- **Toolbar** | Play/Pause/Step buttons, layout presets, and your account
- **Scene View** | where you build and arrange your game world
- **Game View** | a preview of what the player sees through the camera
- **Hierarchy** | a list of every GameObject in the current scene
- **Inspector** | shows the properties of whatever you have selected
- **Project** | your project's asset files (scripts, sprites, sounds, etc.)
- **Console** | displays errors, warnings, and log messages

> ⚠ If your layout looks different, you can reset it via **Window → Layouts → Default** ⚠

## 2. Scene Navigation
Use these controls to move around in the Scene View:

| Action | Control |
|---|---|
| Pan | Middle Mouse Drag |
| Zoom | Scroll Wheel |
| Focus on object | Select it, then press **F** |

![Basic Scene Navigation](resources/Basic%20Navigation.gif)

## 3. GameObjects
A **GameObject** is the base for everything in a scene. This includes characters, lights, cameras, and even empty markers. When it is on its own, it does nothing. It's what you attach to it that gives it a purpose/function.

- To create one, **right-click in the Hierarchy → Create Empty**
- There is also a shortcut to create empty GameObjects: **CTRL + SHIFT + N**

![Creating an Empty GameObject](resources/Create%20Empty%20Game%20Object.gif)

Common built-in types:

| Type | Description |
|---|---|
| Empty | A GameObject with no visuals, used to group or anchor things |
| Sprite | A 2D image, the most common building block in a 2D game |
| Camera | Renders the scene to the screen |
| Light 2D | Illuminates the scene in 2D |

Every **GameObject** has its own **Components** list, with at the very least a **Transform** component. 

![GameObject Transform Component](resources/GameObject%20-%20Transform.png)

> ⚠ **Remember!** Everything in a scene is just a GameObject with various differing components ⚠

## 4. Components
A **Component** adds behavior or properties to a GameObject. You stack components to define what an object is and what it does. As is mentioned before, every GameObject at the very list has a **Transform** component, which stores the object's position, rotation, and scale and cannot be removed.

- Click a GameObject in the Hierarchy to select it, then view its components in the **Inspector**
- To add a component: scroll to the bottom of the Inspector and click **Add Component**

![Adding a Component](resources/GameObject%20-%20Add%20Component.png)

Common components in a 2D project:

| Component | What it does |
|---|---|
| **Sprite Renderer** | Displays a 2D image on a GameObject |
| **Rigidbody 2D** | Makes a GameObject react to physics (gravity, forces) |
| **Box Collider 2D** | Adds a rectangular collision boundary |
| **Circle Collider 2D** | Adds a circular collision boundary |
| **Animator** | Controls animations on a GameObject |
| **Audio Source** | Plays a sound from this GameObject's position |
| **Camera** | Renders what it sees to the screen |

## 5. Play Mode
Play Mode lets you test your game directly in the editor.

![Play Pause Step Buttons](resources/Play%20-%20Pause%20-%20Step.png)

- **Play** | starts the game
- **Pause** | freezes the game mid-run
- **Step** | advances one frame at a time while paused

> ⚠ Any changes you make while in Play Mode are **not saved**. Always exit Play Mode before editing. ⚠

## i. Additional References
- [Unity 6 Manual](https://docs.unity3d.com/6000.0/Documentation/Manual/)
- [Unity Learn: Unity Essentials](https://learn.unity.com/pathway/unity-essentials)
