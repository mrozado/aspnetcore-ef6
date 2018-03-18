using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BCP.Data.Interface;
using BCP.Data.Model;
using BCP.Data.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.ObjectModel;

namespace BCP.Data
{
    public class BCPContext : DbContext
    {
        public DbSet<Persona> Students { get; set; }

        public DbSet<GrupoDeAuditoria> GrupoDeAuditorias { get; set; }

        public DbSet<Auditoria> Auditorias { get; set; }


        public void Detach<T>(T entity) where T : class
        {
            this.Entry<T>(entity).State = EntityState.Detached;
        }

        public void ExecuteCommand(string sql, params object[] parameters)
        {
            this.ExecuteCommand(sql, parameters);
        }

        public T ExecuteScalar<T>(string sql, params object[] parameters)
        {
            return this.SqlQuery<T>(sql, parameters).Single();
        }

        public List<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return this.SqlQuery<T>(sql, parameters).ToList();
        }

        public void Remove<T>(T entity) where T : BaseModel
        {
            if (entity == null)
            {
                return;
            }

            var auditEntity = entity as IAuditEntity;

            if (auditEntity != null)
            {
                if (auditEntity.Auditoria == null)
                {
                    auditEntity.Auditoria = new GrupoDeAuditoria() { Entidad = typeof(T).FullName };
                    this.Save(auditEntity.Auditoria);
                    auditEntity.GrupoDeAuditoriaId = auditEntity.Auditoria.Id;
                }
                else
                {
                    auditEntity.Auditoria =
                        this.GrupoDeAuditorias.FirstOrDefault(g => g.Id == auditEntity.GrupoDeAuditoriaId);
                }

                auditEntity.Auditoria.Auditorias.Add(new Auditoria()
                {
                    UserId = ClaimsPrincipal.Current.Identity.Name,
                    Date = DateTime.Now,
                    Action = (int)AuditTypeEnum.Delete
                });
            }

            var entityLogicalDelete = entity as ILogicalDelete;

            if (entityLogicalDelete != null)
            {
                entityLogicalDelete.IsDeleted = true;
                this.Save((T)entityLogicalDelete);

                return;
            }

            Entry(entity).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public T Save<T>(T entity)
            where T : BaseModel
        {
            this.SetEntity(entity);
            this.SaveChanges();

            return entity;
        }

        public void SetEntity<TEntity>(TEntity entity)
            where TEntity : BaseModel
        {
            if (entity == null)
            {
                return;
            }

            if (entity.Id == 0)
            {
                this.Entry(entity).State = EntityState.Added;

                var auditEntity = entity as IAuditEntity;

                if (auditEntity != null)
                {
                    if (auditEntity.Auditoria == null)
                    {
                        auditEntity.Auditoria = new GrupoDeAuditoria() { Entidad = typeof(TEntity).FullName };
                    }

                    auditEntity.Auditoria.Auditorias.Add(new Auditoria()
                    {
                        UserId = ClaimsPrincipal.Current.Identity.Name,
                        Date = DateTime.Now,
                        Action = (int)AuditTypeEnum.Create
                    });
                }
            }
            else
            {
                this.UpdateEntity<TEntity>(entity);
            }
        }

        private void UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseModel
        {
            PropertyValues currentValues = null;

            var entityLogicalDelete = entity as ILogicalDelete;

            if (entityLogicalDelete != null && !entityLogicalDelete.IsDeleted)
            {
                var auditEntity = entity as IAuditEntity;

                if (auditEntity != null)
                {
                    if (auditEntity.GrupoDeAuditoriaId == 0)
                    {
                        auditEntity.Auditoria = new GrupoDeAuditoria() { Entidad = typeof(TEntity).FullName };
                        this.Save(auditEntity.Auditoria);
                        auditEntity.GrupoDeAuditoriaId = auditEntity.Auditoria.Id;
                    }
                    else
                    {
                        auditEntity.Auditoria =
                            this.GrupoDeAuditorias.FirstOrDefault(g => g.Id == auditEntity.GrupoDeAuditoriaId);
                    }

                    auditEntity.Auditoria.Auditorias.Add(new Auditoria()
                    {
                        UserId = ClaimsPrincipal.Current.Identity.Name,
                        Date = DateTime.Now,
                        Action = (int)AuditTypeEnum.Update
                    });
                }
            }

            EntityEntry<TEntity> entry = null;

            var entitySet = Set<TEntity>();

            ObservableCollection<TEntity> entityCollection = entitySet.Local as ObservableCollection<TEntity>;

            TEntity attachedEntity = entityCollection.FirstOrDefault(_ => _.Id == entity.Id);

            if (attachedEntity == null)
            {
                entry = this.Entry(entity);
                this.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                this.Entry(attachedEntity).CurrentValues.SetValues(entity);

                if (this.Entry(attachedEntity).State != EntityState.Modified)
                {
                    this.Entry(attachedEntity).State = EntityState.Modified;
                }

                entry = this.Entry(attachedEntity);
            }

            var creacion = entity as IAuditEntity;

            if (creacion != null)
            {
                if (currentValues == null)
                {
                    currentValues = entry.CurrentValues.Clone();
                    entry.Reload();
                    entry.CurrentValues.SetValues(currentValues);
                }
            }
        }
    }
}