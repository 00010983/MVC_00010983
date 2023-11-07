using CW_DSCC_10983_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CW_DSCC_10983_MVC.Controllers
{
    public class TicketController : Controller
    {
        // Define the base address for the API.
        Uri baseAddress = new Uri("http://localhost:44398/api");
        // Create an HTTP client for making API requests.
        private readonly HttpClient _httpClient;

        public TicketController()
        {
            // Create a new instance of HttpClient for making HTTP requests.
            _httpClient = new HttpClient();
            // Set the base address for the HTTP client.
            _httpClient.BaseAddress = baseAddress;
        }

        // This action retrieves and displays a list of tickets.
        [HttpGet]
        public IActionResult Index()
        {
            List<Ticket> ticketlist = new List<Ticket>();
            // Send an HTTP GET request to retrieve ticket data from the API.
            HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the JSON response.
                string data = response.Content.ReadAsStringAsync().Result;
                ticketlist = JsonConvert.DeserializeObject<List<Ticket>>(data);
            }
            return View(ticketlist);
        }

        // This action displays a form to create a new ticket.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // This action processes the creation of a new ticket.
        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            try
            {
                // Serialize the ticket object to JSON.
                string data = JsonConvert.SerializeObject(ticket);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // Send an HTTP POST request to add a new ticket.
                HttpResponseMessage response = _httpClient.PostAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Post", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the ticket list.
                    TempData["successMessage"] = "Ticket Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();
        }

        // This action displays a form for editing an existing ticket.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Ticket ticket = new Ticket();
                // Send an HTTP GET request to retrieve ticket data by ID.
                HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Get/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response.
                    string data = response.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<Ticket>(data);
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // This action processes the update of an existing ticket.
        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            try
            {
                // Serialize the ticket object to JSON.
                string data = JsonConvert.SerializeObject(ticket);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // Send an HTTP PUT request to update the ticket data.
                HttpResponseMessage response = _httpClient.PutAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Put", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the ticket list.
                    TempData["successMessage"] = "Ticket Edited";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(ticket);
        }

        // This action displays a confirmation page for deleting a ticket.
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Ticket ticket = new Ticket();
                // Send an HTTP GET request to retrieve ticket data by ID.
                HttpResponseMessage response = _httpClient.GetAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Get/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response.
                    string data = response.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<Ticket>(data);
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // This action processes the deletion of a ticket after confirmation.
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                // Send an HTTP DELETE request to remove the ticket.
                HttpResponseMessage response = _httpClient.DeleteAsync("http://ec2-16-170-237-103.eu-north-1.compute.amazonaws.com/api/TicketsContoller/Delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Display a success message and redirect to the ticket list.
                    TempData["successMessage"] = "Ticket Deleted";
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