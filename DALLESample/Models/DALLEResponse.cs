namespace DALLESample.Models
{
    public class DALLEResponse
    {
        public int created { get; set; }
        public int expires { get; set; }
        public string id { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
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
}
