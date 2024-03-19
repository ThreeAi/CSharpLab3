namespace Lab3
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button pointsButton;
        private Button parametersButton;
        private Button curveButton;
        private Button polylineButton;
        private Button bezierButton;
        private Button filledButton;
        private Button moveButton;
        private Button clearButton;
        private Button saveButton;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pointsButton = new Button();
            parametersButton = new Button();
            curveButton = new Button();
            polylineButton = new Button();
            bezierButton = new Button();
            filledButton = new Button();
            moveButton = new Button();
            clearButton = new Button();
            saveButton = new Button();
            SuspendLayout();
            // 
            // pointsButton
            // 
            pointsButton.Location = new Point(12, 10);
            pointsButton.Name = "pointsButton";
            pointsButton.Size = new Size(91, 35);
            pointsButton.TabIndex = 0;
            pointsButton.Text = "Точки";
            pointsButton.Click += PointsButton_Click;
            // 
            // parametersButton
            // 
            parametersButton.Location = new Point(12, 51);
            parametersButton.Name = "parametersButton";
            parametersButton.Size = new Size(91, 33);
            parametersButton.TabIndex = 1;
            parametersButton.Text = "Параметры";
            parametersButton.Click += ParametersButton_Click;
            // 
            // curveButton
            // 
            curveButton.Location = new Point(12, 90);
            curveButton.Name = "curveButton";
            curveButton.Size = new Size(91, 35);
            curveButton.TabIndex = 2;
            curveButton.Text = "Кривая";
            curveButton.Click += CurveButton_Click;
            // 
            // polylineButton
            // 
            polylineButton.Location = new Point(12, 131);
            polylineButton.Name = "polylineButton";
            polylineButton.Size = new Size(91, 35);
            polylineButton.TabIndex = 3;
            polylineButton.Text = "Ломаная";
            polylineButton.Click += PolylineButton_Click;
            // 
            // bezierButton
            // 
            bezierButton.Location = new Point(12, 172);
            bezierButton.Name = "bezierButton";
            bezierButton.Size = new Size(91, 35);
            bezierButton.TabIndex = 4;
            bezierButton.Text = "Безье";
            bezierButton.Click += BezierButton_Click;
            // 
            // filledButton
            // 
            filledButton.Location = new Point(12, 213);
            filledButton.Name = "filledButton";
            filledButton.Size = new Size(91, 35);
            filledButton.TabIndex = 5;
            filledButton.Text = "Заполненная";
            filledButton.Click += FilledButton_Click;
            // 
            // moveButton
            // 
            moveButton.Location = new Point(12, 295);
            moveButton.Name = "moveButton";
            moveButton.Size = new Size(91, 35);
            moveButton.TabIndex = 7;
            moveButton.Text = "Движение";
            moveButton.Click += MoveButton_Click;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(12, 336);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(91, 35);
            clearButton.TabIndex = 8;
            clearButton.Text = "Очистить";
            clearButton.Click += ClearButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(12, 254);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(91, 35);
            saveButton.TabIndex = 9;
            saveButton.Text = "Сохранение";
            saveButton.Click += SaveButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pointsButton);
            Controls.Add(parametersButton);
            Controls.Add(curveButton);
            Controls.Add(polylineButton);
            Controls.Add(bezierButton);
            Controls.Add(filledButton);
            Controls.Add(moveButton);
            Controls.Add(clearButton);
            Controls.Add(saveButton);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "MainForm";
            Text = "Lab3";
            Paint += Main_Paint;
            KeyDown += MainForm_KeyDown;
            MouseDown += MainForm_MouseDown;
            MouseMove += MainForm_MouseMove;
            MouseUp += MainForm_MouseUp;
            ResumeLayout(false);
        }

        private List<Figure> figures = new List<Figure>() { new Figure() };
        private int actualFigure = 0;

        private void TimerTickHandler(object sender, EventArgs e)
        {
            if (moving)
            {
                figures[actualFigure].moveLeft(speed, this.ClientSize.Width);
                this.Refresh();
            }
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Figure figure in figures)
            {
                Pen linePen = new Pen(figure.LineColor);
                switch (figure.JoinType)
                {
                    case null: break;
                    case JoinType.DrawClosedCurve:
                        e.Graphics.DrawClosedCurve(linePen, figure.Points.ToArray());
                        break;
                    case JoinType.DrawPolygone:
                        e.Graphics.DrawPolygon(linePen, figure.Points.ToArray());
                        break;
                    case JoinType.FillCurve:
                        e.Graphics.FillClosedCurve(Brushes.ForestGreen, figure.Points.ToArray());
                        break;
                    case JoinType.DrawBeziers:
                        e.Graphics.DrawBeziers(linePen, figure.Points.ToArray());
                        break;

                }
                Brush brushPoint = new SolidBrush(figure.PointColor);
                foreach (Point point in figure.Points) {
                    g.FillEllipse(brushPoint, point.X - 4, point.Y - 4, 8, 8);
                }
            }
        }

        private bool bDrag = false; 
        private int iPointToDrag = -1;
        int speed = 5;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            bDrag = figures[actualFigure].FindPoint(e.X, e.Y, out  iPointToDrag);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrag && iPointToDrag >= 0)
            {
                figures[actualFigure].Points[iPointToDrag] = e.Location;
                Refresh();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            bDrag = false;
            iPointToDrag = -1;
        }

        private bool addingPointsEnabled = false;
        private void PointsButton_Click(object sender, EventArgs e)
        {
            addingPointsEnabled = !addingPointsEnabled; 
            if (addingPointsEnabled)
            {
                this.MouseClick += MainForm_MouseClick; 
            }
            else
            {
                this.MouseClick -= MainForm_MouseClick;
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bDrag)
            {
                figures[actualFigure].AddPoint(e.Location);
                this.Refresh();
            }
        }

        private void ParametersButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Настройки параметров";
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.Size = new Size(150, 150);

                var colorPanelPoint = new Panel();
                colorPanelPoint.Location = new Point(130, 10);
                colorPanelPoint.Size = new Size(20, 20);
                colorPanelPoint.BorderStyle = BorderStyle.FixedSingle;
                colorPanelPoint.BackColor = figures[actualFigure].PointColor;
                dialog.Controls.Add(colorPanelPoint);

                var colorPanelLine = new Panel();
                colorPanelLine.Location = new Point(130, 50);
                colorPanelLine.Size = new Size(20, 20);
                colorPanelLine.BorderStyle = BorderStyle.FixedSingle;
                colorPanelLine.BackColor = figures[actualFigure].LineColor;
                dialog.Controls.Add(colorPanelLine);

                var PointColorButton = new Button();
                PointColorButton.Text = "Точки";
                PointColorButton.Location = new Point(10, 10);
                PointColorButton.Click += (s, args) =>
                {
                    using (var colorDialog = new ColorDialog())
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            colorPanelPoint.BackColor = colorDialog.Color;
                            figures[actualFigure].PointColor = colorDialog.Color;
                        }
                    }
                };
                dialog.Controls.Add(PointColorButton);

                var LineColorButton = new Button();
                LineColorButton.Text = "Линии";
                LineColorButton.Location = new Point(10, 50);
                LineColorButton.Click += (s, args) =>
                {
                    using (var colorDialog = new ColorDialog())
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            colorPanelLine.BackColor = colorDialog.Color;
                            figures[actualFigure].LineColor = colorDialog.Color;
                        }
                    }
                };
                dialog.Controls.Add(LineColorButton);

                var exitButton = new Button();
                exitButton.Text = "Закрыть";
                exitButton.Location = new Point(40, 80);
                exitButton.Click += (s, args) =>
                {
                    dialog.Close();
                };
                dialog.Controls.Add(exitButton);

                dialog.ShowDialog();
            }
            this.Refresh();
        }

        private void CurveButton_Click(object sender, EventArgs e)
        {
            figures[actualFigure].JoinType = JoinType.DrawClosedCurve;
            this.Refresh();
        }

        private void PolylineButton_Click(object sender, EventArgs e)
        {
            figures[actualFigure].JoinType = JoinType.DrawPolygone;
            this.Refresh();
        }


        private void BezierButton_Click(object sender, EventArgs e)
        {
           
            figures[actualFigure].JoinType = JoinType.DrawBeziers;
            this.Refresh();
        }

        private void FilledButton_Click(object sender, EventArgs e)
        {
            figures[actualFigure].JoinType = JoinType.FillCurve;
            this.Refresh();
        }

        private bool moving = false;
        private void MoveButton_Click(object sender, EventArgs e)
        {
            moving = !moving;
        }

        private void SaveButton_Click(Object sender, EventArgs e)
        {
            figures.Add(new Figure());
            actualFigure++;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            figures.Clear();
            figures.Add(new Figure());
            actualFigure = 0;
            this.Refresh();
        }

        private void MoveUp()
        {
            figures[actualFigure].moveUp(speed, this.ClientSize.Height);
            this.Refresh();
        }

        private void MoveDown()
        {
            figures[actualFigure].moveDown(speed, this.ClientSize.Height);
            this.Refresh();
        }

        private void MoveRight()
        {
            figures[actualFigure].moveRight(speed, this.ClientSize.Width);
            this.Refresh();
        }

        private void MoveLeft()
        {
            figures[actualFigure].moveLeft(speed, this.ClientSize.Width);
            this.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    MoveButton_Click(sender, e);
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    speed++;
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    if (speed > 1)
                    {
                        speed--; 
                    }
                    break;
                case Keys.Escape:
                    ClearButton_Click(sender, e);
                    break;
            }
            e.Handled = true;
        }

        #endregion

        private Button button1;
    }
}
