# Basic Scripting
An introduction to writing C# scripts in Unity, finishing with a mini platformer project.<br>

## 1. Creating & Attaching a Script
- In the Project panel: **right-click → Create → C# Script**
- Give it a name. The file name and class name must match

![Project panel showing a newly created C# script asset](resources/Project%20Panel%20-%20New%20Script.png)

To attach it to a GameObject, either:
- Drag the script from the Project panel onto a GameObject in the Hierarchy, or
- Select the GameObject → **Add Component** → search for the script name

> ⚠ Renaming a script after creation requires renaming the class inside the file too ⚠

## 2. MonoBehaviour & the Script Lifecycle
Every Unity script inherits from **MonoBehaviour**, which connects it to Unity's game loop and gives it access to lifecycle methods.

```csharp
using UnityEngine;

public class MyScript : MonoBehaviour
{
    void Start()
    {
        // Called once when the GameObject is first enabled
    }

    void Update()
    {
        // Called every frame
    }
}
```

- **`Start()`** runs once at the beginning. Use it to initialize values.
- **`Update()`** runs every frame. Use it for input, movement, and game logic.

Other commonly used MonoBehaviour lifecycle methods are listed below. Unity calls these methods automatically when the matching event occurs. They are not normally written with the `override` keyword.

| Method | When Unity calls it | Common use |
|---|---|---|
| `Awake()` | When the script instance is loaded, before `Start()` | Get component references and initialize the script's own data |
| `OnEnable()` | Every time the component and its GameObject become enabled | Enable Input Actions, subscribe to events, or restart temporary systems |
| `FixedUpdate()` | At fixed physics intervals, which may occur zero, one, or several times per rendered frame | Apply Rigidbody movement, forces, and other physics-related logic |
| `LateUpdate()` | Once per frame after all regular `Update()` calls have finished | Camera following and logic that depends on another object having already moved |
| `OnDisable()` | Every time the component or its GameObject becomes disabled | Disable Input Actions, unsubscribe from events, and stop temporary systems |
| `OnDestroy()` | Before the component or GameObject is destroyed | Perform final cleanup for resources owned by the script |
| `Reset()` | In the Editor when the component is first added or reset from the Inspector | Give Inspector fields useful default values |
| `OnValidate()` | In the Editor when the script is loaded or an Inspector value changes | Clamp values and validate Inspector configuration |

A simplified execution order is:

```text
Awake()
  ↓
OnEnable()
  ↓
Start()
  ↓
FixedUpdate() / Update() / LateUpdate()
  ↓
OnDisable()
  ↓
OnDestroy()
```

`OnEnable()` and `OnDisable()` can run multiple times because a GameObject may be activated and deactivated repeatedly. `Awake()` and `Start()` normally run only once for each script instance.

> `FixedUpdate()` is intended for physics logic, while input is usually read in `Update()` so that short button presses are not missed.

## 3. Variables & the Inspector
A **variable** stores a value your script can read and change.

```csharp
public class MyScript : MonoBehaviour
{
    public int score = 0;
    public float speed = 5f;
    private bool isAlive = true;

    [SerializeField] private float jumpForce = 10f;
}
```

Common types:

| Type | Example | Use for |
|---|---|---|
| `int` | `0`, `10`, `-3` | Whole numbers (score, lives) |
| `float` | `5.5f`, `0.1f` | Decimal numbers (speed, time) |
| `bool` | `true`, `false` | On/off states |
| `string` | `"Player"` | Text |

### Access Modifiers: `public`, `private`, and `protected`

`public`, `private`, and `protected` are called **access modifiers**. They determine which parts of the program are allowed to access a variable or method.

| Access modifier | Same class | Child class | Other scripts |
|---|---:|---:|---:|
| `private` | Yes | No | No |
| `protected` | Yes | Yes | No |
| `public` | Yes | Yes | Yes |

#### `public`

A `public` variable or method can be accessed by other scripts.

```csharp
public class Player : MonoBehaviour
{
    public int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
    }
}
```

Another script can access it through a reference to the `Player` component:

```csharp
player.AddScore(10);
Debug.Log(player.score);
```

In Unity, a supported serialized `public` field is also shown in the Inspector. However, a field should not be made `public` only because you want to edit it in the Inspector. Making it `public` also allows other scripts to change it directly.

Use `public` for members that are intentionally part of the class's external interface, such as methods that other scripts are expected to call.

#### `private`

A `private` variable or method can only be accessed from inside the class where it is declared.

```csharp
public class Player : MonoBehaviour
{
    private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
```

Another script cannot directly write `player.health`. It must use the `TakeDamage()` method instead. This lets the class control how its internal data is changed.

Use `private` for internal data and helper methods that other scripts do not need to access. As a general rule, start with `private` and only increase the access level when necessary.

#### `protected`

A `protected` variable or method can be accessed inside its own class and inside classes that inherit from it, but not by unrelated scripts.

```csharp
public class Character : MonoBehaviour
{
    protected int health = 100;
}

public class Enemy : Character
{
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
```

Here, `Enemy` can access `health` because it inherits from `Character`. A separate script cannot access it directly.

Use `protected` when a member should remain hidden from unrelated scripts but still be available to child classes.

#### `[SerializeField] private`

By default, a `private` field is not shown in the Inspector. Adding **`[SerializeField]`** keeps the field private to other scripts while allowing its value to be edited in the Inspector.

```csharp
[SerializeField] private float jumpForce = 10f;
```

This is usually preferred over `public` when the value only needs to be configured through the Inspector.

A useful rule is:

1. Use `private` for internal data.
2. Use `[SerializeField] private` when the Inspector needs to edit that data.
3. Use `protected` when child classes need access.
4. Use `public` when other scripts are intentionally allowed to use it.

![Inspector showing public and SerializeField variables on a GameObject](resources/Inspector%20-%20MyScript%20Public%20%26%20Serialize.png)

## 4. Reading Input
Unity's Input System reads player input from keyboard, mouse, or gamepad. Add `using UnityEngine.InputSystem;` at the top of any script that uses it.

Use `.isPressed` for held input (like movement) and `.wasPressedThisFrame` for one-shot input (like jumping).

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class MyScript : MonoBehaviour
{
    void Update()
    {
        float horizontal = 0f;
        if (Keyboard.current.dKey.isPressed) horizontal = 1f;
        if (Keyboard.current.aKey.isPressed) horizontal = -1f;

        bool jumped = Keyboard.current.spaceKey.wasPressedThisFrame;

        Debug.Log("Moving: " + horizontal);
    }
}
```

- `Debug.Log()` prints a message to the Console. Useful for checking values without running the full game

![Console panel showing a Debug.Log output](resources/Console%20-%20Moving%20Logs.png)

### Using an Input Action Map
The previous example reads the keyboard directly. This is called **direct device polling** because the script asks a specific device, such as the keyboard, whether a specific key is being pressed.

Unity's Input System can also separate the **game action** from the physical key that triggers it. Instead of asking, *“Is the D key pressed?”*, the script asks, *“What is the value of the Move action?”*

This uses an **Input Action Asset** (`.inputactions`). Inside the asset, actions are grouped into an **Action Map**.

Example structure:

```text
PlayerControls.inputactions
└── Player          ← Action Map
    ├── Move        ← Action
    └── Jump        ← Action
```

To create it:
1. In the Project panel, **right-click → Create → Input Actions**
2. Name it `PlayerControls`
3. Double-click the asset to open the Input Actions editor
4. Create an Action Map named `Player`
5. Create an action named `Move`
6. Set **Action Type** to `Value` and **Control Type** to `Vector2`
7. Add a **2D Vector Composite** binding
8. Assign `W`, `A`, `S`, and `D` to its Up, Left, Down, and Right bindings

![Input Actions editor showing Player/Move with a WASD 2D Vector Composite](resources/Input%20Map%20Config.gif)

The `Move` action returns a `Vector2`:

```text
(-1,  0) = left
( 1,  0) = right
( 0,  1) = up
( 0, -1) = down
```

For a 2D platformer, only the `x` value is needed for horizontal movement.

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveInput.x * speed,
            rb.linearVelocity.y
        );
    }
}
```

After attaching the script:
1. Select the Player GameObject
2. Find the **Move Action** field in the Inspector
3. Drag the `Move` action from `PlayerControls.inputactions` into that field

![Inspector showing Player/Move assigned to the Move Action field](resources/Inspector%20-%20Player%20Action%20Assigned%20to%20Script.png)

The important difference is:

| Direct device polling | Input Action |
|---|---|
| Reads a physical key directly | Reads a game action |
| `Keyboard.current.dKey` | `moveAction.action.ReadValue<Vector2>()` |
| Fast to set up | Requires an Input Action Asset |
| Usually tied to one device | Can support keyboard, gamepad, and rebinding |

Both methods use Unity's **new Input System**. The difference is only how the input is organized and read.

## 5. Moving a GameObject
There are two ways to move a GameObject with a script. But, before we go into that, it is important to understand what **`Time.deltaTime`** is.

### Time.deltaTime
`Time.deltaTime` is basically how much time has passed since the last frame.

`Update()` runs every frame, but frames don't take the same amount of time. For example, on a fast device you might get 120 frames per second, but on a slow one, you only get 30. If you try to move an object `5f` units per frame (using the Update() function), it would move four times faster on the fast machine.

To handle this, we can multiply the speed of said object by `Time.deltaTime`, converting *"move x units per frame"* into *"move x units per second"*. This would make your game behave consistently on any hardware.

> ⚠ Always multiply per-frame values by `Time.deltaTime`. Forgetting it is one of the most common beginner bugs. ⚠

Now that you know what **`Time.deltaTime`** is, let's go into the different ways of moving a GameObject.

### Via Transform
Directly changes the object's position. This implementation is on the simpler side, but it ignores physics.

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class MyScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        float horizontal = 0f;
        if (Keyboard.current.dKey.isPressed) horizontal = 1f;
        if (Keyboard.current.aKey.isPressed) horizontal = -1f;

        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
    }
}
```

### Via Rigidbody2D
Moves the object through physics. Use this when the object has a Rigidbody2D.

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class MyScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = 0f;
        if (Keyboard.current.dKey.isPressed) horizontal = 1f;
        if (Keyboard.current.aKey.isPressed) horizontal = -1f;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
}
```

