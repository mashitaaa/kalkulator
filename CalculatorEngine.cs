namespace CalculatorApp
{
    public class CalculatorEngine
    {
        private double firstNumber = 0;
        private string operation = "";

        public void SetFirstNumber(double value) => firstNumber = value;

        public void SetOperation(string op) => operation = op;

        public string CurrentOperation => operation;

        public double FirstNumber => firstNumber;

        public double Calculate(double secondNumber)
        {
            double result = operation switch
            {
                "+" => firstNumber + secondNumber,
                "−" => firstNumber - secondNumber,
                "-" => firstNumber - secondNumber,
                "×" => firstNumber * secondNumber,
                "*" => firstNumber * secondNumber,
                "÷" or "/" => DivideSafely(firstNumber, secondNumber),
                "^" => Power(firstNumber, secondNumber),
                _ => throw new InvalidOperationException("Operator tidak dikenali."),
            };

            return result;
        }

        private double DivideSafely(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Tidak dapat membagi dengan nol.");
            return a / b;
        }

        public double Sin(double degree) => Math.Sin(degree * Math.PI / 180);
        public double Cos(double degree) => Math.Cos(degree * Math.PI / 180);
        public double Tan(double degree) => Math.Tan(degree * Math.PI / 180);

        public double Sqrt(double value)
        {
            if (value < 0)
                throw new ArgumentException("Tidak dapat menghitung akar dari bilangan negatif.");
            return Math.Sqrt(value);
        }

        public double Log10(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Nilai harus lebih besar dari nol untuk fungsi log.");
            return Math.Log10(value);
        }

        public double Power(double baseValue, double exponent) => Math.Pow(baseValue, exponent);

        public double Negate(double value) => value * -1;

        public double Percent(double value) => value / 100;

        public void Reset()
        {
            firstNumber = 0;
            operation = "";
        }
    }
}
