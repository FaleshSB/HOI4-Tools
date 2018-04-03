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
            ParadoxDataGatherer units = new ParadoxDataGatherer();

            Pages.pages[PageName.DivisionDesigner] = this;

            InitializeComponent();

            contentWrapPanel = new AlignableWrapPanel();
            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;
        }

        protected override void DisplayContent()
        {
            System.Drawing.Size divisionDesignerBackgroundSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(1047)), (int)Math.Round(Opt.ApResMod(276)));
            System.Drawing.Size addUnitSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(72)), (int)Math.Round(Opt.ApResMod(38)));

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

            Canvas devisionDesignerCanvas = new Canvas();
            devisionDesignerCanvas.Height = Opt.ApResMod(276);
            devisionDesignerCanvas.Width = Opt.ApResMod(1047);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + "division_designer_background.png"), divisionDesignerBackgroundSize); ;
            devisionDesignerCanvas.Background = ib;
            divisionWrapPanel.Children.Add(devisionDesignerCanvas);

            StackPanel fullDesignerStackPanel = new StackPanel();
            fullDesignerStackPanel.Orientation = Orientation.Horizontal;
            devisionDesignerCanvas.Children.Add(fullDesignerStackPanel);

            StackPanel supportColumn = new StackPanel();
            supportColumn.Orientation = Orientation.Vertical;
            supportColumn.VerticalAlignment = VerticalAlignment.Top;
            supportColumn.Margin = ScaledThicknessFactory.GetThickness(11, 15, 0, 0);
            fullDesignerStackPanel.Children.Add(supportColumn);
            Image addSupport;
            for (int i = 0; i < 5; i++)
            {
                addSupport = new Image();
                addSupport.Width = Opt.ApResMod(72);
                addSupport.Height = Opt.ApResMod(38);
                addSupport.Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + "add_unit.png"), addUnitSize);
                addSupport.Margin = ScaledThicknessFactory.GetThickness(0, 0, 10, 14);
                supportColumn.Children.Add(addSupport);
            }

            for (int y = 0; y < 5; y++)
            {
                StackPanel unitColumn = new StackPanel();
                unitColumn.Orientation = Orientation.Vertical;
                unitColumn.VerticalAlignment = VerticalAlignment.Top;
                unitColumn.Margin = ScaledThicknessFactory.GetThickness(11, 15, 0, 0);
                fullDesignerStackPanel.Children.Add(unitColumn);
                Image addUnit;
                for (int i = 0; i < 5; i++)
                {
                    addUnit = new Image();
                    addUnit.Width = Opt.ApResMod(72);
                    addUnit.Height = Opt.ApResMod(38);
                    addUnit.Source = ImageResizer.ResizeImage(System.Drawing.Image.FromFile(filteredLocation + "add_unit.png"), addUnitSize);
                    addUnit.Margin = ScaledThicknessFactory.GetThickness(0, 0, 0, 14);
                    unitColumn.Children.Add(addUnit);
                }
            }







            contentWrapPanel.Children.Add(divisionWrapPanel);
            //}
        }
    }
}
