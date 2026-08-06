# Shape Rendering System

This task is part of my **Full Stack Development (.NET + React) — ITIDA Training to Hire** coursework by **Andalusia Academy**.

## Objective

Build a shape rendering system that exercises **abstract classes**, **interfaces**, and **polymorphism**.

## Requirements

- An interface `IDrawable` with a method `void Draw()`
- An abstract class `Shape` with:
  - an `abstract double Area()` method
  - a concrete `void Describe()` method that prints the class name and area
- `Circle` and `Rectangle` that extend `Shape` and implement `IDrawable` — each `Draw()` prints a short ASCII representation
- Several shapes stored in a `Shape[]`, looped over calling both `Describe()` and `Draw()`
- A `Triangle` class added, verifying everything still compiles and runs without changing the loop

### Bonus

A second interface `IResizable` with `void Scale(double factor)`, implemented on `Circle` and `Rectangle`. A method that accepts `IEnumerable<IResizable>` and scales every element by a given factor.

## Concepts Practiced

- Abstract classes vs. interfaces
- Polymorphism via a common base type (`Shape[]`) holding different derived types
- Extensibility — adding a new shape (`Triangle`) without modifying existing looping/processing code
- Multiple interface implementation and working with `IEnumerable<T>`
