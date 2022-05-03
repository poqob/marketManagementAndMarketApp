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

            baseCreator();
            buttonCreator();

        }
        //creates form's base.
        private void baseCreator()
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
            this.CenterToScreen();
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
            baseCreator(ref company);
            menuCreator(ref company);
            this.FormClosed += delegate (object sender, FormClosedEventArgs e)
            {
                destructor(ref company);
            };
        }

        //our forms Destructor. when form was closed, this function will ran.
        private void destructor(ref string company)
        {
            foreach (string catagory in catagories)
            {

                if (File.Exists(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp"))
                {
                    //deleting unnecessary edited tmp files.
                    File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                }
            }

        }

        private void baseCreator(ref string company)
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
            label.Tag = "base";
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
            ordersButton.Tag = "base";
            Controls.Add(ordersButton);



            //MainBorder
            Border.Size = new Size(700, 280);
            Border.Location = new Point(40, 130);
            Border.backColor = BackColor;
            Border.borderColor = Color.White;
            Border.borderWidth = 3;
            Border.cornerRadius = 45;
            Border.ForeColor = Color.White;
            Border.Tag = "base";
            Controls.Add(Border);


        }


        //this method creates catagory buttons and order button
        private void menuCreator(ref string company)
        {
            //local variables
            Point point = new Point(60, 150);
            Font ItalicFont = new Font("Arial", 8, FontStyle.Italic);
            string companyName = company;

            //creating catagory buttons : child, woman, man
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
                menuButton.Tag = "base1";
                menuButton.Click += delegate (object sender, EventArgs e) { contentCreator(sender, e, menuButton.Text, ref companyName); ProductProcess.addToChartControll(ref companyName, false); };
                Controls.Add(menuButton);
                menuButton.BringToFront();
                point.Y += 82;

                // there is no any temp file
                if (!File.Exists(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp"))
                {
                    File.Copy(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                }
                else
                {
                    File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                    File.Copy(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");

                }
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
                string productInformationDirectory = @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp";
                string productInformationDirectoryOriginal = @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt";
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
                label.Text = File.ReadAllText(fileToDisplay) + " $" + getProductCost(ref productInformationDirectoryOriginal, ref i);
                label.ForeColor = Color.White;
                Controls.Add(label);
                label.BringToFront();

                //numeric updown element, helps order process to choose how many product do you want.
                numericUpDown.Location = point2;
                numericUpDown.Size = new Size(80, 20);
                numericUpDown.Visible = true;
                numericUpDown.ForeColor = Color.White;
                numericUpDown.BackColor = BackColor;
                numericUpDown.Maximum = getStockNum(ref productInformationDirectoryOriginal, ref i);
                numericUpDown.Tag = wear;
                numericUpDown.AllowDrop = false;
                numericUpDown.ReadOnly = true;
                numericUpDown.Click += delegate (object sender, EventArgs e) { ProductProcess.stockNumAdjusting(ref productInformationDirectory, ((int)numericUpDown.Value), ref wear); };

                Controls.Add(numericUpDown);
                numericUpDown.BringToFront();

                //for numeric updown form elemet,this button choose maximum product at once. near updowns
                button.Location = point3;
                button.Size = new Size(30, 30);
                button.BackColor = BackColor;
                button.BorderSize = 1;
                button.BorderRadius = 5;
                button.BorderColor = Color.White;
                button.Click += delegate (object sender, EventArgs e) { numericUpDown.Value = numericUpDown.Maximum; };
                button.Text = "M";
                Controls.Add(button);
                button.BringToFront();


                point3.X += 180;
                point2.X += 180;
                point1.X += 180;
                point.X += 180;


                if (i == 3)
                {
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
                    addToChartButton.Click += delegate (object sender, EventArgs e)
                    {

                        /*
                        //reinitialiaze the form after adding products to chart, this part may be more clear but i haven't enough time.
                        ProductProcess.addToChartControll(ref companyName, true);
                        SupliarsMarketContentCreator page;
                        page = new SupliarsMarketContentCreator(companyName);
                        page.Size = new Size(800, 500);
                        this.Dispose();
                        page.CenterToParent();
                        page.ShowDialog();
                        */

                        ProductProcess.addToChartControll(ref companyName, true);
                        Utilities.ResetAllControls(this);
                    };
                    Controls.Add(addToChartButton);
                    addToChartButton.BringToFront();
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
                    wear = "TER:";
                    break;
                case 2:
                    //T-SHIRT's RT:
                    wear = "IRT:";
                    break;
                case 3:
                    //PANTS's TS:
                    wear = "NTS:";
                    break;
                default:
                    wear = "";
                    break;
            }

            //find related data line in information folder.
            int index = content.IndexOf(wear) + 4;
            //find first ','comma sign in related line.
            int index2 = content.IndexOf(",", index);

            //substringing and converting to intager
            stockNum = Convert.ToInt32(content.Substring(index, index2 - index));

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
        public static void stockNumAdjusting(ref string productInformationDirector, int howMany, ref string whichProduct)
        {

            //reading information files.
            //tmp
            string readFile = File.ReadAllText(productInformationDirector);
            //txt
            string readFileOriginal = File.ReadAllText(productInformationDirector.Substring(0, productInformationDirector.Length - 3) + "txt");


            //needed indexes for parsing stock number from original txt file
            int index = readFileOriginal.IndexOf(whichProduct) + 4;

            int index2 = readFileOriginal.IndexOf(",", index);


            //substringing and converting to intager related stock num imformation from original file which is not edited yet.
            //this stock num will be edited after related product added to chart.
            int baseStockNum = Math.Abs(Convert.ToInt32(readFileOriginal.Substring(index, index2 - index)));


            //needed indexes for parsing stock number from temporary tmp file
            int index3 = readFile.IndexOf(whichProduct) + 4;

            int index4 = readFile.IndexOf(",", index);


            //substringing and converting to intager related stock num imformation from tmp file which is edited before.
            int tmpStockNum = Convert.ToInt32(readFile.Substring(index3, index4 - index3));
            //calculating current stock number, while we ordering product.
            int currentStockNum = Math.Abs(baseStockNum - howMany);
            //replacing old stock number with current one.
            readFile = readFile.Replace(whichProduct + tmpStockNum.ToString(), whichProduct + currentStockNum.ToString());
            //applying changes to tmp file. changes: stock numbers.
            File.WriteAllText(productInformationDirector, readFile);
        }


        public static void addToChartControll(ref string company, bool isAddToCharPressed)
        {
            string[] catagories = { "woman", "man", "chıld" };

            if (isAddToCharPressed == true)
            {
                //TODO: delete txt files and convert tmp files to txt files.
                foreach (string catagory in catagories)
                {
                    File.Replace(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".poqob");
                    File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                    File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".poqob");
                    File.Copy(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");

                }
            }
            else
            {
                //TODO:  make tmp file's -woman,man,child- contents same with txt files.
                foreach (string catagory in catagories)
                {

                    if (File.Exists(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp"))
                    {
                        /* //delete edited but not allowed old tmp file to derivate new one.
                         File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");*/
                        File.Delete(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                        File.Copy(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");

                    }

                    // if delete procces was succesful then copy new one from original txt file.
                    if (!File.Exists(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp"))
                    {
                        File.Copy(@"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".txt", @"datas\suppliers\" + company + @"\" + catagory.ToUpper() + ".tmp");
                    }
                }
            }

        }
    }

    //reset NumericUpDown form elements
    public static class Utilities
    {
        public static void ResetAllControls(Control form)
        {
            foreach (Control control in form.Controls)
            {
                //reset numericUpDown buttons
                if (control is NumericUpDown)
                {
                    NumericUpDown upDown = (NumericUpDown)control;
                    upDown.Value = 0;
                }
            }
        }
    }
}
