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

        // No number should have two "," and no comma should be before a number
        if (extension == "," && (lastPart.Contains(',') || joinedOperators.Contains(lastPart))) return;

        // if current input is an operator
        if (joinedOperators.Contains(extension))
        {
            // Return if last part is also an operator except for "-"
            if (joinedOperators.Contains(lastPart) && !(extension == "-")) return;

            foreach (string op in operators){
                // Solve equation if equation contains an operator and the lastPart is not an operator
                if (equation.Text.Contains(op) && !joinedOperators.Contains(lastPart))
                { 
                    SolveEquation();
                }
            }
            // If we add a operator add empty space left and right of operator
            extension = $" {extension} ";
        }

        if (equation.Text == "0" && extension != "," && extension != " - ")
        {
            equation.Text = extension;
        } 
        else
        {
            equation.Text += extension;
        }
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