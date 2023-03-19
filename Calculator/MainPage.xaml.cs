using System.IO.Pipes;

namespace Calculator;

public partial class MainPage : ContentPage
{

    bool bClear;
    bool bfloat;
    bool bOpInProgress;

    Double Op1, Op2;
    string Operation = "";

    public MainPage()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Frame tappedFrame = (Frame)sender;
        var frameContent = tappedFrame.Content;



        var labelType = frameContent.GetType();
        if (labelType == typeof(Label))
        {
            // var innerGrid = frameContent as Grid;
            var innerLabel = (Label)frameContent;

            if (innerLabel != null)
            {

                 


                var actualText = innerLabel.Text;

                //   DisplayAlert("You tapped", actualText, "alright");


                switch (actualText)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (bClear || bOpInProgress)
                        {
                            answerBox.Text = actualText;
                            bClear = false;
                            bOpInProgress = false;
                        }
                        else
                        {
                            answerBox.Text += actualText;
                        }
                        AddDecimalPlace();
                        break;
                    case "AC":
                        ClearCalc();
                        break;
                    case ".":
                        bfloat = true;
                        break;

                    case "+":
                        CalcResult();
                        Operation = "+";
                        Op1 = Convert.ToDouble(answerBox.Text);
                        bOpInProgress = true;
                        bfloat = false;
                        break;

                    case "-":
                        CalcResult();
                        Operation = "-";
                        Op1 = Convert.ToDouble(answerBox.Text);
                        bOpInProgress = true;
                        bfloat = false;
                        break;

                    case "/":
                        CalcResult();
                        Operation = "/";
                        Op1 = Convert.ToDouble(answerBox.Text);
                        bOpInProgress = true;
                        bfloat = false;
                        break;

                    case "*":
                        CalcResult();
                        Operation = "*";
                        Op1 = Convert.ToDouble(answerBox.Text);
                        bOpInProgress = true;
                        bfloat = false;
                        break;

                    case "+/-":
                        var curNum = Convert.ToDouble(answerBox.Text);
                        curNum = -curNum;
                        answerBox.Text = curNum.ToString();
                        AddDecimalPlace();
                        break;

                    case "1/x":
                        var number = Convert.ToDouble(answerBox.Text);
                        if (number.Equals(0) is false)
                        {
                            answerBox.Text = Convert.ToString((1 / number));
                        }
                        else
                        {
                            answerBox.Text = "Cannot divide by zero";
                        }
                        break;

                    case "sqrt":
                        answerBox.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(answerBox.Text)));
                        AddDecimalPlace();
                        bClear = true;
                        break;

                    
                    case "=":
                        CalcResult();
                        break;


                }


            }
        }
    }

    private void CalcResult()
    {
        if (Operation.Equals("") is false)
        {
            Op2 = Convert.ToDouble(answerBox.Text);
            switch (Operation)
            {
                case "+":
                    answerBox.Text = (Op1 + Op2).ToString();
                    break;
                case "-":
                    answerBox.Text = (Op1 - Op2).ToString();
                    break;
                case "*":
                    answerBox.Text = (Op1 * Op2).ToString();
                    break;
                case "/":
                    answerBox.Text = (Op1 / Op2).ToString();
                    break;
            }
            if (answerBox.Text.Contains(".") is false)
            {
                AddDecimalPlace();
            }

            Operation = "";
            bClear = true;
        }
    }

    private void ClearCalc()
    {
        bClear = true;
        answerBox.Text = "0";
        bfloat = false;
        AddDecimalPlace();
        Op1 = Op2 = 0;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        
        string currentValue = answerBox.Text;
        if (currentValue.EndsWith("."))
        {
            if (currentValue.Length > 2)
            {
                answerBox.Text = Convert.ToString(currentValue.Remove((answerBox.Text.Length - 2), 1));

            }
            else if (currentValue.Length == 2)
            {
                answerBox.Text = "0";
            }
        }
        else if (currentValue.Contains(".") && (currentValue.IndexOf(".") != (answerBox.Text.Length - 1)))
        {
            answerBox.Text = currentValue.Remove((answerBox.Text.Length - 1), 1);
        }
        
    }

    private void AddDecimalPlace()
    {
        var boxContent = answerBox.Text;

        if(bfloat is false)
        {
            if(answerBox.Text.Contains("."))
            {                
                boxContent = boxContent.Replace(".", "");
            }

            answerBox.Text = boxContent;
            answerBox.Text += ".";

        }
    }
}

