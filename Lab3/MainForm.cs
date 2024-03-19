
using Timer = System.Windows.Forms.Timer;

namespace Lab3
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            Timer moveTimer = new Timer();
            moveTimer.Interval = 30;
            moveTimer.Tick += new EventHandler(TimerTickHandler);
            moveTimer.Start();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    MoveUp();
                    return true;
                case Keys.Down:
                    MoveDown();
                    return true;
                case Keys.Right:
                    MoveRight();
                    return true;
                case Keys.Left:
                    MoveLeft();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
