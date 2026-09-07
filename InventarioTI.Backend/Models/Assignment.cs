// Modelo que representa la asignación de un dispositivo a una persona.
// Incluye fechas, estado y relaciones con Device.

namespace InventarioTI.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        // Persona a la que se asigna el dispositivo
        public string AssignedTo { get; set; } = "";

        // Fecha de asignación
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Fecha de devolución (si aplica)
        public DateTime? ReturnedAt { get; set; }

        // Estado de la asignación (Activo, Devuelto)
        public string Status { get; set; } = "Activo";

        // Relación con el dispositivo
        public int DeviceId { get; set; }
        public Device Device { get; set; } = new Device();
    }
}