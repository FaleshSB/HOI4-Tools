using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using HOI4_Tools.Model;

namespace HOI4_Tools.View
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : DefaultPage
    {
        private System.Drawing.Size buttonSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(379)), (int)Math.Round(Opt.ApResMod(55)));

        private Dictionary<string, Image> buttons = new Dictionary<string, Image>();
        private string devisionDesignerButtonName = "division_designer_start";
        private Canvas contentCanvas = new Canvas();
        private string filteredLocation;
        private bool isButtonBeingPressed = false;

        private AlignableWrapPanel contentWrapPanel = new AlignableWrapPanel();

        public StartPage()
        {
            string baseLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filteredLocation = System.IO.Path.GetDirectoryName(baseLocation).Replace("file:\\", "") + "\\Misc\\";

            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;

            contentCanvas.Name = "contentCanvas";
            contentCanvas.Height = 900;
            //contentCanvas.Background = new SolidColorBrush(Colors.Black);
            contentWrapPanel.Children.Add(contentCanvas);
            InitializeComponent();
            buttons.Add(devisionDesignerButtonName, CreateButton(devisionDesignerButtonName));
            contentCanvas.Width = buttons[devisionDesignerButtonName].Width;

            NavigationService.Navigate((DivisionDesignerPage)Pages.pages[PageName.DivisionDesigner]);
        }
        private Image CreateButton(string buttonName)
        {
            Image button = new Image();
            button.Name = buttonName;
            button.Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + buttonName + ".png"), buttonSize);
            button.Width = buttonSize.Width;
            button.Height = buttonSize.Height;
            button.UseLayoutRounding = true;
            button.MouseEnter += new MouseEventHandler(ButtonHover);
            button.MouseLeave += new MouseEventHandler(ButtonStopHover);
            button.MouseDown += new MouseButtonEventHandler(ButtonClicked);
            RenderOptions.SetBitmapScalingMode(button, BitmapScalingMode.HighQuality);

            return button;
        }

        private void ButtonHover(object sender, MouseEventArgs e)
        {
            Image button = (Image)sender;
            buttons[button.Name].Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + button.Name + "_hover.png"), buttonSize);
        }
        private void ButtonStopHover(object sender, MouseEventArgs e)
        {
            Image button = (Image)sender;
            buttons[button.Name].Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + button.Name + ".png"), buttonSize);
        }
        private async void ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            if (isButtonBeingPressed) return;
            isButtonBeingPressed = true;
            Image button = (Image)sender;
            buttons[button.Name].Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + button.Name + "_pressed.png"), buttonSize);
            await Task.Delay(Opt.buttonDelay);
            isButtonBeingPressed = false;

            if (button.Name == devisionDesignerButtonName)
            {
                NavigationService.Navigate((DivisionDesignerPage)Pages.pages[PageName.DivisionDesigner]);
            }
        }

        protected override void DisplayContent()
        {
            contentCanvas.Children.Clear();

            Canvas.SetLeft(buttons[devisionDesignerButtonName], 0);
            Canvas.SetTop(buttons[devisionDesignerButtonName], Opt.ApResMod(400));
            contentCanvas.Children.Add(buttons[devisionDesignerButtonName]);
        }
    }
}