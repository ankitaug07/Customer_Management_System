using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Customer_Management_System.Models;

namespace Customer_Management_System.Controllers
{
    public class CustomerController : ApiController
    {
        //GET
        public IHttpActionResult GetAllTodoItem()
        {
            IList<TodoItem> customers = null;
            using (var x = new ContextEntities())
            {
                customers = x.Customers
                            .Select(c => new TodoItem()
                            {
                                Id = c.id,
                                Name = c.name, 
                                IsComplete = c.iscomplete,
                                CompletedAt = c.completedat,
                                CreatedBy = c.createdby,
                                CreatedAt = c.createdat,
                                UpdatedBy = c.updatedby,
                                UpdatedAt = c.updatedat,
                                DeletedBy = c.deletedby,
                                DeletedAt = c.deletedat
                            }).ToList<TodoItem>();
            }
            if (customers.Count == 0)
                return NotFound();

            return Ok(customers);
        }
        //POST
        public IHttpActionResult PostNewTodoItem(TodoItem customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid");

            using (var x = new ContextEntities())
            {
                x.Customers.Add(new Customer()
                {
                    name = customer.Name,
                    iscomplete = customer.IsComplete,
                    completedat = customer.CompletedAt,
                    createdby = customer.CreatedBy,
                    createdat = customer.CreatedAt,
                    updatedby = customer.UpdatedBy,
                    updatedat = customer.UpdatedAt,
                    deletedby = customer.DeletedBy,
                    deletedat = customer.DeletedAt
                });

                x.SaveChanges();
            }

            return Ok();
        }
        //PUT
        public IHttpActionResult PutNewTodoItem(TodoItem customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid");

            using (var x = new ContextEntities())
            {
                var checkExistingCustomer = x.Customers.Where(c => c.id == customer.Id)
                                                        .FirstOrDefault<Customer>();
                if (checkExistingCustomer != null)
                {
                    checkExistingCustomer.name = customer.Name;
                    checkExistingCustomer.iscomplete = customer.IsComplete;
                    checkExistingCustomer.completedat = customer.CompletedAt;
                    checkExistingCustomer.createdby = customer.CreatedBy;
                    checkExistingCustomer.createdat = customer.CreatedAt;
                    checkExistingCustomer.updatedby = customer.UpdatedBy;
                    checkExistingCustomer.updatedat = customer.UpdatedAt;
                    checkExistingCustomer.deletedby = customer.DeletedBy;
                    checkExistingCustomer.deletedat = customer.DeletedAt;
                    x.SaveChanges();
                }
                else
                    return NotFound();
            }

            return Ok();
        }

        //DELETE
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("InValid Customer ID");

            using (var x = new ContextEntities())
            {
                var customer = x.Customers
                    .Where(c => c.id == id)
                    .FirstOrDefault();

                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }

            return Ok();
        }

    }
}
