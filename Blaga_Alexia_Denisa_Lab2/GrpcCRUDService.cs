using Blaga_Alexia_Denisa_Lab2.Models;
using Google.Protobuf.WellKnownTypes;
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
    
}

