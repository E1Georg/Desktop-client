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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;

            Form4 Tmain_db = this.Owner as Form4;
            if (Tmain_db != null)
            {
                int newId = findMaxIdForUserException(ref Tmain_db.dataGridView1) + 1;
                textBox1.Text = newId.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Добавление элементов
            Form4 main_db = this.Owner as Form4;

            if (main_db != null)
            {
                main_db.dataGridView1.AllowUserToAddRows = true;

                main_db.dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text, numericUpDown1.Value.ToString());
                DataBank.id_string = Convert.ToInt32(textBox1.Text);

                main_db.dataGridView1.AllowUserToAddRows = false;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 main_dbb = this.Owner as Form4;
            main_dbb.dataGridView1.AllowUserToAddRows = false;
            DataBank.id_string = -1;
           this.Close();            
        }     

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "1 - Project of Lab 1\n" +
                "2 - Project of Lab 2\n" +
                "3 - Error in Main Menu\n" +
                "4 - Error in DGV, Lab4\n" +
                "5 - Error in AddForm, Lab 4\n" +
                "6 - Error in FindForm, Lab 4\n",
                "Подсказка",                
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
                );
        }
        private static int findMaxIdForUserException(ref DataGridView table)
        {
            int maxId = -1;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int id = Convert.ToInt32(table.Rows[i].Cells[0].Value);
                if (id > maxId) maxId = id;
            }
            return maxId;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }            
    }
}
