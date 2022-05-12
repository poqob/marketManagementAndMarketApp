using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

namespace PQContentWidget
{
    static public class MarketFolderProcess
    {

    }

    //take file path, 
    //crate copy of that file with .tmp extension just in case order instruction keep stock num without ordered products.
    //create copy of that file with .temp extension and store how many product is ordering.
    //work on .tmp file

    //process: numericUpDown
    //arranging stock num for .temp and .tmp
    //if order have given, delete current .txt file and make .tmp->.txt;
    //send .temp file to customer$order file make extension .txt;
    //controlls if there was same file in customer$order:
    //if there was, merge two files and add one of them stock number to another one. same for totalPrice.
    //if there wasn't, do only moving. 

}