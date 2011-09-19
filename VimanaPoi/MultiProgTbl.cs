using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace VimanaPoi
{
    class MultiProgTbl
    {
        public TextBox[] t2progBox;
        public ComboBox[] t2partBox;
        public ComboBox[] t2operBox;
        public TextBox[] t2gpbox;
        public TextBox[] t2bpbox;
        public Control[] t2snd;
        public Control[] t2stp;
        public Button[] t2btn;
        public void MakeReadOnly(Control[] curObj)
        {
            for (int i = 0; i < curObj.Length; i++)
            {
                curObj[i].Enabled = false;
                curObj[i].BackColor = Color.White;
            }
        }
        public void MakeNotReadOnly(Control[] curObj)
        {
            for (int i = 0; i < curObj.Length; i++)
            {
                curObj[i].Enabled = true;
                curObj[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            }
        }
        public bool CheckNumeric(TextBox[] tb)
        {
            foreach (TextBox t in tb)
            {
                if (!IsAllDigits(t.Text) && t.Enabled == true)
                {
                    return false;
                }
            }
            return true;
        }
        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