> `linearVelocity` does not need `Time.deltaTime`. This is one of Unity's main Quality-of-Life features, where velocity is already measured in units per second, and the physics engine handles the rest.

- **`GetComponent<T>()`** retrieves a component attached to the same GameObject. Used very often in Unity scripting.

**When to use which:**

| | Transform | Rigidbody2D |
|---|---|---|
| Follows physics | No | Yes |
| Works with colliders | Poorly | Yes |
| Best for | UI, cameras, simple objects | Characters, projectiles |

## 6. Collision Detection
Unity calls these methods automatically when collisions happen, you just need to define what they do.

```csharp
// Called when this object physically touches another collider
void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collided with: " + collision.gameObject.name);
}

// Called when this object enters a trigger collider
void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Triggered by: " + other.gameObject.name);
}
```

**Collision vs Trigger:**
- A normal **Collider 2D** blocks other objects physically
- A **Trigger** (check **Is Trigger** on the Collider component) lets objects pass through and fires `OnTriggerEnter2D` instead

![Inspector showing the Is Trigger checkbox on a Collider 2D](resources/Inspector%20-%20IsTrigger.png)

To check what you collided with, use **tags**:

```csharp
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("Hit an enemy!");
    }
}
```

## 7. Inheritance & Default Functions
A class can **inherit** from another class using `: BaseClassName`. The class being inherited from is the **base class** (or parent class), and the one inheriting is the **derived class** (or child class). A derived class automatically gets all the base class's methods and variables.

