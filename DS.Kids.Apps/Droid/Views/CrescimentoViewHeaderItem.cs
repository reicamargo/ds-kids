using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Model;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DS.Kids.Apps.Droid.Views
{

	[Register("ds.kids.apps.droid.views.CrescimentoViewHeaderItem")]
	public class CrescimentoViewHeaderItem : RelativeLayout
	{
		#region Fields

		private bool _editable = true;

        private PlotView _graphicalView;
		private LineSeries _mySeries;

        private readonly OxyColor _red = OxyColor.FromArgb(255, 255, 6, 6);
        private readonly OxyColor _yellow = OxyColor.FromArgb(255, 255, 255, 24);
        private readonly OxyColor _green = OxyColor.FromArgb(255, 0, 255, 0);
        private readonly OxyColor _gray = OxyColor.FromArgb(255, 204, 204, 204);

        private const int WeeksInYear = 52;
        private const int WeeksInMonth = 4;

		#endregion

		#region Constructors and Destructors

		public CrescimentoViewHeaderItem(Context context)
			: base(context)
		{
		}

		public CrescimentoViewHeaderItem(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			GetAttributes(attrs);
			Init();
		}

		public CrescimentoViewHeaderItem(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			GetAttributes(attrs);
			Init();
		}

		protected CrescimentoViewHeaderItem(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion

		#region Public Properties

		public bool Editable
		{
			get
			{
				return _editable;
			}
			set
			{
				_editable = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void UpdateValues()
		{
			if(_mySeries != null && _graphicalView != null)
			{
				RefreshMyData(_mySeries);
                _graphicalView.InvalidatePlot(true);
			}
		}

		#endregion

		#region Methods

		private static LineSeries PlotLine(PlotModel plotModel)
		{
            var series = new LineSeries 
            { 
                Color = OxyColor.FromArgb(255, 0, 0, 255),
                MarkerFill = OxyColor.FromArgb(255, 255, 255, 255),
                MarkerStroke = OxyColor.FromArgb(255, 0, 0, 255),
                MarkerType = MarkerType.Circle
            };

			RefreshMyData(series);

			plotModel.Series.Add(series);
			return series;
		}

		private static void PlotIMCLine(PlotModel plotModel, List<double> data, OxyColor color)
		{
            var series = new LineSeries 
            { 
                Color = color
            };

            var twoYearsInMonths = 24;

			for(int i = 0; i < data.Count; i += 12)
			{
                series.Points.Add(new DataPoint((i + twoYearsInMonths) * WeeksInMonth, data[i]));
			}

            plotModel.Series.Add(series);
		}

		private static void RefreshMyData(LineSeries series)
		{
            series.Points.Clear();

			var totalSemanas = new Func<DateTime, int>(date =>
				{
					var age = date.Subtract(LoginHelper.CurrentCrianca.DataNascimento);
					var weeks = (decimal)(age.TotalDays / 7);
					return (int)Math.Truncate(weeks);
				});

			foreach(var crescimento in LoginHelper.CurrentCrianca.Crescimentos)
			{
                series.Points.Add(new DataPoint(totalSemanas(crescimento.DataCriacao), (double)PesoAltura.ObterImc(crescimento.Peso, crescimento.Altura)));
			}
		}

		private void GetAttributes(IAttributeSet attrs)
		{
			var a = Context.ObtainStyledAttributes(attrs, Resource.Styleable.CrescimentoViewHeaderItem);
			var n = a.IndexCount;
			for(var i = 0; i < n; ++i)
			{
				var attr = a.GetIndex(i);
				if(attr == Resource.Styleable.CrescimentoViewHeaderItem_Editable)
				{
					Editable = a.GetBoolean(attr, true);
				}
			}

			a.Recycle();
		}

        private string WeeksToYears(double weeks)
        {
            return Math.Ceiling(weeks / WeeksInYear).ToString();
        }

		private void Init()
		{
            var plotModel = new PlotModel 
            {
                Title = "",
                TitleColor = _gray,
                PlotAreaBorderColor = OxyColor.FromArgb(255, 255, 255, 255),
                PlotMargins = new OxyThickness(GetProportionalDimension(25, 70), 
                                               GetProportionalDimension(5, 10), 
                                               GetProportionalDimension(10, 20), 
                                               GetProportionalDimension(40, 80)),
            };

            var xAxis = new LinearAxis()
            {
                Title = "IDADE",
                TextColor = _gray,
                TitleColor = _gray,
                TicklineColor = _gray,
                AxislineColor = _gray,
                MajorGridlineColor = _gray,
                MinorGridlineColor = _gray,
                AbsoluteMinimum = 96,
                AbsoluteMaximum = 484,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Solid,
                MajorGridlineThickness = GetProportionalDimension(0.6, 2),
                Position = AxisPosition.Bottom,
                IsPanEnabled = Editable,
                IsZoomEnabled = Editable,
                MajorStep = WeeksInYear,
                MinorStep = WeeksInYear,
                LabelFormatter = WeeksToYears,
                FontSize = GetProportionalDimension(10, 24)
            };

            var yAxis = new LinearAxis()
            {
                Title = "kg/m²",
                TextColor = _gray,
                TitleColor = _gray,
                TicklineColor = _gray,
                AxislineColor = _gray,
                MajorGridlineColor = _gray,
                MinorGridlineColor = _gray,
                AbsoluteMinimum = 10,
                AbsoluteMaximum = 31,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Solid,
                MajorGridlineThickness = GetProportionalDimension(0.6, 2),
                Position = AxisPosition.Left,
                MajorStep = 2,
                MinorStep = 2,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                AxisTitleDistance = 8,
                TickStyle = TickStyle.None,
                FontSize = GetProportionalDimension(10, 24)
            };

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            var sexo = LoginHelper.CurrentCrianca.Sexo;
            var percentis = Percentil.Percentis.Where(p => p.Sexo == sexo);

			var percentisSexo = percentis as IList<Percentil.PercentilTipoCrescimento> ?? percentis.ToList();
            PlotIMCLine(plotModel, percentisSexo.Select(p => (double)p.ImcP01).ToList(), _red);
            PlotIMCLine(plotModel, percentisSexo.Select(p => (double)p.ImcP3).ToList(), _yellow);
            PlotIMCLine(plotModel, percentisSexo.Select(p => (double)p.ImcP50).ToList(), _green);
            PlotIMCLine(plotModel, percentisSexo.Select(p => (double)p.ImcP97).ToList(), _yellow);
            PlotIMCLine(plotModel, percentisSexo.Select(p => (double)p.ImcP999).ToList(), _red);

            _mySeries = PlotLine(plotModel);
            
            _graphicalView = new PlotView(Context)
            {
                Model = plotModel
            };

			AddView(_graphicalView);
		}
        
        private const double minScreenWidth = 480d;
        private const double maxScreenWidth = 1080d;
        private const double sizeDiff = maxScreenWidth - minScreenWidth;
        private double _screenSizeMultiplier = -1;
        private double screenSizeMultiplier
        {
            get
            {
                if(_screenSizeMultiplier == -1)
                {
                    _screenSizeMultiplier = (this.Context.Resources.DisplayMetrics.WidthPixels - minScreenWidth) / sizeDiff;
                }

                return _screenSizeMultiplier;
            }
        }

        private double GetProportionalDimension(double min, double max)
        {
            var diff = max - min;
            return min + (diff * screenSizeMultiplier);
        }

		#endregion
	}

}
