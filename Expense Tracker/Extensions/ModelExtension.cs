using System.Reflection;

namespace Expense_Tracker.Extensions
{
    public static class ModelExtension
    {
        public static TEntity ToEntity<TModel, TEntity>(this TModel model)
        {
            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));

            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                property.SetValue(entity, typeof(TModel).GetProperty(property.Name).GetValue(model));
            }
            return entity;
        }
    }
}
