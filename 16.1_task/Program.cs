using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace _16._1_task
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 5;
            Product[] product_array = new Product[N];
            
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine($"Товар № {i+1}");
                Console.Write("Введите код товара: ");
                int code = Convert.ToInt32(Console.ReadLine());                
                Console.Write("Введите наименование товара: ");
                string name = Convert.ToString(Console.ReadLine());                
                Console.Write("Введите стоимость товара: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();
                Product product = new Product(code, name, price);
                product_array[i] = product;                
            }

            JsonSerializerOptions option = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string path = "D:/Products.json";            
            if (!File.Exists(path))
            {
                File.Create(path);
            }            
                        
            for (int i = 0; i < N; i++)
            {
                string json_string = JsonSerializer.Serialize(product_array[i], option);                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(json_string);                   
                }
            }

            Console.ReadLine();
        }
    }

    class Product
    {
        [JsonPropertyName("Код товара")]
        public int CodeProduct { get; set; }

        [JsonPropertyName("Наименование товара")]
        public string NameProduct { get; set; }

        [JsonPropertyName("Цена товара")]
        public double PriceProduct { get; set; }

       public Product (int code, string name, double price)
        {
            CodeProduct = code;
            NameProduct = name;
            PriceProduct = price;
        }

    }
}
