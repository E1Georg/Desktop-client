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
    public partial class Form4 : Form
    {
        public Form4(Form3 oldForm)
        {
            InitializeComponent();
        }

        int[] stringIdForSaveToBD = new int[] { -1 };
        int[] stringIdForRemoveInBD = new int[] { -1 };

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataRefresh();
        }

        private void dataRefresh()
        {
            dataGridView1.Visible = true;
            dataGridView1.AllowUserToAddRows = true;

            using (MyDbContext context = new MyDbContext())
            {
                var exc = context.UserExceptions.ToList();

                var listOfFieldNames = typeof(UserException).GetProperties().Select(f => f.Name).ToList();

                dataGridView1.RowCount = exc.Count;
                dataGridView1.ColumnCount = listOfFieldNames.Count;

                for (int i = 0; i < listOfFieldNames.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderText = listOfFieldNames[i].ToString();
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                int counter = 0;
                foreach (UserException elem in exc)
                {
                    dataGridView1.Rows[counter].Cells[0].Value = Convert.ToString(elem.ID);
                    dataGridView1.Rows[counter].Cells[1].Value = Convert.ToString(elem.Message);
                    dataGridView1.Rows[counter].Cells[2].Value = Convert.ToString(elem.TargetSite);
                    dataGridView1.Rows[counter].Cells[3].Value = Convert.ToString(elem.dateTimeExc);
                    dataGridView1.Rows[counter].Cells[4].Value = Convert.ToString(elem.indexForm);
                    dataGridView1.Rows[counter].ReadOnly = true;
                    counter++;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        // save data in db
        private void SAVE(object sender, EventArgs e)
        {
            if(stringIdForSaveToBD.Length != 1)
            {
                using (MyDbContext context = new MyDbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in stringIdForSaveToBD)
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (item.ToString() == dataGridView1.Rows[i].Cells[0].Value.ToString())
                                    {
                                        string MessageIntoMass = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                        string TargetSiteIntoMass = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                        DateTime dateTimeExcIntoMass = Convert.ToDateTime(dataGridView1.Rows[i].Cells[3].Value);
                                        int indexFormIntoMass = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                                        if (MessageIntoMass.Length > 45) MessageIntoMass.Substring(0, 45);
                                        if (TargetSiteIntoMass.Length > 45) TargetSiteIntoMass.Substring(0, 45);

                                        UserException exc = new UserException
                                        {
                                            Message = MessageIntoMass,
                                            TargetSite = TargetSiteIntoMass,
                                            dateTimeExc = dateTimeExcIntoMass,
                                            indexForm = indexFormIntoMass,
                                        };
                                        context.UserExceptions.Add(exc);
                                        break;
                                    }
                                }
                            }

                            try
                            {
                                context.SaveChanges();
                                Array.Resize(ref stringIdForSaveToBD, 1);
                                stringIdForSaveToBD[0] = -1;
                                MessageBox.Show("Данные в базу данных успешно добавлены!", "Уведомление", MessageBoxButtons.OK);
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show("Данные не были сохранены!\n" + err.Message, "Ошибка", MessageBoxButtons.OK);
                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                    dataRefresh();
                }                                
            }
            else
            {
                MessageBox.Show("Не найдено новых строк!\n Запрос к базе данных отправлен не будет!", "Предупреждение", MessageBoxButtons.OK);
            }
            
            if (stringIdForRemoveInBD.Length != 1)
            {
                using (MyDbContext context = new MyDbContext())
                {
                    for (int i = 1; i < stringIdForRemoveInBD.Length; i++) 
                    {
                        UserException Del = new UserException { ID = stringIdForRemoveInBD[i]};

                        //delete object in db
                        context.UserExceptions.Attach(Del);
                        context.UserExceptions.Remove(Del);
                    }

                    try
                    {
                        context.SaveChanges();
                        Array.Resize(ref stringIdForRemoveInBD, 1);
                        stringIdForSaveToBD[0] = -1;
                        MessageBox.Show("Данные из базы данных успешно удалены!", "Уведомление", MessageBoxButtons.OK);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Данные не были сохранены!\n" + error.Message, "Ошибка", MessageBoxButtons.OK);
                    }
                }
                dataRefresh();
            }
            else
            {
                MessageBox.Show("Не замечено изменений!\n Запрос к базе данных отправлен не будет!", "Предупреждение", MessageBoxButtons.OK);
            }                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 formFive = new Form5();
            formFive.Owner = this;
            formFive.ShowDialog();

            if (DataBank.id_string != -1)
            {
                Array.Resize(ref stringIdForSaveToBD, stringIdForSaveToBD.Length + 1);
                stringIdForSaveToBD[stringIdForSaveToBD.Length - 1] = DataBank.id_string;
                DataBank.id_string = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete string in dgv
            DialogResult dr = MessageBox.Show("Вы действительно хотите удалить выбранную строку?",
                        "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

            if (dr == DialogResult.Yes)
            {
                int delet = dataGridView1.SelectedCells[0].RowIndex;
                int DelInd = Convert.ToInt32(dataGridView1.Rows[delet].Cells[0].Value);
                dataGridView1.Rows.RemoveAt(delet);
                MessageBox.Show("Удалена строка с ID: " + DelInd + "\nЧтобы удалить запись из базы данных нажмите \"Сохранить\"", "Удаление", MessageBoxButtons.OK);

                Array.Resize(ref stringIdForRemoveInBD, stringIdForRemoveInBD.Length + 1);
                stringIdForRemoveInBD[stringIdForRemoveInBD.Length - 1] = DelInd;

                dataGridView1.Refresh();
            }       

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 formSix = new Form6();
            formSix.Owner = this;
            formSix.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 formSeven = new Form7();
            formSeven.Owner = this;
            formSeven.Show();          
        }
    }
}
