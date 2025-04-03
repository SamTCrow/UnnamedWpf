// Licensed to the .NET Foundation under one or more agreements.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.Models;
using Engine.ViewModels;

namespace UnnamedWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly GameSession _gameSession = new();
    private readonly Dictionary<Key, Action> _userInputActions = [];

    public MainWindow()
    {
        InitializeComponent();
        InitializeUserInputActions();
        _gameSession.OnMessageRaised += OnGameMessageRaised;
        DataContext = _gameSession;
    }

    private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
    {
        _gameSession.Move(Direction.North);
    }

    private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
    {
        _gameSession.Move(Direction.South);
    }

    private void OnClick_MoveEast(object sender, RoutedEventArgs e)
    {
        _gameSession.Move(Direction.East);
    }

    private void OnClick_MoveWest(object sender, RoutedEventArgs e)
    {
        _gameSession.Move(Direction.West);
    }

    private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
    {
        _gameSession.AttackCurrentMonster();
    }

    private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
    {
        _gameSession.UseCurrentConsumable();
    }

    private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
    {
        if (_gameSession.CurrentTrader != null)
        {
            TradeScreen tradeScreen = new() { Owner = this, DataContext = _gameSession };
            tradeScreen.ShowDialog();
        }
    }

    private void OnGameMessageRaised(object? sender, GameMessagesEventArgs e)
    {
        GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Mesagge)));
        GameMessages.ScrollToEnd();
    }

    private void OnClick_Craft(object sender, RoutedEventArgs e)
    {
        if (((FrameworkElement)sender).DataContext is Recipe recipe)
        {
            _gameSession.CraftItem(recipe);
        }
    }

    private void InitializeUserInputActions()
    {
        _userInputActions.Add(Key.W, () => _gameSession.Move(Direction.North));
        _userInputActions.Add(Key.A, () => _gameSession.Move(Direction.West));
        _userInputActions.Add(Key.S, () => _gameSession.Move(Direction.South));
        _userInputActions.Add(Key.D, () => _gameSession.Move(Direction.East));
        _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());
        _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());
        _userInputActions.Add(Key.I, () => SetTabFocus("InventoryTabItem"));
        _userInputActions.Add(Key.Q, () => SetTabFocus("QuestsTabItem"));
        _userInputActions.Add(Key.R, () => SetTabFocus("RecipesTabItem"));
        _userInputActions.Add(Key.T, () => OnClick_DisplayTradeScreen(this, new RoutedEventArgs()));
    }

    private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (_userInputActions.ContainsKey(e.Key))
            _userInputActions[e.Key].Invoke();
    }

    private void SetTabFocus(string tabName)
    {
        foreach (var item in PlayerDataTabControl.Items)
        {
            if (item is TabItem tabItem)
            {
                if (tabItem.Name == tabName)
                {
                    tabItem.IsSelected = true;
                    return;
                }
            }
        }
    }
}
