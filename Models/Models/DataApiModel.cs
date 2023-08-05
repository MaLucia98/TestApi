using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class RecaudoVehiculos
    {
        public string estacion { get; set; }
        public string sentido { get; set; }
        public int hora { get; set; }
        public string categoria { get; set; }
        public int valorTabulado { get; set; }
    }
    public class ConteoVehiculos
    {
        public string estacion { get; set; }
        public string sentido { get; set; }
        public int hora { get; set; }
        public string categoria { get; set; }
        public int cantidad { get; set; }
    }

    public class UserConection
    {
        public string userName { get; set; }
        public string password { get; set; }
}
    public class UserResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
