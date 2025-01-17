# MauiApp1
# Ultimate Tic-Tac-Toe Game in .NET MAUI

This project is a **Ultimate Tic-Tac-Toe** game implemented in **.NET MAUI**. The game is designed to showcase a dynamic and engaging UI with custom logic to handle the nested grid gameplay, score tracking, and win conditions. Players take turns playing in smaller grids to capture larger grids, ultimately aiming to win the entire board.

## Features

- **Dynamic Gameplay:**
  - Nested grids (3x3 big grid, each containing a 3x3 small grid).
  - Players (`X` and `O`) alternate turns, navigating based on the rules of Ultimate Tic-Tac-Toe.
  - Winning a small grid marks it as "captured" for a player.

- **Winning Logic:**
  - A player wins a small grid by aligning 3 symbols (row, column, or diagonal).
  - The first player to align 3 captured grids (row, column, or diagonal) wins the entire game.

- **Score Tracking:**
  - Player scores are tracked and displayed dynamically.
  - Each win increments the respective player's score.

- **Reset Functionality:**
  - Reset the game to start fresh without losing the scores.

- **Responsive UI:**
  - Highlights the active grid to guide players on where to make their move.
  - Visual feedback for captured grids.


## Getting Started

### Prerequisites

- **.NET 7 or later**: Ensure you have the latest version of the .NET SDK installed.
- **Visual Studio 2022**: Use the .NET MAUI workload to build and run the project.
- **.NET MAUI**: Ensure your environment is set up for building cross-platform apps.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ultimate-tic-tac-toe.git
   cd ultimate-tic-tac-toe
   Open the project in Visual Studio:

2. start MauiApp1.sln
Build and run the app:

3. Use the Android Emulator, iOS Simulator, or your local device to test the application.
Running the App
   ### Start the app.
- The current player is displayed at the top.
- Tap on the active grid to make a move.
- Follow the rules of Ultimate Tic-Tac-Toe:
- Your move determines the active grid for your opponent.
- Capture grids to win the overall game.
- The scores are updated dynamically for each player.
