using System;
using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.DataAcces;
using Application.Common;
using Application.Models;

namespace Application.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        private static List<Customer> customers;

        #region -- Los datos no se encuentran guardados más que en memoria --

        static CustomerRepository()
        {
            // Carga de 100 clientes de forma predeterminada
            customers = new List<Customer>();

            for (int i = 0; i < 20; i++)
            {
                var customer = new Customer();
                customer.Id = i + 1;
                customer.Name = "CustomerName" + customer.Id;
                customer.LastName = "CustomerLastname" + customer.Id;
                customer.Age = 20 + (int)customer.Id;

                customers.Add(customer);
            }
        }

        #endregion

        public override void Create(Customer entity)
        {
            try
            {
                long lastId = (long)(customers[customers.Count - 1]).Id;
                entity.Id = lastId + 1;
                if (customers != entity)
                {
                    customers.Add(entity);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new TechnicalException(
                    string.Format("No se pudo crear el cliente \"{0} {1}\".", entity.Name, entity.LastName),
                    ex);
            }

        }

        public override List<Customer> GetAll()
        {
            // Devuelve una nueva lista ordenada

            //TODO: resolver el error
            customers.OrderBy(Customer => Customer.Id).ToList();
            
            return customers.ToList();
        }
        public List<Customer> GetAll(string path)
        {
            CustomerSerializer customerSerializer = new CustomerSerializer();
            customers.AddRange(customerSerializer.Read(path));
            return customers;
        }

        public override Customer GetById(long entityId)
        {
            Customer auxCustomer = null;
           
            foreach (Customer customer in customers) 
            {
                if (customer.Id == entityId)
                {
                    auxCustomer = customer;
                }       
            }
            
            return auxCustomer;
        }

        public override void Remove(Customer entity)
        {
            // TODO: implementar
            if (customers.Contains(entity)) 
            {
                customers.Remove(entity);
            }
        }

        public override void Update(Customer entity)
        {
            // TODO: implementar

            foreach (Customer customer in customers)
            {
                if (customer.Id == entity.Id)
                {
                    customer.Name = entity.Name;
                    customer.LastName = entity.LastName;
                    customer.Age = entity.Age;
                }
            }

        }

        public List<Customer> LoadFromFile(string path)
        {

            CustomerSerializer customerSerializer = new CustomerSerializer();
            customers.AddRange(customerSerializer.Read(path));
            return customers;
        }

        public bool SaveToFile(List<Customer> customers)
        {
            CustomerSerializer customerSerializer = new CustomerSerializer();
            return customerSerializer.Save(customers);
        }

    }
}
