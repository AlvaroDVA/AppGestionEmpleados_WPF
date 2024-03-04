using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionEmpleados.Controls;


public partial class MenuRadioButton : UserControl
{   
    public static readonly DependencyProperty PropiedadText = DependencyProperty.Register(
        "Texto", typeof(string), typeof(MenuRadioButton));

    public static readonly DependencyProperty SvgImage = DependencyProperty.Register(
        "SvgData", typeof(string), typeof(MenuRadioButton));

    public static readonly DependencyProperty GroupMenu = DependencyProperty.Register(
        "Group", typeof(string), typeof(MenuRadioButton));


    public static readonly DependencyProperty OnChecked = DependencyProperty.Register(
        "Checked", typeof(bool), typeof(MenuRadioButton));

    public string Texto { get => (string)GetValue(PropiedadText); set => SetValue(PropiedadText, value); }

    public string SvgData { get => (string)GetValue(SvgImage); set => SetValue(SvgImage, value); }
    public string Group { get => (string)GetValue(GroupMenu); set => SetValue(GroupMenu, value); }
    public bool Checked { get => (bool)GetValue(OnChecked); set => SetValue(OnChecked, value); }

    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
    "ClickCommand", typeof(ICommand), typeof(MenuRadioButton));

    public ICommand ClickCommand
    {
        get { return (ICommand)GetValue(ClickCommandProperty); }
        set { SetValue(ClickCommandProperty, value); }
    }
    public MenuRadioButton() {
        InitializeComponent();

    }

}
