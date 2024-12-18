using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Cette classe a été créé via inteligence artificielle (chat GPT)
namespace Projet_boogle
{
    public class WordCloudDisplay : Form
    {
        private Bitmap bitmap;

        public WordCloudDisplay(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.ClientSize = new Size(bitmap.Width, bitmap.Height);
            this.Paint += new PaintEventHandler(this.OnPaint);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.bitmap, 0, 0);
        }

        public static void Show(Bitmap bitmap)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(new WordCloudDisplay(bitmap));
        }
    }
}
