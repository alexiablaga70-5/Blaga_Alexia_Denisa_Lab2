using Blaga_Alexia_Denisa_Lab2.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using DataAccess=Blaga_Alexia_Denisa_Lab2.Data;
using ModelAccess=Blaga_Alexia_Denisa_Lab2.Models;

namespace GrpcCustomersService.Services;
public class GrpcCRUDService : CustomerService.CustomerServiceBase
{

    private DataAccess.Blaga_Alexia_Denisa_Lab2Context db = null;
    public GrpcCRUDService(DataAccess.Blaga_Alexia_Denisa_Lab2Context db)
    {
        this.db = db;
    }
    public override Task<CustomerList> GetAll(Empty empty, ServerCallContext
   context)
    {

        CustomerList pl = new CustomerList();
        var query = from cust in db.Customer
                    select new Customer()
                    {
                        CustomerId = cust.CustomerID,
                        Name = cust.Name,
                        Adress = cust.Adress,
                        Birthdate = cust.BirthDate.ToString("yyyy-MM-dd")
                    };
        pl.Item.AddRange(query.ToArray());
        return Task.FromResult(pl);
    }
    public override Task<Empty> Insert(Customer requestData, ServerCallContext
   context)
    {
        db.Customer.Add(new ModelAccess.Customer
        {
            CustomerID = requestData.CustomerId,
            Name = requestData.Name,
            Adress = requestData.Adress,
            BirthDate = DateTime.Parse(requestData.Birthdate)
        });
        db.SaveChanges();
        return Task.FromResult(new Empty());
    }

public override Task<Customer> Get(CustomerId requestData, ServerCallContext context)
    {
        var data = db.Customer.Find(requestData.Id);

        Customer emp = new Customer()
        {
            CustomerId = data.CustomerID,
            Name = data.Name,
            Adress = data.Adress

        };
        return Task.FromResult(emp);
    }
    public override Task<Customer> Update(Customer request, ServerCallContext context)
    {
        var cust = db.Customer.FirstOrDefault(c => c.CustomerID == request.CustomerId);

        if (cust == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));

        cust.Name = request.Name;
        cust.Adress = request.Adress; // dac? la tine e Address, schimb? în cust.Address
        cust.BirthDate = DateTime.Parse(request.Birthdate);

        db.SaveChanges();

        // proto cere s? returnezi Customer
        return Task.FromResult(request);
    }


    public override Task<Empty> Delete(CustomerId requestData, ServerCallContext
   context)
    {
        var data = db.Customer.Find(requestData.Id);
        db.Customer.Remove(data);

        db.SaveChanges();
        return Task.FromResult(new Empty());
    }
    }