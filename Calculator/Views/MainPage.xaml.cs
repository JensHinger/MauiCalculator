using Calculator.Models;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator.Views;

public partial class MainPage : ContentPage
{
    static string[] operators = { "+", "-", "*", "/" };
    static string joinedOperators = string.Join("", operators);

    public MainPage()
    {
        InitializeComponent();
    }

    private void ExtendEquation(object sender, EventArgs e)
    {
        string extension = ((Button)sender).Text;
        string lastPart = equation.Text.Split(' ').Last();
        

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
        string lastOperator = Regex.Replace(equation.Text.Split(' ').Last(), @"\s+", "");

        if (joinedOperators.Contains(lastOperator))
        {
            return;
        }
        else
        {
            SolveEquation();
        } 
    }

    private void SolveEquation()
    {
        // does not work with negativ numbers currently
        String[] equation_parts = equation.Text.Split(" ");
        

        float first_number = float.Parse(equation.Text.Split(' ')[0], CultureInfo.InvariantCulture.NumberFormat);

        string operation = equation.Text.Split(' ')[1];
        operation = Regex.Replace(operation, @"\s+", "");

        float second_number = float.Parse(equation.Text.Split(' ').Last(), CultureInfo.InvariantCulture.NumberFormat);

        float solution = DoOperation(first_number, operation, second_number);
        equation.Text = solution.ToString();
    }

    private float DoOperation(float num1, string operation, float num2)
    {
        switch (operation)
        {
            case "+": return num1 + num2;
            case "-": return num1 - num2;
            case "*": return num1 * num2;
            case "/": return num1 / num2;
            default: return 0.0f;
        }
    }
}