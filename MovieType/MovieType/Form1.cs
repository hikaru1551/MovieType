using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CKIP;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MovieType
{
    public partial class Form1 : Form
    {        
        Cws myCws;
        CwsWord myCwsWord = new CwsWord();
        CwsType myCwsType = new CwsType();

        public Form1()
        {
            InitializeComponent();
        }

        private void btStar_Click(object sender, EventArgs e)
        {
            try
            {
                myCws.OrginArticle = rtbEnterArticle.Text;

                rtbResult.Text = myCws.Answer().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myCws = myCwsWord;
        }

        private void rdbCKIP_CheckedChanged(object sender, EventArgs e)
        {
            myCws = myCwsWord;
        }

        private void rdbMovieType_CheckedChanged(object sender, EventArgs e)
        {
            myCws = myCwsType;            
        }

        private void btCleared_Click(object sender, EventArgs e)
        {
            rtbResult.Clear();
            rtbEnterArticle.Clear();            
        }
    }
}
