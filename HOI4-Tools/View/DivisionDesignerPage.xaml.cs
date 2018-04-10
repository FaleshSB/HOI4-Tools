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
        private bool isAddingUnit = false;
        private int divisionIsAddingUnitTo;
        private int columnIsAddingUnitTo;
        private ButtonName buttonNamePressedToAddUnit;

        public DivisionDesignerPage()
        {
            Pages.pages[PageName.DivisionDesigner] = this;

            InitializeComponent();

            contentWrapPanel = new AlignableWrapPanel();
            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;

            Division testDivision = new Division();
            testDivision.AddUnit(UnitName.Cavalry, 0);
            testDivision.AddUnit(UnitName.Cavalry, 0);
            testDivision.AddUnit(UnitName.Cavalry, 1);
            testDivision.AddUnit(UnitName.Cavalry, 1);
            testDivision.AddUnit(UnitName.Motorized, 2);
            testDivision.AddUnit(UnitName.Motorized, 2);
            testDivision.AddUnit(UnitName.LightTank, 3);
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



                if (isAddingUnit && divisionIsAddingUnitTo == idAndDivision.Key)
                {
                    StackPanel column;
                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.Cavalry, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.Motorized, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.Mechanized, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.MotorizedRocketArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.Infantry, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.Paratroopers, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.Marines, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.Mountaineers, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.BicycleBattalion, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.Artillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.AntiTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.AntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.RocketArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.LightTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.MediumTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.HeavyTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.SuperHeavyTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.ModernTank, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.LightSPArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.MediumSPArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.HeavySPArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.SuperHeavySPArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.ModernSPArtillery, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.LightTankDestroyer, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.MediumTankDestroyer, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.HeavyTankDestroyer, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.SuperHeavyTankDestroyer, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.ModernTankDestroyer, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                    column = CreateUnitColumn(fullDesignerStackPanel);
                    column.Children.Add(CreateUnitButton(ButtonName.LightSPAntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.MediumSPAntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.HeavySPAntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.SuperHeavySPAntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));
                    column.Children.Add(CreateUnitButton(ButtonName.ModernSPAntiAir, columnIsAddingUnitTo, divisionIsAddingUnitTo));

                }



                else
                {
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
                        if (idAndDivision.Value.unitsInDivision.ContainsKey(column) == false)
                        {
                            shouldDisplayAddUnit = true;
                        }
                        else
                        {
                            foreach (KeyValuePair<UnitName, int> unitAndQuantity in idAndDivision.Value.unitsInDivision[column])
                            {
                                for (int i = 0; i < unitAndQuantity.Value; i++)
                                {
                                    addUnit = new ImageButton((ButtonName)Enum.Parse(typeof(ButtonName), unitAndQuantity.Key.ToString()));
                                    addUnit.MouseDown += new MouseButtonEventHandler(ButtonClicked);
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
                    firstStatsColumn.Margin = ScaledThicknessFactory.GetThickness(11, 0, 0, 0);
                    fullDesignerStackPanel.Children.Add(firstStatsColumn);

                    AddStat(firstStatsColumn, "Max Speed", idAndDivision.Value.maxSpeed.ToString(), idAndDivision.Value.maxSpeedDescription);
                    AddStat(firstStatsColumn, "HP", idAndDivision.Value.maxStrength.ToString(), idAndDivision.Value.maxStrengthDescription);
                    AddStat(firstStatsColumn, "Organization", idAndDivision.Value.maxOrganisation.ToString(), idAndDivision.Value.maxOrganisationDescription);
                    AddStat(firstStatsColumn, "Recovery Rate", "0", "");
                    AddStat(firstStatsColumn, "Recon", "0", "");
                    AddStat(firstStatsColumn, "Suppression", idAndDivision.Value.suppression.ToString(), idAndDivision.Value.suppressionDescription);
                    AddStat(firstStatsColumn, "Weight", idAndDivision.Value.weight.ToString(), idAndDivision.Value.weightDescription);
                    AddStat(firstStatsColumn, "Supply Use", idAndDivision.Value.supplyConsumption.ToString(), idAndDivision.Value.supplyConsumptionDescription);
                    AddStat(firstStatsColumn, "Reliability", idAndDivision.Value.reliability.ToString(), idAndDivision.Value.reliabilityDescription);
                    AddStat(firstStatsColumn, "Trickleback", "0", "");
                    AddStat(firstStatsColumn, "Exp. Loss", "0", "");

                    StackPanel secondStatsColumn = new StackPanel();
                    secondStatsColumn.Orientation = Orientation.Vertical;
                    secondStatsColumn.VerticalAlignment = VerticalAlignment.Top;
                    secondStatsColumn.Margin = ScaledThicknessFactory.GetThickness(2, 0, 0, 0);
                    fullDesignerStackPanel.Children.Add(secondStatsColumn);

                    AddStat(secondStatsColumn, "Soft Attack", idAndDivision.Value.softAttack.ToString(), idAndDivision.Value.softAttackDescription);
                    AddStat(secondStatsColumn, "Hard Attack", idAndDivision.Value.hardAttack.ToString(), idAndDivision.Value.hardAttackDescription);
                    AddStat(secondStatsColumn, "Air Attack", idAndDivision.Value.airAttack.ToString(), idAndDivision.Value.airAttackDescription);
                    AddStat(secondStatsColumn, "Defence", idAndDivision.Value.defense.ToString(), idAndDivision.Value.defenseDescription);
                    AddStat(secondStatsColumn, "Breakthrough", idAndDivision.Value.breakthrough.ToString(), idAndDivision.Value.breakthroughDescription);
                    AddStat(secondStatsColumn, "Armor", idAndDivision.Value.armorValue.ToString(), idAndDivision.Value.armorValueDescription);
                    AddStat(secondStatsColumn, "Piercing", "0", "");
                    AddStat(secondStatsColumn, "Initiative", "0", "");
                    AddStat(secondStatsColumn, "Entrenchment", "0", "");
                    AddStat(secondStatsColumn, "Eq. Cap. Ratio", "0", "");
                    AddStat(secondStatsColumn, "Combat Width", idAndDivision.Value.combatWidth.ToString(), idAndDivision.Value.combatWidthDescription);

                    StackPanel thirdStatsColumn = new StackPanel();
                    thirdStatsColumn.Orientation = Orientation.Vertical;
                    thirdStatsColumn.VerticalAlignment = VerticalAlignment.Top;
                    thirdStatsColumn.Margin = ScaledThicknessFactory.GetThickness(2, 0, 0, 0);
                    fullDesignerStackPanel.Children.Add(thirdStatsColumn);

                    AddStat(thirdStatsColumn, "Manpower", idAndDivision.Value.manpower.ToString(), idAndDivision.Value.manpowerDescription);
                    AddStat(thirdStatsColumn, "Training Time", idAndDivision.Value.trainingTime.ToString(), idAndDivision.Value.trainingTimeDescription);

                }

                contentWrapPanel.Children.Add(divisionWrapPanel);
            }
        }

        private ImageButton CreateUnitButton(ButtonName buttonName, int column, int uniqueDivisionId)
        {
            ImageButton unitButton = new ImageButton(buttonName);
            unitButton.MouseDown += new MouseButtonEventHandler(ButtonClicked);
            unitButton.uniqueDivisionId = uniqueDivisionId;
            unitButton.column = column;
            unitButton.Margin = ScaledThicknessFactory.GetThickness(0, 0, 0, 14);
            return unitButton;
        }

        private StackPanel CreateUnitColumn(StackPanel parent)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            stackPanel.Margin = ScaledThicknessFactory.GetThickness(11, 15, 0, 0);
            parent.Children.Add(stackPanel);
            return stackPanel;
        }

        private void AddStat(StackPanel column, string statDescription, string stat, string longStatDescription)
        {
            Label statLable;
            StackPanel descriptionAndStat = new StackPanel();
            descriptionAndStat.SetValue(ToolTipService.ShowDurationProperty, Int32.MaxValue);
            descriptionAndStat.Orientation = Orientation.Horizontal;
            descriptionAndStat.VerticalAlignment = VerticalAlignment.Top;
            column.Children.Add(descriptionAndStat);

            TextBlock toolTipTextBlock = new TextBlock();
            toolTipTextBlock.FontSize = Opt.ApResMod(14);
            toolTipTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            toolTipTextBlock.Text = longStatDescription;
            toolTipTextBlock.TextWrapping = TextWrapping.Wrap;
            toolTipTextBlock.MaxWidth = Opt.ApResMod(300);

            ToolTip descriptionToolTip = new ToolTip();
            descriptionToolTip.Content = toolTipTextBlock;
            descriptionToolTip.Background = new SolidColorBrush(Color.FromRgb(230, 230, 230));
            descriptionAndStat.ToolTip = descriptionToolTip;

            statLable = new Label();
            statLable.Padding = ScaledThicknessFactory.GetThickness(0, 0, 0, 0);
            statLable.Margin = ScaledThicknessFactory.GetThickness(5, 3, 0, 0);
            statLable.Width = Opt.ApResMod(105);
            statLable.FontSize = Opt.ApResMod(16);
            statLable.Content = statDescription + ":";
            statLable.HorizontalContentAlignment = HorizontalAlignment.Left;
            descriptionAndStat.Children.Add(statLable);
            statLable = new Label();
            statLable.Padding = ScaledThicknessFactory.GetThickness(0, 0, 0, 0);
            statLable.Margin = ScaledThicknessFactory.GetThickness(5, 3, 0, 0);
            statLable.Width = Opt.ApResMod(57);
            statLable.FontSize = Opt.ApResMod(16);
            statLable.Content = stat;
            statLable.HorizontalContentAlignment = HorizontalAlignment.Right;
            statLable.HorizontalAlignment = HorizontalAlignment.Right;
            descriptionAndStat.Children.Add(statLable);
        }

        private void ButtonClicked(object sender, MouseButtonEventArgs e)
        {            
            ImageButton imageButton = (ImageButton)sender;
            if(isAddingUnit == false)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (imageButton.ButtonName != ButtonName.AddUnit && Divisions.divisions[imageButton.uniqueDivisionId].IsColumnFull(imageButton.column) == false)
                    {
                        Divisions.divisions[imageButton.uniqueDivisionId].AddUnit((UnitName)Enum.Parse(typeof(UnitName), imageButton.ButtonName.ToString()), imageButton.column);
                    }
                }
                else
                {
                    isAddingUnit = true;
                    divisionIsAddingUnitTo = imageButton.uniqueDivisionId;
                    columnIsAddingUnitTo = imageButton.column;
                    buttonNamePressedToAddUnit = imageButton.ButtonName;
                }                
            }
            else
            {
                isAddingUnit = false;
                if(buttonNamePressedToAddUnit != ButtonName.AddUnit)
                {
                    Divisions.divisions[imageButton.uniqueDivisionId].unitsInDivision[columnIsAddingUnitTo][(UnitName)Enum.Parse(typeof(UnitName), buttonNamePressedToAddUnit.ToString())]--;
                }
                Divisions.divisions[imageButton.uniqueDivisionId].AddUnit((UnitName)Enum.Parse(typeof(UnitName), imageButton.ButtonName.ToString()), imageButton.column);
            }            
            DisplayContent();
        }
    }
}
