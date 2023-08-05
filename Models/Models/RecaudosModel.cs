namespace Models.Models
{
    public class Report
    {
        public Report()
        {
            DataByStation = new List<DataByStation>();
        }

        public int Cantidad { get; set; }
        public int Total { get; set; }
        public List<DataByStation> DataByStation { get; set; }

    }
    public class DataByStation
    {
        public DataByStation()
        {
            DataByDate = new List<DataByDate>();
        }

        public string Estacion { get; set; }
        public int EstacionId { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public List<DataByDate> DataByDate { get; set; }
    }
    public class DataByDate
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
    }
  
}