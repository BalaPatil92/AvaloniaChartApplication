using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaChartApplication
{
    public partial class MainWindow : Window
    {
        private bool _isSideMenuCollapsed = false;

        public MainWindow()
        {
            InitializeComponent();

            // Set initial state - Real-Time Monitor is active by default
            ResetMenuButtonStyles();
            HighlightMenuButton(RealtimeMonitorBtn);
        }

        private void MenuToggle_Click(object? sender, RoutedEventArgs e)
        {
            // Toggle sidebar visibility with responsive design
            if (_isSideMenuCollapsed)
            {
                SideMenuBorder.Width = 250;
                _isSideMenuCollapsed = false;
            }
            else
            {
                SideMenuBorder.Width = 60;
                _isSideMenuCollapsed = true;
            }
        }

        private void OpenAnotherWindow_Click(object? sender, RoutedEventArgs e)
        {
            // Reset all menu buttons and highlight the clicked one
            ResetMenuButtonStyles();
            HighlightMenuButton(RealtimeMonitorBtn);

            // Show the line chart view in the main content area
            var lineChartView = new LineChartView();
            MainContentArea.Content = lineChartView;
        }

        private void PatientVitals_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            ShowPlaceholderContent("📊 Patient Vitals", "Patient vital signs monitoring interface will be displayed here.");
        }

        private void DialysisProgress_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            ShowPlaceholderContent("📈 Dialysis Progress", "Dialysis progress tracking charts will be displayed here.");
        }

        private void MultiParameter_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            ShowPlaceholderContent("📋 Multi-Parameter", "Multi-parameter monitoring dashboard will be displayed here.");
        }

        private void Dashboard_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show default dashboard content
            var dashboardContent = new ScrollViewer
            {
                Content = new StackPanel
                {
                    Margin = new Avalonia.Thickness(30),
                    Spacing = 25,
                    Children =
                    {
                        new Border
                        {
                            Background = Avalonia.Media.Brush.Parse("White"),
                            CornerRadius = new Avalonia.CornerRadius(12),
                            Padding = new Avalonia.Thickness(25),
                            Child = new StackPanel
                            {
                                Spacing = 15,
                                Children =
                                {
                                    new TextBlock
                                    {
                                        Text = "🏠 Home Dashboard",
                                        FontSize = 24,
                                        FontWeight = Avalonia.Media.FontWeight.Bold,
                                        Foreground = Avalonia.Media.Brush.Parse("#10B981")
                                    },
                                    new TextBlock
                                    {
                                        Text = "Welcome to the Chart Monitor. Select a chart from the sidebar to begin monitoring.",
                                        FontSize = 16,
                                        Foreground = Avalonia.Media.Brush.Parse("#6B7280"),
                                        TextWrapping = Avalonia.Media.TextWrapping.Wrap
                                    }
                                }
                            }
                        }
                    }
                }
            };
            
            MainContentArea.Content = dashboardContent;
        }

        private void ShowPlaceholderContent(string title, string description)
        {
            var placeholderContent = new Grid
            {
                Children =
                {
                    new Border
                    {
                        Background = Avalonia.Media.Brush.Parse("White"),
                        CornerRadius = new Avalonia.CornerRadius(8),
                        Margin = new Avalonia.Thickness(10),
                        Padding = new Avalonia.Thickness(40),
                        Child = new StackPanel
                        {
                            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                            Spacing = 20,
                            Children =
                            {
                                new TextBlock
                                {
                                    Text = title,
                                    FontSize = 24,
                                    FontWeight = Avalonia.Media.FontWeight.Bold,
                                    Foreground = Avalonia.Media.Brush.Parse("#10B981"),
                                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                                },
                                new TextBlock
                                {
                                    Text = description,
                                    FontSize = 16,
                                    Foreground = Avalonia.Media.Brush.Parse("#6B7280"),
                                    TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                                    TextAlignment = Avalonia.Media.TextAlignment.Center
                                },
                                new Button
                                {
                                    Content = "← Back to Real-Time Monitor",
                                    Background = Avalonia.Media.Brush.Parse("#10B981"),
                                    Foreground = Avalonia.Media.Brush.Parse("White"),
                                    CornerRadius = new Avalonia.CornerRadius(6),
                                    Padding = new Avalonia.Thickness(20, 10),
                                    FontSize = 14,
                                    FontWeight = Avalonia.Media.FontWeight.SemiBold,
                                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                                }
                            }
                        }
                    }
                }
            };

            // Add click handler to the back button
            if (placeholderContent.Children[0] is Border border &&
                border.Child is StackPanel stackPanel &&
                stackPanel.Children[2] is Button backBtn)
            {
                backBtn.Click += (s, e) => OpenAnotherWindow_Click(RealtimeMonitorBtn, e);
            }

            MainContentArea.Content = placeholderContent;
        }

        private void ResetMenuButtonStyles()
        {
            // Reset all menu buttons to default inactive style
            var menuButtons = new[]
            {
                DashboardBtn, PatientVitalsBtn, DialysisProgressBtn,
                RealtimeMonitorBtn, MultiParameterBtn
            };

            foreach (var button in menuButtons)
            {
                if (button != null)
                {
                    button.Classes.Clear();
                    button.Classes.Add("menu");
                }
            }
        }

        private void HighlightMenuButton(Button? button)
        {
            if (button != null)
            {
                button.Classes.Clear();
                button.Classes.Add("menu");
                button.Classes.Add("active");
            }
        }
    }
}
