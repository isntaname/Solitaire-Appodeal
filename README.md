# Solitaire-Like Card Stack Game

This is a simplified Solitaire-inspired card stack game built in Unity. The prototype demonstrates moving cards between stacks with drag-and-drop functionality and includes an **Undo** feature for reverting the last move. A basic UI is also provided.

This project addresses the core requirements of the test task:

- Create a Unity prototype of an **Undo Move** system.
- Implement a basic undo mechanism that allows the player to revert at least one previous move.
- Focus on minimal card movement between 2–3 stacks (not a full Solitaire game).

---

## Tech Requirements

- Unity 6.0 LTS (6000.0.49f1) is required to open and run this project.

---

## What I’d Improve With More Time

- Implement a full Solitaire game with proper card rules and win conditions.
- Add automated tests for core gameplay logic (omitted, since gameplay wasn’t the focus of this case study).
- Decouple UI from game logic more cleanly.
- Refactor the `CommandManager` to avoid the Singleton pattern and instead use dependency injection.

---

## AI-Assisted Parts

This project was primarily AI-assisted using **Cursor**. AI helped in designing, coding, and refining core components:

- **Command Pattern & Undo:** Implementing the `CommandManager`, including undo/redo logic and unit tests.
- **Core Game Logic:** Basic card movement mechanics, drag-and-drop interactions, and stack behavior.
- **Card Movement Restrictions:** Ensured only the top card in a stack can be moved.
- **Undo Improvements:** Undo button updates using event-driven logic.
- **Proofreading of this README**

### Example Init Prompts Used

- `"Implement CommandManager for ICommand that supports undo functionallity."`
- `"Write NUnit tests for CommandManager."`
- `"Implement a simple drag-and-drop system to move cards between stacks."`
- `"Restrict card movement to the top card in each stack."`

---

## How to Run

1. Clone or download the repository.
2. Open the project with **Unity 6.0 LTS (6000.0.49f1)**.
3. Open the scene at `Assets/Scenes/SampleScene`.
4. Press **Play** to run the prototype.

---

## Contact

If you have any questions about the project, feel free to reach out to me directly.
