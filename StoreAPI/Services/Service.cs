using StoreAPI.DTOs;
using StoreAPI.Models;
using StoreAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Services
{
    public class Service<T> where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual OperationResult GetAll()
        {
            try
            {
                var result = _repository.Query();
                return OperationResult.Ok(result);
            }
            catch (Exception ex)
            {
                return OperationResult.FailException(ex);
            }
        }

        public virtual async Task<OperationResult> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                return entity != null
                    ? OperationResult.Ok(entity)
                    : OperationResult.Ok(default);
            }
            catch (Exception ex)
            {
                return OperationResult.FailException(ex);
            }
        }

        public virtual async Task<OperationResult> CreateAsync(T entity)
        {
            var validation = ValidateModel(entity);
            if (!validation.Success)
                return validation;

            try
            {
                await _repository.CreateAsync(entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Ok(entity);
            }
            catch (Exception ex)
            {
                return OperationResult.FailException(ex);
            }
        }

        public virtual async Task<OperationResult> UpdateAsync(long id, T entity)
        {
            var validation = ValidateModel(entity);
            if (!validation.Success)
            {
                return validation;
            }

            try
            {
                var existing = await _repository.FindByIdAsync(id);
                if (existing == null)
                    return OperationResult.Fail([new ValidationResult("Registro não encontrado")]);

                _repository.Update(entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Ok(entity);
            }
            catch (Exception ex)
            {
                return OperationResult.FailException(ex);
            }
        }

        public virtual async Task<OperationResult> DeleteAsync(long id)
        {
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                    return OperationResult.Fail([new ValidationResult("Registro não encontrado")]);

                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.FailException(ex);
            }
        }

        private static OperationResult ValidateModel(T entity)
        {
            var validationContext = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(entity, validationContext, results, true))
            {
                return OperationResult.Fail(results.ToArray());
            }

            return OperationResult.Ok();
        }
    }

}
