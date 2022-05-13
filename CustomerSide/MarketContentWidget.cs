using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

namespace PQContentWidget
{
    public class MarketContentWidget : Button
    {
        //variables
        RoundLabel baseLabel = new RoundLabel();
        Font ItalicFont = new Font("Arial", 6, FontStyle.Italic);
        PictureBox pictureBox1 = new PictureBox();
        NumericUpDown numericUpDown = new NumericUpDown();

        private string fotoToDisplay;
        private string price;
        private string stock;
        private string productBrandAndName;
        private string explanation;
        private string filePath;

        public MarketContentWidget(ref string fotoPath, string unitPrice, ref string totalStock, ref string brandAndName, ref string explanation, string filePath)
        {
            //attempting parameters to variables.
            this.fotoToDisplay = fotoPath;
            this.productBrandAndName = brandAndName;
            this.price = unitPrice;
            this.stock = totalStock;
            this.explanation = explanation;
            this.filePath = filePath;
            this.BackColor = Color.FromArgb(255, 230, 204);
            this.Size = new Size(120, 170);
            this.Padding = new Padding(60);
            widget();
        }

        private void widget()
        {

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


            //label that keeps stock num and price. 
            //TODO: label's color resists to changing.
            baseLabel.Size = new Size(100, 50);
            baseLabel.BackColor = BackColor;
            baseLabel.cornerRadius = 20;
            baseLabel.borderWidth = 1;
            baseLabel.borderColor = Color.White;
            baseLabel.BorderStyle = BorderStyle.None;
            baseLabel.Location = new Point(10, 80);
            baseLabel.Text = productBrandAndName + "\nStock:" + stock + "\nPrice:" + price + "$";
            baseLabel.Font = ItalicFont;
            this.Controls.Add(baseLabel);
            baseLabel.BringToFront();

            //numeric increament
            numericUpDown.Size = new Size(100, 18);
            numericUpDown.BackColor = BackColor;
            numericUpDown.Location = new Point(10, 135);
            numericUpDown.Visible = true;
            numericUpDown.Maximum = Convert.ToInt32(stock);
            numericUpDown.ForeColor = Color.Black;
            numericUpDown.AllowDrop = false;
            numericUpDown.Click += delegate (object sender, EventArgs e) { MarketFolderProcess.stockNumArranger(ref filePath, Convert.ToInt32(numericUpDown.Value)); };
            this.Controls.Add(numericUpDown);
            numericUpDown.BringToFront();

            this.Click += delegate (object sender, EventArgs e) { clickAction(); };

        }

        //click action for widget button.
        private void clickAction()
        {
            MessageBox.Show(explanation, productBrandAndName + " information", MessageBoxButtons.OK);
        }




    }
}