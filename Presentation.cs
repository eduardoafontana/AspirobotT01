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
    public partial class Presentation : Form
    {
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        public Presentation()
        {
            InitializeComponent();

            CreateEnvironmentPresentation();

            CreateEnvironmentElements();

            Engine.Init();

            Engine.environment.RaiseChangeEnvironment += new Environment.ChangingEnvironmentActuator(presentation_OnEnvironmentChange);
            Engine.robot.robotDisplay.RaiseDisplayRobot += new RobotDisplay.DisplayingRobotActuator(presentation_OnRobotDisplayChange);

            Engine.Start();
        }

        private void presentation_OnRobotDisplayChange(RobotDisplay robotDisplay)
        {
            Invoke(new Action(() =>
            {
                lblElectricity.Text = robotDisplay.Electricity.ToString();
            }));
        }

        private void presentation_OnEnvironmentChange(List<Place> places)
        {
            Invoke(new Action(() =>
            {
                //label1.Text = places.Where(x => x.element != null).Count().ToString();
                //int i = 0;
                //int j = 0;
                //place = String.Format("{0}{1} -> {2}:{3}{4}", place, count, i, j, System.Environment.NewLine);
                //i++;
                //if (i >= Config.environmentDimension)
                //{
                //    i = 0;
                //    j++;
                //}
                //label3.Text = place;

                int count = 0;

                string place = String.Empty;

                foreach (var item in places)
                {
                    TableLayoutPanel subPanel = (TableLayoutPanel)tableLayoutPanel1.Controls.Find("subPanel" + count.ToString(), false)[0];
                    TableLayoutPanel subSubPanel = (TableLayoutPanel)subPanel.Controls.Find("subSubPanel" + count.ToString(), false)[0];

                    PictureBox pictureDirty = (PictureBox)subSubPanel.Controls.Find("dirty" + count.ToString(), false)[0];

                    if (item.dirty != null)
                    {
                        pictureDirty.ImageLocation = item.dirty.ImagePath;
                        pictureDirty.Visible = true;
                    }
                    else
                    {
                        pictureDirty.Visible = false;
                    }

                    PictureBox pictureJewel = (PictureBox)subSubPanel.Controls.Find("jewel" + count.ToString(), false)[0];

                    if (item.jewel != null)
                    {
                        pictureJewel.ImageLocation = item.jewel.ImagePath;
                        pictureJewel.Visible = true;
                    }
                    else
                    {
                        pictureJewel.Visible = false;
                    }

                    PictureBox pictureRobot = (PictureBox)subPanel.Controls.Find("robot" + count.ToString(), false)[0];

                    if (item.robot != null)
                    {
                        pictureRobot.ImageLocation = item.robot.ImagePath;
                        pictureRobot.Visible = true;
                    }
                    else
                    {
                        pictureRobot.Visible = false;
                    }

                    count++;
                }
            }));
        }

        private void CreateEnvironmentPresentation()
        {
            int placeSize = Config.environmentPlaceSize * Config.environmentDimension;

            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();

            tableLayoutPanel1.BackColor = System.Drawing.Color.Gainsboro;
            tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = Config.environmentDimension;
            tableLayoutPanel1.RowCount = Config.environmentDimension;
            tableLayoutPanel1.Size = new System.Drawing.Size(placeSize + 10, placeSize + 10);
            tableLayoutPanel1.Location = new System.Drawing.Point(30, 50);
            tableLayoutPanel1.Name = "tableLayoutPanel1";

            for (int e = 0; e < Config.environmentDimension; e++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, Config.environmentPlaceSize));
                tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, Config.environmentPlaceSize));
            }

            this.Controls.Add(tableLayoutPanel1);

            int i = 0;
            int j = 0;

            for (int c = 0; c < Config.environmentSize; c++)
            {
                TableLayoutPanel tableLayoutPanel3 = new TableLayoutPanel();
                tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
                tableLayoutPanel3.ColumnCount = 2;
                tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel3.Name = "subSubPanel" + c.ToString();
                tableLayoutPanel3.RowCount = 1;
                tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
                tableLayoutPanel3.Margin = Padding.Empty;

                TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
                tableLayoutPanel2.ColumnCount = 1;
                tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel2.Name = "subPanel" + c.ToString();
                tableLayoutPanel2.RowCount = 2;
                tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
                tableLayoutPanel2.Margin = Padding.Empty;
                tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 1);

                tableLayoutPanel1.Controls.Add(tableLayoutPanel2, i, j);

                i++;

                if (i >= Config.environmentDimension)
                {
                    i = 0;
                    j++;
                }
            }
        }

        private void CreateEnvironmentElements()
        {
            int c = 0;

            foreach (TableLayoutPanel subPanel in tableLayoutPanel1.Controls)
            {
                TableLayoutPanel subSubPanel = (TableLayoutPanel)subPanel.Controls.Find("subSubPanel" + c.ToString(), false)[0];

                PictureBox pictureBox1 = new PictureBox();
                ((ISupportInitialize)(pictureBox1)).BeginInit();
                pictureBox1.InitialImage = null;
                pictureBox1.Name = "dirty" + c.ToString();
                pictureBox1.Size = new System.Drawing.Size(Config.elementSize, Config.elementSize);
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox1.Anchor = AnchorStyles.None;
                pictureBox1.Visible = false;
                subSubPanel.Controls.Add(pictureBox1, 0, 0);
                ((ISupportInitialize)(pictureBox1)).EndInit();


                PictureBox pictureBox2 = new PictureBox();
                ((ISupportInitialize)(pictureBox2)).BeginInit();
                pictureBox2.InitialImage = null;
                pictureBox2.Name = "jewel" + c.ToString();
                pictureBox2.Size = new System.Drawing.Size(Config.elementSize, Config.elementSize);
                pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox2.Anchor = AnchorStyles.None;
                pictureBox1.Visible = false;
                subSubPanel.Controls.Add(pictureBox2, 1, 0);
                ((ISupportInitialize)(pictureBox2)).EndInit();

                PictureBox pictureBox3 = new PictureBox();
                ((ISupportInitialize)(pictureBox3)).BeginInit();
                pictureBox3.InitialImage = null;
                pictureBox3.Name = "robot" + c.ToString();
                pictureBox3.Size = new System.Drawing.Size(Config.elementSize, Config.elementSize);
                pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox3.Anchor = AnchorStyles.None;
                pictureBox3.Visible = false;
                subPanel.Controls.Add(pictureBox3, 0, 0);
                ((ISupportInitialize)(pictureBox3)).EndInit();

                c++;
            }
        }

        private void Presentation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Engine.Stop();
        }
    }
}
