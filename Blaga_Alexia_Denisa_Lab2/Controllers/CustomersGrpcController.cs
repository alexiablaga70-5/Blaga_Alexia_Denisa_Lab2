using Grpc.Net.Client;
using GrpcCustomersService; // sau GrpcCustomerService - depinde cum e la tine
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;


namespace Blaga_Alexia_Denisa_Lab2.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;

        public CustomersGrpcController()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5214");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            Customer customer = client.Get(new CustomerId() { Id = (int)id });
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            Empty response = client.Delete(new CustomerId()
            {
                Id = id
            });
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            var customer = client.Get(new CustomerId { Id = id });
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);
                client.Update(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }
    }
}