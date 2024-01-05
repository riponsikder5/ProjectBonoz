namespace BonozApplication.Managers
{
    public abstract class BaseDataManager
    {
        protected BanazDbContext _dbContext;

        public BaseDataManager(BanazDbContext model)
        {
            _dbContext = model;
        }


        #region APIs

        protected T FindEntity<T>(int primaryKey) where T : class
        {
            try
            {
                return _dbContext.Set<T>().Find(primaryKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected T GetEntityFirstRowData<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return _dbContext.Set<T>().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected T GetEntityLastRowData<T>(Func<T, bool> predicate) where T : class
        {
            try
            {
                return _dbContext.Set<T>().LastOrDefault(predicate);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected bool GetEntityAny<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return _dbContext.Set<T>().Where(predicate).Any();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected IList<T> GetEntityListData<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return _dbContext.Set<T>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected IList<T> GetEntityListData<T>() where T : class
        {
            try
            {
                return _dbContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected T GetRowData<T>(string interpolatedStoredProc) where T : class
        {
            try
            {
                return _dbContext.Set<T>().FromSqlRaw(interpolatedStoredProc).AsEnumerable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected IList<T> GetLookupCollection<T>() where T : class
        {
            try
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected IList<T> GetListData<T>(string interpolatedStoredProc) where T : class
        {
            try
            {
                return _dbContext.Set<T>().FromSqlRaw(interpolatedStoredProc).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                throw;
            }
        }

        protected async Task<bool> ExecuteSqlAsync(string interpolatedStoredProc)
        {
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(interpolatedStoredProc);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected bool MarkedForDelete(string interpolatedStoredProc)
        {
            try
            {
                _dbContext.Database.ExecuteSqlRaw(interpolatedStoredProc);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected bool RemoveEntity<T>(int id) where T : class
        {
            try
            {
                var entity = _dbContext.Set<T>().Find(id);
                _dbContext.Remove<T>(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected async Task<bool> AddUpdateEntityAsync<T>(T entity, bool keepDettached = true) where T : class
        {
            try
            {
                if (_dbContext.Entry<T>(entity).IsKeySet)
                    _dbContext.Update<T>(entity);
                else
                    _dbContext.Add<T>(entity);

                await _dbContext.SaveChangesAsync();

                if (keepDettached)
                {
                    _dbContext.Entry<T>(entity).State = EntityState.Detached;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected bool AddUpdateEntity<T>(T entity, bool keepDettached = true) where T : class
        {
            try
            {
                if (_dbContext.Entry<T>(entity).IsKeySet)
                    _dbContext.Update<T>(entity);
                else
                    _dbContext.Add<T>(entity);

                _dbContext.SaveChanges();

                if (keepDettached)
                {
                    _dbContext.Entry<T>(entity).State = EntityState.Detached;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected bool AddUpdateRange<T>(List<T> entities, bool keepDettached = true) where T : class
        {
            try
            {
                _dbContext.AddRange(entities);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
        #endregion
    }
}
