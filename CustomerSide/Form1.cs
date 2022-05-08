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
using System.Text.RegularExpressions;
using System.IO;
using CustomerSide;

namespace ndpProje
{
    public partial class Form1 : Form
    {

        //variables
        string customersPath = @"ManagerSide\datas\customers";

        private string currentUserName;
        private string currentPassword;
        //heading
        RoundLabel label = new RoundLabel();
        //username area
        TextBox userName = new TextBox();
        //password area
        TextBox pass = new TextBox();
        //logIn button
        RJButton logInButton = new RJButton();
        //signIn button
        RJButton signInButton = new RJButton();



        public Form1()
        {
            InitializeComponent();
            //backround color.
            BackColor = Color.FromArgb(255, 230, 204);

            //heading
            label.Size = new Size(550, 80);
            label.Location = new Point(120, 10);
            label.backColor = BackColor;
            label.borderColor = Color.Black;
            label.borderWidth = 2;
            label.cornerRadius = 20;
            label.Text = "jigglypuff market";
            label.ForeColor = Color.Black;
            Controls.Add(label);


            //textField for name
            userName.Size = new Size(220, 25);
            userName.Location = new Point(300, 180);
            userName.PlaceholderText = "username";
            userName.TextChanged += new System.EventHandler(changeOfUserName);
            userName.BackColor = BackColor;
            userName.ForeColor = Color.Black;
            userName.BorderStyle = BorderStyle.FixedSingle;
            userName.MaxLength = 15;
            Controls.Add(userName);


            //textField for password
            pass.Size = new Size(220, 25);
            pass.Location = new Point(300, 240);
            pass.PlaceholderText = "password";
            pass.TextChanged += new System.EventHandler(changeOfPassword);

            pass.BackColor = BackColor;
            pass.ForeColor = Color.Black;
            pass.BorderStyle = BorderStyle.FixedSingle;
            pass.MaxLength = 5;
            Controls.Add(pass);







            //LogIn button
            logInButton.Size = new Size(100, 70);
            logInButton.Location = new Point(300, 300);
            logInButton.BackColor = BackColor;
            logInButton.BorderColor = Color.Black;
            logInButton.BorderRadius = 10;
            logInButton.BorderSize = 2;
            logInButton.Text = "LogIn";
            logInButton.ForeColor = Color.Black;
            logInButton.Click += new System.EventHandler(this.logInButtonAction);
            Controls.Add(logInButton);

            //SignIn button
            signInButton.Size = new Size(100, 70);
            signInButton.Location = new Point(415, 300);
            signInButton.BackColor = BackColor;
            signInButton.BorderColor = Color.Black;
            signInButton.BorderRadius = 10;
            signInButton.BorderSize = 2;
            signInButton.Text = "SingUp";
            signInButton.ForeColor = Color.Black;
            signInButton.Click += new System.EventHandler(this.signUpButtonAction);
            Controls.Add(signInButton);

            currentPassword = pass.Text;
            currentUserName = userName.Text;

        }
        //these two functions updates inputs dynamically
        private void changeOfUserName(object sender, EventArgs e)
        {
            currentUserName = this.userName.Text;
        }
        private void changeOfPassword(object sender, EventArgs e)
        {
            currentPassword = this.pass.Text;
        }
        //above two

        private void logInButtonAction(object sender, EventArgs e)
        {
            logIn(ref currentUserName, ref currentPassword);
        }

        private void signUpButtonAction(object sender, EventArgs e)
        {
            SignUpForm signUpPage = new SignUpForm();
            signUpPage.Size = new Size(800, 500);
            this.Hide();
            signUpPage.ShowDialog();
            this.Show();
        }



        private void logIn(ref string name, ref string pass)
        {

            string folderName = customersPath + @"\" + name;

            if (passwordControll(ref currentPassword, ref folderName, nameControll(ref name, ref folderName), ref name))
            {
                //start marketplacePage
                Market market = new Market(ref name);
                market.Size = this.Size;
                
                this.Hide();
                market.ShowDialog();

            }



        }

        //this method controlls is name match any folder name in customers folder.
        //returns true if name is exist in customer folder.
        private bool nameControll(ref string name, ref string folderAdress)
        {
            if (Directory.Exists(folderAdress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //this method reads customer's data file to get password.
        //returns true if the password that is entered true, false if it is not.
        private bool passwordControll(ref string passw, ref string folderAdress, bool isNameTrue, ref string name)
        {


            if (isNameTrue)
            {
                //reading cutomer data and obtaining password
                string userDataFileAdress = folderAdress + @"\" + name + "$info" + @"\" + "data.txt";
                string realPassword = File.ReadAllText(userDataFileAdress);

                int passwordStartIndex = realPassword.IndexOf("&password:");
                realPassword = realPassword.Substring(passwordStartIndex + 10, 5);
                if (currentPassword == realPassword)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
