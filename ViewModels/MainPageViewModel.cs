using MathGraphicsWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathGraphicsWPF.ViewModels
{
    internal class MainPageViewModel : Base.ViewModel
    {
        #region Slider Values

        private int _minValue;

        private int _maxValue;

        public int MinValue
        {
            get => _minValue;
            set 
            {
                if (MinValue < MaxValue)
                {
                    Set(ref _minValue, value);
                }else
                {
                    Set(ref _minValue, -10);
                } 
            }
        }
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                if (MaxValue > MinValue)
                {
                    Set(ref _maxValue, value);
                }
                else
                {
                    Set(ref _maxValue, 10);
                }
            }
        }

        #endregion

        #region Select type
        private List<string> _graphParams;
        public List<string> GraphParams
        { 
            get => _graphParams;
            set => Set(ref _graphParams, value);
        }

        private string _selectedGraphic;
        public string SelectedGraphic 
        {
            get => _selectedGraphic;
            set => _selectedGraphic = value;
        }

        #endregion


        #region Graphic

        private IEnumerable<Models.DataPoint> _dataPoints;

        public IEnumerable<DataPoint> DataPoints 
        {
            get => _dataPoints; 
            set => Set(ref _dataPoints, value);
        }

        private void generatePoints()
        {
            var points = new List<Models.DataPoint>();
            if (SelectedGraphic.Equals("sin"))
            {
                for (double i = MinValue * Math.PI; i < MaxValue * Math.PI; i += 0.1)
                {
                    var y = Math.Sin(i);
                    points.Add(new Models.DataPoint { XValue = i, YValue = y });
                }
            }else if (SelectedGraphic.Equals("cos"))
            {
                for (double i = MinValue * Math.PI; i < MaxValue * Math.PI; i += 0.1)
                {
                    var y = Math.Cos(i);
                    points.Add(new Models.DataPoint { XValue = i, YValue = y });
                }
            }else if (SelectedGraphic.Equals("tg"))
            {
                for (double i = MinValue * Math.PI; i < MaxValue * Math.PI; i += 0.1)
                {
                    var y = Math.Tan(i);
                    points.Add(new Models.DataPoint { XValue = i, YValue = y });
                }
            }else
            {
                for (double i = MinValue * Math.PI; i < MaxValue * Math.PI; i += 0.1)
                {
                    var y = Math.Atan(1/i);
                    points.Add(new Models.DataPoint { XValue = i, YValue = y });
                }
            }
            DataPoints = points;
        }

        #endregion

        #region Buttons
        public ICommand GetResult { get; }

        public bool CanGetResultExecute(object parameter) => true;
        public void GetResultExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(SelectedGraphic))
            {
                generatePoints();
            }else
            {
                MessageBox.Show("Graphic type didn't choose");
            }
        }
        #endregion
        public MainPageViewModel()
        {
            GraphParams = Models.GraphicParams.possibleValues;
            GetResult = new Infrastructure.LambdaCommand(GetResultExecute, CanGetResultExecute);
        }
    }
}