Marking a base class method **`virtual`** gives it a **default implementation**: a body that runs unless a derived class chooses to replace it.

```csharp
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health = 100;

    // any Enemy that doesn't override this just uses it as-is
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + health);
    }
}
```

A derived class can then either leave a `virtual` method alone and inherit the default, or use `override` to replace it with its own version.

```csharp
// uses Enemy's default TakeDamage() untouched
public class BasicEnemy : Enemy
{
}

// replaces the default entirely
public class ArmoredEnemy : Enemy
{
    public override void TakeDamage(int amount)
    {
        int reduced = amount / 2;
        health -= reduced;
        Debug.Log(gameObject.name + " absorbed damage with armor. Took " + reduced + " instead.");
    }
}
```

Rewriting the whole method works, but if you only want to add behavior on top of the default instead of replacing it, call **`base.MethodName()`** from inside the override to run the base class's version first:

```csharp
public class ArmoredEnemy : Enemy
{
    public override void TakeDamage(int amount)
    {
        int reduced = amount / 2;
        base.TakeDamage(reduced); // runs Enemy's default TakeDamage() with the reduced amount
        Debug.Log("Armor absorbed half the damage!");
    }
}
```

This is what is called Object-Oriented Programming (OOP). Instead of writing a separate, full script for every enemy type, we group the shared data and behavior (like `health` and a default `TakeDamage()`) into one base class, and derived classes only write the parts that are actually different. It keeps our code shorter and avoids repeating the same logic in every script.

