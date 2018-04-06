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
            Pages.pages[PageName.DivisionDesigner] = this;

            InitializeComponent();

            contentWrapPanel = new AlignableWrapPanel();
            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;

            Division testDivision = new Division();
            Divisions.divisions[0] = testDivision;
        }

        protected override void DisplayContent()
        {
            System.Drawing.Size divisionDesignerBackgroundSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(1047)), (int)Math.Round(Opt.ApResMod(276)));
            System.Drawing.Size addUnitSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(72)), (int)Math.Round(Opt.ApResMod(38)));

            string filteredLocation;
            string baseLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            filteredLocation = System.IO.Path.GetDirectoryName(baseLocation).Replace("file:\\", "") + "\\Misc\\";

            contentWrapPanel.Children.Clear();

            foreach (KeyValuePair<int, Division> idAndDivision in Divisions.divisions)
            {
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

                for (int column = 0; column < 5; column++)
                {
                    StackPanel unitColumn = new StackPanel();
                    unitColumn.Orientation = Orientation.Vertical;
                    unitColumn.VerticalAlignment = VerticalAlignment.Top;
                    unitColumn.Margin = ScaledThicknessFactory.GetThickness(11, 15, 0, 0);
                    fullDesignerStackPanel.Children.Add(unitColumn);
                    int count = 0;
                    bool shouldDisplayAddUnit = false;
                    ImageButton addUnit;
                    if(idAndDivision.Value.unitsInDivision.ContainsKey(column) == false)
                    {
                        shouldDisplayAddUnit = true;
                    }
                    else
                    {
                        foreach(KeyValuePair<UnitName, int> unitAndQuantity in idAndDivision.Value.unitsInDivision[column])
                        {
                            for (int i = 0; i < unitAndQuantity.Value; i++)
                            {
                                addUnit = new ImageButton((ButtonName)Enum.Parse(typeof(ButtonName), unitAndQuantity.Key.ToString()));
                                addUnit.uniqueDivisionId = idAndDivision.Key;
                                addUnit.column = column;
                                addUnit.Margin = ScaledThicknessFactory.GetThickness(0, 0, 0, 14);
                                unitColumn.Children.Add(addUnit);
                                count++;
                            }
                        }
                    }
                    if (shouldDisplayAddUnit || count < 5)
                    {
                        addUnit = new ImageButton(ButtonName.AddUnit);
                        addUnit.MouseDown += new MouseButtonEventHandler(ButtonClicked);
                        addUnit.uniqueDivisionId = idAndDivision.Key;
                        addUnit.column = column;
                        addUnit.Margin = ScaledThicknessFactory.GetThickness(0, 0, 0, 14);
                        unitColumn.Children.Add(addUnit);
                    }
                }


                StackPanel firstStatsColumn = new StackPanel();
                firstStatsColumn.Orientation = Orientation.Vertical;
                firstStatsColumn.VerticalAlignment = VerticalAlignment.Top;
                firstStatsColumn.Margin = ScaledThicknessFactory.GetThickness(11, 15, 0, 0);
                fullDesignerStackPanel.Children.Add(firstStatsColumn);

                AddStat(firstStatsColumn, "Speed", idAndDivision.Value.maxSpeed.ToString());
                AddStat(firstStatsColumn, "HP", idAndDivision.Value.maxStrength.ToString());
                AddStat(firstStatsColumn, "Organization", idAndDivision.Value.maxOrganisation.ToString());
                AddStat(firstStatsColumn, "Suppression", idAndDivision.Value.suppression.ToString());
                AddStat(firstStatsColumn, "Weight", idAndDivision.Value.weight.ToString());
                AddStat(firstStatsColumn, "Supply Consumtion", idAndDivision.Value.supplyConsumption.ToString());
                AddStat(firstStatsColumn, "Reliability", idAndDivision.Value.reliability.ToString());

                contentWrapPanel.Children.Add(divisionWrapPanel);
            }
        }

        private void AddStat(StackPanel column, string statDescription, string stat)
        {
            Label statLable;
            StackPanel descriptionAndStat = new StackPanel();
            descriptionAndStat.Orientation = Orientation.Horizontal;
            descriptionAndStat.VerticalAlignment = VerticalAlignment.Top;
            column.Children.Add(descriptionAndStat);
            descriptionAndStat.Width = Opt.ApResMod(180);

            
            statLable = new Label();
            statLable.Width = Opt.ApResMod(140);
            statLable.FontSize = Opt.ApResMod(16);
            statLable.Content = statDescription + ": ";
            statLable.HorizontalContentAlignment = HorizontalAlignment.Left;
            descriptionAndStat.Children.Add(statLable);
            statLable = new Label();
            statLable.Width = Opt.ApResMod(40);
            statLable.FontSize = Opt.ApResMod(16);
            statLable.Content = stat;
            statLable.HorizontalContentAlignment = HorizontalAlignment.Right;
            statLable.HorizontalAlignment = HorizontalAlignment.Right;
            descriptionAndStat.Children.Add(statLable);
        }

        private void ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            Divisions.divisions[imageButton.uniqueDivisionId].AddUnit(UnitName.Infantry, imageButton.column);
            DisplayContent();
        }
    }
}
