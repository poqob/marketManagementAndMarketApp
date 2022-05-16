using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using CustomerPage;
using Supliars;
using Stock;
using ManagerSide;

namespace ndpProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            mainFormButtonCreator();
            //create today folder. in giro
            ExpensesAndSales.folderControl();
            ExpensesAndSales.giro();
            //backround color.
            BackColor = Color.FromArgb(38, 38, 38);

            //heading
            RoundLabel label = new RoundLabel();
            label.Size = new Size(550, 80);
            label.Location = new Point(120, 10);
            label.backColor = BackColor;
            label.borderColor = Color.White;
            label.borderWidth = 2;
            label.cornerRadius = 20;
            label.Text = "ManageMarketAppV0.1";
            label.ForeColor = Color.White;
            Controls.Add(label);
        }
        private void mainFormButtonCreator()
        {
            //first button location
            Point point = new Point(85, 150);
            //buttonses texts
            String[] buttonNames = { "suppliers", "customers", "stocks", "expenses and sales" };

            //creating buttons
            for (int i = 0; i < 4; i++)
            {
                RJButton button = new RJButton();

                button.Size = new Size(130, 130);
                button.Location = point;
                button.BackColor = Color.FromArgb(38, 38, 38);
                button.BorderColor = Color.White;
                button.BorderRadius = 20;
                button.BorderSize = 2;
                button.Text = buttonNames[i];

                point.X += 250;
                if (i == 2)
                {
                    point.Y += 150;
                    point.X = 85;
                }
                //attemting click actions to each button
                switch (i)
                {
                    case 0:
                        button.Click += new System.EventHandler(this.supplier);
                        break;
                    case 1:
                        button.Click += new System.EventHandler(this.customers);
                        break;
                    case 2:
                        button.Click += new System.EventHandler(this.stocks);
                        break;
                    case 3:
                        button.Click += new System.EventHandler(this.expensesAndSales);
                        break;

                }

                Controls.Add(button);
            }

        }



        private void expensesAndSales(object sender, EventArgs e)
        {
            ExpensesAndSalesUI.messager();
        }

        private void stocks(object sender, EventArgs e)
        {
            Stocks stocksPage = new Stocks();
            stocksPage.Size = new Size(800, 500);
            this.Hide();
            stocksPage.ShowDialog();
            this.Show();
        }

        private void customers(object sender, EventArgs e)
        {
            CustomersPage customerPage = new CustomersPage();
            customerPage.Size = new Size(800, 500);
            this.Hide();
            customerPage.ShowDialog();
            this.Show();

        }

        private void supplier(object sender, EventArgs e)
        {
            SupliarsMarketPlace supliarsPage = new SupliarsMarketPlace();
            supliarsPage.Size = new Size(800, 500);
            this.Hide();
            supliarsPage.ShowDialog();
            this.Show();
        }
    }
}
