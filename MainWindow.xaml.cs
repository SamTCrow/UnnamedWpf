// Licensed to the .NET Foundation under one or more agreements.

using System.Windows;
using Engine.ViewModels;

namespace UnnamedWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private GameSession _gameSession;

    public MainWindow()
    {
        InitializeComponent();
        _gameSession = new GameSession();
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
}
