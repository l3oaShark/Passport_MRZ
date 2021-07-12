using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Passport_MRZ.Base
{
    public class ViewModelBase : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase()
        {

        }

        public IRegionManager _regionManager;

        public ViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(OnNavigate, CanNavigate);
        }

        protected virtual bool CanNavigate(string uri)
        {
            return true;
        }

        protected virtual void OnNavigate(string uri)
        {

            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }
        /// <summary>
        /// Raise when property value propertychanged or override propertychage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(GetPropertyName(propertyExpression));
        }
        /// <summary>
        /// Raise when property value propertychanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void AssignPermission()
        {

        }
    }
}
