# MovingAround

## Introduction

A football simulation game written by nagizero (398607 at github) on Unity. In the game, two players will fight for their pride in the traditional football game.

Two skills are learnt by both players. "Dash" will multipy your speed limit by 2 for 5 seconds, and "Magnet" will create a solid magnet on the ground in front of you, which has a strong attraction for the ball.

Since ver 1.1.2, players can pick up items on the ground and use them.

## Controls

### Player 1:

- move: axis keys
- Dash: comma(',')
- Magnet: period('.')
- Use Item: Slash('/')

### Player 2:

- move: WASD
- Dash: 1 (above alphabet keys)
- Magnet: 2 (above alphabet keys)
- Use Item: 3 (above alphabet keys)

## Update log

### ver 1.1.2

- The first Item of the game: "Medicine"! Along with abstarct class TimeEffectItem and Item builded for developers.
- Dash Property changed.

### ver 1.1.1

- Ball Linear Drag: 0.01 -> 0.5

Make it easier for the ball to speed down.

- Control of Player 2 changed.

- Field Edge collider changed slightly.

### ver 1.1.0

- Human running maximum speed: 12 -> 15

By letting players run quicklier, they could perform better strategies.

- Skill: "Dash" CD: 30s -> 20s, Time: 5s -> 2s

Dash is an easy but useful skill. By decreasing its CD time and improving its effecting time, players could use it more frequently, making a match more interesting.

However, it is now a bit hard to control. Maybe this issue will be checked in forming versions.

## Demo play

Warning: these demo clips are recorded by game version 1.0.

![dash](https://raw.githubusercontent.com/398607/MovingAround/master/dashdemo.gif)
![magnet](https://raw.githubusercontent.com/398607/MovingAround/master/magnetdemo.gif)