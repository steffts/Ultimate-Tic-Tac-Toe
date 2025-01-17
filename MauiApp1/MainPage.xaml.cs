using System.Collections.ObjectModel;
using MauiApp1.Data;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private GameLogic _gameLogic;
    private GameDatabase _gameDatabase;

    public MainPage()
    {
        InitializeComponent();

        _gameLogic = new GameLogic(UpdateUI);

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "game.db");

        _gameDatabase = new GameDatabase(dbPath);

        ResetGame();
    }

    private void ResetGame()
    {
        _gameLogic.ResetGame();
        UpdateUI();
    }

    private void UpdateUI()
    {
        CurrentPlayerLabel.Text = $"Играч на ход: {_gameLogic.CurrentPlayer}";

        foreach (var child in BigGrid.Children)
        {
            if (child is ContentView gridView)
            {
                var row = Grid.GetRow(gridView);
                var col = Grid.GetColumn(gridView);

                if ((row, col) == _gameLogic.ActiveBigGrid)
                    gridView.BackgroundColor = Colors.LightGreen; 
                else
                    gridView.BackgroundColor = Colors.Transparent; 
            }
        }
        ScoreLabel.Text = $"Резултат: X - {_gameLogic.PlayerXScore}, O - {_gameLogic.PlayerOScore}";

    }

    private void OnCellClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        if (_gameLogic.HandleCellClick(button))
        {
            if (_gameLogic.IsGameOver)
            {
                DisplayAlert("Край на играта", $"Играч {_gameLogic.CurrentPlayer} печели!", "ОК");
                ResetGame();
            }
            else
            {
                UpdateUI();
            }
        }
        else
        {
            DisplayAlert("Невалиден ход", "Моля, играйте в активния grid.", "ОК");
        }
    }
    public void HighlightGrid(int bigRow, int bigCol, char winner)
    {
        _gameLogic.OnGridHighlighted += HighlightGrid;

        foreach (var child in BigGrid.Children)
        {
            if (child is ContentView gridView &&
                Grid.GetRow(gridView) == bigRow &&
                Grid.GetColumn(gridView) == bigCol)
            {
                gridView.BackgroundColor = winner == 'X' ? Colors.Red : Colors.Blue;
            }
        }
    }
    private void OnResetGameClicked(object sender, EventArgs e)
    {
        
        _gameLogic.ResetGame();
        UpdateUI(); 
    }


}
