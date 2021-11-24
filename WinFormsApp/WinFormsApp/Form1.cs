using ClassLibraryForArray;
using Microsoft.VisualBasic;
using System;
using System.IO;
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

            IntArray.Notify += StatusBarMessage;
        }

        private void StatusBarMessage(string message)
        {
            toolStripStatusLabel3.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "0" || textBox2.Text == "0")
                    throw new Exception($"Incorrect array length.");

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
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                "toolStripMenuItem4" => new Context(InputArrManually, new Arguments(dataGridView1)),
                "toolStripMenuItem5" => new Context(InputArrManually, new Arguments(dataGridView2)),
                "toolStripMenuItem6" => new Context(InputArrRandom, new Arguments(dataGridView1, textBox1)),
                "toolStripMenuItem7" => new Context(InputArrRandom, new Arguments(dataGridView2, textBox2)),
                "toolStripMenuItem8" => new Context(InputArrFromFile, new Arguments(dataGridView1)),
                "toolStripMenuItem9" => new Context(InputArrFromFile, new Arguments(dataGridView2)),
                "toolStripMenuItem10" => new Context(SumArray, new Arguments(dataGridView1)),
                "toolStripMenuItem11" => new Context(SumArray, new Arguments(dataGridView2)),
                "toolStripMenuItem12" => new Context(CountMultiples, new Arguments(dataGridView1)),
                "toolStripMenuItem13" => new Context(CountMultiples, new Arguments(dataGridView2)),
                "toolStripMenuItem20" => new Context(OperationsWithArrays, new Arguments(dataGridView1, dataGridView2, "minus")),
                "toolStripMenuItem21" => new Context(OperationsWithArrays, new Arguments(dataGridView2, dataGridView1, "minus")),
                "toolStripMenuItem24" => new Context(OperationsWithArrays, new Arguments(dataGridView1, dataGridView2, "plus")),
                "toolStripMenuItem15" => new Context(DecrementAndIncrement, new Arguments(dataGridView1, "plus")),
                "toolStripMenuItem16" => new Context(DecrementAndIncrement, new Arguments(dataGridView2, "plus")),
                "toolStripMenuItem18" => new Context(DecrementAndIncrement, new Arguments(dataGridView1, "minus")),
                "toolStripMenuItem19" => new Context(DecrementAndIncrement, new Arguments(dataGridView2, "minus")),
                "toolStripMenuItem23" => new Context(FindСlosestToAvg, new Arguments(dataGridView1)),
                "toolStripMenuItem25" => new Context(FindСlosestToAvg, new Arguments(dataGridView2))
            };

            method.DoMethod();
        }


        private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView obj = sender as DataGridView;
            int i = obj.Name.Length - 1;
            try
            {
                if (obj.Rows[0].Cells[e.ColumnIndex].Value == null)
                    throw new Exception("You have not entered anything into the cell.\nIt will be assigned the value 0.");
                Convert.ToInt32(obj.Rows[0].Cells[e.ColumnIndex].Value);
            }
            catch (FormatException)
            {
                MessageBox.Show($"Input error in {obj.Name[i]} array. The cell number {e.ColumnIndex + 1} has an incorrect value format.", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Rows[0].Cells[e.ColumnIndex].Value = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj.Rows[0].Cells[e.ColumnIndex].Value = 0;
            }
        }

        private void InputArrManually(Arguments obj)
        {
            obj.dataGridView.ReadOnly = false;
        }

        private void InputArrRandom(Arguments obj)
        {
            try
            {
                int a = Convert.ToInt32(textBox4.Text);
                int b = Convert.ToInt32(textBox5.Text);

                IntArray arr = IntArray.RandomIntArray(obj.dataGridView.ColumnCount, a, b);

                for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
                    obj.dataGridView.Rows[0].Cells[i].Value = arr[i].ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show($"Incorrect intervals.", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void InputArrFromFile(Arguments obj)
        {
            try
            {
                string fileAddress = Interaction.InputBox("Enter the directory of the file\nfrom which you want to extract the array.\nExample: C:\\Windows\\input.txt");
                IntArray arr = IntArray.ArrayFromTextFile(fileAddress);
                if (arr.Length == 0)
                    throw new Exception("File is empty.");

                obj.dataGridView.ColumnCount = arr.Length;
                for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
                {
                    obj.dataGridView.Columns[i].Name = (i + 1).ToString();
                    obj.dataGridView.Rows[0].Cells[i].Value = arr[i].ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show($"File is incorrect format.", "FILE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"File not found.", "FILE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "FILE_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SumArray(Arguments obj)
        {
            IntArray n = new IntArray(obj.dataGridView.ColumnCount);

            for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                n[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);

            IntArray.SumArray(n);
        }

        private void CountMultiples(Arguments obj)
        {
            int i = obj.dataGridView.Name.Length - 1;
            try
            {
                int x = Convert.ToInt32(textBox3.Text);
                if (x == 0)
                    throw new FormatException();

                IntArray arr = new IntArray(obj.dataGridView.ColumnCount);

                for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                {
                    if (obj.dataGridView.Rows[0].Cells[j].Value == null)
                        throw new Exception($"In array { obj.dataGridView.Name[i] } cell number {obj.dataGridView.Rows[0].Cells[j]} is not filled");
                    arr[j] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[j].Value);
                }
                
                IntArray.CountMultiples(arr, x);
            }
            catch (FormatException)
            {
                MessageBox.Show($"The number to search for multiples of it\nhas an incorrect format or is equal to 0", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"{exc.Message}", "INPUT_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OperationsWithArrays(Arguments obj)
        {
            int[] columnCount = new int[2];
            columnCount[0] = obj.dataGridView.ColumnCount;
            columnCount[1] = obj.dataGridViewOpt.ColumnCount;

            if (obj.dataGridView.ColumnCount < obj.dataGridViewOpt.ColumnCount)
                obj.dataGridView.ColumnCount = obj.dataGridViewOpt.ColumnCount;
            else
                obj.dataGridViewOpt.ColumnCount = obj.dataGridView.ColumnCount;

            IntArray a = new IntArray(obj.dataGridView.ColumnCount);
            IntArray b = new IntArray(obj.dataGridViewOpt.ColumnCount);

            for (int i = 0; i < obj.dataGridViewOpt.ColumnCount; i++)
            {
                a[i] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[i].Value);
                b[i] = Convert.ToInt32(obj.dataGridViewOpt.Rows[0].Cells[i].Value);
            }

            IntArray result;
            if (obj.operation == "plus")
                result = a + b;
            else
                result = a - b;

            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = obj.dataGridView.ColumnCount;
            for (int i = 0; i < obj.dataGridViewOpt.ColumnCount; i++)
            {
                dataGridView3.Columns[i].Name = (i + 1).ToString();
                dataGridView3.Rows[0].Cells[i].Value = result[i];
            }
            obj.dataGridView.ColumnCount = columnCount[0];
            obj.dataGridViewOpt.ColumnCount = columnCount[1];
        }
        private void DecrementAndIncrement(Arguments obj)
        {
            IntArray arr = new IntArray(obj.dataGridView.ColumnCount);

            for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
                arr[i] = Convert.ToInt32(obj.dataGridView.Rows[0].Cells[i].Value);

            if (obj.operation == "plus")
                arr++;
            else
                arr--;

            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = obj.dataGridView.ColumnCount;

            for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
            {
                dataGridView3.Columns[i].Name = (i + 1).ToString();
                dataGridView3.Rows[0].Cells[i].Value = arr[i];
            }

        }

        private void FindСlosestToAvg(Arguments obj)
        {
            double[] n = new double[obj.dataGridView.ColumnCount];
            for (int i = 0; i < obj.dataGridView.ColumnCount; i++)
                n[i] = Convert.ToDouble(obj.dataGridView.Rows[0].Cells[i].Value);

            IntArray.FindСlosestToAvg(n);
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
        public Context(Action<Arguments> menuAction, Arguments args)
        {
            this.menuAction = menuAction;
            this.args = args;
        }
        public void DoMethod()
        {
            menuAction(args);
        }
    }
}
