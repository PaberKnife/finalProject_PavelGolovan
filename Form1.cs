using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProject_PavelGolovan
{
    public partial class Form1 : Form
    {
        bool instanceCount = false;
        bool theEnd = false;

        Distance distance = new Distance();
        Coins coins = new Coins();

        ActiveUpgrade[] activeUpgrades = new ActiveUpgrade[3];
        PassiveUpgrade[] passiveUpgrades = new PassiveUpgrade[2];

        public Form1()
        {
            InitializeComponent();
        }

        private async void boost()
        {
            if (instanceCount == false)
            {
                instanceCount = true;
                rocketPictureBox.Image = Properties.Resources.rocketActive;

                await Task.Delay(1000);

                rocketPictureBox.Image = Properties.Resources.rocket;
                instanceCount = false;
            }
        }

        private async void animation(int x, int y)
        {
            while (true)
            {
                await Task.Delay(30);
                if (x >= -7868 && y <= 701)
                {
                    x -= 1;
                    y += 1;
                    animatedBackgroundPictureBox.Location = new Point(x, y);
                }
                else
                {
                    x = 800;
                    y = -6878;
                    animatedBackgroundPictureBox.Location = new Point(x, y);
                }
            }
        }

        private async void update()
        {
            while (true)
            {
                await Task.Delay(500);

                distanceLabel.Text = distance.Amount.ToString() + "      KM";
                coinsLabel.Text = coins.Amount.ToString();

                if (theEnd == false)
                {
                    theEnd = distance.Progress(dotPictureBox, theEnd);
                }

                autopilotAmountLabel.Text = passiveUpgrades[0].Amount.ToString();
                autopilotCostLabel.Text = passiveUpgrades[0].Cost.ToString() + " $";
                autopilotPowerLabel.Text = "+ " + (passiveUpgrades[0].Power * passiveUpgrades[0].Amount).ToString() + " KM/S";

                astronautAmountLabel.Text = activeUpgrades[0].Amount.ToString();
                astronautCostLabel.Text = activeUpgrades[0].Cost.ToString() + " $";
                astronautPowerLabel.Text = "+ " + (activeUpgrades[0].Power * activeUpgrades[0].Amount).ToString() + " KM/CLICK";

                nuclearFuelAmountLabel.Text = activeUpgrades[1].Amount.ToString();
                nuclearFuelCostLabel.Text = activeUpgrades[1].Cost.ToString() + " $";
                nuclearFuelPowerLabel.Text = "+ " + (activeUpgrades[1].Power * activeUpgrades[1].Amount).ToString() + " KM/CLICK";

                aerodynamicsAmountLabel.Text = passiveUpgrades[1].Amount.ToString();
                aerodynamicsCostLabel.Text = passiveUpgrades[1].Cost.ToString() + " $";
                aerodynamicsPowerLabel.Text = "+ " + (passiveUpgrades[1].Power * passiveUpgrades[1].Amount).ToString() + " KM/S";

                alienTechnologyAmountLabel.Text = activeUpgrades[2].Amount.ToString();
                alienTechnologyCostLabel.Text = activeUpgrades[2].Cost.ToString() + " $";
                alienTechnologyPowerLabel.Text = "+ " + (activeUpgrades[2].Power * activeUpgrades[2].Amount).ToString() + " KM/CLICK";
            }
        }

        private void saveGame()
        {
            StreamWriter outputFile;

            outputFile = File.AppendText("savefile.sf");
            outputFile.WriteLine(distance.Name + ":" + distance.Amount.ToString() + "," + distance.ActiveIncome.ToString() + "," + distance.PassiveIncome.ToString() + "\n" +
                coins.Name + ":" + coins.Amount.ToString() + "," + coins.ActiveIncome.ToString() + "," + coins.PassiveIncome.ToString());
            foreach (ActiveUpgrade upgrade in activeUpgrades)
            {
                outputFile.WriteLine(upgrade.Name + ":" + upgrade.Type + "," + upgrade.Cost.ToString() + "," + upgrade.Amount.ToString() + "," + upgrade.Power.ToString());
            }
            foreach (PassiveUpgrade upgrade in passiveUpgrades)
            {
                outputFile.WriteLine(upgrade.Name + ":" + upgrade.Type + "," + upgrade.Cost.ToString() + "," + upgrade.Amount.ToString() + "," + upgrade.Power.ToString());
            }

            outputFile.Close();

            MessageBox.Show("The game was succesfully saved!");
        }

        private void loadGame(string filename)
        {
            while (true)
            {
                StreamReader inputFile;

                string line;
                char[] delim = { ':', ',' };

                try
                {
                    inputFile = File.OpenText(filename);
                }
                catch
                {
                    try
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            loadGame(openFileDialog.FileName);
                        }
                    }
                    catch
                    {
                        break;
                    }
                    break;
                }

                try
                {
                    // Load Distance:
                    line = inputFile.ReadLine();
                    string[] tokens = line.Split(delim);
                    distance.Amount = int.Parse(tokens[1]);
                    distance.ActiveIncome = int.Parse(tokens[2]);
                    distance.PassiveIncome = int.Parse(tokens[3]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }

                try
                {
                    // Load Coins:
                    line = inputFile.ReadLine();
                    string[] tokens = line.Split(delim);
                    coins.Amount = int.Parse(tokens[1]);
                    coins.ActiveIncome = int.Parse(tokens[2]);
                    coins.PassiveIncome = int.Parse(tokens[3]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }

                foreach (ActiveUpgrade upgrade in activeUpgrades)
                {
                    // Load Active Upgrades:
                    line = inputFile.ReadLine();
                    string[] tokens = line.Split(delim);
                    upgrade.Cost = int.Parse(tokens[2]);
                    upgrade.Amount = int.Parse(tokens[3]);
                    upgrade.Power = int.Parse(tokens[4]);
                }

                foreach (PassiveUpgrade upgrade in passiveUpgrades)
                {
                    // Load Passive Upgrades:
                    line = inputFile.ReadLine();
                    string[] tokens = line.Split(delim);
                    upgrade.Cost = int.Parse(tokens[2]);
                    upgrade.Amount = int.Parse(tokens[3]);
                    upgrade.Power = int.Parse(tokens[4]);
                }
                MessageBox.Show("The game was succesfully loaded!");
                inputFile.Close();
                break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrivateFontCollection customFont = new PrivateFontCollection();
            customFont.AddFontFile("DREAMS.ttf");

            backgroundPictureBox.Controls.Add(rocketPictureBox);
            backgroundPictureBox.Controls.Add(animatedBackgroundPictureBox);

            // Upgrades Settings:
            activeUpgrades[0] = new ActiveUpgrade();
            activeUpgrades[0].Name = "Astronaut";
            activeUpgrades[0].Cost = 1000;
            activeUpgrades[0].Power = 10;
            activeUpgrades[0].Multiplier = 1.4;

            activeUpgrades[1] = new ActiveUpgrade();
            activeUpgrades[1].Name = "Nuclear Fuel";
            activeUpgrades[1].Cost = 5000;
            activeUpgrades[1].Power = 50;
            activeUpgrades[1].Multiplier = 1.6;

            activeUpgrades[2] = new ActiveUpgrade();
            activeUpgrades[2].Name = "Alien Technology";
            activeUpgrades[2].Cost = 50000;
            activeUpgrades[2].Power = 200;
            activeUpgrades[2].Multiplier = 2;

            passiveUpgrades[0] = new PassiveUpgrade();
            passiveUpgrades[0].Name = "Autopilot";
            passiveUpgrades[0].Cost = 100;
            passiveUpgrades[0].Power = 50;
            passiveUpgrades[0].Multiplier = 1.2;

            passiveUpgrades[1] = new PassiveUpgrade();
            passiveUpgrades[1].Name = "Aerodynamics";
            passiveUpgrades[1].Cost = 10000;
            passiveUpgrades[1].Power = 2000;
            passiveUpgrades[1].Multiplier = 1.8;

            coins.Passive();
            distance.Passive();
            animation(800,-6878);
            update();
        }

        private void rocketPictureBox_Click(object sender, EventArgs e)
        {
            boost();
            distance.Active();
            coins.Active();
            distanceLabel.Text = distance.Amount.ToString() + "      KM";
            coinsLabel.Text = coins.Amount.ToString();
        }

        private void autoPilotPictureBox_Click(object sender, EventArgs e)
        {
            passiveUpgrades[0].Purchase(coins, distance);
        }

        private void astronautPictureBox_Click(object sender, EventArgs e)
        {
            activeUpgrades[0].Purchase(coins, distance);
        }

        private void nuclearFuelPictureBox_Click(object sender, EventArgs e)
        {
            activeUpgrades[1].Purchase(coins, distance);
        }

        private void aerodynamicsPictureBox_Click(object sender, EventArgs e)
        {
            passiveUpgrades[1].Purchase(coins, distance);
        }

        private void alienTechnologyPictureBox_Click(object sender, EventArgs e)
        {
            activeUpgrades[2].Purchase(coins, distance);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveGame();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            loadGame("savefile.sf");
            theEnd = false;
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PAVEL GOLOVAN 2022\n\n" +
                "DESCRIPTION\n" +
                "Hello and welcome to the 'Spaceflight' game!\n" +
                "The goal of this game is to fly from earth to the moon. (384 400 km)\n\n" +
                "RULES\n" +
                "Just click the space ship and travel towards your target.\n" +
                "With each click you travel 1 km.\n" +
                "With each travelled km you gain 1$.\n\n" +
                "UPGRADES\n" +
                "Buy upgrades to speed things up.\n" +
                "There are two kinds of upgrades: Active and Passive.\n" +
                "Active upgrades make your click more powerful.\n" +
                "Passive upgrades make your spaceship travel automatically.\n" +
                "With each purchased upgrade, it becomes more expensive.");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}