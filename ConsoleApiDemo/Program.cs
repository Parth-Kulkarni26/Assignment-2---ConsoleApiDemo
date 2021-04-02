using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            GetAllProduct().Wait();
            Console.WriteLine("Enter the Id");
            int id = Convert.ToInt32(Console.ReadLine());
            GetProductById(id).Wait();
            Console.WriteLine("Enter the Product Id");
            product.ProductId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Product Name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter the QuntyInStock");
            product.QtyInStock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Description");
            product.Description = Console.ReadLine();
            Console.WriteLine("Enter the Supplier");
            product.Suppiler = Console.ReadLine();
            Insert(product).Wait();
            GetAllProduct().Wait();
            Put().Wait();
            GetAllProduct().Wait();
            Delete().Wait();
            GetAllProduct().Wait();
            Console.ReadKey();
        }
        static async Task GetAllProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44377/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/");
                if (response.IsSuccessStatusCode)
                {
                    var jasonString = response.Content.ReadAsStringAsync();
                    jasonString.Wait();
                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(jasonString.Result);
                    foreach (var temp in ProductList)
                    {
                        Console.WriteLine("Product Id:" + temp.ProductId + " ProductName:" + temp.Name);
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }

            }
        }
        static async Task GetProductById(int id)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44377/");

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);

                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();

                    Console.WriteLine("Id:{0}\tName:{1}", product.ProductId, product.Name);

                    //  Console.WriteLine("No of Employee in Department: {0}", department.Employees.Count);

                }

                else

                {

                    Console.WriteLine(response.StatusCode);

                }
            }
        }
        static async Task Insert(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44377/");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Product", product);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
        }
        static async Task Put()

        {

            using (var client = new HttpClient())

            {

                //Send HTTP requests from here. 
                client.BaseAddress = new Uri("https://localhost:44377/");

                //PUT Method  
                var product = new Product() { ProductId = 9, Description = "Description Updated" };
                int id = 1;
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Product/" + id, product);

                if (response.IsSuccessStatusCode)



                {

                    Console.WriteLine(response.StatusCode);

                }

                else

                {

                    Console.WriteLine(response.StatusCode);

                }

            }

        }



        static async Task Delete()

        {

            using (var client = new HttpClient())

            {

                //Send HTTP requests from here. 

                client.BaseAddress = new Uri("https://localhost:44377/");





                int id = 1;

                HttpResponseMessage response = await client.DeleteAsync("api/Product/" + id);

                if (response.IsSuccessStatusCode)

                {

                    Console.WriteLine(response.StatusCode);

                }

                else

                    Console.WriteLine(response.StatusCode);

            }

        }



    }

}

