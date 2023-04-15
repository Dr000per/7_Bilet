using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_Bilet
{
    public partial class Form1 : Form
    {
        DataBase db = new DataBase();
        DataTable tovar;
        int currentRow = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel_next_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentRow != tovar.Rows.Count)
            {
                if (currentRow < tovar.Rows.Count - 1)
                {
                    currentRow++;
                    SetData();
                }
            }
        }

        private void linkLabel_before_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentRow >= 1)
            {
                currentRow--;
                SetData();
            }
        }

        private void SetData()
        {

            var tovRow1 = tovar.Rows[currentRow].ItemArray;
            label_article1.Text = tovRow1[1].ToString();
            label_title.Text = tovRow1[2].ToString();
            label_price.Text = tovRow1[3].ToString();

        }

        private void UpdateData()
        {
            tovar = db.ExecuteSql(@"select pricecat.name as Категория_товара, tovar.article as Артикул, tovar.name as Название_товара, tovar.price as Цена_товара from tovar, pricecat where tovar.id_pricecat = pricecat.id;");
            SetData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            UpdateData();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var y = Convert.ToDouble(row.Cells[3].Value);
                if (y < 5000)
                {
                    chart1.Series[0].Points.AddY(y);
                }
                else if (y > 10000)
                {
                    chart1.Series[2].Points.AddY(y);
                }
                else
                {
                    chart1.Series[1].Points.AddY(y);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateData();
            dataGridView1.DataSource = tovar;
        }
    }
}
