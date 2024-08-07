using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common
{
    public class ResponseMessage
    {
        public string? MessageType {  get; set; }
        public string? Message { get; set; }
        
        public ResponseMessage(string messageType, string message) { 
            MessageType=messageType;
            Message = message;
        }
    }
    public class configuration
    {
        public string Add(string table) { return $"{table} agregado correctamente"; }
        public string ErrorAdd(string table) { return $"{table} Error al guardar"; }
        public string Update(string table) { return $"{table} Actualizado correctamente"; }
        public string ErrorUpdate(string table) { return $"{table} Error al actualizar"; }
        public string Delete(string table) { return $"{table} Eliminado correctamente"; }
        public string ErrorDelete(string table) { return $"{table} Error al eliminar"; }
        public string ChangeState(string table) { return $"{table} Se cambio de estado correctamente"; }
        public string ErrorChangeState(string table) { return $"{table} Error al cambiar el estado"; }

    }
    public enum MessageType
    {
        success,
        error,
        warning
    }


}
