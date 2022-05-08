using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using PQContentWidget;

namespace Stock
{

    public class Stocks : Form
    {
        //variables.
        FlowLayoutPanel panel = new FlowLayoutPanel();
        RoundLabel Border = new RoundLabel();
        string[] catagories = { "woman", "man", "chıld" };



        public Stocks()
        {
            baseCreator();
        }

        private void baseCreator()
        {
            //local variables
            Point point = new Point(60, 60);
            BackColor = Color.FromArgb(38, 38, 38);
            Font ItalicFont = new Font("Arial", 12, FontStyle.Italic);

            //MainBorder
            Border.Size = new Size(740, 420);
            Border.Location = new Point(20, 20);
            Border.backColor = BackColor;
            Border.borderColor = Color.White;
            Border.borderWidth = 3;
            Border.cornerRadius = 45;
            Border.ForeColor = Color.White;
            Border.Tag = "base";
            Controls.Add(Border);


            //creating catagory buttons : child, woman, man
            foreach (string catagory in catagories)
            {
                RJButton menuButton = new RJButton();
                menuButton.Size = new Size(110, 90);
                menuButton.Location = point;
                menuButton.BackColor = Color.FromArgb(38, 38, 38);
                menuButton.BorderColor = Color.White;
                menuButton.BorderRadius = 15;
                menuButton.BorderSize = 2;
                menuButton.Text = catagory;
                menuButton.Font = ItalicFont;
                menuButton.Click += delegate (object sender, EventArgs e) { contentCreator(catagory); };
                Controls.Add(menuButton);
                menuButton.BringToFront();
                point.Y += 120;
            }

            //flow panel that keeps our widgets.

            panel.Location = new Point(200, 40);
            panel.Size = new Size(540, 380);
            panel.BackColor = Color.FromArgb(38, 38, 38);
            panel.FlowDirection = FlowDirection.LeftToRight;

            panel.AutoScroll = true;

            panel.WrapContents = true;
            Controls.Add(panel);
            panel.BringToFront();
        }

        private void contentCreator(string catagory)
        {
            //here we will code new widget which can store picture up, product name, product stock number and also product current price and we will use call this widget with parameters.
            string fotoPath;
            string stockNumber;
            string price;
            string brandAndProduct;
            string currentFileContent;
            int index;
            int index0;

            System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles(@"datas\allStock\" + catagory.ToUpper() + @"\", "*.txt", SearchOption.TopDirectoryOnly);
            //panel clears itself because if isn't new generated widgets will have piled up.
            panel.Controls.Clear();

            foreach (string file in files)
            {
                //create only related widgets.-to catagory like CHILD,MAN,WOMAN-
                if (file.Contains(catagory.ToUpper()))
                {
                    //getting brand and product name to variable.
                    brandAndProduct = file.Substring(16 + catagory.Length, file.IndexOf(catagory.ToUpper(), 16 + catagory.Length) - 16 - catagory.Length) + " ";
                    brandAndProduct += file.Substring(file.IndexOf(catagory.ToUpper(), 16 + catagory.Length) + catagory.Length, file.Length - 4 - (file.IndexOf(catagory.ToUpper(), 16 + catagory.Length) + catagory.Length));
                    string fileName = file.Substring(16 + catagory.Length, file.Length - 16 - catagory.Length);
                    // brandAndProduct += "\n";

                    //read which file we are dealing.
                    currentFileContent = File.ReadAllText(file);


                    //getting stock number as string.
                    index = currentFileContent.IndexOf(",");
                    index0 = currentFileContent.IndexOf(":");
                    stockNumber = currentFileContent.Substring(index0 + 1, index - index0 - 1);

                    //getting price number as string.
                    index0 = currentFileContent.IndexOf("$");
                    price = currentFileContent.Substring(index + 1, index0 - index - 1);

                    //getting fotographs path as string.
                    index0 = currentFileContent.IndexOf("&", 2) + 1;
                    index = currentFileContent.IndexOf("\n", index0);
                    fotoPath = currentFileContent.Substring(index0, index - index0);



                    //creating button widged according to parameters that above.
                    ContentWidget modalWidget = new ContentWidget(ref fotoPath, Convert.ToInt32((Convert.ToInt32(price) / Convert.ToInt32(stockNumber))).ToString(), ref stockNumber, ref brandAndProduct, file, ref fileName);
                    modalWidget.Size = new Size(120, 150);
                    panel.Controls.Add(modalWidget);
                    modalWidget.BringToFront();
                }
            }
        }
    }
}