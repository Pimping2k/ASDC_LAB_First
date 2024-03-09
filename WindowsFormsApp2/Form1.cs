using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Random rand = new Random();
        public int ArraySize = 500;
        private int[] array;
        private PictureBox[] arrayBars;
        private Timer timer;
        private int currentIndex;
        private Label[] arrayLabels;
        private Action sortingAlgorithm;
        private Stopwatch stopwatch;
        public Form1()
        {
            InitializeComponent();
            InitializeArray();
            InitializeArrayBars();
            InitializeTimer();
        }

        private int GetArrayRandomSize()
        {
            return rand.Next(1, 100);
        }

        //CREATE ARRAY ↓↓↓↓
        private void InitializeArray()
        {
            array = new int[ArraySize];
            Random random = new Random();

            for (int i = 0; i < ArraySize; i++)
            {
                array[i] = random.Next(1, 150);
            }
        }
        //CREATE BARS OF ARRAY ↓↓↓
        private void InitializeArrayBars()
        {
            arrayBars = new PictureBox[ArraySize];
            arrayLabels = new Label[ArraySize];

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Location = new Point(10, 150),
            };

            for (int i = 0; i < ArraySize; i++)
            {
                arrayBars[i] = new PictureBox
                {
                    Height = array[i],
                    Width = 30,
                    BackColor = Color.Black,
                };

                arrayLabels[i] = new Label
                {
                    Text = array[i].ToString(),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                };

                flowLayoutPanel.Controls.Add(arrayBars[i]);
                flowLayoutPanel.Controls.Add(arrayLabels[i]);
            }

            Controls.Add(flowLayoutPanel);
        }

        private void BubbleSort()
        {
            for (int j = 0; j < ArraySize - currentIndex - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(j, j + 1);
                }
            }
        }
        //CREATE TIMER ↓↓↓
        private void InitializeTimer()
        {
            timer = new Timer();
            //timer.Interval = 200;
            timer.Tick += Timer_Tick;
            stopwatch = new Stopwatch();
        }
        //SWAP METHOD
        private void Swap(int index1, int index2)
        {
            int temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < ArraySize)
            {
                PerformSortingStep();
                UpdateArrayBars();
            }
            else
            {
                timer.Stop();
                stopwatch.Stop();
                currentIndex = 0;
                lblTimer.Text = $"Время: {stopwatch.Elapsed.TotalSeconds:F5} сек";
            }
        }
        //FOR SWAPING STEP FOR ALGORITHMS↓↓↓
        private void PerformSortingStep()
        {
            sortingAlgorithm.Invoke();
            currentIndex++;
        }
        //FOR ARRAYS MEASURE ↓↓↓
        private void UpdateArrayBars()
        {
            for (int i = 0; i < ArraySize; i++)
            {
                arrayBars[i].Height = array[i];
                arrayLabels[i].Text = array[i].ToString();
            }
        }

        private void SelectionSort()
        {
            int minIndex = currentIndex;
            for (int i = currentIndex + 1; i < ArraySize; i++)
            {
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }
            }

            Swap(currentIndex, minIndex);
        }

        //SELECTION SORT BUTTON ↓↓↓
        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                sortingAlgorithm = SelectionSort;
                StartSorting();
            }
        }

        private void InsertionSort()
        {
            int key = array[currentIndex];
            int j = currentIndex - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = key;
        }
        //DELEGATE FOR SWAPING ALGRORITHMS
        private void StartSorting()
        {
            InitializeArray();
            currentIndex = 0;
            timer.Start();
            stopwatch.Restart();
        }
        //INSERTION SORT BUTTON ALGORITHM↓↓↓
        private void button3_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                sortingAlgorithm = InsertionSort;
                StartSorting();
            }
        }
        //BUBBLE SORT BUTTON ALGORITHM ↓↓↓
        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                sortingAlgorithm = BubbleSort;
                StartSorting();
            }
        }
    }
}
