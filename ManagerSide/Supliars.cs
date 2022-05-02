using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace Supliars
{
    public class SupliarsMarketPlace : Form
    {
        string[] supliars = { "ERTAN", "HOOMAN", "ASPAVA" };

        RoundLabel label = new RoundLabel();
        Font LargeFont = new Font("Arial", 16, FontStyle.Bold);
        RoundLabel Border = new RoundLabel();
        public SupliarsMarketPlace()
        {
            BackColor = Color.FromArgb(38, 38, 38);

            //heading
            label.Size = new Size(150, 50);
            label.Location = new Point(40, 40);
            label.backColor = BackColor;
            label.borderColor = Color.White;
            label.borderWidth = 3;
            label.cornerRadius = 40;
            label.Text = "Supliers";
            label.Font = LargeFont;

            label.ForeColor = Color.White;
            Controls.Add(label);

            //MainBorder
            Border.Size = new Size(700, 250);
            Border.Location = new Point(40, 130);
            Border.backColor = BackColor;
            Border.borderColor = Color.White;
            Border.borderWidth = 3;
            Border.cornerRadius = 45;
            Border.ForeColor = Color.White;
            Controls.Add(Border);

            buttonCreator();

        }

        //creates supliars companies buttons to choose.
        private void buttonCreator()
        {
            Point point = new Point(120, 210);
            Font ItalicFont = new Font("Arial", 12, FontStyle.Italic);

            foreach (string item in supliars)
            {
                RJButton button = new RJButton();

                button.Size = new Size(130, 80);
                button.Location = point;
                button.BackColor = Color.FromArgb(38, 38, 38);
                button.BorderColor = Color.White;
                button.BorderRadius = 20;
                button.BorderSize = 2;
                button.Text = item;
                button.Font = ItalicFont;
                button.Click += delegate (object sender, EventArgs e) { openCompanyPage(sender, e, button.Text); };

                Controls.Add(button);
                button.BringToFront();
                point.X += 200;
            }
        }

        //opens supliar companies market page.
        private void openCompanyPage(object sender, EventArgs e, string company)
        {
            SupliarsMarketContentCreator page;
            page = new SupliarsMarketContentCreator(company);
            page.Size = new Size(800, 500);
            this.Hide();
            page.ShowDialog();
        }
    }

    /*
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    */

    class SupliarsMarketContentCreator : Form
    {
        RoundLabel label = new RoundLabel();
        RJButton ordersButton = new RJButton();

        Font LargeFont = new Font("Arial", 13, FontStyle.Italic);
        RoundLabel Border = new RoundLabel();

        string[] catagories = { "woman", "man", "chıld" };
        public SupliarsMarketContentCreator(string company)
        {
            BackColor = Color.FromArgb(38, 38, 38);

            //heading
            label.Size = new Size(200, 55);
            label.Location = new Point(40, 40);
            label.backColor = BackColor;
            label.borderColor = Color.White;
            label.borderWidth = 3;
            label.cornerRadius = 23;
            label.Text = company + "  Stocks";
            label.Font = LargeFont;

            label.ForeColor = Color.White;
            Controls.Add(label);


            //orders button
            ordersButton.Size = new Size(80, 55);
            ordersButton.Location = new Point(650, 40);
            ordersButton.BackColor = BackColor;
            ordersButton.BorderColor = Color.White;
            ordersButton.BorderSize = 1;
            ordersButton.BorderRadius = 20;
            ordersButton.Text = "see orders";
            ordersButton.ForeColor = Color.White;
            Controls.Add(ordersButton);



            //MainBorder
            Border.Size = new Size(700, 280);
            Border.Location = new Point(40, 130);
            Border.backColor = BackColor;
            Border.borderColor = Color.White;
            Border.borderWidth = 3;
            Border.cornerRadius = 45;
            Border.ForeColor = Color.White;
            Controls.Add(Border);



            menuCreator(ref company);


        }


        //this method creates catagory buttons and order button
        private void menuCreator(ref string company)
        {
            //local variables
            Point point = new Point(60, 150);
            Font ItalicFont = new Font("Arial", 8, FontStyle.Italic);
            string companyName = company;

            //creating catagory buttons like child, woman, man
            foreach (string catagory in catagories)
            {
                RJButton menuButton = new RJButton();
                menuButton.Size = new Size(100, 70);
                menuButton.Location = point;
                menuButton.BackColor = Color.FromArgb(38, 38, 38);
                menuButton.BorderColor = Color.White;
                menuButton.BorderRadius = 15;
                menuButton.BorderSize = 2;
                menuButton.Text = catagory;
                menuButton.Font = ItalicFont;
                menuButton.Click += delegate (object sender, EventArgs e) { contentCreator(sender, e, menuButton.Text, ref companyName); };
                Controls.Add(menuButton);
                menuButton.BringToFront();
                point.Y += 82;
            }







        }

        //this method creates catagory contents -pictures, and informations
        private void contentCreator(object sender, EventArgs e, string catagory, ref string company)
        {
            Point point = new Point(230, 140);
            Point point1 = new Point(230, 230);
            Point point2 = new Point(230, 300);
            Point point3 = new Point(315, 298);

            //this part of the code can be specialize. to explain, may i create modals with product pics and information about product.
            //bu i couldn't have time and ,i've to finish fast.

            for (int i = 1; i < 4; i++)
            {
                string fotoToDisplay = @"datas\suppliers\" + company + @"\fotoData\" + catagory.ToUpper() + @"\" + i + ".png";
                string fileToDisplay = @"datas\suppliers\" + company + @"\fotoData\" + catagory.ToUpper() + @"\" + i + ".txt";
                string productInformationDirectory = @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt";
                string companyName = company;
                string wear;
                switch (i)
                {
                    case 1:
                        //swater's ER:
                        wear = "TER:";
                        break;
                    case 2:
                        //T-SHIRT'S RT:
                        wear = "IRT:";
                        break;
                    case 3:
                        //PANTS'S TS:
                        wear = "NTS:";
                        break;

                    default:
                        wear = "";
                        break;
                }
                //respectively form controls
                PictureBox pictureBox1 = new PictureBox();
                Label label = new Label();
                NumericUpDown numericUpDown = new NumericUpDown();
                RJButton button = new RJButton();

                //definin image for picturebox and configuring pictureBox
                Bitmap MyImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                MyImage = new Bitmap(fotoToDisplay);
                pictureBox1.ClientSize = new Size(80, 80);
                pictureBox1.Location = point;
                pictureBox1.Image = (Image)MyImage;
                Controls.Add(pictureBox1);
                pictureBox1.BringToFront();

                //product information label, it contains cost and somethings about product
                label.BackColor = BackColor;
                label.Location = point1;
                label.Size = new Size(120, 65);
                label.Text = File.ReadAllText(fileToDisplay) + " $" + getProductCost(ref productInformationDirectory, ref i);
                label.ForeColor = Color.White;
                Controls.Add(label);
                label.BringToFront();

                //numeric updown element, helps order process to choose how many product do you want.
                numericUpDown.Location = point2;
                numericUpDown.Size = new Size(80, 20);
                numericUpDown.Visible = true;
                numericUpDown.ForeColor = Color.White;
                numericUpDown.BackColor = BackColor;
                numericUpDown.Maximum = getStockNum(ref productInformationDirectory, ref i);
                numericUpDown.Tag = wear;
                //numericUpDown.Click += delegate (object sender, EventArgs e) { ProductProcess.productInformationCollector(((int)numericUpDown.Value), ref productInformationDirectory, ref fileToDisplay, ref fotoToDisplay, ref companyName, ref catagory, numericUpDown.Tag.ToString()); };

                Controls.Add(numericUpDown);
                numericUpDown.BringToFront();

                //for numeric updown form elemet,this button choose maximum product at once. near updowns
                button.Location = point3;
                button.Size = new Size(30, 30);
                button.BackColor = BackColor;
                button.BorderSize = 1;
                button.BorderRadius = 5;
                button.BorderColor = Color.White;
                //button.Click += delegate (object sender, EventArgs e) { numericUpDown.Value = numericUpDown.Maximum; ProductProcess.productInformationCollector(((int)numericUpDown.Maximum), ref productInformationDirectory, ref fileToDisplay, ref fotoToDisplay, ref companyName, ref catagory, numericUpDown.Tag.ToString()); };
                button.Text = "M";
                Controls.Add(button);
                button.BringToFront();


                point3.X += 180;
                point2.X += 180;
                point1.X += 180;
                point.X += 180;


                RJButton addToChartButton = new RJButton();
                Point addToChartButtonPoint = new Point(380, 345);

                addToChartButton.Location = addToChartButtonPoint;
                addToChartButton.Size = new Size(150, 50);
                addToChartButton.Text = "ADD TO CHART";
                addToChartButton.BorderSize = 1;
                addToChartButton.BorderRadius = 10;
                addToChartButton.BorderColor = Color.White;
                addToChartButton.ForeColor = Color.White;
                addToChartButton.BackColor = BackColor;
                addToChartButton.Click += delegate (object sender, EventArgs e) { ProductProcess.productInformationCollector(((int)numericUpDown.Value), ref productInformationDirectory, ref fileToDisplay, ref fotoToDisplay, ref companyName, ref catagory, numericUpDown.Tag.ToString()); };
                Controls.Add(addToChartButton);
                addToChartButton.BringToFront();
                if (i == 3)
                {
                    addToChartButton.Dispose();
                    addToChartButton.Dispose(); 
                }
            }


        }

        private int getStockNum(ref string productInformationDirectory, ref int whichProduct)
        {
            //1=sweater ,2=tshirt, 3=pants
            //read product information (that has stock information in it) file.
            string content = File.ReadAllText(productInformationDirectory);
            string wear;
            int stockNum;
            //determining to get which product stock number.
            switch (whichProduct)
            {
                case 1:
                    //SWEATER's ER:
                    wear = "ER:";
                    break;
                case 2:
                    //T-SHIRT's RT:
                    wear = "RT:";
                    break;
                case 3:
                    //PANTS's TS:
                    wear = "TS:";
                    break;
                default:
                    wear = "";
                    break;
            }

            //find related data line in information folder.
            int index = content.IndexOf(wear);
            //find first ','comma sign in related line.
            int index2 = content.IndexOf(",", index);

            //substringing and converting to intager
            stockNum = Convert.ToInt32(content.Substring(index + 3, index2 - index - 3));

            return stockNum;
        }
        private double getProductCost(ref string productInformationDirectory, ref int whichProduct)
        {

            //1=sweater ,2=tshirt, 3=pants
            //read product information (that has stock information in it) file.
            string content = File.ReadAllText(productInformationDirectory);
            string wear;
            int productCost;
            //determining to get which product stock number.
            switch (whichProduct)
            {
                case 1:
                    //SWEATER's ER:
                    wear = "ER:";
                    break;
                case 2:
                    //T-SHIRT's RT:
                    wear = "RT:";
                    break;
                case 3:
                    //PANTS's TS:
                    wear = "TS:";
                    break;
                default:
                    wear = "";
                    break;
            }

            //find related data line in information folder.
            int index = content.IndexOf(wear);
            //find first ','comma sign in related line.
            int index2 = content.IndexOf(",", index);
            //finding firs '$'dollar sign to pick cost. in data folder, cost is located between ',' and '$' sign.
            index = content.IndexOf("$", index2);
            //substringing and converting to intager
            productCost = Convert.ToInt32(content.Substring(index2 + 1, index - index2 - 1));

            return productCost;
        }

    }

    /*
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    *******************************************************************************************************************************************************************
    */

    static class ProductProcess
    {

        public static void productInformationCollector(int howMany, ref string productInformationDirectory, ref string fileToDisplay, ref string fotoToDisplay, ref string company, ref string catagory, string whichProduct)
        {
            //whichProduct, 1:sweater 2:T-shirt 3:pants
            //ProductModel model = new ProductModel(ref howMany, ref productInformationDirectory, ref fileToDisplay, ref fotoToDisplay, ref company, ref catagory,ref whichProduct);
            stockNumAdjusting(ref productInformationDirectory, ref howMany, ref whichProduct);
        }


        private static void stockNumAdjusting(ref string productInformationDirector, ref int howMany, ref string whichProduct)
        {

            //read information fie
            string readFile = File.ReadAllText(productInformationDirector);


            int index = readFile.IndexOf(whichProduct);

            int index2 = readFile.IndexOf(",", index);


            //substringing and converting to intager related stock num imformation
            int currentStockNum = Convert.ToInt32(readFile.Substring(index + 4, index2 - index - 4));
            //minusing currentStockNum variable of howMany times
            currentStockNum -= howMany;

            readFile = readFile.Remove(index + 4, index2 - index - 4);
            readFile = readFile.Insert(index + 4, currentStockNum.ToString());


            File.WriteAllText(productInformationDirector, readFile);
            MessageBox.Show(readFile + currentStockNum.ToString());


        }

    }

    class ProductModel
    {
        public int stock = 0;

        string productInformationDirectoryAdress = "";

        string productMaterialInformationAdress = "";

        string productFotoAdress = "";

        public string companyName = "";

        //woman man child
        public string catagoryOfproduct = "";

        public ProductModel(ref int howMany, ref string productInformationDirectory, ref string fileToDisplay, ref string fotoToDisplay, ref string company, ref string catagory, string product)
        {
            stock = howMany;
            productInformationDirectoryAdress = productInformationDirectory;
            productMaterialInformationAdress = fileToDisplay;
            productFotoAdress = fotoToDisplay;
            companyName = company;
            catagoryOfproduct = catagory;


        }
    }
}

/*//order button
            RJButton orderButton = new RJButton();
            Point orderButtonPoint = new Point(500, 345);

            orderButton.Location = orderButtonPoint;
            orderButton.Size = new Size(100, 50);
            orderButton.Text = "ORDER";
            orderButton.BorderSize = 1;
            orderButton.BorderRadius = 10;
            orderButton.BorderColor = Color.White;
            orderButton.ForeColor = Color.White;
            orderButton.BackColor = BackColor;
            Controls.Add(orderButton);
            orderButton.BringToFront();*/