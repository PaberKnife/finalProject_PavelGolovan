using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject_PavelGolovan
{
    class Distance : Points
    {
        public Distance()
        {
            Name = "Distance";
            Amount = 0;
            ActiveIncome = 1;
            PassiveIncome = 0;
        }

        public bool Progress(PictureBox dot, bool theEnd)
        {
            if (Amount <= 384400 && theEnd == false)
            {
                dot.Location = new Point(119 + (int)(Amount / 500), 102);
                return false;
            }
            else
            {
                dot.Location = new Point(886, 102);
                MessageBox.Show("Congratulations! You've reached the moon.");
                return true;
            } 
        }
    }
}