| Keyword | Goes on | Meaning |
|---|---|---|
| `virtual` | Base class method | Provides a default implementation; can be overridden |
| `override` | Derived class method | Replaces the default with new behavior |
| `base.MethodName()` | Inside an override | Calls the base class's version of that method |

> ⚠ Only `virtual` (or `abstract`) methods can be overridden. Trying to `override` a normal method is a compile error. ⚠

---

## Mini Project: Platformer

Putting it all together, we'll make a small platformer with a moving player and three platform types.

![Final result, player sprite on platforms of different colors](resources/Mini%20Project%20-%20Initial%20Showcase.gif)

### Step 1: Scene Setup
1. Create an **Empty GameObject** named `Player`. Add a **Sprite Renderer**, a **Rigidbody2D** (Body Type: Dynamic), and a **Box Collider 2D**. Tag it `Player`.
2. Create three platform sprites and tag each one `Ground`.
3. Set the Camera projection to **Orthographic** if it isn't already.

![Hierarchy showing Player and platform GameObjects](resources/Mini%20Project%20-%20Scene%20Layout.png)

### Step 2: Player Movement
Create a script called `PlayerController` and attach it to the Player.

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = 0f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal = 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal = -1f;

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            Platform platform = collision.gameObject.GetComponent<Platform>();
            if (platform != null)
            {
                platform.OnPlayerLand(rb);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
```

> `GetComponent<Platform>()` works on all three platform types because they all inherit from `Platform`.

### Step 3: The Base Platform Class
Create a script called `Platform`. This is the **base class** all platform types will inherit from.

```csharp
using UnityEngine;

public class Platform : MonoBehaviour
{
    public virtual void OnPlayerLand(Rigidbody2D playerRb)
    {
        // default behavior: do nothing extra
    }
}
```

Key OOP concepts here:
- **Class**: `Platform` is a class, a blueprint for platform behavior
- **`virtual`**: marks a method as overridable by child classes; here the default (see section 7) is just "do nothing extra"
- **Instance**: each platform GameObject in the scene is an instance of a Platform class

### Step 4: Platform Types
Create a script for each platform type. Each one **inherits** from `Platform`. Like we saw in section 7, a class can leave a `virtual` method's default alone or override it. `NormalPlatform` does the former, `BouncyPlatform` and `MovingPlatform` do the latter.

**NormalPlatform.cs**: inherits base behavior, nothing extra needed
```csharp
using UnityEngine;

public class NormalPlatform : Platform
{
    // inherits OnPlayerLand() from Platform, no changes
}
```

**BouncyPlatform.cs**: launches the player upward
```csharp
using UnityEngine;

public class BouncyPlatform : Platform
{
    [SerializeField] private float bounceForce = 15f;

    public override void OnPlayerLand(Rigidbody2D playerRb)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
    }
}
```

**MovingPlatform.cs**: slides back and forth
```csharp
using UnityEngine;

public class MovingPlatform : Platform
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 3f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance) - distance / 2f;
        transform.position = new Vector2(startPosition.x + offset, startPosition.y);
    }
}
```

> `BouncyPlatform` and `MovingPlatform` use `override` to replace `OnPlayerLand()` with their own version, while `NormalPlatform` just inherits the default. This is **polymorphism**: the same method call, `platform.OnPlayerLand(rb)`, produces different behavior depending on the object's type.

### Step 5: Wiring It Up
1. Attach `NormalPlatform`, `BouncyPlatform`, or `MovingPlatform` to each platform GameObject
2. Turn each platform into a **Prefab** (drag from Hierarchy into the Project panel) so you can reuse them across scenes
3. Press **Play** and test. The player should move, jump, bounce off the bouncy platform, and ride the moving one

![Scene View in Play Mode showing the player on a platform](resources/Mini%20Project%20-%20Final%20Showcase.gif)

> Swapping a platform's type is as simple as removing one script and attaching another. The player code never needs to change.

## i. Additional References
- [Unity 6 Manual: Scripting](https://docs.unity3d.com/6000.0/Documentation/Manual/ScriptingSection.html)
- [Unity 6 Manual: MonoBehaviour](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.html)
- [Microsoft C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Unity Learn: Junior Programmer Pathway](https://learn.unity.com/pathway/junior-programmer)
