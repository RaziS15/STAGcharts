using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace MyRhinoPlugin1.MyRhinoPlugin1
{
    public partial class ChartWindow : Window
    {
        public PlotModel PlotModel { get; private set; }

        public ChartWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Add null check for safety
            var data = MyRhinoPlugin1.Instance.LoadedDailyStages;
            PlotModel = CreatePlotModel(data);
        }

        private PlotModel CreatePlotModel(List<DailyStageData> data)
        {
            var model = new PlotModel { Title = "Stage Progress Over 30 Days" };

            // Configure axes
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Pieces",
                MinimumPadding = 0.1,
                MaximumPadding = 0.1
            });

            // Dynamic day range based on actual data
            var minDay = data.Any() ? data.Min(d => d.Day) : 1;
            var maxDay = data.Any() ? data.Max(d => d.Day) : 30;

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Day",
                Minimum = Math.Max(1, minDay - 1),
                Maximum = maxDay + 1,
                MajorStep = Math.Max(1, (maxDay - minDay) / 10) // Reasonable step size
            });

            // Add series with different colors
            model.Series.Add(CreateLineSeries(data, "Design", d => d.Design, OxyColors.Blue));
            model.Series.Add(CreateLineSeries(data, "Engineering", d => d.Engineering, OxyColors.Red));
            model.Series.Add(CreateLineSeries(data, "Production", d => d.Production, OxyColors.Green));
            model.Series.Add(CreateLineSeries(data, "Shipping", d => d.Shipping, OxyColors.Orange));

            return model;
        }

        private LineSeries CreateLineSeries(List<DailyStageData> data, string title,
            Func<DailyStageData, int> selector, OxyColor color)
        {
            var series = new LineSeries
            {
                Title = title,
                Color = color,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerFill = color
            };

            // Sort data by day to ensure proper line drawing
            var sortedData = data.OrderBy(d => d.Day);

            foreach (var d in sortedData)
            {
                series.Points.Add(new DataPoint(d.Day, selector(d)));
            }

            return series;
        }

        // Method to update the chart with new data
        public void UpdateChart(List<DailyStageData> newData)
        {
            PlotModel = CreatePlotModel(newData);
            // Trigger property change notification if needed
            // OnPropertyChanged(nameof(PlotModel));
        }
    }
}