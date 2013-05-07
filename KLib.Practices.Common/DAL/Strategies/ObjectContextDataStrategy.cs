using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace KLib.Practices.Common.DAL.Strategies
{
    internal sealed class ObjectContextDataStrategy<TEntity> : DataStrategy<TEntity>
        where TEntity : class
    {
        private ObjectContext ObjContext
        {
            get { return Context as ObjectContext; }
        }

        private ObjectSet<TEntity> ObjSet
        {
            get { return ObjContext.CreateObjectSet<TEntity>(); }
        }

        private string QualifiedEntitySetName
        {
            get { return string.Format("{0}.{1}", ObjContext.DefaultContainerName, ObjSet.EntitySet.Name); }
        }

        public ObjectContextDataStrategy(ObjectContext context)
            : base(context)
        {
        }

        public override IQueryable<TEntity> List()
        {
            return ObjSet;
        }

        public override TEntity FindByKey(params object[] pk)
        {
            var keys = ObjSet.EntitySet.ElementType.KeyMembers;
            if (pk.Length != keys.Count)
                throw new ArgumentException("Input values does not match the number of entity primary key");

            var i = 0;
            var keyMemebers = keys.Select(k =>
                {
                    var km = new EntityKeyMember(k.Name, pk[i++]);                
                    return km;
                }).ToArray();

            var entityKey = new EntityKey(QualifiedEntitySetName, keyMemebers);
            object result;
            ObjContext.TryGetObjectByKey(entityKey, out result);
            return result as TEntity;
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjSet.FirstOrDefault(predicate);
        }

        public override void AddNew(TEntity entity)
        {
            ObjSet.AddObject(entity);
        }

        public override void Delete(TEntity entity)
        {
            var entry = GetEntryWithAttach(entity);
            entry.Delete();
        }

        public override void Update(TEntity entity, IEnumerable<string> updatedProperties)
        {
            if (updatedProperties == null) throw new ArgumentException("No changed property found");
            var entry = GetEntryWithAttach(entity);
            foreach (var propertyName in updatedProperties) 
                entry.SetModifiedProperty(propertyName);
        }

        public override void ReplaceWith(TEntity entity)
        {
            var entry = GetEntryWithAttach(entity);
            entry.SetModified();
        }

        public override int Count()
        {
            return ObjSet.Count();
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjSet.Count(predicate);
        }

        public override long LongCount()
        {
            return ObjSet.LongCount();
        }

        public override long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return ObjSet.LongCount(predicate);
        }

        private ObjectStateEntry GetEntryWithAttach(TEntity entity)
        {
            ObjectStateEntry entry;
            var key = ObjContext.CreateEntityKey(QualifiedEntitySetName, entity);
            if (!ObjContext.ObjectStateManager.TryGetObjectStateEntry(key, out entry))
            {
                ObjSet.Attach(entity);
                entry = ObjContext.ObjectStateManager.GetObjectStateEntry(key);
            }
            return entry;
        }
    }
}
