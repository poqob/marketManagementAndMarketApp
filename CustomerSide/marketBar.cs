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

        string customerName;
        string balance;

        string folderAdress;

        RoundLabel marketLogo = new RoundLabel();

        RoundLabel label = new RoundLabel();

        Label labelName = new Label();

        Label balanceLabel = new Label();

        RJButton ordersButton = new RJButton();
        RJButton acountButton = new RJButton();

        public MarketBar(string customerName, string balance, ref string folderAdress)
        {
            this.customerName = customerName;
            this.balance = balance;
            fetchMoney(ref customerName);
            this.folderAdress = folderAdress;
            baseCreator();
        }

        public void fetchMoney(ref string customerName)
        {
            //indexes.
            int index0;
            //ManagerSide\datas\customers\gala\gala$info\data.txt //exmp adress

            string originalFile = @"ManagerSide\datas\customers\" + customerName + "\\" + customerName + "$info\\" + "data.txt";

            string temporaryContentFile = File.ReadAllText(originalFile);


            if (temporaryContentFile.Contains("&balance:"))
            {
                index0 = temporaryContentFile.IndexOf("&balance:");
                this.balance = temporaryContentFile.Substring(index0 + 9);
            }



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

            //marketLogo label
            marketLogo.Location = new Point(10, 12);
            marketLogo.Size = new Size(130, 52);
            marketLogo.cornerRadius = 20;
            marketLogo.borderColor = Color.Black;
            marketLogo.borderWidth = 2;
            marketLogo.Text = "Market";
            marketLogo.Font = new Font("Arial", 12, FontStyle.Regular);
            Controls.Add(marketLogo);
            marketLogo.BringToFront();

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
            acountButton.Click += delegate (object sender, EventArgs e) { accountButtonFunction(); };
            label.Controls.Add(acountButton);
            acountButton.BringToFront();
        }

        private void accountButtonFunction()
        {
            AccountPage accountPage = new AccountPage();
            accountPage.ShowDialog();
        }

        private void ordersButtonFunction()
        {

            OrdersPage ordersPage = new OrdersPage(ref this.customerName, ref this.balance, ref folderAdress);
            ordersPage.ShowDialog();
        }
    }
}