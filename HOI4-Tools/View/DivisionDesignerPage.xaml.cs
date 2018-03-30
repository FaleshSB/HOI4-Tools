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
using HOI4_Tools.Model;

namespace HOI4_Tools.View
{
    /// <summary>
    /// Interaction logic for DivisionDesignerPage.xaml
    /// </summary>
    public partial class DivisionDesignerPage : DefaultPage
    {
        private AlignableWrapPanel contentWrapPanel;

        public DivisionDesignerPage()
        {

            Units units = new Units();





            Pages.pages[PageName.DivisionDesigner] = this;

            InitializeComponent();

            contentWrapPanel = new AlignableWrapPanel();
            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;
        }

        protected override void DisplayContent()
        {
            System.Drawing.Size buttonSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(1035)), (int)Math.Round(Opt.ApResMod(342)));

            string filteredLocation;
            string baseLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filteredLocation = System.IO.Path.GetDirectoryName(baseLocation).Replace("file:\\", "") + "\\Misc\\";

            contentWrapPanel.Children.Clear();

            //foreach (Division division in Division.divisions.OrderByDescending(build => build.displayOrder).ToList())
            //{
                AlignableWrapPanel divisionWrapPanel = new AlignableWrapPanel();
                divisionWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;

                AlignableWrapPanel spacerWrapPanel = new AlignableWrapPanel();
                spacerWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;

                Canvas topSpacer = new Canvas();
                topSpacer.Width = 99999;
                topSpacer.Height = Opt.ApResMod(50);
                spacerWrapPanel.Children.Add(topSpacer);

                divisionWrapPanel.Children.Add(spacerWrapPanel);

                Image divisionBackground = new Image();
                divisionBackground.Width = Opt.ApResMod(1035);
                divisionBackground.Height = Opt.ApResMod(342);
                divisionBackground.Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + "division_background.png"), buttonSize);

                divisionWrapPanel.Children.Add(divisionBackground);

                contentWrapPanel.Children.Add(divisionWrapPanel);
            //}
        }
    }
}
