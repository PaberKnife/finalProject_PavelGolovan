using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject_PavelGolovan
{
    class Upgrade
    {
        private string _name;
        private string _type;
        private int _cost;
        private int _amount;
        private int _power;
        private double _multiplier;

        public Upgrade()
        {
            _name = "";
            _type = "";
            _cost = -1;
            _amount = -1;
            _power = -1;
            _multiplier = -1;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        public double Multiplier
        {
            get { return _multiplier; }
            set { _multiplier = value; }
        }
    }
}