using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNet.Core.Entities;
using TestNet.Core.Repositories;

namespace TestNet.Infrastructure.Repositories
{
    public class DapperUnitOfWork : IDapperUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IRepository<Product>? _productRepository;
        private bool _dispose;

        public DapperUnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("TestNet"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        public IRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ?? (_productRepository = new DapperRepository<Product>(_transaction));
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _productRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _dispose = true;
            }
        }
        ~DapperUnitOfWork()
        {
            dispose(false);
        }

    }
}
