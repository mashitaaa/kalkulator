using System.Drawing.Drawing2D;

namespace CalculatorApp
{
    public class RoundedButton : Button
    {
        public int Radius { get; set; } = 14;

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0)
                return;

            int r = Math.Min(Radius, Math.Min(Width, Height) / 2);

            if (r <= 0)
            {
                this.Region = new Region(new Rectangle(0, 0, Width, Height));
                return;
            }

            var path = new GraphicsPath();
            int d = r * 2;

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(Width - d, 0, d, d, 270, 90);
            path.AddArc(Width - d, Height - d, d, d, 0, 90);
            path.AddArc(0, Height - d, d, d, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }
    }
}
