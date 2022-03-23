# Spinnin Monke

A demo scene with a controllable monkey and third-person camera.

## Features

Controls:
  - Using `Input Manager` to unify controls on all devices.
  - `WASD`/Arrows to move (internally using `Input.GetAxis`).
  - Hold `Fire2` (usually `Right Mouse Button`) to rotate camera around the monkey.

Camera:
  - Simplistic RTS-style camera but focused on a player.

Smooth movement:
  - Uses linear interpolation on velocity to allow fow accurate collisions.
  - Smooth rotation using vectors (not highly advanced and cool, but works).

![spinnin-monke](https://user-images.githubusercontent.com/49134679/156817403-ca96e82f-b557-4f53-876d-8445c0289410.png)

## Credits

Used Asset Store packages:
  - [6 x 3D Cute Toy Models](https://assetstore.unity.com/packages/3d/characters/6-x-3d-cute-toy-models-105033)
