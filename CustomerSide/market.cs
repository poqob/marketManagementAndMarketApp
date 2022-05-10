using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace OrdersPage
{
    public class Market : Form
    {   //variables.
        RJButton menuButton = new RJButton();
        FlowLayoutPanel panel = new FlowLayoutPanel();
        RoundLabel lable0 = new RoundLabel();//ordered products label, i'm thinking make flow panel or panle there
        RoundLabel label1 = new RoundLabel();//market, balance and buttons label

        string[] catagories = { "woman", "man", "chıld" };
        string customerName;
        string folderAdress;
        public Market(ref string customerName,ref string folderAdress)
        {
            this.customerName = customerName;
            this.folderAdress=folderAdress;
            baseCreator();
        }

        private void baseCreator()
        {
            //local variables
            Point point = new Point(20, 85);
            BackColor = Color.FromArgb(255, 230, 204);
            Font ItalicFont = new Font("Arial", 12, FontStyle.Italic);

            //marketBorder
            lable0.Size = new Size(610, 340);
            lable0.Location = new Point(180, 85);
            lable0.backColor = BackColor;
            lable0.borderColor = Color.Black;
            lable0.borderWidth = 2;
            lable0.cornerRadius = 60;
            lable0.ForeColor = Color.Black;
            lable0.Tag = "base";
            Controls.Add(lable0);

            //order section
            label1.Size = new Size(140, 340);
            label1.Location = point;
            label1.backColor = BackColor;
            label1.borderColor = Color.Black;
            label1.borderWidth = 2;
            label1.cornerRadius = 60;
            label1.ForeColor = Color.Black;
            label1.Tag = "base";
            Controls.Add(label1);


            //order button
            menuButton.Size = new Size(100, 60);
            menuButton.Location = new Point(40, 350);
            menuButton.BackColor = Color.FromArgb(255, 230, 204);
            menuButton.BorderColor = Color.Black;
            menuButton.BorderRadius = 15;
            menuButton.BorderSize = 1;
            menuButton.Text = "order";
            menuButton.Font = ItalicFont;
            menuButton.ForeColor=Color.Black;
            menuButton.Click += delegate (object sender, EventArgs e) { };
            Controls.Add(menuButton);
            menuButton.BringToFront();


            //flow panel that keeps our widgets.

            panel.Location = new Point(195, 100);
            panel.Size = new Size(580, 300);
            panel.BackColor = BackColor;
            panel.FlowDirection = FlowDirection.LeftToRight;

            panel.AutoScroll = true;

            panel.WrapContents = true;
            Controls.Add(panel);
            panel.BringToFront();

            MarketBar marketBar=new MarketBar(customerName,getBalance(ref folderAdress));

            //app bar
            Controls.Add(marketBar);
        }

        //to get balance value from customer information file.
        private string getBalance(ref string folderAdress){

            
            return "2000";
        }
    }
}

//LOOK TODO FIle