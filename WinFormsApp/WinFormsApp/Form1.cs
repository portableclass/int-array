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
            //var menuActions = new Dictionary<string, Values>
            //{
            //    //{ "inputArrHand", new Values(inputArrHand, new Arguments(obj.Name == "toolStripMenuItem4"? dataGridView1 : dataGridView2))},
            //    //{ "toolStripMenuItem5", new Values(inputArrHand, new Arguments(dataGridView2))},
            //    { "toolStripMenuItem6", new Values(inputArrRandom, new Arguments(dataGridView1, textBox1))},
            //    { "toolStripMenuItem7", new Values(inputArrRandom, new Arguments(dataGridView2, textBox2))},
            //    { "toolStripMenuItem8", new Values(inputArrFromFile, new Arguments(dataGridView1))},
            //    { "toolStripMenuItem9", new Values(inputArrFromFile, new Arguments(dataGridView2))},
            //    { "toolStripMenuItem10", new Values(sumElementsArray, new Arguments(dataGridView1))},
            //    { "toolStripMenuItem11", new Values(sumElementsArray, new Arguments(dataGridView2))},
            //    { "toolStripMenuItem12", new Values(countMultiples, new Arguments(dataGridView1))},
            //    { "toolStripMenuItem13", new Values(countMultiples, new Arguments(dataGridView2))},
            //    //////////////////////////////////////////////////////////////////////////////////
            //    { "StripMenuItemSubtraction1", new Values(operationsArrays, new Arguments(dataGridView2, dataGridView1, "minus"))},
            //    { "StripMenuItemSubtraction2", new Values(operationsArrays, new Arguments(dataGridView1, dataGridView2, "minus"))},
            //    { "ToolStripMenuItemSum", new Values(operationsArrays, new Arguments(dataGridView1, dataGridView2, "plus"))},
            //    { "toolStripMenuItem16", new Values(decrementAndIncrement, new Arguments(dataGridView1, "plus"))},
            //    { "toolStripMenuItem17", new Values(decrementAndIncrement, new Arguments(dataGridView2, "plus"))},
            //    { "toolStripMenuItem18", new Values(decrementAndIncrement, new Arguments(dataGridView1, "minus"))},
            //    { "toolStripMenuItem19", new Values(decrementAndIncrement, new Arguments(dataGridView2, "minus"))},
            //    { "toolStripMenuItem20", new Values(moduleArray, new Arguments(dataGridView1))},
            //    { "toolStripMenuItem21", new Values(moduleArray, new Arguments(dataGridView2))},
            //    { "toolStripMenuItem22", new Values(searchY, new Arguments(dataGridView1))},
            //    { "toolStripMenuItem23", new Values(searchY, new Arguments(dataGridView2))},
            //};
            //menuActions[obj.Name].callMethod();

            var method = obj.Name switch
            {
                "toolStripMenuItem4" => new Context(inputArrHand, dataGridView1),
                "toolStripMenuItem5" => new Context(inputArrHand, dataGridView2)
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



        //private void inputArrHand(Arguments obj)
        //{
        //    int i = obj.dataGridView.Name.Length - 1;

        //    try
        //    {
        //        if (obj.dataGridView.ColumnCount == 0)
        //            obj.dataGridView.ColumnCount = Convert.ToInt32(textBox1.Text);

        //        obj.dataGridView.ReadOnly = false;
        //        obj.dataGridView.RowCount = 1;
        //        for (int j = 0; j < obj.dataGridView.ColumnCount; ++j)
        //        {
        //            obj.dataGridView.Columns[j].Name = (j + 1).ToString();
        //        }
        //    }
        //    catch (FormatException)
        //    {

        //        MessageBox.Show($"Enter length of {obj.dataGridView.Name[i]} array", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        private void inputArrHand(DataGridView obj)
        {
            int i = obj.Name.Length - 1;

            try
            {
                if (obj.ColumnCount == 0)
                    obj.ColumnCount = Convert.ToInt32(textBox1.Text);

                obj.ReadOnly = false;
                obj.RowCount = 1;
                // нахуя если при батн клике колво столбцов итак изменится
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    obj.Columns[j].Name = (j + 1).ToString();
                }
            }
            catch (FormatException)
            {

                MessageBox.Show($"Enter length of {obj.Name[i]} array", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void inputArrRandom(Arguments obj)
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
                // почему +1, программа крашится если диапазон начинать с меньшего числа
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
        private void inputArrFromFile(Arguments obj)
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
        private void sumElementsArray(Arguments obj)
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
        private void countMultiples(Arguments obj)
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
                //count = IntArray.CountMultiples(n, x);
                //MessageBox.Show($"Количество элементов массива { obj.dataGridView.Name[i]} кратных числу {x}:\n\n\t\t {count}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void operationsArrays(Arguments obj)
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

                ////////// схуяли 3
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
        private void decrementAndIncrement(Arguments obj)
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
        private void moduleArray(Arguments obj)
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

                //n = IntArray.ModuleArray(n);

                //for (int j = 0; j < obj.dataGridView.ColumnCount; j++)
                //{
                //    obj.dataGridView.Rows[0].Cells[j].Value = n[j];
                //}

            }
            catch (Exception exc)
            {

                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void searchY(Arguments obj)
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

                //int y = IntArray.SearchY(n);
                //MessageBox.Show($"Значение y для 15 варианта в массиве {obj.dataGridView.Name[i]}:\n\n\t {y}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {

                MessageBox.Show($"{exc.Message} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
    class Values
    {
        Action<Arguments> menuAction;
        public Arguments args { get; set; }
        public Values(Action<Arguments> menuAction, Arguments args)
        {
            this.menuAction = menuAction;
            this.args = args;
        }
        public void callMethod()
        {
            menuAction(args);
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
            this.dataGridView = obj;
        }
        public Arguments(DataGridView obj, TextBox textBox)
        {
            this.dataGridView = obj;
            this.textBox = textBox;
        }
        public Arguments(DataGridView obj, DataGridView objSec, string operation)
        {
            this.dataGridView = obj;
            this.dataGridViewOpt = objSec;
            this.operation = operation;
        }
        public Arguments(DataGridView obj, string operation)
        {
            this.dataGridView = obj;
            this.operation = operation;
        }
    }
    class Context
    {
        Action<DataGridView> menuAction;
        public DataGridView obj { get; set; }
        public Context(Action<DataGridView> menuAction, DataGridView obj)
        {
            this.menuAction = menuAction;
            this.obj = obj;
        }
        public void CallMethod()
        {
            menuAction(obj);
        }
    }
}
