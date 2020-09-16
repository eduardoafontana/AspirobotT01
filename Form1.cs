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
using static AspirobotT01.Environment;

namespace AspirobotT01
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        public Form1()
        {
            InitializeComponent();

            CreateEnvironmentPresentation();

            CreateEnvironmentElements();

            Engine.Init();

            Engine.environment.RaiseAddElement += new Environment.AddingElementActuator(environment_OnAddElement);
            Engine.robot.RaiseDoorOpening += new Robot.OpeningDoor(door_OnDoorOpeningRobot);

            Engine.Start();
        }

        private void environment_OnAddElement(List<Place> places, int position)
        {
            Invoke(new Action(() =>
            {
                label1.Text = places.Where(x => x.element != null).Count().ToString();

                foreach (Control item in tableLayoutPanel1.Controls)
                {
                    item.Visible = false;
                }

                int i = 0;
                int j = 0;
                int count = 0;

                string place = String.Empty;

                foreach (var item in places)
                {
                    if (item.element != null)
                    {
                        tableLayoutPanel1.Controls.Find("element"+count.ToString(), false)[0].Visible = true;

                        place = String.Format("{0}{1} -> {2}:{3}{4}", place, count, i, j, System.Environment.NewLine);
                    }

                    count++;
                    i++;

                    if (i >= Config.environmentDimension)
                    {
                        i = 0;
                        j++;
                    }
                }

                label3.Text = place;
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

        private void CreateEnvironmentPresentation()
        {
            int placeSize = Config.environmentPlaceSize * Config.environmentDimension;

            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();

            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = Config.environmentDimension;
            this.tableLayoutPanel1.RowCount = Config.environmentDimension;
            this.tableLayoutPanel1.Size = new System.Drawing.Size(placeSize, placeSize);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.TabIndex = 2;

            for (int i = 0; i < Config.environmentDimension; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, Config.environmentPlaceSize));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Config.environmentPlaceSize));
            }

            this.Controls.Add(this.tableLayoutPanel1);
        }

        private void CreateEnvironmentElements()
        {
            int i = 0;
            int j = 0;

            for (int c = 0; c < Config.environmentSize; c++)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.Name = "element" + c.ToString();
                label.Size = new System.Drawing.Size(35, 13);
                label.Text = "A" + c.ToString();
                label.Visible = false;

                tableLayoutPanel1.Controls.Add(label, i, j);

                i++;

                if (i >= Config.environmentDimension)
                {
                    i = 0;
                    j++;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Engine.Stop();
        }
    }
}
