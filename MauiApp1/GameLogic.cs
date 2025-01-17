using System;
using System.Reflection;

namespace MauiApp1;

public class GameLogic
{
    public char CurrentPlayer { get; private set; } = 'X';

    private readonly char[,] _bigGridState = new char[3, 3];
    private readonly char[,,] _smallGridState = new char[3, 3, 9];
    private readonly Action _updateUI;

    public (int Row, int Col) ActiveBigGrid { get; private set; } = (1, 1); // Започваме от центъра
    public bool IsGameOver { get; private set; }

    public int PlayerXScore { get; private set; }
    public int PlayerOScore { get; private set; }

    private readonly char[,] _bigGridOwners = new char[3, 3];


    public GameLogic(Action updateUI)
    {
        _updateUI = updateUI;

    }

    public void ResetGame()
    {
        CurrentPlayer = 'X';
        Array.Clear(_bigGridState, 0, _bigGridState.Length);
        Array.Clear(_smallGridState, 0, _smallGridState.Length);
        Array.Clear(_bigGridOwners, 0, _bigGridOwners.Length);
        ActiveBigGrid = (1, 1);
        IsGameOver = false;
        _updateUI();
    }

    public bool HandleCellClick(Button button)
    {
        // Получаване на координатите на grid-овете
        var parentGrid = (Grid)button.Parent;
        var parentView = (ContentView)parentGrid.Parent;
        int bigRow = Grid.GetRow(parentView);
        int bigCol = Grid.GetColumn(parentView);

        // Проверка дали ходът е в активния grid или е свободен
        if (ActiveBigGrid != (-1, -1) && (bigRow, bigCol) != ActiveBigGrid)
            return false;

        // Получаване на малките координати
        int smallRow = Grid.GetRow(button);
        int smallCol = Grid.GetColumn(button);
        int smallIndex = smallRow * 3 + smallCol;

        // Проверка дали клетката е вече заета
        if (!string.IsNullOrEmpty(button.Text))
            return false;

        button.Text = CurrentPlayer.ToString();
        button.TextColor = CurrentPlayer == 'X' ? Colors.Black : Colors.White;
        _smallGridState[bigRow, bigCol, smallIndex] = CurrentPlayer;

  
        if (CheckSmallGridWin(bigRow, bigCol))
        {
            _bigGridState[bigRow, bigCol] = CurrentPlayer;
        }


        if (CheckBigGridWin())
        {
            IsGameOver = true;
            return true;
        }

        ActiveBigGrid = (smallRow, smallCol);

      
        if (_bigGridState[ActiveBigGrid.Row, ActiveBigGrid.Col] != '\0')
        {
            ActiveBigGrid = (-1, -1); // Свободен ход
        }

        CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';

        _updateUI();
        return true;
    }

    private bool CheckSmallGridWin(int bigRow, int bigCol)
    {
        for (int i = 0; i < 3; i++)
        {
            if (_smallGridState[bigRow, bigCol, i * 3] == CurrentPlayer &&
                _smallGridState[bigRow, bigCol, i * 3 + 1] == CurrentPlayer &&
                _smallGridState[bigRow, bigCol, i * 3 + 2] == CurrentPlayer)
            {
                _bigGridOwners[bigRow, bigCol] = CurrentPlayer;
                HighlightGrid(bigRow, bigCol, CurrentPlayer);
                return true;
            }

            if (_smallGridState[bigRow, bigCol, i] == CurrentPlayer &&
                _smallGridState[bigRow, bigCol, i + 3] == CurrentPlayer &&
                _smallGridState[bigRow, bigCol, i + 6] == CurrentPlayer)
            {
                _bigGridOwners[bigRow, bigCol] = CurrentPlayer;
                HighlightGrid(bigRow, bigCol, CurrentPlayer);
                return true;
            }
        }


        if (_smallGridState[bigRow, bigCol, 0] == CurrentPlayer &&
            _smallGridState[bigRow, bigCol, 4] == CurrentPlayer &&
            _smallGridState[bigRow, bigCol, 8] == CurrentPlayer)
        {
            _bigGridOwners[bigRow, bigCol] = CurrentPlayer;
            HighlightGrid(bigRow, bigCol, CurrentPlayer);
            return true;
        }

        if (_smallGridState[bigRow, bigCol, 2] == CurrentPlayer &&
            _smallGridState[bigRow, bigCol, 4] == CurrentPlayer &&
            _smallGridState[bigRow, bigCol, 6] == CurrentPlayer)
        {
            _bigGridOwners[bigRow, bigCol] = CurrentPlayer;
            HighlightGrid(bigRow, bigCol, CurrentPlayer);
            return true;
        }

        return false;
    }


    private bool CheckBigGridWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_bigGridOwners[i, 0] == CurrentPlayer &&
                _bigGridOwners[i, 1] == CurrentPlayer &&
                _bigGridOwners[i, 2] == CurrentPlayer)
            {
                AwardPoint(CurrentPlayer);
                return true;
            }

            if (_bigGridOwners[0, i] == CurrentPlayer &&
                _bigGridOwners[1, i] == CurrentPlayer &&
                _bigGridOwners[2, i] == CurrentPlayer)
            {
                AwardPoint(CurrentPlayer);
                return true;
            }
        }

        if (_bigGridOwners[0, 0] == CurrentPlayer &&
            _bigGridOwners[1, 1] == CurrentPlayer &&
            _bigGridOwners[2, 2] == CurrentPlayer)
        {
            AwardPoint(CurrentPlayer);
            return true;
        }

        if (_bigGridOwners[0, 2] == CurrentPlayer &&
            _bigGridOwners[1, 1] == CurrentPlayer &&
            _bigGridOwners[2, 0] == CurrentPlayer)
        {
            AwardPoint(CurrentPlayer);
            return true;
        }

        return false;
    }
    public event Action<int, int, char> OnGridHighlighted;
    private void HighlightGrid(int bigRow, int bigCol, char winner)
    {
        
        OnGridHighlighted?.Invoke(bigRow, bigCol, winner);


    }


    private void AwardPoint(char player)
    {
        if (player == 'X')
            PlayerXScore++;
        else if (player == 'O')
            PlayerOScore++;
    }


}
