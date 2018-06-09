using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_13
{
    class CImageBase // базовый класс, определяет параметры изображения
    {
        Bitmap _bitmap; //ссылка на обьект класса Bitmap (точечный рисунок)
        private int X; // координаты (верх.(top) и левый угол (left)) обьекта Bitmap
        private int Y;
        public int Left { get { return X; } set { X = value; } } // свойства для доступа к координатам обьекта Bitmap
        public int Top { get { return Y; } set { Y = value; } }

        public CImageBase(Bitmap _recouce)  // конструктор, создаёт обьекта класса Bitmap из указанного изображения _recouce
        {
            _bitmap = new Bitmap(_recouce);
            
        }

        public void DrawImage(Graphics k)
        {
            k.DrawImage(_bitmap, X, Y);  // метод рисует указаное изображение Bitmap, используя его размер, в месте заданом координатами x и y
        }
    }
}

