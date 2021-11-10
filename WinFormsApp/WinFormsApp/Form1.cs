using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int coulmnCount = 15;
            int rowCount = 1;
            this.dataGridView1.ColumnCount = coulmnCount;
            this.dataGridView2.ColumnCount = coulmnCount;
            this.dataGridView1.RowCount = rowCount;
            this.dataGridView2.RowCount = rowCount;
            for (int j = 0; j < coulmnCount; ++j)
            {
                this.dataGridView1.Columns[j].Name = (j + 1).ToString();
                this.dataGridView2.Columns[j].Name = (j + 1).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int coulmnCount1, coulmnCount2;

            if (textBox1.Text != "")
            {
                coulmnCount1 = Convert.ToInt32(textBox1.Text);
                this.dataGridView1.ColumnCount = coulmnCount1;
                if (coulmnCount1 > 15)
                    for (int j = 0; j < coulmnCount1; ++j)
                        this.dataGridView1.Columns[j].Name = (j + 1).ToString();
            }

            if (textBox2.Text != "")
            {
                coulmnCount2 = Convert.ToInt32(textBox2.Text);
                this.dataGridView2.ColumnCount = coulmnCount2;
                if (coulmnCount2 > 15)
                    for (int j = 0; j < coulmnCount2; ++j)
                        this.dataGridView2.Columns[j].Name = (j + 1).ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar == 8))
                return;

            e.Handled = true;
        }
    }
}
