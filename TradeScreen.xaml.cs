// Licensed to the .NET Foundation under one or more agreements.

using System.Windows;
using Engine.Models;
using Engine.ViewModels;

namespace UnnamedWpf;

/// <summary>
/// Logica di interazione per TradeScreen.xaml
/// </summary>
public partial class TradeScreen : Window
{
    public GameSession? Session => DataContext as GameSession;

    public TradeScreen()
    {
        InitializeComponent();
    }

    private void OnClick_Sell(object sender, RoutedEventArgs e)
    {
        var item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
        if (item != null)
        {
            Session?.CurrentPlayer.AddGold(item.Item.Price);
            Session?.CurrentTrader?.AddItem(item.Item);
            Session?.CurrentPlayer.RemoveItem(item.Item);
        }
    }

    public void OnClick_Buy(object sender, RoutedEventArgs e)
    {
        var item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
        if (item != null)
        {
            if (Session?.CurrentPlayer.Gold >= item.Item.Price)
            {
                Session.CurrentPlayer.RemoveGold(item.Item.Price);
                Session.CurrentTrader?.RemoveItem(item.Item);
                Session.CurrentPlayer.AddItem(item.Item);
            }
            else
            {
                MessageBox.Show("You Do not have enough gold");
            }
        }
    }

    public void OnClick_Close(object sender, RoutedEventArgs e) => Close();
}
