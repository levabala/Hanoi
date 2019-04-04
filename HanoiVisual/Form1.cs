using HanoiModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HanoiVisual
{
    public partial class Form1 : Form
    {
        const int count = 8;
        List<int> steps = Hanoi.doHanoi(count).ToList();
        int currentStep;
        List<int> columnFirst;
        List<int> columnSecond;
        List<int> columnThird;
        List<List<int>> columns;
        Brush[] palette;



        public Form1()
        {
            InitializeComponent();            

            reset();
        }

        private void reset()
        {
            currentStep = 0;
            columnFirst = new int[count].Select((val, i) => count - (i)).ToList();
            columnSecond = new List<int>();
            columnThird = new List<int>();

            columns = new List<List<int>>()
            {
                columnFirst,
                columnSecond,
                columnThird,
            };

            Random rnd = new Random();
            palette = new Brush[count + 1].Select(v =>
                (Brush)typeof(Brushes).GetProperties().ElementAt(
                    rnd.Next(typeof(Brushes).GetProperties().Count() - 1)
                ).GetValue(null, null)
            ).ToArray();

            panelDrawing.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            panelDrawing.Paint += PanelDrawing_Paint;
            panelDrawing.Resize += PanelDrawing_Resize;
            KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    nextStep();
                    panelDrawing.Invalidate();
                    break;
                case Keys.R:
                    reset();
                    break;
            }
        }

        private void PanelDrawing_Resize(object sender, EventArgs e)
        {
            panelDrawing.Invalidate();
        }

        private void PanelDrawing_Paint(object sender, PaintEventArgs e)
        {
            visualizeState(
                columnFirst,
                columnSecond,
                columnThird,
                panelDrawing.Width,
                panelDrawing.Height, 
                e.Graphics
            );
        }

        private void nextStep()
        {
            if (currentStep >= steps.Count)
                return;

            int stepValue = steps[currentStep];
            int from = stepValue / 10;
            int to = stepValue - from * 10;

            int movingNum = columns[from].Last();

            columns[from].RemoveAt(columns[from].Count - 1);
            columns[to].Add(movingNum);

            currentStep++;
        }        

        private void visualizeState(
            List<int> column1, List<int> column2, List<int> column3,
            float drawWidth, float drawHeight, Graphics g
        )
        {            
            int maxNum = column1.Concat(column2).Concat(column3).Max();

            float gapX = 100;
            float gapY = 100;

            float actualWidth = drawWidth - gapX * 2;
            float actualHeight = drawHeight - gapY * 2;

            float columnWidth = actualWidth / 3;
            float columnHeight = actualHeight;

            float minBobWidth = 10;
            float maxBobWidth = columnWidth - minBobWidth;

            float bobHeight = columnHeight / maxNum;

            void drawColumn(
                float x, float y, float width, float height,
                List<int> column
            )
            {
                // vertical line
                g.DrawLine(
                    new Pen(Color.Black, 2),
                    x + width / 2,
                    y,
                    x + width / 2,
                    y + height
                );

                // bobs
                for (int i = 0; i < column.Count; i++)
                {
                    int num = column[i];
                    float bobWidth = maxBobWidth * (num / (float)(maxNum));

                    float bx = x + width / 2 - bobWidth / 2;
                    float by = y + height - bobHeight * (i + 1);

                    g.FillRectangle(palette[num], bx, by, bobWidth, bobHeight);
                    g.DrawRectangle(Pens.Black, bx, by, bobWidth, bobHeight);
                }                
            }

            // draw first one
            float x1 = gapX;
            float y1 = gapY;

            drawColumn(x1, y1, columnWidth, columnHeight, column1);

            // draw second one
            float x2 = gapX + actualWidth / 3;
            float y2 = gapY;

            drawColumn(x2, y2, columnWidth, columnHeight, column2);

            // draw third one
            float x3 = gapX + actualWidth / 3 * 2;
            float y3 = gapY;

            drawColumn(x3, y3, columnWidth, columnHeight, column3);
        }
    }
}
