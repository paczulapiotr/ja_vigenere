﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vigenere_Window
{



    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Click_Start(object sender, EventArgs e)
        {

            VCipher v = new VCipher();

            if (comboBox1.SelectedIndex == 0) // Selected C#
            {
                string pw = textBox1.Text; //Plain Text Cipher Key
                string s0 = textBox2.Text; //Plain Text Cipher Text
                textBox3.Text = v.encrypt(s0, pw, 1); // Update Encrypted box.

            } else if (comboBox1.SelectedIndex == 1) // Selected Asm
            {
                string pw = textBox1.Text;
                string s0 = textBox3.Text;
                textBox2.Text = v.encrypt(s0, pw, -1);

            } else
            {
                MessageBox.Show("No dropdown selected in ComboBox.");
            }

        }
    }

    class VCipher
    {
        public string encrypt(string txt, string pw, int d)
        {
            int pwi = 0, tmp;
            string ns = "";
            txt = txt.ToUpper();
            pw = pw.ToUpper();
            foreach (char t in txt)
            {
                if (t < 65) continue;
                tmp = t - 65 + d * (pw[pwi] - 65);
                if (tmp < 0) tmp += 26;
                ns += Convert.ToChar(65 + (tmp % 26));
                if (++pwi == pw.Length) pwi = 0;
            }

            return ns;
        }

    };
}