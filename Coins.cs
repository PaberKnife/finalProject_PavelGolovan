using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject_PavelGolovan
{
    class Coins : Points
    {
        public Coins()
        {
            Name = "Coins";
            Amount = 0;
            ActiveIncome = 1;
            PassiveIncome = 0;
        }

        public int Purchase(int cost)
        {
            Amount -= cost;
            return Amount;
        }
    }
}