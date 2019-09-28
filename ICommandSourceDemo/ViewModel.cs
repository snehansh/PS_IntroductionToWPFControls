using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ICommandSourceDemo
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DataObject _model;
        public DataObject Model
        {
            get { return _model; }
            set
            {
                _model = value;
                NotifyPropertyChanged("Model");
            }
        }

        public RelayCommand UpdateCommand { get; private set; }

        public ViewModel()
        {
            Model = new DataObject() { Name = "Brian Lagunas" };

            UpdateCommand = new RelayCommand(UpdateModel);
        }

        void UpdateModel(object param)
        {
            Model.Name = DateTime.Now.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
