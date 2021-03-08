using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculatortest
{
    public partial class Main : Form
    {
        // List чисел
        List<int> listNumbers = new List<int>();
        // List операторов
        List<char> listOperators = new List<char>();
        // Результат
        double Result;
        // Значения строки для записи в  listNumbers (Смотреть в txtEnterNumbers)
        String txtForNumbers;
        
        public Main()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(eventhandlerKeyUp);
        }


        //-----------------------------------------------------------------------------------------------------
        //------------------------------- Кнопка Enter (Основные вычисления) ----------------------------------
        //-----------------------------------------------------------------------------------------------------
        private void btnEnter_Click(object sender, EventArgs e)
        {
            // Поиск умножения 
            string searchMulty = "*";
            // MultiplicationAmountount: Количество всех операторов умножить 
            int i1 = 0, MultiplicationAmountount = 0;
            while ((i1 = txtEnterNumbers.Text.IndexOf(searchMulty, i1 )) != -1) { ++MultiplicationAmountount; i1 += searchMulty.Length; }

            // Поиск деления
            string searchDivide = ":";
            // DivisionAmountount: Количество всех операторов делить
            int i2 = 0, DivisionAmountount = 0;
            while ((i2 = txtEnterNumbers.Text.IndexOf(searchDivide, i2 )) != -1) { ++DivisionAmountount; i2 += searchDivide.Length; }

            // Поиск плюса
            string searchPlus = "+";
            // AdditionAmountount: Количество всех операторов плюс
            int i3  = 0, AdditionAmountount = 0;
            while ((i3 = txtEnterNumbers.Text.IndexOf(searchPlus, i3 )) != -1) { ++AdditionAmountount; i3 += searchPlus.Length; }

            // Поиск минуса
            string searchMinus = "-";
            // SubtractionAmountount: Количество всех операторов минус
            int i4 = 0, SubtractionAmountount = 0;
            while ((i4 = txtEnterNumbers.Text.IndexOf(searchMinus, i4 )) != -1) { ++SubtractionAmountount; i4 += searchMinus.Length; }
            
            // Выполняем все умножения и деления
            int iDivisionAndMultiplication = 0;
            while (DivisionAmountount > 0 | MultiplicationAmountount > 0)
            {
                if (txtEnterNumbers.Text[iDivisionAndMultiplication] == Convert.ToChar(searchMulty))
                {
                    listNumbers[listOperators.IndexOf('*') + 1] = 
                        listNumbers[listOperators.IndexOf('*')] * 
                            listNumbers[listOperators.IndexOf('*') + 1];

                    // Удаляем ненужные значения
                    listNumbers.RemoveAt(listOperators.IndexOf('*'));
                    listOperators.Remove('*');
                    MultiplicationAmountount--;
                }
                if (txtEnterNumbers.Text[iDivisionAndMultiplication] == Convert.ToChar(searchDivide))
                {
                    try
                    {
                        listNumbers[listOperators.IndexOf(':') + 1] =
                            listNumbers[listOperators.IndexOf(':')] /
                                listNumbers[listOperators.IndexOf(':') + 1];
                    }
                    catch(DivideByZeroException)
                    {
                        MessageBox.Show("Division by zero", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    // Удаляем ненужные значения
                    listNumbers.RemoveAt(listOperators.IndexOf(':'));
                    listOperators.Remove(':');
                    DivisionAmountount--;
                }
                iDivisionAndMultiplication++;
            }

            // Выполняем все сложения и вычитания 
            int iPlusAndMinus = 0;
            while (AdditionAmountount > 0 | SubtractionAmountount > 0)
            {
                if (txtEnterNumbers.Text[iPlusAndMinus] == Convert.ToChar(searchPlus))
                {
                    // Вычисление
                    listNumbers[listOperators.IndexOf('+') + 1] =
                        listNumbers[listOperators.IndexOf('+')] +
                            listNumbers[listOperators.IndexOf('+') + 1];

                    // Удаляем ненужные значения
                    listNumbers.RemoveAt(listOperators.IndexOf('+'));
                    listOperators.Remove('+');
                    AdditionAmountount--;
                }
                if (txtEnterNumbers.Text[iPlusAndMinus] == Convert.ToChar(searchMinus))
                {
                    // Вычисление
                    listNumbers[listOperators.IndexOf('-') + 1] =
                        listNumbers[listOperators.IndexOf('-')] -
                            listNumbers[listOperators.IndexOf('-') + 1];

                    // Удаляем ненужные значения
                    listNumbers.RemoveAt(listOperators.IndexOf('-'));
                    listOperators.Remove('-');
                    SubtractionAmountount--;
                }
                iPlusAndMinus++;
            }

            // Записываем последнее число как результат
            Result = Convert.ToDouble(listNumbers.Last());
            // Выводим на экран
            txtAnswer.Text = Convert.ToString(Result);
            // Фокус на ответ 
            txtAnswer.Focus();
        }

        //-----------------------------------------------------------------------------------------------------
        //-------------------------------------------- Ввод текста --------------------------------------------
        //-----------------------------------------------------------------------------------------------------
        private void txtEnterNumbers_TextChanged(object sender, EventArgs e)
        {
            // Заменяем все операторы на "!" для удобства; записываем новое число после последнего "!";
            txtForNumbers = txtEnterNumbers.Text.Replace("*", "!").Replace(":", "!").Replace("+", "!").Replace("-", "!");
            txtForNumbers = txtForNumbers.Substring(txtForNumbers.LastIndexOf("!") + 1);
            try
            {
                listNumbers[listNumbers.Count - 1] = Int32.Parse(txtForNumbers);
            }
            catch(FormatException)
            {
                return;
            }
            
        }


        // ----------------------------- При запуске программы -----------------------------
        private void Main_Load(object sender, EventArgs e)
        {
            listNumbers.Add(new int());
        }
        

        //-----------------------------------------------------------------------------------------------------
        //-------------------------- Кнопки сложения, вычитания, деления, умножения ---------------------------
        //-----------------------------------------------------------------------------------------------------
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            // Пользователь начинает вводить новое число; Создание нового объекта в listNumbers
            listNumbers.Add(new int());
            // Добовляем новый оператор в listNumbers
            listOperators.Add('*');

            // Если пользователь сам не ввел оператор
            if (txtEnterNumbers.Text.Last() != '*')
            {
                //Выводим на экран
                txtEnterNumbers.Text += '*';
            }

        }
        private void btnDevide_Click(object sender, EventArgs e)
        {
            // Пользователь начинает вводить новое число; Создание нового объекта в listNumbers
            listNumbers.Add(new int());
            // Добовляем новый оператор в listNumbers
            listOperators.Add(':');

            // Если пользователь сам не ввел оператор
            if (txtEnterNumbers.Text.Last() != ':')
            {
                //Выводим на экран
                txtEnterNumbers.Text += ':';
            }
        }
        private void btnPlus_Click(object sender, EventArgs e)
        {
            // Пользователь начинает вводить новое число; Создание нового объекта в listNumbers
            listNumbers.Add(new int());
            // Добовляем новый оператор в listNumbers
            listOperators.Add('+');

            // Если пользователь сам не ввел оператор
            if (txtEnterNumbers.Text.Last() != '+')
            {
                //Выводим на экран
                txtEnterNumbers.Text += '+';
            }
        }
        private void btnMinus_Click(object sender, EventArgs e)
        {
            // Пользователь начинает вводить новое число; Создание нового объекта в listNumbers
            listNumbers.Add(new int());
            // Добовляем новый оператор в listNumbers
            listOperators.Add('-');

            // Если пользователь сам не ввел оператор
            if (txtEnterNumbers.Text.Last() != '-')
            {
                //Выводим на экран
                txtEnterNumbers.Text += '-';
            }
        }

        //-----------------------------------------------------------------------------------------------------
        //-------------------------------- Кнопки корня, квадрата, логарифма ----------------------------------
        //-----------------------------------------------------------------------------------------------------
        private void btnRadical_Click(object sender, EventArgs e)
        {
            Result = Convert.ToDouble( Math.Sqrt(listNumbers[listNumbers.Count - 1]));
            txtAnswer.Text = Convert.ToString(Result);
            if (txtAnswer.Text.Length > 6)
            {
                txtAnswer.Text = txtAnswer.Text.Remove(6);
            }
        }
        private void btnDegree_Click(object sender, EventArgs e)
        {
            Result = Convert.ToDouble(Math.Pow(listNumbers[listNumbers.Count - 1], 2));
            txtAnswer.Text = Convert.ToString(Result);
        }


        //--------------------------------------------------------------------------------
        //----------------------------------- Кнопка Reset --------------------------------
        //--------------------------------------------------------------------------------
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Удаляем все эелементы listNumbers
            listNumbers.Clear();
            listOperators.Clear();

            // Удаляем все с экрана
            txtAnswer.Text = "";
            txtEnterNumbers.Text = "";

            // Создаем объект для ввода нового числа
            listNumbers.Add(new int());
        }


        //--------------------------------------------------------------------------------------------
        //----------------------------------- Обработчик событий KeyUp --------------------------------
        //--------------------------------------------------------------------------------------------
        private void eventhandlerKeyUp(object sender, KeyEventArgs e)
        {
            // Если пользователь нажал Enter
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            // Если пользователь нажал умножить
            if (e.KeyCode == Keys.Multiply)
            {
                btnMultiply_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            if(e.KeyCode == Keys.D8 && (e.Shift))
            {
                btnMultiply_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            
            // Если пользователь нажал делить
            if (e.KeyCode == Keys.Divide)
            {
                btnDevide_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            if (e.KeyCode == Keys.OemSemicolon && (e.Shift))
            {
                btnDevide_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            
            // Если пользователь нажал сложение
            if (e.KeyCode == Keys.Add)
            {
                btnPlus_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            if (e.KeyCode == Keys.Oemplus)
            {
                btnPlus_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }    

            // Если пользователь нажал минус
            if (e.KeyCode == Keys.Subtract)
            {
                btnMinus_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
            if (e.KeyCode == Keys.OemMinus)
            {
                btnMinus_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }

            // Если пользователь нажал Backspace
            if (e.KeyCode == Keys.Back)
            {
                btnReset_Click(null, null);
                // Прекращаем выполнение eventhandlerKeyUp
                return;
            }
        }
    }
}
