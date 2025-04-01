// Licensed to the .NET Foundation under one or more agreements.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        var item = ((FrameworkElement)sender).DataContext as GameItem;
        if (item != null)
        {
            Session?.CurrentPlayer.AddGold(item.Price);
            Session?.CurrentTrader?.AddItem(item);
            Session?.CurrentPlayer.RemoveItem(item);
        }
    }

    public void OnClick_Buy(object sender, RoutedEventArgs e)
    {
        var item = ((FrameworkElement)sender).DataContext as GameItem;
        if (item != null)
        {
            if (Session?.CurrentPlayer.Gold >= item.Price)
            {
                Session.CurrentPlayer.RemoveGold(item.Price);
                Session.CurrentTrader?.RemoveItem(item);
                Session.CurrentPlayer.AddItem(item);
            }
            else
            {
                MessageBox.Show("You Do not have enough gold");
            }
        }
    }

    public void OnClick_Close(object sender, RoutedEventArgs e) => Close();
}
