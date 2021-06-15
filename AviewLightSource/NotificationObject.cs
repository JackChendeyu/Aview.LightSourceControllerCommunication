using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMSimple.Wpf
{
    class NotificationObject : System.ComponentModel.INotifyPropertyChanged
    {
        #region System.ComponentModel.INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }
            foreach (var propertyNmae in propertyNames)
            {
                this.RaisePropertyChanged(propertyNmae);
            }
        }

        protected virtual void RaisePropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {
            var propertyName = ExtractPropertyChanged(propertyExpression);
            this.RaisePropertyChanged(propertyName);
        }

        protected virtual string ExtractPropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }
            if(!(propertyExpression.Body is System.Linq.Expressions.MemberExpression memberExpression))
            {
                throw new ArgumentException("PropertySupport_NotMemberAccessExpression_Exception", nameof(propertyExpression));
            }
            if(!(memberExpression.Member is System.Reflection.PropertyInfo propertyInfo))
            {
                throw new ArgumentException("PropertySupport_ExpressionNotProperty_Exception", nameof(propertyExpression));
            }
            var getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("PropertySupport_StaticExpression_Exception", nameof(propertyExpression));
            }
            return memberExpression.Member.Name;


        }

    }
}
