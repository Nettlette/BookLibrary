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
        public Chart PagesByAuthor;
        public Chart HoursByAuthor;
        public Chart SubcategoriesRead;
        public Chart LocationsRead;
        public Chart AuthorLocations;
        public List<string> LastBooksFinished;
        public List<string> BooksInProgress;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            BooksWeek = _context.BooksRead.Count(x => x.EndDate >= StartOfWeek);
            BooksMonth = _context.BooksRead.Count(x => x.EndDate >= StartOfMonth);
            BooksYear = _context.BooksRead.Count(x => x.EndDate >= StartOfYear);
            LastBooksFinished = _context.BooksReadIndex
                                    .Where(x => x.StartDate != null && x.EndDate != null)
                                    .OrderByDescending(x => x.EndDate)
                                    .Take(5)
                                    .Select(x => x.Title + " by " + x.Authors)
                                    .ToList();
            BooksInProgress = _context.BooksReadIndex
                                    .Where(x => x.StartDate != null && x.EndDate == null)
                                    .OrderByDescending(x => x.StartDate)
                                    .Take(5)
                                    .Select(x => x.Title + " by " + x.Authors)
                                    .ToList();

            FNFRead = GenerateFNFReadPieChart();
            BooksRead12Month = GenerateBooksRead12MonthBarChart();
            TimeToFinish = GenerateTimeToFinishBarChart();
            TopAuthors = GenerateTopAuthorsPieChart();
            BooksPublished = GenerateBooksPublishedScatterChart();
            PagesByAuthor = GeneratePagesByAuthorPieChart();
            HoursByAuthor = GenerateHoursByAuthorPieChart();
            SubcategoriesRead = GenerateSubcategoriesReadBarChart();
            LocationsRead = GenerateLocationsReadBarChart();
            AuthorLocations = GenerateAuthorLocationsBarChart();
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
            chart.Options.MaintainAspectRatio = false;
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
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                }
            };

            var ta = _context.TopAuthors.Where(x => x.ReadCount >= 10).OrderByDescending(x => x.ReadCount).ToList();
            foreach(var t in ta)
            {
                data.Labels.Add(t.Name);
                dataset.Data.Add(t.ReadCount);
            }
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            chart.Options.MaintainAspectRatio = false;
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

        private Chart GenerateHoursByAuthorPieChart()
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
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                }
            };

            var ta = _context.PagesHoursByAuthorView.Where(x => x.Hours >= 15).OrderByDescending(x => x.Hours).ToList();
            foreach(var t in ta)
            {
                data.Labels.Add(t.Name);
                dataset.Data.Add((double?)t.Hours);
            }
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            chart.Options.MaintainAspectRatio = false;
            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Hours Read by Author" }
            };
            chart.Options.Plugins.Legend = new Legend
            {
                Display = false
            };
            return chart;
        }

        private Chart GeneratePagesByAuthorPieChart()
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
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                }
            };

            var ta = _context.PagesHoursByAuthorView.Where(x => x.Pages >= 5000).OrderByDescending(x => x.Pages).ToList();
            foreach(var t in ta)
            {
                data.Labels.Add(t.Name);
                dataset.Data.Add(t.Pages);
            }
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            chart.Options.Plugins = new Plugins();
            chart.Options.MaintainAspectRatio = false;
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Pages Read by Author" }
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
                MaintainAspectRatio = false,
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
                MaintainAspectRatio = false,
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
            DateTime EndDate = new DateTime(DateTime.Now.Year - 10, 1, 1);

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();
            
            LineScatterDataset fictionDataset = new LineScatterDataset()
            {
                Label = "Fiction Books Read by Year Published",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple
                },
                BorderWidth = new List<int>() { 1 },
                Data = new List<LineScatterData>(),
                ShowLine = false
            };
            LineScatterDataset nonfictionDataset = new LineScatterDataset()
            {
                Label = "Nonfiction Books Read by Year Published",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Red
                },
                BorderWidth = new List<int>() { 1 },
                Data = new List<LineScatterData>(),
                ShowLine = false
            };

            fictionDataset.Data = _context.BooksPublished.Where(x => x.Category == Models.Category.Fiction && x.EndDate > EndDate)
                .OrderBy(x => x.EndDate)
                .Select(a => new LineScatterData { X = a.EndDate.ToString("yyyy/M/d"), Y = a.Published.ToString() })
                .ToList();

            nonfictionDataset.Data = _context.BooksPublished.Where(x => x.Category == Models.Category.Nonfiction && x.EndDate > EndDate)
                .OrderBy(x => x.EndDate)
                .Select(a => new LineScatterData { X = a.EndDate.ToString("yyyy/M/d"), Y = a.Published.ToString() })
                .ToList();
            data.Labels = _context.BooksPublished.Where(x => x.EndDate > EndDate).OrderBy(x => x.EndDate).Select(a => a.EndDate.ToString("yyyy/M/d")).ToList();
            data.Labels = data.Labels.Distinct().ToList();
            data.Labels = data.Labels.OrderBy(x => x).ToList();
            data.Datasets = new List<Dataset>();
            data.Datasets.Add(fictionDataset);
            data.Datasets.Add(nonfictionDataset);

            chart.Data = data;

            var options = new Options
            {
                MaintainAspectRatio = false,
                Scales = new Dictionary<string, Scale>()
                {
                    {"y", new CartesianLinearScale()
                        {
                            Max = 2025,
                            
                            Ticks = new CartesianLinearTick()
                            {
                                StepSize = 25,
                                
                            }
                        } 
                    },
                    //{"x", new CartesianLinearScale()
                    //    {
                            
                    //    }
                    //}
                }
            };
            chart.Options = options;
            chart.Options.Plugins = new Plugins();
            chart.Options.Plugins.Title = new Title
            {
                Display = true,
                Text = new List<string>() { "Books Read by Year Published" }
            };
            return chart;
        }

        private Chart GenerateSubcategoriesReadBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();

            BarDataset dataset = new BarDataset()
            {
                Label = "Subcategories",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple,
                    ColorPalatte.Pink,
                    ColorPalatte.Blue,
                    ColorPalatte.Grey,
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };

            dataset.Data = _context.SubcategoryChartView.Where(x => x.CountRead > 15).OrderByDescending(x => x.CountRead).Select(x => (double?)x.CountRead).ToList();
            data.Labels = _context.SubcategoryChartView.Where(x => x.CountRead > 15).OrderByDescending(x => x.CountRead).Select(x => x.Name).ToList();

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                MaintainAspectRatio = false,
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
                Text = new List<string>() { "Popular Subcategories" }
            };
            return chart;
        }

        private Chart GenerateLocationsReadBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();

            BarDataset dataset = new BarDataset()
            {
                Label = "Locations",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple,
                    ColorPalatte.Pink,
                    ColorPalatte.Blue,
                    ColorPalatte.Grey,
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };

            dataset.Data = _context.LocationChartView.Where(x => x.CountRead > 10).OrderByDescending(x => x.CountRead).Select(x => (double?)x.CountRead).ToList();
            data.Labels = _context.LocationChartView.Where(x => x.CountRead > 10).OrderByDescending(x => x.CountRead).Select(x => x.Name).ToList();

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                MaintainAspectRatio = false,
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
                Text = new List<string>() { "Popular Book Locations" }
            };
            return chart;
        }

        private Chart GenerateAuthorLocationsBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>();

            BarDataset dataset = new BarDataset()
            {
                Label = "Author Locations",
                BackgroundColor = new List<ChartColor>
                {
                    ColorPalatte.Purple,
                    ColorPalatte.Pink,
                    ColorPalatte.Blue,
                    ColorPalatte.Grey,
                    ColorPalatte.Black,
                    ColorPalatte.DarkRed,
                    ColorPalatte.Green,
                    ColorPalatte.Yellow,
                    ColorPalatte.Red
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = .75,
                MinBarLength = 2,
                CategoryPercentage = 1.0,
                Data = new List<double?>()
            };

            dataset.Data = _context.AuthorLocationsChart.OrderByDescending(x => x.Count).Select(x => (double?)x.Count).ToList();
            data.Labels = _context.AuthorLocationsChart.OrderByDescending(x => x.Count).Select(x => x.Name).ToList();

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                MaintainAspectRatio = false,
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
                Text = new List<string>() { "Where The Authors Are From" }
            };
            return chart;
        }
    }
}