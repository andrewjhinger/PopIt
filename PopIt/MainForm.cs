using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopIt
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Set up progress bar
            remainingToolStripProgressBar.Minimum = 0;
            remainingToolStripProgressBar.Maximum = _timerMilliseconds;
            remainingToolStripProgressBar.Step = processTimer.Interval;
        }

        PoppersList _poppersList = new PoppersList();           // Poppers list
        private Color _popperColor = Color.Red;                 // Popper color
        private int _hits = 0;                                  // Hit count
        private int _popperCount = 5;                           // Number of poppers
        private bool _togglePopper = false;                     // A toggle for determining which popper to display
        private int _timerMilliseconds = 2000;                  // Refresh time
        private int _popperSize = 30;                           // Size of our popper
        private int _maximumPoppers = 10;                       // Maximum number of poppers
        Random _random = new Random();                          // Random generator

        private void addPoppers()
        {
            // Add until we reach the current maximum
            while (_poppersList.Length < _popperCount)
            {
                // Generate random x and y positions (don't worry if they overlap)
                int x = _random.Next(0, mainPictureBox.ClientRectangle.Width - _popperSize - 1);
                int y = _random.Next(0, mainPictureBox.ClientRectangle.Height - _popperSize - 1);

                // Create a rectangle from 
                Rectangle rectangle = new Rectangle(x, y, _popperSize, _popperSize);
                if (_togglePopper)
                    _poppersList.Add(new StringPopper(rectangle, _popperColor));
                else
                    _poppersList.Add(new OvalPopper(rectangle, _popperColor));

                _togglePopper = !_togglePopper;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the application
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            addPoppers();
            mainPictureBox.Invalidate();
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Disable timer
            processTimer.Enabled = false;

            // Check all poppers to see if we hit
            for (int i = 0; i < _poppersList.Length; i++)
            {
                if (_poppersList[i].Hit(new Point(e.X, e.Y)))
                {
                    // Update status label with hits using pre-increment
                    popsToolStripStatusLabel.Text = "Pops: " + (++_hits).ToString();
                    // Delete popper from list
                    _poppersList.Delete(_poppersList[i]);
                }
            }
            // Re-enable timer and force repaint
            processTimer.Enabled = true;
            mainPictureBox.Invalidate();
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Clear everything 
            e.Graphics.Clear(mainPictureBox.BackColor);

            // Draw all poppers
            for (int i = 0; i < _poppersList.Length; i++)
                _poppersList[i].Draw(e.Graphics);
        }

        private void processTimer_Tick(object sender, EventArgs e)
        {
            // Disable timer
            processTimer.Enabled = false;

            // Determine next progress bar value
            int value = remainingToolStripProgressBar.Value - remainingToolStripProgressBar.Step;

            // Prevent progress bar value from going below 0, and update progress bar
            if (value < 0) value = 0;
            remainingToolStripProgressBar.Value = value;

            // Only process if we are short any poppers, and we've reached the end of the timer
            if (_poppersList.Length < _popperCount && value == 0)
            {
                if (_poppersList.Length == 0)
                {
                    // Only increment popper count if cleared within time frame
                    if (_popperCount <= _maximumPoppers) _popperCount += 1;
                }
                // Add necessary poppers
                addPoppers();
            }
            // Reset progress bar
            if (value == 0) remainingToolStripProgressBar.Value = remainingToolStripProgressBar.Maximum;

            // Re-enable timer and force repaint
            processTimer.Enabled = true;
            mainPictureBox.Invalidate();
        }
    }
}
