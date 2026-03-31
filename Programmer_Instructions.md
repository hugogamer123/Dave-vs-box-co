# Collaborating Instructions
So just so you know, because I am kindof new to unity so hopefully this will sum up some stuff to make sure everything works.

If there's something that isn't clear or missing just make an issue post on GitHub and ill update this.

## Making walls
As one of the mechanics of the game is grabbing onto walls and jumping, there are specific ways to ensure that the Player movement script knows to perform the action.

For wall-jumping ensure the the wall (or tilemap) has a collider and rigidbody 2d with static body type, and please mark the object with the "Ground" layer.

## Physics Objects
To make a physics object be affected by the magnet, it's simple. Just mark it with the "push and pull" layer and give it a RigidBody2D. If needed, set a mass for your object.

## Events system
This project contains an event system. This system allows you to raise Events and subscribe certain game objects to them without setting references manually.

**How to use it:**
1. Create a `GameEvent` asset in the editor.
2. Add `GameEventListener` components to interested GameObjects.
3. Assign the same `GameEvent` asset to each listener.
4. Hook inspector callbacks on the listener’s response.
5. Raise the event from code with gameEvent.Raise(...). You can either raise it globally, making every listener respond at once, or directly, making only one selected listener respond.

For event raising example you can see `InteractionCaller` script.
For event subscription exampla you can see dialogue NPCs that have `GameEventListener` components.
