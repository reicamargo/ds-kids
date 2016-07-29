using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DS.Kids.Model.Support
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Validations.ResultCodes Validate()
        {
            return Validations.ResultCodes.Success;
        }
    }
}
