using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace OrdersPage
{



    public class OrdersPage : Form
    {

        string customerName = "HOOMAN";
        string balance = "1000";

        RoundLabel label2 = new RoundLabel();

        Label labelName = new Label();

        Label balanceLabel = new Label();

        RoundLabel label1 = new RoundLabel();
        RoundLabel lable0 = new RoundLabel();

        FlowLayoutPanel panel = new FlowLayoutPanel();


        public OrdersPage()
        {

            this.Size = new Size(800, 500);
            this.CenterToParent();
            this.BackColor = Color.FromArgb(255, 230, 204);
            baseCreator();



        }
        private void baseCreator()
        {

            this.Size = new Size(800, 500);
            this.Location = new Point(15, 5);
            this.BackColor = Color.FromArgb(255, 230, 204);


            //balanceLabel label
            label2.Location = new Point(165, 10);
            label2.Size = new Size(610, 55);
            label2.BackColor = Color.FromArgb(255, 230, 204);
            label2.borderColor = Color.Black;
            label2.cornerRadius = 20;
            label2.borderWidth = 2;
            label2.BringToFront();
            Controls.Add(label2);


            //balance label texts
            //name label
            labelName.Location = new Point(10, 8);
            labelName.Size = new Size(110, 40);
            labelName.BackColor = Color.FromArgb(255, 230, 204);
            labelName.Font = new Font("Arial", 11, FontStyle.Regular);
            labelName.Text = customerName;
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            labelName.BringToFront();
            label2.Controls.Add(labelName);

            //balance label
            balanceLabel.Location = new Point(125, 8);
            balanceLabel.Size = new Size(150, 40);
            balanceLabel.BackColor = Color.FromArgb(255, 230, 204);
            balanceLabel.Font = new Font("Arial", 8, FontStyle.Regular);
            balanceLabel.Text = "balance: " + balance + "$";
            balanceLabel.TextAlign = ContentAlignment.MiddleCenter;
            balanceLabel.BringToFront();
            label2.Controls.Add(balanceLabel);

            //order label
            label1.Location = new Point(5, 10);
            label1.Size = new Size(140, 55);
            label1.BackColor = Color.FromArgb(255, 230, 204); ;
            label1.borderColor = Color.Black;
            label1.borderWidth = 2;
            label1.cornerRadius = 20;
            label1.Text = "Orders";
            label1.Font = new Font("Arial", 12, FontStyle.Regular);
            label1.BringToFront();
            Controls.Add(label1);

            //marketBorder
            lable0.Size = new Size(770, 360);
            lable0.Location = new Point(5, 85);
            lable0.backColor = BackColor;
            lable0.borderColor = Color.Black;
            lable0.borderWidth = 2;
            lable0.cornerRadius = 60;
            lable0.ForeColor = Color.Black;
            lable0.Tag = "base";
            Controls.Add(lable0);

            panel.Location = new Point(20, 20);
            panel.Size = new Size(730, 320);
            panel.BackColor = BackColor;
            panel.WrapContents = true;
            panel.FlowDirection = FlowDirection.LeftToRight;
            lable0.Controls.Add(panel);

        }

        // load ordered products into panel from customer's order file.
        private void productLoader(){}




    }
}