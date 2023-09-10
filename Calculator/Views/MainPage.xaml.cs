using Calculator.Models;

namespace Calculator.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ExtendEquation(object sender, EventArgs e)
    {
        string extension = ((Button)sender).Text;

        equation.Text += extension;
    }
    
    private void SolveEquation(object sender, EventArgs e)
    {
        return;
    }
}