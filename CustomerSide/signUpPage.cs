using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;
using OrdersPage;

class SignUpForm : Form
{
    //variables
    string customersPath = @"ManagerSide\datas\customers";

    bool isGenderChoosed = false;

    //numbers
    Regex numRegex = new Regex(@"^\d$");
    enum genderChoosem
    {
        male, female
    }

    private string currentUserName;
    private string currentPassword;
    //heading
    RoundLabel label = new RoundLabel();
    //username area
    TextBox userName = new TextBox();
    //password area
    TextBox pass = new TextBox();
    //logIn button
    RJButton logIn = new RJButton();
    //signIn button
    RJButton signIn = new RJButton();
    //label for radio button area
    RoundLabel rdlbl = new RoundLabel();
    //radioButtonForChoosingGender, E=male , K=female
    RadioButton genderE = new RadioButton();
    RadioButton genderK = new RadioButton();
    Label genderTexts = new Label();

    genderChoosem gender;


    public SignUpForm()
    {
        //backround color.
        BackColor = Color.FromArgb(255, 230, 204);

        //heading
        label.Size = new Size(550, 80);
        label.Location = new Point(120, 10);
        label.backColor = BackColor;
        label.borderColor = Color.Black;
        label.borderWidth = 2;
        label.cornerRadius = 20;
        label.Text = "Enter a name and a password which is must be 5 digit";
        label.ForeColor = Color.Black;
        Controls.Add(label);


        //textField for name
        userName.Size = new Size(220, 25);
        userName.Location = new Point(300, 120);
        userName.PlaceholderText = "username";
        userName.TextChanged += new System.EventHandler(changeOfUserName);
        userName.BackColor = BackColor;
        userName.ForeColor = Color.Black;
        userName.BorderStyle = BorderStyle.FixedSingle;
        userName.MaxLength = 15;
        Controls.Add(userName);


        //textField for password
        pass.Size = new Size(220, 25);
        pass.Location = new Point(300, 180);
        pass.PlaceholderText = "password";
        pass.TextChanged += new System.EventHandler(changeOfPassword);

        pass.BackColor = BackColor;
        pass.ForeColor = Color.Black;
        pass.BorderStyle = BorderStyle.FixedSingle;
        pass.MaxLength = 5;
        Controls.Add(pass);


        //label for radio button
        rdlbl.Size = new Size(200, 40);
        rdlbl.Location = new Point(310, 230);
        rdlbl.backColor = BackColor;
        rdlbl.borderColor = Color.Black;
        rdlbl.borderWidth = 2;
        rdlbl.cornerRadius = 20;
        rdlbl.ForeColor = Color.Black;
        Controls.Add(rdlbl);


        //radio button area gender texts
        genderTexts.Text = "male :         female :";
        genderTexts.Location = new Point(320, 240);
        genderTexts.Size = new Size(175, 20);
        Controls.Add(genderTexts);
        genderTexts.BringToFront();

        //radio button for choosing gender
        genderE.Location = new Point(370, 240);
        genderE.Size = new Size(20, 20);
        genderE.Click += new System.EventHandler(this.genderChoosemFunc);
        Controls.Add(genderE);
        genderE.BringToFront();

        //radio button for choosing gender
        genderK.Location = new Point(460, 240);
        genderK.Size = new Size(20, 20);
        genderK.Click += new System.EventHandler(this.genderChoosemFunc);
        Controls.Add(genderK);
        genderK.BringToFront();


        //SignUp button
        signIn.Size = new Size(200, 100);
        signIn.Location = new Point(310, 300);
        signIn.BackColor = BackColor;
        signIn.BorderColor = Color.Black;
        signIn.BorderRadius = 10;
        signIn.BorderSize = 2;
        signIn.Text = "SingUp";
        signIn.ForeColor = Color.Black;
        signIn.Click += new System.EventHandler(this.signUpAction);
        Controls.Add(signIn);

        currentPassword = pass.Text;
        currentUserName = userName.Text;

    }

    private void signUpAction(object sender, EventArgs e)
    {
        /*TO DO:: 
    name input regex
    is username in cutomer list.
    if there is,throw you can't use this userName.

    if name is suitable, program will shift to second stage that is password controll...

    if verifications are okay, then create a file named userName
    create two more files. one of them is userName$info other is userName$order
    */
        if (nameControll(ref currentUserName) && passwordControll(ref currentPassword) && isGenderChoosed)
        {
            createCustomer(ref currentUserName);
        }
    }



    //these two functions updates inputs dynamically
    private void changeOfUserName(object sender, EventArgs e)
    {
        currentUserName = this.userName.Text;
        nameControll(ref currentUserName);
    }
    private void changeOfPassword(object sender, EventArgs e)
    {
        currentPassword = this.pass.Text;
        passwordControll(ref currentPassword);
    }
    //above two
    private void genderChoosemFunc(object sender, EventArgs e)
    {
        isGenderChoosed = true;
        if (genderE.Checked)
        {
            genderK.Checked = false;
            gender = genderChoosem.male;
        }
        else
        {
            genderE.Checked = false;
            gender = genderChoosem.female;
        }
    }


    //this function dynamically scans is name input already exist in customer list.
    private bool nameControll(ref string name)
    {
        //is customer exist, via it's regedit.
        string folderName = customersPath + @"\" + name;

        if (Directory.Exists(folderName))
        {
            userName.ForeColor = Color.Red;
            return false;//username cant taken.
        }
        else
        {
            userName.ForeColor = Color.Green;
            return true;// username can taken.
        }
    }

    //this method dynamically read password.
    private bool passwordControll(ref string passw)
    {
        bool isPassSuitable = false;
        //the password must be 5 digit
        //and only can be numeric
        //controlls is password numeric
        foreach (char l in passw)
        {
            if (numRegex.IsMatch(l.ToString()) && passw.Length == 5)
            {
                this.pass.ForeColor = Color.Green;
                isPassSuitable = true;
            }
            else
            {
                this.pass.ForeColor = Color.Red;
                isPassSuitable = false;
            }
        }
        return isPassSuitable;
    }

    private void createCustomer(ref string name)
    {
        //main customer folder
        string folderName = customersPath + @"\" + name;
        //creating neccesary folders that will keeps customers info and order data.
        Directory.CreateDirectory(folderName);
        Directory.CreateDirectory(folderName + @"\" + name + "$info");
        Directory.CreateDirectory(folderName + @"\" + name + "$order");

        //creating txt file that is include customer's data
        StreamWriter sw = new StreamWriter(folderName + @"\" + name + "$info" + @"\" + "data.txt");
        sw.WriteLine("&name:" + name);
        sw.WriteLine("&password:" + currentPassword);
        sw.WriteLine("&gender:" + gender);
        sw.WriteLine("&balance:" + 1000);
        sw.Close();
        sw.Dispose();


        //start marketplacePage
        Market market=new Market(ref name);
        market.Size=this.Size;
        market.ShowDialog();
        this.Dispose();
        this.Close();

    }


}