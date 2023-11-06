using CW_DSCC_10983_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CW_DSCC_10983_MVC.Controllers
{
    public class CustomerController : Controller
    {
        // Define the base address for the API.
        Uri baseAddress = new Uri("http://localhost:44398/api");
        // Create an HTTP client for making API requests.
        private readonly HttpClient _httpClient;

        public CustomerController()
        {
            // Create a new instance of HttpClient for making HTTP requests.
            _httpClient = new HttpClient();
            // Set the base address for the HTTP client.
            _httpClient.BaseAddress = baseAddress;
        }

        // This action retrieves and displays a list of customers.
        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customerlist = new List<Customer>();
            // Send an HTTP GET request to retrieve customer data from the API.
            HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the JSON response.
                string data = response.Content.ReadAsStringAsync().Result;
                customerlist = JsonConvert.DeserializeObject<List<Customer>>(data);
            }
            return View(customerlist);
        }

        // This action displays a form to create a new customer.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // This action processes the creation of a new customer.
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                // Serialize the customer object to JSON.
                string data = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // Send an HTTP POST request to add a new customer.
                HttpResponseMessage response = _httpClient.PostAsync("http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Post", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the customer list.
                    TempData["successMessage"] = "Customer Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();
        }

        // This action displays a form for editing an existing customer.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Customer customer = new Customer();
                // Send an HTTP GET request to retrieve customer data by ID.
                HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Get/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response.
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<Customer>(data);
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // This action processes the update of an existing customer.
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            try
            {
                // Serialize the customer object to JSON.
                string data = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // Send an HTTP PUT request to update the customer data.
                HttpResponseMessage response = _httpClient.PutAsync($"http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Put", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the customer list.
                    TempData["successMessage"] = "Customer Edited";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(customer);
        }

        // This action displays a confirmation page for deleting a customer.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Customer customer = new Customer();
                // Send an HTTP GET request to retrieve customer data by ID.
                HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Get/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response.
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<Customer>(data);
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // This action processes the deletion of a customer after confirmation.
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Send an HTTP DELETE request to remove the customer.
                HttpResponseMessage response = _httpClient.DeleteAsync("http://ec2-16-170-157-215.eu-north-1.compute.amazonaws.com/api/Customer/Delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the customer list.
                    TempData["successMessage"] = "Customer Deleted";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }

}