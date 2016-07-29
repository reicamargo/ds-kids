using System;
using System.Collections.Generic;
using System.Linq;

using CoreGraphics;

using CorePlot;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Model;

using Foundation;

namespace DS.Kids.Apps.iOS.Controls
{

	internal static class GraphHelper
	{

		private static CPTLineStyle _minorGridLineStyle;

		public static void InitPlot(out CPTGraphHostingView hostView, out CPTXYGraph graph, out MyDataSource dataSource, CGRect frame, float myPlotSymbolSize, bool editable)
		{
			ConfigureHost(out hostView, frame, editable);
			ConfigureGraph(hostView, out graph, editable);
			ConfigurePlots(hostView, out dataSource, myPlotSymbolSize, editable);
			ConfigureAxes(hostView, editable);
		}

		private static void ConfigureHost(out CPTGraphHostingView hostView, CGRect frame, bool editable)
		{
			frame.Y = 0;
			frame.X = 0;
			hostView = new CPTGraphHostingView(frame)
							{
								AllowPinchScaling = editable
							};
		}

		private static void ConfigureGraph(CPTGraphHostingView hostView, out CPTXYGraph graph, bool editable)
		{
			// 1 - Create the graph
			graph = new CPTXYGraph(hostView.Bounds, CPTScaleType.Linear, CPTScaleType.Linear)
							{
								BackgroundColor = new CGColor(1, 1, 1)
							};
			hostView.HostedGraph = graph;

			// 2 - Set graph title
			const string title = "";
			graph.Title = title;

			// 3 - Create and set text style
			var titleStyle = CPTTextStyle.CreateTextStyle();
			titleStyle.Color = CPTColor.BlackColor;
			titleStyle.FontSize = 16.0f;
			graph.TitleTextStyle = titleStyle;
			graph.TitlePlotAreaFrameAnchor = CPTRectAnchor.TopLeft;
			graph.TitleDisplacement = new CGPoint(30.0f, 0.0f);

			// 4 - Set padding for plot area
			graph.PlotAreaFrame.PaddingLeft = 32.0f;
			graph.PlotAreaFrame.PaddingBottom = 32.0f;

			// 5 - Enable user interactions for plot space
			var plotSpace = (CPTXYPlotSpace)graph.DefaultPlotSpace;
			plotSpace.AllowsUserInteraction = editable;
			graph.ReloadData();
		}

		private static void ConfigurePlots(CPTGraphHostingView hostView, out MyDataSource dataSource, float myPlotSymbolSize, bool editable)
		{
			var sexo = LoginHelper.CurrentCrianca.Sexo;

			var percentis = Percentil.Percentis.Where(p => p.Sexo == sexo);

			var percentisSexo = percentis as IList<Percentil.PercentilTipoCrescimento> ?? percentis.ToList();

			var lineType = CPTScatterPlotInterpolation.Curved;

			AddPlot(hostView, new BaseLineDataSource(percentisSexo.Select(p => p.ImcP01).ToList()), CPTColor.RedColor, 0, lineType);
			AddPlot(hostView, new BaseLineDataSource(percentisSexo.Select(p => p.ImcP3).ToList()), CPTColor.YellowColor, 0, lineType);
			AddPlot(hostView, new BaseLineDataSource(percentisSexo.Select(p => p.ImcP50).ToList()), CPTColor.GreenColor, 0, lineType);
			AddPlot(hostView, new BaseLineDataSource(percentisSexo.Select(p => p.ImcP97).ToList()), CPTColor.YellowColor, 0, lineType);
			AddPlot(hostView, new BaseLineDataSource(percentisSexo.Select(p => p.ImcP999).ToList()), CPTColor.RedColor, 0, lineType);

			dataSource = new MyDataSource();
			AddPlot(hostView, dataSource, CPTColor.BlueColor, myPlotSymbolSize, CPTScatterPlotInterpolation.Linear);

			UpdatePlotScape(hostView, editable);
		}

