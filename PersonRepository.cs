using aharoT5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aharoT5
{
    public class PersonRepository
    {
        string _dbPath;

        private SQLiteConnection conn;
        public string StatusMessage  { get; set; }

        private void Init ()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona> ();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
           
        }

        public void AddNewPerson (string name)
        {
            int result = 0;

            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");
                Persona person = new() {Name = name};
                result = conn.Insert(person);
                StatusMessage = string.Format("Persona eliminada correctamente", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error no se inserto", name, ex.Message);
                
            }
        }

        public List<Persona> getAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al listar", ex.Message);
                
            }
            return new List<Persona>();
        }

        public void UpdatePerson(Persona id, Persona person)
        {
            try
            {
                Init();
                if (person != null)
                {
                    Console.WriteLine("Actualizando persona: " + id.Id, person.Name);
                    conn.Update(person);
                    StatusMessage = string.Format("Persona actualizada correctamente", id.Id, person.Name);
                }
                else
                {
                    Console.WriteLine("El objeto persona es nulo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la persona: " + ex.Message);
                StatusMessage = string.Format("Error al actualizar la persona", ex.Message);
            }

        }

        public void DeletePerson(Persona id,Persona person)
        {
            try
            {
                Init();
                if (person != null)
                {
                    conn.Delete(person);
                    StatusMessage = string.Format("Persona eliminada correctamente ",id.Id, person.Name);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona: ", ex.Message);
            }
        }
    }
}
