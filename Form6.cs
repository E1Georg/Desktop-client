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
    public partial class Form6 : Form
    {
        int main_I = 0;                
        public Form6()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 main_db = this.Owner as Form4;
            textBox1.Focus();

            if (main_I != 0) main_db.dataGridView1.Rows[main_I - 1].Selected = false;
            else main_db.dataGridView1.Rows[main_I].Selected = false;

            for (int i = main_I; i < main_db.dataGridView1.Rows.Count; i++)  
            {
                for (int j = 0; j < 5; j++)
                {
                    if (textBox1.Text.Contains(main_db.dataGridView1.Rows[i].Cells[j].Value.ToString()))
                    {
                        main_db.dataGridView1.CurrentCell = main_db.dataGridView1.Rows[i].Cells[j];
                        main_I = i + 1;
                        if (main_I == main_db.dataGridView1.Rows.Count) main_I = 0;

                        main_db.dataGridView1.Rows[i].Selected = true;
                        return;
                    }
                }

                main_I = i;
                if (main_I == main_db.dataGridView1.Rows.Count - 1) 
                {
                    main_I = 0;
                    i = -1;

                    DialogResult dr = MessageBox.Show("Начать с начала?", 
                        "Поиск завершён!", 
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                    if (dr == DialogResult.No)
                    {
                        button2.PerformClick();
                        return;
                    }
                }                
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void Form6_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
