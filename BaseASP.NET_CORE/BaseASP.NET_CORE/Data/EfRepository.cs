﻿using BaseASP.NET_CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseASP.NET_CORE.Data
{
	/// <summary>
	/// Represents the Entity Framework repository
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		#region Fields

		private readonly DataContext _context;

		private DbSet<TEntity> _entities;

		#endregion Fields

		#region Ctor

		public EfRepository(DataContext context)
		{
			this._context = context;
		}

		#endregion Ctor

		#region Utilities

		/// <summary>
		/// Rollback of entity changes and return full error message
		/// </summary>
		/// <param name="exception">Exception</param>
		/// <returns>Error message</returns>
		protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
		{
			//rollback entity changes
			if (_context is DbContext dbContext)
			{
				var entries = dbContext.ChangeTracker.Entries()
					.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

				entries.ForEach(entry => entry.State = EntityState.Unchanged);
			}
			_context.SaveChanges();
			return exception.ToString();
		}

		#endregion Utilities

		#region Methods

		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param name="id">Identifier</param>
		/// <returns>Entity</returns>
		public virtual TEntity GetById(object id)
		{
			return Entities.Find(id);
		}

		/// <summary>: 'Specified cast is not valid.'

		/// Insert entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Insert(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Add(entity);

				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Insert entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Insert(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Entities.AddRange(entities);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Update(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Update(entity);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Update entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Update(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Entities.UpdateRange(entities);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public virtual void Delete(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			try
			{
				Entities.Remove(entity);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		/// <summary>
		/// Delete entities
		/// </summary>
		/// <param name="entities">Entities</param>
		public virtual void Delete(IEnumerable<TEntity> entities)
		{
			if (entities == null)
				throw new ArgumentNullException(nameof(entities));

			try
			{
				Entities.RemoveRange(entities);
				_context.SaveChanges();
			}
			catch (DbUpdateException exception)
			{
				//ensure that the detailed error text is saved in the Log
				throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
			}
		}

		#endregion Methods

		#region Properties

		/// <summary>
		/// Gets a table
		/// </summary>
		public virtual IQueryable<TEntity> Table => Entities;

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

		/// <summary>
		/// Gets an entity set
		/// </summary>
		protected virtual DbSet<TEntity> Entities
		{
			get
			{
				if (_entities == null)
					_entities = _context.Set<TEntity>();

				return _entities;
			}
		}

		#endregion Properties
	}
}