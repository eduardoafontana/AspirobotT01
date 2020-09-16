using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspirobotT01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Engine.Init();

            Engine.environment.RaiseDoorOpening += new Environment.OpeningDoor(door_OnDoorOpeningEnv);
            Engine.robot.RaiseDoorOpening += new Robot.OpeningDoor(door_OnDoorOpeningRobot);

            Engine.Start();
        }

        private void door_OnDoorOpeningEnv(string count)
        {
            Console.WriteLine("enviroment event");

            Invoke(new Action(() =>
            {
                label1.Text = count;
            }));
        }

        private void door_OnDoorOpeningRobot(string count)
        {
            Console.WriteLine("robot event");

            Invoke(new Action(() =>
            {
                label2.Text = count;
            }));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Engine.Stop();
        }
    }
}
