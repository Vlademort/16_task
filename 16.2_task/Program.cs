using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace _16._2_task
{
    class Program
    {
        static void Main(string[] args)
        {
            string json_string = "";
            int count = 0;
            double max_price = 0;
            int index_max_price = 0;
            string path = "D:/Products.json";
            

            using (StreamReader sr = new StreamReader(path))
            {
                json_string = sr.ReadToEnd();
            }

            string my_string = json_string.Replace("{", "\t{");
            string[] product_array = my_string.Split("\t", StringSplitOptions.RemoveEmptyEntries);
            foreach (var sub in product_array)
            {
                //Console.WriteLine($"Подстрока\n{sub}");
                count += 1;
            }


            Product[] path_product = new Product[count];
            JsonSerializerOptions option = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            for (int i = 0; i < count; i++)
            {
                path_product[i] = JsonSerializer.Deserialize<Product>(product_array[i], option);
                if (max_price < path_product[i].PriceProduct)
                {
                    max_price = path_product[i].PriceProduct;
                    index_max_price = i;
                }
            }

            Console.WriteLine($"{path_product[index_max_price].NameProduct} - самый дорогой товар");
            Console.WriteLine("{0:f2} - его цена", path_product[index_max_price].PriceProduct);
            Console.ReadKey();

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

    }
}
