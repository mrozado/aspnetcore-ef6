using System.Linq;
using BCP.Data;
using BCP.Data.Interface;
using BCP.Data.Model;


namespace BCP.Bussines
{
    public abstract class BaseService<T> where T : BaseModel
    {
        private BCPContext Context;

        public BaseService(BCPContext _context)
        {
            this.Context = _context;
        }
        
        public void Dispose()
        {
            this.Context.Dispose();
        }

        public virtual T Get(int id)
        {
            var entity = this.Context.Set<T>().Find(id);
            var isLogical = entity as ILogicalDelete;
            if (isLogical == null)
            {
                return entity;
            }
            else
            {
                if (isLogical.IsDeleted)
                {
                    return null;
                }
                else
                {
                    return entity;
                }
             }
        }

        public virtual IQueryable<T> List()
        {
            var isLogical = typeof(T).GetInterfaces().Contains(typeof(ILogicalDelete));
            if (!isLogical)
            {
                return this.Context.Set<T>();
            }
            else
            {
                var returnCol = this.Context.Set<T>().Where(x => ((ILogicalDelete)x).IsDeleted == false);
                return returnCol;
            }
        }

        public virtual void Remove(int id)
        {
            var entity = this.Get(id);
            this.Context.Remove(entity);
        }

        public virtual T Save(T entity)
        {
            this.Validate(entity);
            return this.Context.Save(entity);
        }

        protected virtual void Validate(T entity)
        {
        }
    }
}
