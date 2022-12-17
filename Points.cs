using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace finalProject_PavelGolovan
{
    class Points
    {
        private string _name;
        private int _amount;
        private int _activeIncome;
        private int _passiveIncome;

        public Points()
        {
            _name = "";
            _amount = -1;
            _activeIncome = -1;
            _passiveIncome = -1;
        }

        public int Active()
        {
            _amount += _activeIncome;
            return _amount;
        }

        public async void Passive()
        {
            while (true)
            {
                await Task.Delay(1000);
                _amount += _passiveIncome;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int ActiveIncome
        {
            get { return _activeIncome; }
            set { _activeIncome = value; }
        }

        public int PassiveIncome
        {
            get { return _passiveIncome; }
            set { _passiveIncome = value; }
        }
    }
}