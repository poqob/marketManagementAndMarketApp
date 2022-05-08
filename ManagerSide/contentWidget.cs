using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

//this widget takes some parameters -product picture path, product price, product stock number, product brand- and according to parameters code will create a structure.
//i'm planing to do something like flutter widget.
//widget's up will has got products picture, just below has in order of : product name and brand, stock number and price.

namespace PQContentWidget
{
    public class ContentWidget : Button
    {
        //variables
        RoundLabel baseLabel = new RoundLabel();
        Font ItalicFont = new Font("Arial", 6, FontStyle.Italic);
        PictureBox pictureBox1 = new PictureBox();
        private string fotoToDisplay;

        private string filePath;
        private string price;
        private string stock;
        private string fileName;
        private string productBrandAndName;
        public ContentWidget(ref string fotoPath, string price, ref string stock, ref string brandAndName, string filePath, ref string fileName)
        {
            //attempting parameters to variables.
            this.fotoToDisplay = fotoPath;
            this.productBrandAndName = brandAndName;
            this.price = price;
            this.stock = stock;
            this.filePath = filePath;
            this.fileName = fileName;
            this.BackColor = Color.FromArgb(30, 30, 30);
            widget();
        }

        //our widget that includes a picture box which keeps picture data of product and a label to keep price and stock data.
        private void widget()
        {
            BackColor = Color.FromArgb(30, 30, 30);
            //label that keeps stock num and price. 
            //TODO: label's color resists to changing.
            baseLabel.Size = new Size(100, 50);
            baseLabel.BackColor = BackColor;
            baseLabel.cornerRadius = 20;
            baseLabel.borderWidth = 1;
            baseLabel.borderColor = Color.White;
            baseLabel.BorderStyle = BorderStyle.None;
            baseLabel.Location = new Point(10, 80);
            baseLabel.Text = productBrandAndName + "\nStock:" + stock + "\nPrice:" + price;
            baseLabel.Font = ItalicFont;
            this.Controls.Add(baseLabel);
            baseLabel.BringToFront();

            //definin image for picturebox and configuring pictureBox
            Bitmap MyImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(fotoToDisplay);
            pictureBox1.Width = 60;
            pictureBox1.Height = 60;
            pictureBox1.Location = new Point(28, 10);
            pictureBox1.Image = (Image)MyImage;
            this.Controls.Add(pictureBox1);
            pictureBox1.BringToFront();

            this.Click += delegate (object sender, EventArgs e)
            {
                ProductPricingPage pricingPage = new ProductPricingPage(ref price, ref stock, ref productBrandAndName, ref filePath, ref fileName);
                pricingPage.Size = new Size(400, 600);
                pricingPage.ShowDialog();
            };

        }
    }

    public class ProductPricingPage : Form
    {
        //variables.
        bool answer;
        string brandAndName;
        string oldPrice;
        string stock;
        string filePath;
        string fileName;

        Label label = new Label();//do question label

        Label label1 = new Label();//please enter num warning label

        TextBox textBox = new TextBox();
        Font font = new Font("Arial", 12, FontStyle.Regular);

        RJButton buttonYes = new RJButton();
        RJButton buttonNo = new RJButton();


        public ProductPricingPage(ref string price, ref string stock, ref string productBrandAndName, ref string filePath, ref string fileName)
        {
            this.oldPrice = price;
            this.brandAndName = productBrandAndName;
            this.filePath = filePath;
            this.stock = stock;
            this.fileName = fileName;
            dialogBuilder();
        }

        private void dialogBuilder()
        {
            //dialog header
            this.BackColor = Color.FromArgb(30, 30, 30);
            label.Size = new Size(280, 100);
            label.Location = new Point(40, 40);
            label.Text = "do you want to add the " + brandAndName + " to the market place ?";
            label.Font = font;
            label.ForeColor = Color.White;
            label.BackColor = BackColor;
            Controls.Add(label);
            label.BringToFront();

            //input area
            textBox.BackColor = BackColor;
            textBox.ForeColor = Color.White;
            textBox.Text = this.oldPrice;
            textBox.Location = new Point(40, 150);
            textBox.Size = new Size(100, 80);
            Controls.Add(textBox);
            textBox.BringToFront();

            //warning message
            label1.Size = new Size(280, 100);
            label1.Location = new Point(40, 210);
            label1.Text = "-Please enter only numberic inputs.";
            label1.Font = font;
            label1.ForeColor = Color.White;
            label1.BackColor = BackColor;
            Controls.Add(label1);
            label.BringToFront();

            //answer yes button
            buttonYes.Size = new Size(120, 70);
            buttonYes.Location = new Point(50, 420);
            buttonYes.BackColor = BackColor;
            buttonYes.BorderSize = 2;
            buttonYes.BorderRadius = 15;
            buttonYes.ForeColor = Color.White;
            buttonYes.BorderColor = Color.White;
            buttonYes.Font = new Font("Arial", 12, FontStyle.Italic);
            buttonYes.Text = "yes";
            buttonYes.Click += delegate (object s, EventArgs e) { answer = true; action(ref answer, ref filePath, ref stock); };
            Controls.Add(buttonYes);
            buttonYes.BringToFront();


            //answer no button
            buttonNo.Size = new Size(120, 70);
            buttonNo.Location = new Point(220, 420);
            buttonNo.BackColor = BackColor;
            buttonNo.BorderSize = 2;
            buttonNo.BorderRadius = 15;
            buttonNo.ForeColor = Color.White;
            buttonNo.BorderColor = Color.White;
            buttonNo.Text = "no";
            buttonNo.Font = new Font("Arial", 12, FontStyle.Italic);
            buttonNo.Click += delegate (object s, EventArgs e) { answer = false; action(ref answer, ref filePath, ref stock); };
            Controls.Add(buttonNo);
            buttonNo.BringToFront();

        }

        //controlling which button pressed and also taking action according to choosem.
        private void action(ref bool answer, ref string filePath, ref string stock)
        {
            //if answer is not true do nothing and exit small page.
            if (answer != true)
            {
                this.Dispose();
                this.Enabled = false;
            }
            //else answer is true, apply changes to product folder.
            else
            {

                //fetching file data
                string file = File.ReadAllText(filePath);
                int index0 = file.IndexOf(",") + 1;
                int index1 = file.IndexOf("$");

                //getting old total price from file to obtain unit price.
                int oldTotalPrice = Convert.ToInt32(file.Substring(index0, index1 - index0));

                //calculating new totalPrice
                string newTotalPrice = (Convert.ToInt32(textBox.Text) * Convert.ToInt32(stock)).ToString();

                //changes
                file = file.Remove(index0, index1 - index0);
                file = file.Insert(index0, newTotalPrice);

                //apply changes
                File.WriteAllText(filePath, file);

                //moving filePath file to market place folder which is productForSale.
                File.Move(filePath, @"datas\productsForSale\" + fileName);
                this.Dispose();
                this.Enabled = false;
            }
        }
    }
}

//TODO: 
//the input area only accepts number
//if user press yes, move the file to productForSale folder.
// think about is product can be seen while in productForSale or isn't.