using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject_PavelGolovan
{
    class ActiveUpgrade : Upgrade
    {
        public ActiveUpgrade()
        {
            Type = "Active";
            Amount = 0;
        }

        public void Purchase(Coins coins, Distance distance)
        {
            if (coins.Amount >= Cost)
            {
                coins.Purchase(Cost);
                coins.ActiveIncome += Power;
                distance.ActiveIncome += Power;
                Cost = (int)(Cost * Multiplier);
                Amount += 1;
            }
            else
            {
                MessageBox.Show("You don't have enough money.");
            }
        }
    }
}