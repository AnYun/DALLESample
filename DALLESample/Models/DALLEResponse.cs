namespace DALLESample.Models
{
    public class DALLEResponse
    {
        public int created { get; set; }
        public int expires { get; set; }
        public string id { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
        public Error error { get; set; }

    }

    public class Result
    {
        public int created { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string url { get; set; }
    }
    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
