using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reto2_api.Repositories;

namespace reto2_api.Service
{
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _opcionRepository;

        public OpcionService(IOpcionRepository opcionRepository)
        {
            _opcionRepository = opcionRepository;
        }

        public async Task<List<Opcion>> GetAllAsync()
        {
            return await _opcionRepository.GetAllAsync();
        }

        public async Task<Opcion?> GetByIdAsync(int id)
        {
            return await _opcionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Opcion opcion)
        {
            if (opcion == null)
                throw new ArgumentNullException(nameof(opcion), "la opción no puede ser nula.");

            if (string.IsNullOrWhiteSpace(opcion.Texto))
                throw new ArgumentException("el texto de la opción no puede estar vacío.", nameof(opcion.Texto));

            if (opcion.Pregunta == null || opcion.Pregunta.IdPregunta <= 0)
                throw new ArgumentException("la pregunta asociada a la opción no es válida.", nameof(opcion.Pregunta));

            await _opcionRepository.AddAsync(opcion);
        }

        public async Task UpdateAsync(Opcion opcion)
        {
            var existingOpcion = await _opcionRepository.GetByIdAsync(opcion.IdOpcion);
            if (existingOpcion == null)
                throw new KeyNotFoundException("opción no encontrada.");

            if (string.IsNullOrWhiteSpace(opcion.Texto))
                throw new ArgumentException("el texto de la opción no puede estar vacío.", nameof(opcion.Texto));

            if (opcion.Pregunta == null || opcion.Pregunta.IdPregunta <= 0)
                throw new ArgumentException("la pregunta asociada a la opción no es válida.", nameof(opcion.Pregunta));

            await _opcionRepository.UpdateAsync(opcion);
        }

        public async Task DeleteAsync(int id)
        {
            var opcion = await _opcionRepository.GetByIdAsync(id);
            if (opcion == null)
                throw new KeyNotFoundException("opción no encontrada.");

            await _opcionRepository.DeleteAsync(id);
        }

        ///METODO OPCIONES DE PREGUNTA
        public async Task<List<Opcion>> GetByPreguntaIdAsync(int idPregunta)
        {
            if (idPregunta <= 0)
                throw new ArgumentException("el id de la pregunta debe ser un número positivo.", nameof(idPregunta));

            return await _opcionRepository.GetByPreguntaIdAsync(idPregunta);
        }

        ///METODO SOLUCION DE PREGUNTA
        public async Task<Opcion?> GetSolucionByPreguntaIdAsync(int idPregunta)
        {
            if (idPregunta <= 0)
                throw new ArgumentException("el id de la pregunta debe ser un número positivo.", nameof(idPregunta));

            return await _opcionRepository.GetSolucionByPreguntaIdAsync(idPregunta);
        }
    }
}
