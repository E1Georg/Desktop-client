using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form3 : Form    {       
        
        public Form3()
        {
            InitializeComponent();
        } 
        
        private void Form3_Load(object sender, EventArgs e) { }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            MessageBox.Show("Автор: Шумков Ефим\n", "Информация", MessageBoxButtons.OK);
        }

        private void лабораторнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 firstForm = new Form1();
            this.Hide();
            firstForm.ShowDialog();

            if (DataBank.key == 1)
            {
                this.Show();
                DataBank.key = 0;
            }
            else
            {
                this.Close();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Visible = true;
                DataBank.needDB = true;
            }
            else
            {            
                DataBank.needDB = false;
                button1.Visible = false;
            }
           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button1.Visible = true;
                DataBank.needDB = true;
            }
            else
            {
                button1.Visible = false;
                DataBank.needDB = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 fourForm = new Form4(this);
            fourForm.Show();
        }
    }
}
