using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;

        private readonly Color colorBackground = Color.FromArgb(198, 226, 240);
        private readonly Color colorButtonNumber = Color.White;
        private readonly Color colorButtonOperator = Color.FromArgb(100, 149, 190);
        private readonly Color colorButtonScientific = Color.FromArgb(140, 120, 200);
        private readonly Color colorButtonClear = Color.FromArgb(230, 130, 130);
        private readonly Color colorDisplayPanel = Color.FromArgb(220, 235, 245);

        private RoundedPanel panelDisplay = null!;
        private TextBox txtDisplay = null!;
        private RoundedPanel panelScientific = null!;
        private RoundedButton btnClear = null!;
        private TableLayoutPanel panelButtons = null!;
        private RoundedPanel panelHistory = null!;
        private ListBox lstHistory = null!;
        private RoundedButton btnClearHistory = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(700, 520);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = colorBackground;
            this.Text = "Kalkulator Scientific";

            panelDisplay = new RoundedPanel
            {
                Location = new Point(20, 20),
                Size = new Size(420, 70),
                BackColor = colorDisplayPanel,
                Radius = 16
            };

            txtDisplay = new TextBox
            {
                Text = "0",
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = colorDisplayPanel,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true
            };
            panelDisplay.Controls.Add(txtDisplay);

            // ===== Panel Tombol Scientific =====
            panelScientific = new RoundedPanel
            {
                Location = new Point(20, 100),
                Size = new Size(420, 45),
                BackColor = colorBackground,
                Radius = 12
            };

            string[] scientificLabels = { "sin", "cos", "tan", "sqr", "lo", "x^y" };
            int scX = 0;
            foreach (var label in scientificLabels)
            {
                var btn = new RoundedButton
                {
                    Text = label,
                    Size = new Size(65, 40),
                    Location = new Point(scX, 0),
                    BackColor = colorButtonScientific,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Radius = 12,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btn.FlatAppearance.BorderSize = 0;

                if (label == "x^y")
                {
                    btn.Tag = "^";
                    btn.Click += OperatorButton_Click;
                }
                else
                {
                    btn.Click += BtnScientific_Click;
                }

                panelScientific.Controls.Add(btn);
                scX += 70;
            }

            btnClear = new RoundedButton
            {
                Text = "C (Clear)",
                Location = new Point(20, 155),
                Size = new Size(420, 40),
                BackColor = colorButtonClear,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Radius = 14,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += btnClear_Click;

            panelButtons = new TableLayoutPanel
            {
                Location = new Point(20, 205),
                Size = new Size(420, 280),
                ColumnCount = 4,
                RowCount = 5,
                BackColor = colorBackground
            };
            for (int i = 0; i < 4; i++)
                panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int i = 0; i < 5; i++)
                panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            AddGridButton(panelButtons, "±", 0, 0, colorButtonOperator, BtnPlusMinus_Click);
            AddGridButton(panelButtons, "%", 1, 0, colorButtonOperator, BtnPercent_Click);
            AddGridButton(panelButtons, "⌫", 2, 0, colorButtonOperator, BtnBackspace_Click);
            AddGridButton(panelButtons, "÷", 3, 0, colorButtonOperator, OperatorButton_Click);

            AddGridButton(panelButtons, "7", 0, 1, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "8", 1, 1, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "9", 2, 1, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "×", 3, 1, colorButtonOperator, OperatorButton_Click);

            AddGridButton(panelButtons, "4", 0, 2, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "5", 1, 2, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "6", 2, 2, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "−", 3, 2, colorButtonOperator, OperatorButton_Click);

            AddGridButton(panelButtons, "1", 0, 3, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "2", 1, 3, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "3", 2, 3, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, "+", 3, 3, colorButtonOperator, OperatorButton_Click);

            AddGridButton(panelButtons, "0", 0, 4, colorButtonNumber, NumberButton_Click);
            AddGridButton(panelButtons, ".", 1, 4, colorButtonNumber, NumberButton_Click);
            var btnEquals = AddGridButton(panelButtons, "=", 2, 4, colorButtonOperator, btnEquals_Click);
            panelButtons.SetColumnSpan(btnEquals, 2);

            panelHistory = new RoundedPanel
            {
                Location = new Point(460, 20),
                Size = new Size(220, 430),
                BackColor = colorDisplayPanel,
                Radius = 16
            };

            var lblHistoryTitle = new Label
            {
                Text = "Riwayat",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            lstHistory = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9),
                BackColor = colorDisplayPanel,
                TabStop = false
            };

            lstHistory.DoubleClick += LstHistory_DoubleClick;

            btnClearHistory = new RoundedButton
            {
                Text = "Bersihkan",
                Dock = DockStyle.Bottom,
                Height = 36,
                BackColor = colorButtonClear,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Radius = 12,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnClearHistory.FlatAppearance.BorderSize = 0;
            btnClearHistory.Click += BtnClearHistory_Click;

            panelHistory.Controls.Add(lstHistory);
            panelHistory.Controls.Add(btnClearHistory);
            panelHistory.Controls.Add(lblHistoryTitle);

            this.Controls.Add(panelHistory);
            this.Controls.Add(panelButtons);
            this.Controls.Add(btnClear);
            this.Controls.Add(panelScientific);
            this.Controls.Add(panelDisplay);

            this.ResumeLayout(false);
        }
        private RoundedButton AddGridButton(TableLayoutPanel grid, string text, int col, int row, Color backColor, EventHandler handler)
        {
            var btn = new RoundedButton
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                BackColor = backColor,
                ForeColor = backColor == colorButtonNumber ? Color.Black : Color.White,
                FlatStyle = FlatStyle.Flat,
                Radius = 14,
                Font = new Font("Segoe UI", 13, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += handler;
            grid.Controls.Add(btn, col, row);
            return btn;
        }
    }
}
