using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject_PavelGolovan
{
    class PassiveUpgrade : Upgrade
    {
        public PassiveUpgrade()
        {
            Type = "Passive";
            Amount = 0;
        }

        public void Purchase(Coins coins, Distance distance)
        {
            if (coins.Amount >= Cost)
            {
                coins.Purchase(Cost);
                coins.PassiveIncome += Power;
                distance.PassiveIncome += Power;
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