		public static void UpdatePlotScape(CPTGraphHostingView hostView, bool editable = true)
		{
			var plotSpace = (CPTXYPlotSpace)hostView.HostedGraph.DefaultPlotSpace;
			var allBaseLinePlots = hostView.HostedGraph.AllPlots.ToList();

			var myPlot = hostView.HostedGraph.AllPlots
				.FirstOrDefault(p => ((CPTScatterPlot)p).DataSource is MyDataSource) as CPTScatterPlot;

			allBaseLinePlots.RemoveAll(p => Equals(p, myPlot));

			plotSpace.Delegate = null;

			plotSpace.ScaleToFitPlots(allBaseLinePlots.ToArray());
			plotSpace.XRange = ExpandRange(plotSpace.XRange, 1.03f);
			plotSpace.YRange = ExpandRange(plotSpace.YRange, 1.2f);

			if(editable)
			{
				plotSpace.GlobalXRange = plotSpace.XRange;
				plotSpace.GlobalYRange = plotSpace.YRange;

				if(myPlot != null)
				{
					var dataSource = myPlot.DataSource as MyDataSource;
					if(dataSource != null)
					{
						var lastData = dataSource.Data.LastOrDefault();
						if(lastData != null)
						{
							// Pega o ï¿½ltimo registro do grï¿½fico e alinha com o centro da tela.
							var semana = lastData.Semana;
							plotSpace.XRange = ExpandRange(new CPTPlotRange(semana, 1), 12 * 4);
						}
					}
				}
				plotSpace.YRange = plotSpace.GlobalYRange;

				plotSpace.Delegate = new MyCPTPlotSpaceDelegate();
			}
		}

		class MyCPTPlotSpaceDelegate : CPTPlotSpaceDelegate
		{

			public override CGPoint WillDisplace(CPTPlotSpace space, CGPoint proposedDisplacementVector)
			{
				return new CGPoint(proposedDisplacementVector.X, 0);
			}

			public override CPTPlotRange WillChangePlotRange(CPTPlotSpace space, CPTPlotRange newRange, CPTCoordinate coordinate)
			{
				var plotSpace = (CPTXYPlotSpace)space;
				if(coordinate == CPTCoordinate.Y)
				{
					return plotSpace.YRange;
				}
				if(coordinate == CPTCoordinate.X)
				{
					if(newRange.LengthDouble <= 3 * 4)
					{
						return plotSpace.XRange;
					}

					if(newRange.LengthDouble <= 12 * 4)
					{
						_minorGridLineStyle.LineWidth = 0.25f;
					}
					else
					{
						_minorGridLineStyle.LineWidth = 0;
					}
					var axisSet = (CPTXYAxisSet)space.Graph.AxisSet;
					axisSet.XAxis.MinorGridLineStyle = _minorGridLineStyle;
				}
				return newRange;
			}

		}

		private static void AddPlot(CPTGraphHostingView hostView, CPTScatterPlotDataSource dataSource, CPTColor plotColor, float plotSymbolSize, CPTScatterPlotInterpolation interpolation)
		{
			// 1 - Get graph and plot space
			var graph = hostView.HostedGraph;
			var plotSpace = (CPTXYPlotSpace)graph.DefaultPlotSpace;

			// 2 - Create the plot
			var plot = new CPTScatterPlot
							{
								DataSource = dataSource
								//Identifier = CPDTickerSymbolAAPL;
							};
			graph.AddPlot(plot, plotSpace);

			// 4 - Create styles and symbols
			var plotLineStyle = plot.DataLineStyle;
			plotLineStyle.LineWidth = 2.5f;
			plotLineStyle.LineColor = plotColor;
			plot.DataLineStyle = plotLineStyle;

			if(plotSymbolSize > 0)
			{
				var plotSymbolLineStyle = CPTLineStyle.LineStyle;
				plotSymbolLineStyle.LineColor = plotColor;

				var plotSymbol = CPTPlotSymbol.EllipsePlotSymbol;
				plotSymbol.Fill = CPTFill.FromColor(CPTColor.WhiteColor);
				plotSymbol.LineStyle = plotSymbolLineStyle;
				plotSymbol.Size = new CGSize(plotSymbolSize, plotSymbolSize);
				plot.PlotSymbol = plotSymbol;
				plotLineStyle.LineWidth = 2.0f;
			}

			plot.Interpolation = interpolation;
		}

