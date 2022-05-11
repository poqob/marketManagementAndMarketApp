using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

namespace PQContentWidget
{
    public class MarketContentWidget : Label
    {
        //variables
        RoundLabel baseLabel = new RoundLabel();
        Font ItalicFont = new Font("Arial", 6, FontStyle.Italic);
        PictureBox pictureBox1 = new PictureBox();
        private string fotoToDisplay;

        private string price;
        private string stock;
        private string productBrandAndName;

        public MarketContentWidget(ref string fotoPath, string unitPrice, ref string totalStock, ref string brandAndName,ref string explanation)
        {
            //attempting parameters to variables.
            this.fotoToDisplay = fotoPath;
            this.productBrandAndName = brandAndName;
            this.price = unitPrice;
            this.stock = totalStock;
            this.BackColor = Color.FromArgb(255, 230, 204);
            this.Size = new Size(120, 150);
            this.Padding = new Padding(60);
            widget();
        }

        private void widget()
        {
            //label that keeps stock num and price. 
            //TODO: label's color resists to changing.
            baseLabel.Size = new Size(100, 50);
            baseLabel.BackColor = BackColor;
            baseLabel.cornerRadius = 20;
            baseLabel.borderWidth = 1;
            baseLabel.borderColor = Color.White;
            baseLabel.BorderStyle = BorderStyle.None;
            baseLabel.Location = new Point(10, 80);
            baseLabel.Text = productBrandAndName + "\nStock:" + stock + "\nPrice:" + price+"$";
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
        }
    }
}