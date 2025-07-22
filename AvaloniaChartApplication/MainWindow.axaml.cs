using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaChartApplication
{
    public partial class MainWindow : Window
    {
        private ScrollViewer _defaultContent;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            
            // Store the default content
            _defaultContent = (ScrollViewer)MainContentArea.Content;
        }

        private void OpenAnotherWindow_Click(object? sender, RoutedEventArgs e)
        {
            // Reset all menu buttons and highlight the clicked one
            ResetMenuButtonStyles();
            HighlightMenuButton(LineChartBtn);
            
            // Show the line chart view in the main content area
            var lineChartView = new LineChartView();
            MainContentArea.Content = lineChartView;
        }

        private void Dashboard_Click(object? sender, RoutedEventArgs e)
        {
            // Reset all menu buttons to default style
            ResetMenuButtonStyles();
            
            // Highlight selected button
            if (sender is Button btn)
            {
                btn.Background = Avalonia.Media.Brush.Parse("#EBF4FF");
                btn.Foreground = Avalonia.Media.Brush.Parse("#2E5BBA");
                btn.BorderBrush = Avalonia.Media.Brush.Parse("#4A90E2");
                btn.BorderThickness = new Avalonia.Thickness(0, 0, 3, 0);
            }
            
            // Show the default dashboard content
            MainContentArea.Content = _defaultContent;
        }

        private void Analytics_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show analytics placeholder content
            ShowPlaceholderContent("📈 Analytics", "Analytics dashboard coming soon...");
        }

        private void BarChart_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show bar chart placeholder content
            ShowPlaceholderContent("📊 Bar Charts", "Bar chart functionality coming soon...");
        }

        private void PieChart_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show pie chart placeholder content
            ShowPlaceholderContent("🥧 Pie Charts", "Pie chart functionality coming soon...");
        }

        private void ScatterChart_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show scatter chart placeholder content
            ShowPlaceholderContent("⚫ Scatter Plots", "Scatter plot functionality coming soon...");
        }

        private void ImportData_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show import data placeholder content
            ShowPlaceholderContent("📥 Import Data", "Data import functionality coming soon...");
        }

        private void ExportData_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show export data placeholder content
            ShowPlaceholderContent("📤 Export Data", "Data export functionality coming soon...");
        }

        private void Preferences_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show preferences placeholder content
            ShowPlaceholderContent("⚙️ Preferences", "Application preferences coming soon...");
        }

        private void Settings_Click(object? sender, RoutedEventArgs e)
        {
            // Show settings placeholder content (header button, not menu)
            ShowPlaceholderContent("⚙️ Settings", "Application settings coming soon...");
        }

        private void Help_Click(object? sender, RoutedEventArgs e)
        {
            ResetMenuButtonStyles();
            HighlightMenuButton(sender as Button);
            
            // Show help placeholder content
            ShowPlaceholderContent("❓ Help & Support", "Help documentation coming soon...");
        }

        private void ShowPlaceholderContent(string title, string description)
        {
            var placeholderContent = new ScrollViewer
            {
                Content = new StackPanel
                {
                    Margin = new Avalonia.Thickness(40),
                    Spacing = 30,
                    Children =
                    {
                        new Border
                        {
                            Background = Avalonia.Media.Brush.Parse("White"),
                            CornerRadius = new Avalonia.CornerRadius(10),
                            Padding = new Avalonia.Thickness(30),
                            Child = new StackPanel
                            {
                                Spacing = 15,
                                Children =
                                {
                                    new TextBlock
                                    {
                                        Text = title,
                                        FontSize = 28,
                                        FontWeight = Avalonia.Media.FontWeight.Bold,
                                        Foreground = Avalonia.Media.Brush.Parse("#2E5BBA")
                                    },
                                    new TextBlock
                                    {
                                        Text = description,
                                        FontSize = 16,
                                        Foreground = Avalonia.Media.Brush.Parse("#6B7280"),
                                        TextWrapping = Avalonia.Media.TextWrapping.Wrap
                                    }
                                }
                            }
                        },
                        new Border
                        {
                            Background = Avalonia.Media.Brush.Parse("White"),
                            CornerRadius = new Avalonia.CornerRadius(10),
                            Padding = new Avalonia.Thickness(30),
                            Child = new StackPanel
                            {
                                Spacing = 15,
                                Children =
                                {
                                    new TextBlock
                                    {
                                        Text = "Return to Dashboard",
                                        FontSize = 20,
                                        FontWeight = Avalonia.Media.FontWeight.Bold,
                                        Foreground = Avalonia.Media.Brush.Parse("#2E5BBA")
                                    },
                                    new Button
                                    {
                                        Content = "🏠 Go to Dashboard",
                                        Background = Avalonia.Media.Brush.Parse("#4A90E2"),
                                        Foreground = Avalonia.Media.Brush.Parse("White"),
                                        BorderBrush = Avalonia.Media.Brush.Parse("Transparent"),
                                        CornerRadius = new Avalonia.CornerRadius(8),
                                        Padding = new Avalonia.Thickness(20, 12),
                                        FontSize = 14,
                                        FontWeight = Avalonia.Media.FontWeight.SemiBold,
                                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Add click handler to the dashboard button
            if (placeholderContent.Content is StackPanel stackPanel && 
                stackPanel.Children[1] is Border border &&
                border.Child is StackPanel innerStack &&
                innerStack.Children[1] is Button dashboardBtn)
            {
                dashboardBtn.Click += (s, e) => Dashboard_Click(DashboardBtn, e);
            }

            MainContentArea.Content = placeholderContent;
        }

        private void ResetMenuButtonStyles()
        {
            // Reset all menu buttons to default inactive style
            var menuButtons = new[]
            {
                DashboardBtn, AnalyticsBtn, LineChartBtn, BarChartBtn, 
                PieChartBtn, ScatterChartBtn, ImportDataBtn, ExportDataBtn, 
                PreferencesBtn, HelpBtn
            };

            foreach (var button in menuButtons)
            {
                if (button != null)
                {
                    button.Background = Avalonia.Media.Brush.Parse("Transparent");
                    button.Foreground = Avalonia.Media.Brush.Parse("#6B7280");
                    button.BorderBrush = Avalonia.Media.Brush.Parse("Transparent");
                    button.BorderThickness = new Avalonia.Thickness(0);
                }
            }
        }

        private void HighlightMenuButton(Button? button)
        {
            if (button != null)
            {
                button.Background = Avalonia.Media.Brush.Parse("#EBF4FF");
                button.Foreground = Avalonia.Media.Brush.Parse("#2E5BBA");
                button.BorderBrush = Avalonia.Media.Brush.Parse("#4A90E2");
                button.BorderThickness = new Avalonia.Thickness(0, 0, 3, 0);
            }
        }
    }
}