		private static CPTPlotRange ExpandRange(CPTPlotRange plotRange, NSDecimal factor)
		{
			NSDecimal oldLength = plotRange.Length;
			NSDecimal newLength;
			NSDecimal.Multiply(out newLength, ref oldLength, ref factor, NSRoundingMode.Bankers);

			NSDecimal locationOffset;
			NSDecimal result;
			NSDecimal.Subtract(out result, ref oldLength, ref newLength, NSRoundingMode.Bankers);
			NSDecimal den2 = 2;
			NSDecimal.Divide(out locationOffset, ref result, ref den2, NSRoundingMode.Bankers);
			NSDecimal newLocation;
			NSDecimal location = plotRange.Location;
			NSDecimal.Add(out newLocation, ref location, ref locationOffset, NSRoundingMode.Bankers);

			var range = new CPTPlotRange(newLocation, newLength);
			return range;
		}

		private static void ConfigureAxes(CPTGraphHostingView hostView, bool editable)
		{
			// 1 - Create styles
			var axisTitleStyle = CPTTextStyle.CreateTextStyle();
			axisTitleStyle.Color = CPTColor.FromRgba(0.8f, 0.8f, 0.8f, 1);
			//axisTitleStyle.FontName = @"Helvetica-Bold";
			axisTitleStyle.FontSize = 12.0f;
			var axisLineStyle = CPTLineStyle.LineStyle;
			axisLineStyle.LineWidth = 0.0f;
			axisLineStyle.LineColor = axisTitleStyle.Color;
			var axisTextStyle = new CPTMutableTextStyle
									{
										Color = axisTitleStyle.Color,
										//FontName = @"Helvetica-Bold", 
										FontSize = 11.0f
									};

			var tickLineStyle = CPTLineStyle.LineStyle;
			tickLineStyle.LineColor = axisLineStyle.LineColor;
			tickLineStyle.LineWidth = 1.0f;

			var majorGridLineStyle = CPTLineStyle.LineStyle;
			majorGridLineStyle.LineColor = axisLineStyle.LineColor;
			majorGridLineStyle.LineWidth = 0.5f;

			_minorGridLineStyle = CPTLineStyle.LineStyle;
			_minorGridLineStyle.LineColor = axisLineStyle.LineColor;
			_minorGridLineStyle.LineWidth = editable ? 0.25f : 0;

			// 2 - Get axis set
			var axisSet = (CPTXYAxisSet)hostView.HostedGraph.AxisSet;

			// 3 - Configure x-axis
			var x = axisSet.XAxis;
			x.Title = "IDADE";
			x.TitleTextStyle = axisTitleStyle;
			x.TitleOffset = 24.0f;
			x.AxisLineStyle = axisLineStyle;
			x.MajorGridLineStyle = majorGridLineStyle;
			x.MinorGridLineStyle = _minorGridLineStyle;
			x.LabelingPolicy = CPTAxisLabelingPolicy.None;
			x.LabelTextStyle = axisTextStyle;
			x.LabelOffset = -14.0f;
			x.MinorTickLineStyle = tickLineStyle;
			x.MajorTickLineStyle = tickLineStyle;
			x.MajorTickLength = 12.0f;
			x.MinorTickLength = 1.0f;
			x.MajorIntervalLength = 12 * 4;
			x.MinorTicksPerInterval = 12;
			x.TickDirection = CPTSign.Negative;
			x.AxisConstraints = CPTConstraints.FromLowerOffset(-x.MajorTickLength - x.LabelOffset + 7);

			var xLabels = new NSMutableSet();
			var xMajorLocations = new NSMutableSet();
			var xMinorLocations = new NSMutableSet();
			for (var i = 2 * 12 * 4; i <= 10 * 12 * 4; i += 4)
			{
				if(i % (12 * 4) == 0)
				{
					var label = new CPTAxisLabel((i / (12 * 4)).ToString(), x.LabelTextStyle)
									{
										TickLocation = i,
										Offset = x.MajorTickLength
									};
					xLabels.Add(label);
					xMajorLocations.Add(new NSDecimalNumber(i));
				}
				else
				{
					xMinorLocations.Add(new NSDecimalNumber(i));
				}
			}
			x.AxisLabels = xLabels;
			x.MajorTickLocations = xMajorLocations;
			x.MinorTickLocations = xMinorLocations;

			// 4 - Configure y-axis
			var y = axisSet.YAxis;
			y.Title = "kg/m²";
			y.TitleTextStyle = axisTitleStyle;
			y.TitleOffset = -26.0f;
			y.TitleRotation = (float)(Math.PI / 2);
			y.AxisLineStyle = axisLineStyle;
			y.MajorGridLineStyle = majorGridLineStyle;
			y.LabelingPolicy = CPTAxisLabelingPolicy.None;
			y.LabelTextStyle = axisTextStyle;
			y.LabelOffset = 8.0f;
			y.MajorTickLineStyle = axisLineStyle;
			y.MajorTickLength = 2.0f;
			y.TickDirection = CPTSign.Positive;
			y.AxisConstraints = CPTConstraints.FromLowerOffset(-y.LabelOffset - 2);

			var yLabels = new NSMutableSet();
			var yMajorLocations = new NSMutableSet();
			for(var i = 0; i <= 40; i += 2)
			{
				if(i >= 10 && i <= 32)
				{
					var label = new CPTAxisLabel(i.ToString(), y.LabelTextStyle)
									{
										TickLocation = i,
										Offset = -y.MajorTickLength - y.LabelOffset
									};
					yLabels.Add(label);
				}
				yMajorLocations.Add(new NSDecimalNumber(i));
			}
			y.AxisLabels = yLabels;
			y.MajorTickLocations = yMajorLocations;
		}

