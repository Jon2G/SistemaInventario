using Prism.Mvvm;

namespace Inventario.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "HandyControl Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
