using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace CodeSolutions.AS3JobSearch
{
    internal class ItemContact
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    internal class ItemCompany
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public ItemAddress Address { get; set; } // <-- Fix here
        public string Employees { get; set; }
        public string Link { get; set; }
        public string ContactPerson { get; set; }
    }

    internal class ItemAddress
    {
        public string Country { get; set; }
        public string Address { get; set; }
        public string AddresssNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    internal class Item
    {
        public long Id { get; set; } // <-- Fix here
        [JsonProperty("Jobtitle")]
        public string JobTitle { get; set; } // <-- Fix here
        public string Text { get; set; }
        public string Url { get; set; }
        public ItemCompany Company { get; set; }
        public ItemContact Contact { get; set; }
        public string FormattedLocation { get; set; }
        public string Source { get; set; }
        public string Date { get; set; }
        public string FormattedDate { get; set; }
        public string Level { get; set; }
        public string Location { get; set; }
        public string JobId { get; set; }
        public bool PrimaryAd { get; set; }
        public bool CompanySite { get; set; }
        public string[] ListOfLinks { get; set; }   
        public bool IsSavedAdvert { get; set; }
        public string CreatedDateSavedAdvert { get; set; }
        public string Occupations { get; set; }
        public string Classifications { get; set; }
    }

    internal class SearchResult
    {
        public uint TotalResults { get; set; }
        public uint Limit { get; set; }
        public uint Offset { get; set; }
        public Item[] Results { get; set; }
    }

    internal class As3Results(string xmlResultsFilePath)
    {
        public Item[] Results { get; private set; } = ConvertJsonToSearchResult(xmlResultsFilePath).Results;

        private static SearchResult ConvertJsonToSearchResult(string xmlResultsFilePath)
        {
            using StreamReader sr = new(xmlResultsFilePath);
            string json = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<SearchResult>(json);
        }

        private void FilterOutSwedishLanguageResults()
        {
            Results = Results.Where(item => !item.Text.Contains("ö", StringComparison.OrdinalIgnoreCase)
                              || !item.Text.Contains("ä", StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public void PrintResults ()
        {
            FilterOutSwedishLanguageResults();
            Console.WriteLine($"Total results after filtering out Swedish language results: {Results.Length}");
            foreach (var item in Results)
            {
                Console.WriteLine($"Job Title (Id): {item.JobTitle} ({item.Id})");
                Console.WriteLine($"Company: {item.Company?.Name} ({item.Company?.Url})");
                Console.WriteLine($"Location: {item.Location}");
                Console.WriteLine($"Posted Date: {item.FormattedDate}");
                Console.WriteLine($"Job URL: {item.Url}");
                Console.WriteLine($"Contact Person Name: {item.Contact.Name}");
                Console.WriteLine($"Contact Person Email: {item.Contact.Email}");
                Console.WriteLine($"Contact Person Title: {item.Contact.Title}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
