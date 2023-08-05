namespace Models.Models
{
    public class Report
    {
        public Report()
        {
            DataByDate = new List<DataByDate>();
            TotalByStation = new List<TotalByStation>();
        }

        public int Cantidad { get; set; }
        public int Total { get; set; }
        public List<DataByDate> DataByDate { get; set; }
        public List<TotalByStation> TotalByStation { get; set; }

    }
    public class DataByStation
    {       
        public string Estacion { get; set; }
        public int EstacionId { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }        
    }
    public class DataByDate
    {
        public DataByDate()
        {
            DataByStation = new List<DataByStation>();
        }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public List<DataByStation> DataByStation { get; set; }
    }

    public class TotalByStation
    {     
       
        public int Cantidad { get; set; }
        public int Total { get; set; }        
    }

}