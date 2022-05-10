using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;
using OrdersPage;
using Accountpage;

namespace OrdersPage
{
    public class MarketBar : Label
    {

        string customerName ;
        string balance ;

        OrdersPage ordersPage = new OrdersPage();

        AccountPage accountPage = new AccountPage();


        RoundLabel label = new RoundLabel();

        Label labelName = new Label();

        Label balanceLabel = new Label();

        RoundLabel label1 = new RoundLabel();

        RJButton ordersButton = new RJButton();
        RJButton acountButton = new RJButton();

        public MarketBar(string customerName,string balance)
        {
            this.customerName=customerName;
            this.balance=balance;
            baseCreator();
        }

        private void baseCreator()
        {

            this.Size = new Size(800, 70);
            this.Location = new Point(15, 5);
            this.BackColor = Color.FromArgb(255, 230, 204);

            //balanceLabel label
            label.Location = new Point(165, 10);
            label.Size = new Size(610, 55);
            label.BackColor = Color.FromArgb(255, 230, 204);
            label.borderColor = Color.Black;
            label.cornerRadius = 20;
            label.borderWidth = 2;
            label.BringToFront();
            Controls.Add(label);

            //balance label texts
            //name label
            labelName.Location = new Point(10, 8);
            labelName.Size = new Size(110, 40);
            labelName.BackColor = Color.FromArgb(255, 230, 204);
            labelName.Font = new Font("Arial", 11, FontStyle.Regular);
            labelName.Text = customerName;
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            labelName.BringToFront();
            label.Controls.Add(labelName);

            //balance label
            balanceLabel.Location = new Point(125, 8);
            balanceLabel.Size = new Size(150, 40);
            balanceLabel.BackColor = Color.FromArgb(255, 230, 204);
            balanceLabel.Font = new Font("Arial", 8, FontStyle.Regular);
            balanceLabel.Text = "balance: " + balance + "$";
            balanceLabel.TextAlign = ContentAlignment.MiddleCenter;
            balanceLabel.BringToFront();
            label.Controls.Add(balanceLabel);



            //orders button
            ordersButton.Location = new Point(510, 6);
            ordersButton.Size = new Size(80, 40);
            ordersButton.Text = "orders";
            ordersButton.Font = new Font("Arial", 8, FontStyle.Regular);
            ordersButton.BorderRadius = 10;
            ordersButton.BorderColor = Color.Black;
            ordersButton.BorderSize = 1;
            ordersButton.ForeColor = Color.Black;
            ordersButton.BackColor = BackColor;
            ordersButton.Click += delegate (object sender, EventArgs e) { ordersButtonFunction(); };
            label.Controls.Add(ordersButton);
            ordersButton.BringToFront();

            //acount button
            acountButton.Location = new Point(420, 6);
            acountButton.Size = new Size(80, 40);
            acountButton.Text = "account";
            acountButton.Font = new Font("Arial", 8, FontStyle.Regular);
            acountButton.BorderRadius = 10;
            acountButton.BorderColor = Color.Black;
            acountButton.BorderSize = 1;
            acountButton.ForeColor = Color.Black;
            acountButton.BackColor = BackColor;
            acountButton.Click += delegate (object sender, EventArgs e) { accountButtonFunction();};
            label.Controls.Add(acountButton);
            acountButton.BringToFront();
        }

        private void accountButtonFunction()
        {
            accountPage.ShowDialog();
        }

        private void ordersButtonFunction()
        {
            ordersPage.ShowDialog();
        }
    }
}