		internal class MyData
		{

			public MyData(int semana, decimal imc)
			{
				Semana = semana;
				Imc = imc;
			}

			public float Semana { get; private set; }

			public decimal Imc { get; private set; }

		}

		public class MyDataSource : CPTScatterPlotDataSource
		{

			public MyData[] Data { get; private set; }

			public MyDataSource()
			{
				UpdateValues();
			}

			public void UpdateValues()
			{
				var totalSemanas = new Func<DateTime, int>(date =>
					{
						var age = date.Subtract(LoginHelper.CurrentCrianca.DataNascimento);
						var weeks = (decimal)(age.TotalDays / 7);
						return (int)Math.Truncate(weeks);
					});

				Data = LoginHelper.CurrentCrianca.Crescimentos.Select(c => new MyData(totalSemanas(c.DataCriacao), PesoAltura.ObterImc(c.Peso, c.Altura))).ToArray();
			}

			public override nint NumberOfRecordsForPlot(CPTPlot plot)
			{
				return Data.Length;
			}

			public override NSNumber NumberForPlot(CPTPlot plot, CPTPlotField field, nuint index)
			{
				var valueCount = Data.Length;
				switch(field)
				{
					case CPTPlotField.ScatterPlotFieldX:
						if((int)index < valueCount)
						{
							return Data[index].Semana;
						}
						break;
					case CPTPlotField.ScatterPlotFieldY:
						return (float)Data[index].Imc;
				}

				return NSDecimalNumber.Zero;
			}

		}

		private class BaseLineDataSource : CPTScatterPlotDataSource
		{

			private readonly List<MyData> _data;

			public BaseLineDataSource(List<decimal> data)
			{
				_data = new List<MyData>();
				for(int i = 0; i < data.Count; i+=12)
				{
					_data.Add(new MyData((i + 24) * 4, data[i]));
				}
			}

			public override nint NumberOfRecordsForPlot(CPTPlot plot)
			{
				return _data.Count;
			}

			public override NSNumber NumberForPlot(CPTPlot plot, CPTPlotField field, nuint index)
			{
				switch(field)
				{
					case CPTPlotField.ScatterPlotFieldX:
						if((int)index < _data.Count)
						{
							return _data.ElementAt((int)index).Semana;
						}
						break;
					case CPTPlotField.ScatterPlotFieldY:
						return (float)_data.ElementAt((int)index).Imc;
				}

				return NSDecimalNumber.Zero;
			}

		}

	}

}
