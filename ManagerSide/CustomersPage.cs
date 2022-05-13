using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;

namespace CustomerPage
{
    public class CustomersPage : Form
    {
        //creating flow layout panel that's task is have form controlls(buttons) and provide dynamic visual to scale up or down app frame.
        FlowLayoutPanel dynamicFlowLayoutPanel = new FlowLayoutPanel();

        public CustomersPage()
        {
            // creating form's base
            BackColor = Color.FromArgb(38, 38, 38);
            dynamicFlowLayoutPanel.Location = new Point(0, 0);
            dynamicFlowLayoutPanel.Dock = DockStyle.Fill;
            dynamicFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            dynamicFlowLayoutPanel.WrapContents = true;
            dynamicFlowLayoutPanel.TabIndex = 0;
            dynamicFlowLayoutPanel.AutoScroll = true;
            Controls.Add(dynamicFlowLayoutPanel);
            customerUIButonCreator();
        }

        private void customerUIButonCreator()
        {
            foreach (var item in Directory.GetDirectories(@"..\ManagerSide\datas\customers\"))
            {
                //creating buttons which names are customer names from customer data file. attempt them to flow layout panel
                RJButton button = new RJButton();
                button.Size = new Size(90, 90);
                button.BackColor = Color.FromArgb(38, 38, 38);
                button.BorderColor = Color.White;
                button.BorderRadius = 20;
                button.BorderSize = 2;
                button.Text = item.Substring(31);
                button.Click += delegate (object sender, EventArgs e) { kk(sender, e, button.Text, @"..\ManagerSide\datas\customers\"); };
                this.dynamicFlowLayoutPanel.Controls.Add(button);

            }
        }


        private void kk(object sender, EventArgs e, string customerName, string folderAdres)
        {
            string path = folderAdres + customerName + @"\" + customerName + @"$info\data.txt";
            string pathOfOrder = folderAdres + customerName + @"\" + customerName + @"$order\data.txt";
            string content = "*do you want to delete your customer from your customer registry\n-press yes to delete and no to ignore.\n";
            content += "\n-------------customer------------\n";
            content += File.ReadAllText(path);
            content += "\n--------------orders-------------\n";
            //content += File.ReadAllText(pathOfOrder);


            //message box for informating or to delete user
            DialogResult answer = MessageBox.Show(content, customerName, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
            if (answer == DialogResult.Yes)
            {
                Directory.Delete(folderAdres + customerName, true);

                this.Dispose();
                this.Close();
            }

        }
    }
}