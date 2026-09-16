using System.Globalization;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private readonly CalculatorEngine engine = new CalculatorEngine();

        private bool isNewInput = true;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Kalkulator Scientific";
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (isNewInput || txtDisplay.Text == "0")
            {
                txtDisplay.Text = button.Text == "." ? "0." : button.Text;
                isNewInput = false;
            }
            else
            {
                if (button.Text == "." && txtDisplay.Text.Contains('.'))
                    return;

                txtDisplay.Text += button.Text;
            }
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                engine.SetFirstNumber(value);

                string op = button.Tag?.ToString() ?? button.Text;
                engine.SetOperation(op);

                isNewInput = true;
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double secondNumber))
                {
                    double firstNumberForHistory = engine.FirstNumber;
                    string opForHistory = engine.CurrentOperation;

                    double result = engine.Calculate(secondNumber);
                    txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);

                    if (!string.IsNullOrEmpty(opForHistory))
                        AddToHistory($"{firstNumberForHistory} {opForHistory} {secondNumber} = {result}");

                    isNewInput = true;
                }
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Error Pembagian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDisplay.Text = "0";
                isNewInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            engine.Reset();
            txtDisplay.Text = "0";
            isNewInput = true;
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (isNewInput) return;

            if (txtDisplay.Text.Length > 1)
                txtDisplay.Text = txtDisplay.Text[..^1];
            else
            {
                txtDisplay.Text = "0";
                isNewInput = true;
            }
        }

        private void BtnPlusMinus_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                txtDisplay.Text = engine.Negate(value).ToString(CultureInfo.InvariantCulture);
        }

        private void BtnPercent_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                double result = engine.Percent(value);
                txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                AddToHistory($"{value}% = {result}");
                isNewInput = true;
            }
        }
        private void BtnScientific_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;

                if (!double.TryParse(txtDisplay.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    return;

                double result;
                string expression;

                switch (button.Text.ToLower())
                {
                    case "sin":
                        result = engine.Sin(value);
                        expression = $"sin({value}) = {result}";
                        break;
                    case "cos":
                        result = engine.Cos(value);
                        expression = $"cos({value}) = {result}";
                        break;
                    case "tan":
                        result = engine.Tan(value);
                        expression = $"tan({value}) = {result}";
                        break;
                    case "sqr":
                        result = engine.Sqrt(value);
                        expression = $"sqrt({value}) = {result}";
                        break;
                    case "lo":
                        result = engine.Log10(value);
                        expression = $"log({value}) = {result}";
                        break;
                    default:
                        return;
                }

                txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
                AddToHistory(expression);
                isNewInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToHistory(string entry)
        {
            lstHistory.Items.Insert(0, entry);
        }

        private void LstHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lstHistory.SelectedItem == null) return;

            string entry = lstHistory.SelectedItem.ToString() ?? "";
            int equalIndex = entry.LastIndexOf('=');

            if (equalIndex >= 0)
            {
                string resultPart = entry[(equalIndex + 1)..].Trim();
                txtDisplay.Text = resultPart;
                isNewInput = true;
            }
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            lstHistory.Items.Clear();
        }
    }
}
