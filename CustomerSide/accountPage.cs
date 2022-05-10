using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;
namespace Accountpage
{
    public class AccountPage:Form
    {
        public AccountPage(){
            this.Size = new Size(800, 500);
            this.CenterToParent();
            this.BackColor = Color.FromArgb(255, 230, 204);
        }
    }
}