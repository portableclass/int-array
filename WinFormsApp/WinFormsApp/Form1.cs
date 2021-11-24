using ClassLibraryForArray;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            dataGridView1.ColumnCount = coulmnCount;
            dataGridView2.ColumnCount = coulmnCount;
            dataGridView1.RowCount = rowCount;
            dataGridView2.RowCount = rowCount;
            for (int j = 0; j < coulmnCount; ++j)
            {
                dataGridView1.Columns[j].Name = (j + 1).ToString();
                dataGridView2.Columns[j].Name = (j + 1).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int coulmnCount1, coulmnCount2;
            int prev1, prev2;

            if (textBox1.Text != "")
            {
                prev1 = dataGridView1.ColumnCount;
                coulmnCount1 = Convert.ToInt32(textBox1.Text);
                dataGridView1.ColumnCount = coulmnCount1;
                if (coulmnCount1 > prev1)
                    for (int j = 0; j < coulmnCount1; ++j)
                        dataGridView1.Columns[j].Name = (j + 1).ToString();
            }

            if (textBox2.Text != "")
            {
                prev2 = dataGridView1.ColumnCount;
                coulmnCount2 = Convert.ToInt32(textBox2.Text);
                dataGridView2.ColumnCount = coulmnCount2;
                if (coulmnCount2 > prev2)
                    for (int j = 0; j < coulmnCount2; ++j)
                        dataGridView2.Columns[j].Name = (j + 1).ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (e.KeyChar == 8))
                return;

            e.Handled = true;
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = sender as ToolStripMenuItem;

            var method = obj.Name switch
            {
                "toolStripMenuItem4" => new Context(InputArrHand, new Arguments(dataGridView1)),
                "toolStripMenuItem5" => new Context(InputArrHand, new Arguments(dataGridView2)),
                "toolStripMenuItem6" => new Context(InputArrRandom, new Arguments(dataGridView1, textBox1)),
                "toolStripMenuItem7" => new Context(InputArrRandom, new Arguments(dataGridView2, textBox2)),
                "toolStripMenuItem8" => new Context(InputArrFromFile, new Arguments(dataGridView1)),
                "toolStripMenuItem9" => new Context(InputArrFromFile, new Arguments(dataGridView2)),
                "toolStripMenuItem10" => new Context(SumElementsArray, new Arguments(dataGridView1)),
                "toolStripMenuItem11" => new Context(SumElementsArray, new Arguments(dataGridView2)),
                "toolStripMenuItem12" => new Context(CountMultiples, new Arguments(dataGridView1)),
                "toolStripMenuItem13" => new Context(CountMultiples, new Arguments(dataGridView2)),
                "toolStripMenuItem20" => new Context(OperationsArrays, new Arguments(dataGridView1, dataGridView2, "minus")),
                "toolStripMenuItem21" => new Context(OperationsArrays, new Arguments(dataGridView2, dataGridView1, "minus")),
                "toolStripMenuItem24" => new Context(OperationsArrays, new Arguments(dataGridView1, dataGridView2, "plus")),
                "toolStripMenuItem15" => new Context(DecrementAndIncrement, new Arguments(dataGridView1, "plus")),
                "toolStripMenuItem16" => new Context(DecrementAndIncrement, new Arguments(dataGridView2, "plus")),
                "toolStripMenuItem18" => new Context(DecrementAndIncrement, new Arguments(dataGridView1, "minus")),
                "toolStripMenuItem19" => new Context(DecrementAndIncrement, new Arguments(dataGridView2, "minus")),
                "toolStripMenuItem23" => new Context(FindСlosestToAvg, new Arguments(dataGridView1)),
                "toolStripMenuItem25" => new Context(FindСlosestToAvg, new Arguments(dataGridView2))
            };

            method.CallMethod();
        }


        private void cellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            try
            {
                if (grid.Rows[0].Cells[e.ColumnIndex].Value == null)
                    throw new FormatException();
                Convert.ToInt32(grid.Rows[0].Cells[e.ColumnIndex].Value);
            }
            catch (FormatException)
            {
                MessageBox.Show($"Ошибка ввода в таблице 1 значение ячейки {e.ColumnIndex + 1} не соответсвует формату", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                grid.Rows[0].Cells[e.ColumnIndex].Value = "";
            }


        }


        private void InputArrHand(Arguments args)
        {
            args.dataGridView.ReadOnly = false;
        }

        private void InputArrRandom(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;
            try
            {
                int a, b;
                a = Convert.ToInt32(textBox4.Text);
                b = Convert.ToInt32(textBox5.Text);

                if (obj.dataGridView.ColumnCount == 0)
                {
                    if (obj.textBox.Text == "0")
                        throw new FormatException();
                    obj.dataGridView.ColumnCount = Convert.ToInt32(obj.textBox.Text);
                }


                obj.dataGridView.RowCount = 1;
                IntArray n = IntArray.RandomIntArray(obj.dataGridView.ColumnCount, a, b + 1);
                for (int j = 0; j < obj.dataGridView.ColumnCount; ++j)
                {
                    obj.dataGridView.Columns[j].Name = (j + 1).ToString();
                    obj.dataGridView.Rows[0].Cells[j].Value = n[j].ToString();
                }

            }
            catch (FormatException)
            {

                MessageBox.Show($"Enter correct intervals or length of {obj.dataGridView.Name[i]} array", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }
        private void InputArrFromFile(Arguments obj)
        {
            try
            {
                string fileAddress = Microsoft.VisualBasic.Interaction.InputBox("Введите путь для открытия файла. Например: C:\\Users\\User\\Desktop\\t\\t.txt");
                IntArray n = IntArray.ArrayFromTextFile(fileAddress);
                if (n.Length == 0)
                    throw new Exception("file is empty");

                obj.dataGridView.ColumnCount = n.Length;
                obj.dataGridView.RowCount = 1;
                for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
                {
                    obj.dataGridView.Columns[i].Name = (i + 1).ToString();
                    obj.dataGridView.Rows[0].Cells[i].Value = n[i].ToString();
                }

            }
            catch (FormatException)
            {
                MessageBox.Show($"File is incorrect format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"File no found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SumElementsArray(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;
            int sum = 0;
            try
            {
                if (obj.dataGridView.ColumnCount == 0)
                    throw new Exception("array length is null");
                IntArray n = new IntArray(obj.dataGridView.ColumnCount);
                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                {
                    n[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);
                }
                sum = IntArray.SumArray(n);
                MessageBox.Show($"Сумма элементов массива {obj.dataGridView.Name[i]}:\n\n\t {sum}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CountMultiples(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;

            try
            {
                int count = 0;
                int x = Convert.ToInt32(textBox3.Text);
                if (obj.dataGridView.ColumnCount == 0)
                    throw new Exception("array length is null");
                IntArray n = new IntArray(obj.dataGridView.ColumnCount);

                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                {
                    if (obj.dataGridView.Rows[0].Cells[j].Value == null)
                        throw new Exception("not all cells of { obj.Name[i] } array were filled");
                    n[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);
                }
                count = IntArray.CountMultiples(n, x);
                MessageBox.Show($"Количество элементов массива { obj.dataGridView.Name[i]} кратных числу {x}:\n\n\t\t {count}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (FormatException)
            {

                MessageBox.Show($"Введите число которому надо найти кратное ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OperationsArrays(Arguments obj)
        {

            try
            {
                int[] columnCount = new int[2];
                if (obj.dataGridView.ColumnCount == 0)
                    throw new Exception("1 array length is null");

                if (obj.dataGridViewOpt.ColumnCount == 0)
                    throw new Exception("2 array length is null");

                columnCount[0] = obj.dataGridView.ColumnCount;
                columnCount[1] = obj.dataGridViewOpt.ColumnCount;

                if (obj.dataGridView.ColumnCount < obj.dataGridViewOpt.ColumnCount)
                    obj.dataGridView.ColumnCount = obj.dataGridViewOpt.ColumnCount;
                else
                    obj.dataGridViewOpt.ColumnCount = obj.dataGridView.ColumnCount;

                IntArray a = new IntArray(obj.dataGridView.ColumnCount);
                IntArray b = new IntArray(obj.dataGridViewOpt.ColumnCount);



                for (int j = 0; j < obj.dataGridViewOpt.ColumnCount; j++)
                {
                    a[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);
                    b[j] = Convert.ToInt32(obj.dataGridViewOpt.Rows[0].Cells[j].Value);
                }

                IntArray result;
                if (obj.operation == "plus")
                    result = a + b;
                else
                    result = a - b;

                dataGridView3.RowCount = 1;
                dataGridView3.ColumnCount = obj.dataGridView.ColumnCount;
                for (int j = 0; j < obj.dataGridViewOpt.ColumnCount; j++)
                {
                    dataGridView3.Columns[j].Name = (j + 1).ToString();
                    dataGridView3.Rows[0].Cells[j].Value = result[j];
                }
                obj.dataGridView.ColumnCount = columnCount[0];
                obj.dataGridViewOpt.ColumnCount = columnCount[1];
            }
            catch (Exception exc)
            {

                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void DecrementAndIncrement(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;
            try
            {
                if (obj.dataGridView.ColumnCount == 0)
                    throw new Exception($"{obj.dataGridView.Name[i]} array length is null");
                IntArray n = new IntArray(obj.dataGridView.ColumnCount);
                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                {
                    n[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);
                }

                IntArray result = n;
                if (obj.operation == "plus")
                    result++;
                else
                    result--;
                dataGridView3.RowCount = 1;
                dataGridView3.ColumnCount = obj.dataGridView.ColumnCount;
                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                {
                    dataGridView3.Columns[j].Name = (j + 1).ToString();
                    dataGridView3.Rows[0].Cells[j].Value = result[j];
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FindСlosestToAvg(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;
            try
            {
                if (obj.dataGridView.ColumnCount == 0)
                    throw new Exception($"{obj.dataGridView.Name[i]} array length is null");
                IntArray n = new IntArray(obj.dataGridView.ColumnCount);
                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                    n[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);

                IntArray result = IntArray.FindСlosestToAvg(n);
                string message = "";
                for (int j = 0; j < result.Length; j++)
                    message += $" {result[j]};";

                MessageBox.Show($"The elements closest to the arithmetic mean in the array {obj.dataGridView.Name[i]} have indexes:\n\n\t {message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exc)
            {

                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
    class Arguments
    {
        public DataGridView dataGridView { get; set; }
        public DataGridView dataGridViewOpt { get; set; }
        public TextBox textBox { get; set; }
        public string operation { get; set; }
        public Arguments(DataGridView obj)
        {
            dataGridView = obj;
        }
        public Arguments(DataGridView obj, TextBox textBox)
        {
            dataGridView = obj;
            this.textBox = textBox;
        }
        public Arguments(DataGridView obj, DataGridView objOpt, string operation)
        {
            dataGridView = obj;
            dataGridViewOpt = objOpt;
            this.operation = operation;
        }
        public Arguments(DataGridView obj, string operation)
        {
            dataGridView = obj;
            this.operation = operation;
        }
    }
    class Context
    {
        Action<Arguments> menuAction;
        private Arguments args { get; set; }
        private TextBox textBox { get; set; }
        public Context(Action<Arguments> menuAction, Arguments args)
        {
            this.menuAction = menuAction;
            this.args = args;
        }
        public void CallMethod()
        {
            menuAction(args);
        }
    }
}
