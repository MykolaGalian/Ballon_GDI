using lab_13.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_13
{
    class CBallon  : CImageBase  // класс наследник CImageBase, описывает "шарик"
    {
        private Rectangle _BallonShotArea = new Rectangle();// создание прямоугольника для срабатывания попадания в шарик

        public CBallon() : base(_recouce: Resources.ballon) // конструктор, задает область (Rectangle) для срабатывания попадания в шарик, использование картинки шарика из файла ресурсов
        {
            _BallonShotArea.X = Left; // координаты области (.X - левый верхний угол прямоугольника), при создании Left=0 (левый вехний угол формы)
            _BallonShotArea.Y = Top;  // координаты области (.Y - левый верхний угол прямоугольника), при создании Top=0
            _BallonShotArea.Width = 70; // ширина прямоугольной области для попадания в шарик
            _BallonShotArea.Height = 80; // высота прямоугольной области для попадания
        }
        public void Update_Ballon(int X, int Y)  // обновление положения шарика (по передаваемым случайным координатам)
        {
            Left = X;
            Top = Y;
            _BallonShotArea.X = Left;
            _BallonShotArea.Y = Top;
        }

        public bool Hit(int X, int Y)  // возвращает true если область прицела содержится  в шарике
        {
            Rectangle aim = new Rectangle(X, Y, 3, 3); // создание области (прямоугольника) для курсора (прицел) с координатами текущей позиции курсора мыши
            if (_BallonShotArea.Contains(aim)) return true;  // проверка содержится ли области прицела в шарике

            return false;
         }
    }
}
