using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

namespace Stock
{

    public class Stocks:Form
    {
        RoundLabel label = new RoundLabel();
        RJButton ordersButton = new RJButton();

        Font LargeFont = new Font("Arial", 13, FontStyle.Italic);
        RoundLabel Border = new RoundLabel();
        string[] catagories = { "woman", "man", "chıld" };



        public Stocks(){


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
        }

        private void contentCreator(){
            //here we will code new widget which can store picture up, product name, product stock number and also product current price and we will use call this widget with parameters.
            


        }
    }
}