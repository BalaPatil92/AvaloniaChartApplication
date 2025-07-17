using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaChartApplication;

public partial class LinecharRealtime : Window
{
    public List<ISeries> Series { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }

    private DispatcherTimer _timer;
    private List<List<double>> _datasets;
    private int _maxPointsPerSeries = 300000;
    private int _pointsPerSecond;
    private int _remainingSeconds;
    private Random _rand = new Random();

    public LinecharRealtime()
    {
        InitializeComponent();
        WindowState = WindowState.Maximized;
        Series = new List<ISeries>();
        XAxes = new Axis[]
        {
                new Axis { Name = "Datapoints on Timespan", TextSize = 12 }
        };
        YAxes = new Axis[]
        {
                new Axis { Name = "Value Series", TextSize = 12 }
        };

        DataContext = this;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;
    }

    private void StartButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(PointsPerSecondBox.Text, out _pointsPerSecond) ||
            !int.TryParse(DurationBox.Text, out int durationMinutes) ||
            !int.TryParse(DatasetCountBox.Text, out int datasetCount))
        {
            ShowError("Enter valid numeric values.");
            return;
        }

        _remainingSeconds = durationMinutes * 60;
        _datasets = new List<List<double>>();
        Series.Clear();

        for (int i = 0; i < datasetCount; i++)
        {
            var dataset = new List<double>();
            _datasets.Add(dataset);

            var color = new SKColor(
                (byte)_rand.Next(50, 256),
                (byte)_rand.Next(50, 256),
                (byte)_rand.Next(50, 256));

            var lineSeries = new LineSeries<double>
            {
                Values = dataset,
                Name = $"Dataset {i + 1}",
                Stroke = new SolidColorPaint(color, 1.5f),
                Fill = null,
                GeometrySize = 1,
                LineSmoothness = 0
            };

            Series.Add(lineSeries);
        }

        ChartContainer.IsVisible = true;
        Chart.Series = Series;
        _timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_remainingSeconds <= 0)
        {
            _timer.Stop();
            return;
        }

        for (int i = 0; i < _datasets.Count; i++)
        {
            var list = _datasets[i];

            for (int j = 0; j < _pointsPerSecond; j++)
            {
                list.Add(_rand.NextDouble() * 100);
            }

            if (list.Count > _maxPointsPerSeries)
            {
                list.RemoveRange(0, list.Count - _maxPointsPerSeries);
            }
        }

        _remainingSeconds--;
        Chart.Series = null;
        Chart.Series = Series;
    }

    private void StopButton_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    private void ClearButton_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        Series.Clear();
        Chart.Series = null;

        PointsPerSecondBox.Text = "";
        DurationBox.Text = "";
        DatasetCountBox.Text = "";

        ChartContainer.IsVisible = false;
    }

    private async void ShowError(string message)
    {
        await MessageBox(message);
    }

    private Task MessageBox(string text)
    {
        var dialog = new Window
        {
            Width = 300,
            Height = 150,
            Title = "Error"
        };

        var button = new Button
        {
            Content = "OK",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            Width = 80
        };

        button.Click += (_, _) => dialog.Close();

        dialog.Content = new StackPanel
        {
            Margin = new Thickness(20),
            Children =
        {
            new TextBlock { Text = text, Margin = new Thickness(0, 0, 0, 20) },
            button
        }
        };

        return dialog.ShowDialog(this);
    }

}