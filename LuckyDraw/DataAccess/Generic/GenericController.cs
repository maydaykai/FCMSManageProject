using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;

namespace LuckyDraw.DataAccess.Generic
{
    /// <summary>
    /// 注意泛型的实例化以及数据共享特点
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDataContext"></typeparam>
    [System.ComponentModel.DataObject]
    public class GenericController<TEntity, TDataContext>
        where TDataContext : DataContext
        where TEntity : class
    {
        private static TDataContext _dataContext;
        private static PropertyInfo[] _columns;
        private static PropertyInfo _primaryKey;

        protected static object MObjLock = new object();//同步锁
        private static string TDataContextKey = typeof (TDataContext).FullName;

        #region Properties

        protected static TDataContext DataContext
        {
            get
            {
                // We are in a web app, use a request scope
                if (HttpContext.Current != null)
                {
                    //Different Repository will use different instance
                    var dataContext = (TDataContext)HttpContext.Current.Items[TDataContextKey];

                    if (dataContext == null)
                    {
                        dataContext = Activator.CreateInstance<TDataContext>();
                        HttpContext.Current.Items.Add(TDataContextKey, dataContext);
                    }

                    return dataContext;
                }
                else
                {
                    //If this is not a web app then just create a datacontext
                    //which will have the same lifespan as the app itself
                    //This is only really to support unit tests and should not
                    //be used in any production code. A better way to use this
                    //code with unit tests is to mock the HttpContext
                    //its instances number depend on number(table) * number(datacontext)
                    if (_dataContext == null)
                    {
                        _dataContext = Activator.CreateInstance<TDataContext>();
                    }

                    return _dataContext;
                }
            }
        }

        protected static Table<TEntity> EntityTable
        {
            get
            {
                return DataContext.GetTable<TEntity>();
            }
        }

        protected static string EntityName
        {
            get
            {
                return EntityType.Name;
            }
        }

        protected static Type EntityType
        {
            get
            {
                return typeof(TEntity);
            }
        }

        protected static string TableName
        {
            get
            {
                var att = EntityType.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
                return att == null ? "" : ((TableAttribute)att).Name;
            }
        }

        public static DataLoadOptions LoadOptions
        {
            get
            {
                return DataContext.LoadOptions;
            }
            set
            {
                DataContext.LoadOptions = value;
            }
        }

        protected static PropertyInfo[] Columns
        {
            get
            {
                if (_columns == null)
                {
                    //Get all the properties of this entity which have a Column attribute
                    //this is how Linq to Sql does its mapping.
                    _columns = (from p in EntityType.GetProperties()
                                where p.GetIndexParameters().Length == 0 &
                                      p.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() != null
                                select p).ToArray<PropertyInfo>();
                }

                return _columns;
            }
        }

        protected static PropertyInfo PrimaryKey
        {
            get
            {
                if (_primaryKey == null)
                {
                    foreach (PropertyInfo pi in Columns)
                    {
                        foreach (ColumnAttribute col in pi.GetCustomAttributes(typeof(ColumnAttribute), false))
                        {
                            if (col.IsPrimaryKey)
                                _primaryKey = pi;
                        }
                    }
                }

                return _primaryKey;
            }
        }

        #endregion

        #region Helper methods

        protected static object GetPrimaryKeyValue(TEntity entity)
        {
            return PrimaryKey.GetValue(entity, null);
        }

        protected static TEntity GetEntity(TEntity entity)
        {
            return GetEntity(GetPrimaryKeyValue(entity));
        }

        protected static void UpdateOriginalFromChanged(ref TEntity destination, TEntity source)
        {
            //Update all the column properties using reflection
            foreach (PropertyInfo pi in Columns)
            {
                pi.SetValue(destination, pi.GetValue(source, null), null);
            }
            //foreach (PropertyInfo pi in Columns)
            //{
            //    try
            //    {
            //        pi.SetValue(destination, pi.GetValue(source, null), null);
            //    }
            //    catch (Exception e)
            //    { 
            //    }
            //}
        }

        #endregion

        #region Generic CRUD methods

        //---------------------Selects----------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static IQueryable<TEntity> SelectAll()
        {
            return EntityTable;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static IQueryable<TEntity> SelectAll(int maximumRows, int startRowIndex)
        {
            return EntityTable.Skip(startRowIndex).Take(maximumRows);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static IQueryable<TEntity> SelectAll(string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression))
            {
                return SelectAll();
            }
            return EntityTable.OrderBy(sortExpression);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static IQueryable<TEntity> SelectAll(string sortExpression, int maximumRows, int startRowIndex)
        {
            if (string.IsNullOrEmpty(sortExpression))
            {
                return SelectAll(maximumRows, startRowIndex);
            }
            return SelectAll(sortExpression).Skip(startRowIndex).Take(maximumRows);
        }

        public static int Count()
        {
            return EntityTable.Count();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<TEntity> SelectAllAsList()
        {
            return EntityTable.ToList();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static TEntity GetEntity(object id)
        {
            Type tp = id.GetType();
            string query = "";
            if (tp.Name.ToLower() == "string")
            {
                query = string.Format("Select * from {0} where {1} = '{2}'", new object[] { TableName, PrimaryKey.Name, id });
            }
            else
            {
                query = string.Format("Select * from {0} where {1} = {2}", new object[] { TableName, PrimaryKey.Name, id });
            }

            return DataContext.ExecuteQuery<TEntity>(query).FirstOrDefault();
        }

        //----------------------Insert------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static void Insert(TEntity entity)
        {
            Insert(entity, true);
        }

        public static void Insert(TEntity entity, bool submitChanges)
        {
            EntityTable.InsertOnSubmit(entity);
            if (submitChanges)
                DataContext.SubmitChanges();
        }

        //-----------------------Update-----------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static void Update(TEntity entity)
        {
            Update(entity, true);
        }

        public static void Update(TEntity entity, bool submitChanges)
        {
            TEntity original = GetEntity(entity);
            UpdateOriginalFromChanged(ref original, entity);
            if (submitChanges)
                DataContext.SubmitChanges();
        }

        //----------------------Delete-------------------------------------------
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void Delete(TEntity entity)
        {
            Delete(entity, true);
        }

        public static void Delete(TEntity entity, bool submitChanges)
        {
            TEntity delete = GetEntity(entity);
            EntityTable.DeleteOnSubmit(delete);
            if (submitChanges)
                DataContext.SubmitChanges();
        }

        #endregion

        public static void SubmitChanges()
        {
            DataContext.SubmitChanges();
        }
    }
}