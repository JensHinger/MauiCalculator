using Calculator.Models;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

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
        string lastPart = equation.Text.Split(' ').Last();
        string[] operators = { "+", "-", "*", "/" };
        string joinedOperators = string.Join("", operators);

        // Return is lastPart contains a ","
        if (extension == "," && lastPart.Contains(',')) return;

        // if current input is an operator
        if (joinedOperators.Contains(extension))
        {
            // Return if last part is also an operator
            if (joinedOperators.Contains(lastPart)) return;

            // If equation contains operator and current symbol was either "+-*/" don't add
            // but solve the current equation and add it afterwards
            foreach (string op in operators){
                if (equation.Text.Contains(op))
                { 
                    SolveEquation();
                }
            }
            // If we add a operator add empty space left and right of operator
            extension = $" {extension} ";
        }

        equation.Text += extension;
    }
    
    private void ClickedSolve(object sender, EventArgs e)
    {
        // Equation will always either be only a number (20,55) or
        // a equation with two values and one operator(20,55 + 10,5) 
        SolveEquation();
    }

    private void SolveEquation()
    {
        equation.Text = "Solved";
    }
}