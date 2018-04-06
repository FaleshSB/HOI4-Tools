using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HOI4_Tools.Model
{
    public class ImageButton : System.Windows.Controls.Image
    {
        public int uniqueDivisionId;
        public int column;
        public ButtonName ButtonName;
        private Size buttonSize;
        private string filteredLocation;

        public ImageButton(ButtonName buttonName)
        {
            buttonSize = new System.Drawing.Size((int)Opt.ApResMod(72), (int)Opt.ApResMod(38));
            
            this.ButtonName = buttonName;
            this.filteredLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Misc\\";

            Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + buttonName + ".png"), buttonSize);
            Width = buttonSize.Width;
            Height = buttonSize.Height;
            UseLayoutRounding = true;
            MouseEnter += new MouseEventHandler(ButtonHover);
            MouseLeave += new MouseEventHandler(ButtonStopHover);
            MouseDown += new MouseButtonEventHandler(ButtonClicked);
            Cursor = Cursors.Hand;
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.HighQuality);
        }

        private async void ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + ButtonName.ToString() + "_pressed.png"), buttonSize);
            await Task.Delay(Opt.buttonDelay);
            Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + ButtonName.ToString() + "_hover.png"), buttonSize);
        }

        private void ButtonStopHover(object sender, MouseEventArgs e)
        {
            Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + ButtonName.ToString() + ".png"), buttonSize);
        }

        private void ButtonHover(object sender, MouseEventArgs e)
        {
            Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + ButtonName.ToString() + "_hover.png"), buttonSize);
        }
    }
}
