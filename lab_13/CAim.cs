using lab_13.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_13
{
    class CAim : CImageBase   // класс наследник CImageBase, описывает прицел
    {
        private Rectangle _AimArea = new Rectangle();// создание прямоугольника для срабатывания попадания в шарик

        public CAim() : base(_recouce: Resources.aim)   // конструктор  обекта для описаня/управления прицелом 
        
        {
            _AimArea.X = Left; // координаты области (левый верхний угол прямоугольника)
            _AimArea.Y = Top;
            _AimArea.Width = 60; // размеры прямоугольной области прицела (для позиционирования курсора мыши в центре)
            _AimArea.Height = 60;
        }
    public void Update_Aim(int X, int Y)  // обновление положения прицела (по передаваемым по координатам указателя мыши)
        {
        Left = X;
        Top = Y;
            _AimArea.X = Left;
            _AimArea.Y = Top;
    }
  }
}
