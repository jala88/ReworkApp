namespace ReworkApp.Entities
{
    public class Solicitud
    {
        public int id_solicitud { get; set; }
        public int id_usuario { get; set; }
        public int id_tarjeta { get; set; }
        public int id_tipo_rework { get; set; }
        public int id_parte_tarjeta { get; set; }
        public string comentario { get; set; }
        public DateTime fecha { get; set; }

        public bool estado { get; set; }
    }
}
