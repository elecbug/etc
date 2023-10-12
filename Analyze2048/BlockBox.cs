using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyze2048
{
    public class BlockBox : Label
    {
        private int level;
        public int Level
        {
            get { return this.level; }
            set 
            {
                if (value > 0) this.level = value;
                else throw new ArgumentOutOfRangeException();

                TextSetter();
            }
        }

        private void TextSetter()
        {
            base.Text = $"{(int)Math.Pow(2, this.level)}";
        }

        public BlockBox() 
        {
            this.Level = 1;

            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.ForeColor = Color.White;
            base.BackColor = Color.Black;
            base.BorderStyle = BorderStyle.Fixed3D;
        }
    }
}
