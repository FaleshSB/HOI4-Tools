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
        private System.Drawing.Size buttonSize = new System.Drawing.Size((int)Math.Round(Opt.ApResMod(168)), (int)Math.Round(Opt.ApResMod(36)));

        private Dictionary<string, Image> buttons = new Dictionary<string, Image>();
        private string manageSquadsButtonName = "manage_squads_start";
        private string browseCardsButtonName = "browse_cards_start";
        private string calculateStatsButtonName = "calculate_stats_start";
        private string quizButtonName = "quiz_start";
        private Canvas contentCanvas = new Canvas();
        private string filteredLocation;
        private bool isButtonBeingPressed = false;

        private AlignableWrapPanel contentWrapPanel = new AlignableWrapPanel();

        public StartPage()
        {
            contentWrapPanel.Name = "contentWrapPanel";
            contentWrapPanel.HorizontalContentAlignment = HorizontalAlignment.Center;
            contentScrollViewer.Content = contentWrapPanel;

            contentCanvas.Name = "contentCanvas";
            contentCanvas.Height = 900;
            //contentCanvas.Background = new SolidColorBrush(Colors.Black);
            contentWrapPanel.Children.Add(contentCanvas);
            InitializeComponent();/*
            buttons.Add(manageSquadsButtonName, CreateButton(manageSquadsButtonName));
            buttons.Add(browseCardsButtonName, CreateButton(browseCardsButtonName));
            buttons.Add(calculateStatsButtonName, CreateButton(calculateStatsButtonName));
            buttons.Add(quizButtonName, CreateButton(quizButtonName));
            contentCanvas.Width = buttons[quizButtonName].Width;*/
        }
    }
}
