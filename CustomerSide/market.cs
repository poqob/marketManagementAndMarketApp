using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace CustomerSide
{
    public class Market : Form
    {   //variables.
        FlowLayoutPanel panel = new FlowLayoutPanel();
        RoundLabel lable0 = new RoundLabel();//name label
        RoundLabel label1 = new RoundLabel();//market, balance and buttons label
        RoundLabel label2 = new RoundLabel();//ordered products label, i'm thinking make flow panel or panle there
        string[] catagories = { "woman", "man", "chıld" };
        string customerName;
        public Market(ref string customerName)
        {
            this.customerName = customerName;
            baseCreator();
        }

        private void baseCreator()
        {
            //local variables
            Point point = new Point(60, 60);
            BackColor = Color.FromArgb(38, 38, 38);
            Font ItalicFont = new Font("Arial", 12, FontStyle.Italic);

            //MainBorder
            Border.Size = new Size(740, 420);
            Border.Location = new Point(20, 20);
            Border.backColor = BackColor;
            Border.borderColor = Color.White;
            Border.borderWidth = 3;
            Border.cornerRadius = 45;
            Border.ForeColor = Color.White;
            Border.Tag = "base";
            Controls.Add(Border);


            //creating catagory buttons : child, woman, man
            foreach (string catagory in catagories)
            {
                RJButton menuButton = new RJButton();
                menuButton.Size = new Size(110, 90);
                menuButton.Location = point;
                menuButton.BackColor = Color.FromArgb(38, 38, 38);
                menuButton.BorderColor = Color.White;
                menuButton.BorderRadius = 15;
                menuButton.BorderSize = 2;
                menuButton.Text = catagory;
                menuButton.Font = ItalicFont;
                menuButton.Click += delegate (object sender, EventArgs e) {  };
                Controls.Add(menuButton);
                menuButton.BringToFront();
                point.Y += 120;
            }

            //flow panel that keeps our widgets.

            panel.Location = new Point(200, 40);
            panel.Size = new Size(540, 380);
            panel.BackColor = BackColor;
            panel.FlowDirection = FlowDirection.LeftToRight;

            panel.AutoScroll = true;

            panel.WrapContents = true;
            Controls.Add(panel);
            panel.BringToFront();
        }
    }
}

//LOOK TODO FIle