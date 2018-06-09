using lab_13.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_13
{
    public partial class Form1 : Form
    {
        CBallon _Bal;   //ссылка на обьект класса CBallon  (шарик)
        CSplat _Splat;  //ссылка на обьект класса CSplat  (взрыв)
        CAim _Aim;    //ссылка на обьект класса CAim   (цель)
         
        Random rnd = new Random(); // обьект класса Random (генератор случайных чисел)
        System.Media.SoundPlayer gun_ = new System.Media.SoundPlayer(Resources.gun); // создание плеера для воспроизведения звука выстрела
        
        public Form1()  // конструктор для Form1
        {
            InitializeComponent();
            _Bal = new CBallon();  // cоздание обьектов классов (шарик, взрыв, прицел)
            _Splat = new CSplat();  
            _Aim = new CAim(); 
        }
        bool splat = false; // для события попадания в шарик
        int _splatTime = 0; // интервал видимости взрыва 
        int _gameFrame = 0; // интервал обновления положения шарика 

        int _totShots = 0; // кол-во выстрелов
        int _totHits = 0; // кол-во попаданий
        int _totMis = 0; // кол-во промахов
        double _Averege = 0; // процент попаданий
                
        /* Вместо обработки события Paint переопределяем метод OnPaint () базового класса,
        который используется для запуска события Paint.  */
        protected override void OnPaint(PaintEventArgs e)
        {
            //Рисуем  текст в пределах указанных границ прямоугольника, с помощью указанного контекста устройства, шрифта, цвета и инструкции форматирования.
            TextFormatFlags flags = TextFormatFlags.Left; // Центрирует текст по горизонтали в ограничивающем прямоугольнике.
            Font _font = new System.Drawing.Font("Tahoma", 10, FontStyle.Bold);
            
            // Рисуем результаты стрельбы
            TextRenderer.DrawText(e.Graphics, "Выстрелов: " + _totShots.ToString(), _font, 
                new Rectangle(10, 10, 150, 50), SystemColors.ControlText, flags);

            TextRenderer.DrawText(e.Graphics, "Попаданий: " + _totHits.ToString(), _font, 
                new Rectangle(10, 30, 150, 50), SystemColors.ControlText, flags);

            TextRenderer.DrawText(e.Graphics, "Процент попаданий: " + _Averege.ToString("F1") + "%", _font, //"F1" - одна цифра после запятой 
                new Rectangle(10, 50, 210, 50), SystemColors.ControlText, flags);

            if (splat == true) { _Splat.DrawImage(e.Graphics); } // отрисовка взрыва шарика (если событие попадания в шарик true)

            else { _Bal.DrawImage(e.Graphics); } // если взрыва нет, то отриcовка шарика
            
            _Aim.DrawImage(e.Graphics);  // отрисовка прицела всегда
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            _Aim.Update_Aim(e.X - Resources.aim.Width / 2, e.Y - Resources.aim.Height / 2); // обновление положения прицела  по текущим координатам курсора мыши
                                               // сдвиг, для центрирования курсора по центру прицела (Bitmap.Width - ширина объекта картники Bitmap)
            this.Refresh(); // перерисовка формы
        }

        private void timerGame_Tick(object sender, EventArgs e)  // перерисовка по таймеру каждыую 1 мс
        {
            if (_gameFrame >= 50)  // обновление положения шарика каждые 50 мс
            {
                UpdateBallon();
                _gameFrame = 0;
            }
            _gameFrame++; // счетчик отображения шарика

            if (splat)   // если есть событие попадания по шарику то задаем время отрисовки взрыва шарика
            {
                if (_splatTime >= 20)
                {
                    splat = false;  
                    _splatTime = 0;  // обнуление таймера для отрисовки взрыва шарика
                }
                _splatTime++; // счетчик мс отображения взрыва
            }
            this.Refresh(); //перерисовка изображений на форме
        }



        private void UpdateBallon()  // функция обновления положения шарика по случайным координатам внутри формы
        {
            _Bal.Update_Ballon(rnd.Next(Resources.ballon.Width, this.Width - Resources.ballon.Width * 2), // Random.Next (Int32, Int32) возвращает случайное целое 
            rnd.Next(Resources.ballon.Height, this.Height - Resources.ballon.Height * 2));                                       //число в указанном диапазоне.
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_Bal.Hit(e.X, e.Y))  // если область прицела содержится  в шарике
            {
                splat = true;
                _Splat.Left = _Bal.Left; //задание координат для отрисовки взрыва, относительно текущих координат шарика
                _Splat.Top = _Bal.Top;

                _totHits++; // счетчик попаданий
            }
            else _totMis++; // счетчик промахов

            _totShots = _totHits + _totMis;
            _Averege = (double)_totHits / (double)_totShots * 100.0; // подсчет ср. кол-ва попаданий
            gun_.Play(); // звук выстрела
        }
    }
}
