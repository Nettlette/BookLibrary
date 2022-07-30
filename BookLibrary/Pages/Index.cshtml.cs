using BookLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChartJSCore;
using ChartJSCore.Models;
using System.Text;
using ChartJSCore.Helpers;
using BookLibrary.Extensions;

namespace BookLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly ApplicationDbContext _context;
        public int BooksWeek;
        public int BooksMonth;
        public int BooksYear;
        private DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        private DateTime StartOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime StartOfYear = new DateTime(DateTime.Today.Year, 1, 1);
        public string TestChart;
        public Chart FNFRead;
        public Chart BooksRead12Month;
        public Chart TimeToFinish;
        public Chart TopAuthors;
        public Chart BooksPublished;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            BooksWeek = _context.BooksRead.Count(x => x.EndDate >= StartOfWeek);
            BooksMonth = _context.BooksRead.Count(x => x.EndDate >= StartOfMonth);
            BooksYear = _context.BooksRead.Count(x => x.EndDate >= StartOfYear);

            FNFRead = GenerateFNFReadPieChart();
            BooksRead12Month = GenerateBooksRead12MonthBarChart();
            TimeToFinish = GenerateTimeToFinishBarChart();
            TopAuthors = GenerateTopAuthorsPieChart();
            BooksPublished = GenerateBooksPublishedScatterChart();
        }

        public void OnGet()
        {

        }

        private Chart GenerateFNFReadPieChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Pie;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "Fiction", "NonFiction" };

            PieDataset dataset = new PieDataset()
            {
                Label = "Books",
                Data = new List<double?>() {
                    _context.BooksReadIndex.Count(x=>x.Category==Models.Category.Fiction),
                    _context.BooksReadIndex.Count(x=>x.Category==Models.Category.Nonfiction),
                },
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple,
                    ColorPalatte.Pink
                }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Percent of Books Read" }
            };
            return chart;
        }

        private Chart GenerateTopAuthorsPieChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Pie;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();

            PieDataset dataset = new PieDataset()
            {
                Label = "Authors",
                Data = new List<double?>(),
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple,
                    ColorPalatte.Pink,
                    ColorPalatte.Blue,
                    ColorPalatte.Grey,
                    ColorPalatte.Black
                }
            };

            var ta = _context.TopAuthors.Where(x => x.ReadCount >= 10).OrderBy(x => x.ReadCount).ToList();
            foreach(var t in ta)
            {
                data.Labels.Add(t.Name);
                dataset.Data.Add(t.ReadCount);
            }
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Top Authors Read" }
            };
            chart.Options.Plugins.Legend = new Legend
            {
                Display = false
            };
            return chart;
        }

        private Chart GenerateBooksRead12MonthBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();
            DateTime StartDate = DateTime.Now;
            StartDate = StartDate.AddMonths(-12).AddDays(-StartDate.Day + 1);

            BarDataset fictionDataset = new BarDataset()
            {
                Label = "Fiction",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Blue
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                //BarThickness = 6,
                //MaxBarThickness = 8,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };
            BarDataset nonfictionDataset = new BarDataset()
            {
                Label = "Nonfiction",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                //BarThickness = 6,
                //MaxBarThickness = 8,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };

            for (var i=0; i<=12; i++)
            {
                data.Labels.Add(StartDate.ToString("MMMM"));
                fictionDataset.Data.Add(_context.BooksReadIndex.Count(x => x.Category == Models.Category.Fiction && x.EndDate >= StartDate && x.EndDate < StartDate.AddMonths(1)));
                nonfictionDataset.Data.Add(_context.BooksReadIndex.Count(x => x.Category == Models.Category.Nonfiction && x.EndDate >= StartDate && x.EndDate < StartDate.AddMonths(1)));
                StartDate = StartDate.AddMonths(1);
            }

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(fictionDataset);
            data.Datasets.Add(nonfictionDataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new CartesianLinearScale()
                        {
                            BeginAtZero = true,
                            Stacked = true
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true,
                                DrawTicks = false
                            },
                            Stacked = true
                        }
                    },
                }
            };

            chart.Options = options;

            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Books Read in the last year" }
            };
            return chart;
        }

        private Chart GenerateTimeToFinishBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "Fiction", "Nonfiction", "Total" };

            BarDataset dataset = new BarDataset()
            {
                Label = "Time To Finish",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Pink,
                    ColorPalatte.Blue,
                    ColorPalatte.Grey
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };

            dataset.Data.Add((double?)_context.ReaderStats.Average(x => x.FictionAvg));
            dataset.Data.Add((double?)_context.ReaderStats.Average(x => x.NonFictionAvg));
            dataset.Data.Add((double?)_context.ReaderStats.Average(x => x.TotalAvg));

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new CartesianLinearScale()
                        {
                            BeginAtZero = true
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true,
                                DrawTicks = false
                            }
                        }
                    },
                }
            };

            chart.Options = options;

            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Legend = new Legend();
            chart.Options.Plugins.Legend.Display = false;
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Average Days to Finish" }
            };
            return chart;
        }

        private Chart GenerateBooksPublishedScatterChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Scatter;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();

            LineScatterDataset dataset = new LineScatterDataset()
            {
                Label = "Books Read by Year Published",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Blue
                },
                BorderWidth = new List<int>() { 1 },
                Data = new List<LineScatterData>()
            };

            dataset.Data = _context.BooksPublished.Where(x => x.EndDate > new DateTime(2007, 1, 1))
                .OrderBy(x => x.EndDate)
                .Select(a => new LineScatterData { X = a.EndDate.ToString("MM/dd/yyyy"), Y = a.Published.ToString() })
                .ToList();

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            /*var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    {"y", new Scale()
                        {
                            Max = 2050
                        } 
                    },
                    { "x", new TimeScale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true,
                                DrawTicks = false
                            },
                            Type = "time"
                            
                        }
                    },
                }
            };*/
            chart.Options = new Options();
            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Books Read by Year Published" }
            };
            return chart;
        }
    }
}