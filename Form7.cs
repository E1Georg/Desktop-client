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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Form4 Tmain_db = this.Owner as Form4;
            int delet = Tmain_db.dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = Tmain_db.dataGridView1.Rows[delet].Cells[0].Value.ToString();
            textBox2.Text = Tmain_db.dataGridView1.Rows[delet].Cells[1].Value.ToString();
            textBox3.Text = Tmain_db.dataGridView1.Rows[delet].Cells[2].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(Tmain_db.dataGridView1.Rows[delet].Cells[3].Value);
            numericUpDown1.Value = Convert.ToInt32(Tmain_db.dataGridView1.Rows[delet].Cells[4].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            using (MyDbContext context = new MyDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    int check = Convert.ToInt32(textBox1.Text);
                    UserException exc = context.UserExceptions.FirstOrDefault(p => p.ID == check);

                    if (exc != null)
                    {
                        exc.Message = textBox2.Text;
                        exc.TargetSite = textBox3.Text;
                        exc.dateTimeExc = Convert.ToDateTime(dateTimePicker1.Value);
                        exc.indexForm = Convert.ToInt32(numericUpDown1.Value);
                    }
                    context.SaveChanges();


                    DialogResult dr = MessageBox.Show("Внести новые данные в базу данных?",
                     "Сохранить?",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);

                    if (dr == DialogResult.Yes) transaction.Commit();
                    else transaction.Rollback();                   
                }                
            }
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
    }
}
