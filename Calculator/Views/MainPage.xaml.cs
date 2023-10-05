using Calculator.Models;
using System.Diagnostics;
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
        string lastPart = equation.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Last();

        Debug.WriteLine("Lastpart " + lastPart + " |");

        // No number should have two "," and no comma should be before a number
        if (extension == "," && (lastPart.Contains(',') || joinedOperators.Contains(lastPart))) return;
        else if (extension == "-" && lastPart == "-" && !equation.Text.EndsWith(' ')) return;

        // if current input is an operator
        if (joinedOperators.Contains(extension))
        {
            // Return if last part is also an operator except for "-" and 
            if (joinedOperators.Contains(lastPart) && !(extension == "-")) return;

            foreach (string equation_part in equation.Text.Split(" ")){
                // Solve equation if equation contains an operator and the lastPart is not an operator
                if (joinedOperators.Contains(equation_part) && !joinedOperators.Contains(lastPart))
                { 
                    SolveEquation();
                }
            }

            // If we add a operator add empty space left and right of operator 
            if (joinedOperators.Contains(lastPart) || lastPart == "0")
            {
                extension = $"{extension}";
            } else
            {
                extension = $" {extension} ";
            }
            
        }

        if (equation.Text == "0" && extension != ",")
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
        // Values can also be negativ (-20 * -2)
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
        String[] equation_parts = equation.Text.Split(" ");
        float first_number = float.PositiveInfinity;
        float second_number = float.PositiveInfinity;
        string operation = default;

        for (int i = 0; i < equation_parts.Length; i++)
        {
            float temp_number;
            Debug.WriteLine(equation_parts[i]);

            if (equation_parts[i] == ""){
                // I am not sure why this happens -> literally an empty string in the equation_parts list
                continue;
            } 
            else if (equation_parts[i][0] == '-' && float.TryParse(equation_parts[i], out _))
            {

                var fmt = new NumberFormatInfo
                {
                    NegativeSign = "-"
                };
                temp_number = float.Parse(equation_parts[i], fmt);

            } else if (float.TryParse(equation_parts[i], out temp_number)) { } 
            else
            {
                operation = equation_parts[i];
                Debug.WriteLine("Operator @ " + i + operation);
                continue;
            }

            if (first_number == float.PositiveInfinity) first_number = temp_number;
            else second_number = temp_number;
        }

        // This should be fine
        float solution = DoOperation(first_number, operation, second_number);
        equation.Text = solution.ToString();
    }

    private float DoOperation(float num1, string operation, float num2)
    {
        Debug.WriteLine(num1.ToString() + "   " + operation + "   " + num2.ToString());

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